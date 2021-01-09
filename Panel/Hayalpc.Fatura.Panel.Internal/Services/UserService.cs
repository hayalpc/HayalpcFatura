using AutoMapper;
using Hayalpc.Library.Common;
using Hayalpc.Library.Common.DataTables;
using Hayalpc.Library.Common.Dtos;
using Hayalpc.Library.Common.Enums;
using Hayalpc.Library.Common.Helpers;
using Hayalpc.Library.Common.Helpers.Interfaces;
using Hayalpc.Library.Common.Models;
using Hayalpc.Library.Common.Results;
using Hayalpc.Library.Log;
using Hayalpc.Library.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Hayalpc.Fatura.Data;
using Hayalpc.Fatura.Data.Models;
using Hayalpc.Fatura.Panel.Internal.Helpers;
using Hayalpc.Fatura.Panel.Internal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using Hayalpc.Library.Common.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using DevExtreme.AspNet.Data;

namespace Hayalpc.Fatura.Panel.Internal.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserRoleService userRole;
        private readonly IRolePermissionService rolePermission;
        private readonly IRoleService role;
        private readonly ITokenCreator tokenCreator;
        private readonly IResetPasswordService resetPasswordService;
        private readonly IMailHelper mailHelper;

        private readonly IMerchantService merchantService;
        private readonly IServiceService serviceService;

        public UserService(IUserRoleService userRole, IRolePermissionService rolePermission, IRoleService role, ITokenCreator tokenCreator, IResetPasswordService resetPasswordService, IMailHelper mailHelper, IMerchantService merchantService, IServiceService serviceService,
            IRepository<User> repository, IHpLogger logger, IHpUnitOfWork<HpDbContext> unitOfWork, IMemoryCache memoryCache) : base(repository, logger, unitOfWork, memoryCache)
        {
            this.userRole = userRole;
            this.rolePermission = rolePermission;
            this.role = role;
            this.tokenCreator = tokenCreator;
            this.resetPasswordService = resetPasswordService;
            this.mailHelper = mailHelper;
            this.merchantService = merchantService;
            this.serviceService = serviceService;
        }

        public List<UserBulletin> Bulletin()
        {
            var result = new List<UserBulletin>();

            var bulletins = repository.GetQuery<UserBulletin>(x => x.UserId == RequestHelper.UserId && x.Status == UserBulletinStatus.Unread && x.ExpireDate >= DateTime.Now && x.StartDate <= DateTime.Now).ToList();
            if (bulletins.Count > 0)
            {
                result = bulletins;
            }
            return result;
        }

        public IDataResult<UserDataDto> Data()
        {
            var data = new UserDataDto
            {
                MerchantList = merchantService.Search(new DataSourceLoadOptionsBase
                {
                    Sort = new SortingInfo[] { new SortingInfo { Selector = "Name" } }
                }).Data.Select(m => new SelectListItem { Text = $"{m.Name}", Value = m.Id.ToString() }).ToList(),
                ServiceList = serviceService.Search(new DataSourceLoadOptionsBase
                {
                    Sort = new SortingInfo[] { new SortingInfo { Selector = "Name" } }
                }).Data.Select(m => new SelectListItem { Text = $"{m.Name}", Value = m.Id.ToString() }).ToList(),
            };
            return new SuccessDataResult<UserDataDto>(data);
        }

        public IDataResult<SessionModel> Login(LoginRequest request)
        {
            var user = repository.GetQuery(u => u.Password == request.Password && u.Email == request.Email && u.Status == Status.Active).FirstOrDefault();
            if (user != null)
            {
                if (user.WrongLoginTryCount > WebSiteDefinitionHelper.WrongPasswordCount)
                {
                    return new ErrorDataResult<SessionModel>("AccountBlockedMustChangePassword");
                }

                if (DateTime.Now.AddDays(-WebSiteDefinitionHelper.PasswordExpireDay) >= user.LastPasswordChangeDate)
                {
                    return new ErrorDataResult<SessionModel>("PasswordExpiredMustChangePassword");
                }

                var session = new SessionModel();
                var userDto = user.Convert<UserDto>();

                userDto.UserRoles = userRole.GetByUserId(user.Id).Convert<List<UserRoleDto>>();

                session.JwtToken = tokenCreator.CreateToken(request, userDto);

                userDto.Bulletins = new List<UserBulletinDto>();
                var bulletinList = repository.GetQuery<UserBulletin>(x => x.Status == UserBulletinStatus.Unread && x.UserId == user.Id && x.ExpireDate >= DateTime.Now && x.StartDate <= DateTime.Now
                ).ToList();
                if (bulletinList.Count > 0)
                    userDto.Bulletins = bulletinList.Convert<List<UserBulletinDto>>();

                var roles = userRole.GetByUserId(user.Id);
                var allPermissions = rolePermission.GetAll();

                var userPermissions = new List<RolePermission>();

                foreach (var role in roles)
                {
                    var mainPermissions = allPermissions.Where(x => x.RoleId == role.RoleId && x.RolePermissionId == 0 && x.Status == Status.Active).ToList();
                    foreach (var mainPermission in mainPermissions)
                    {
                        userPermissions.Add(mainPermission);
                        var subPermissions = allPermissions.Where(x => x.RolePermissionId == mainPermission.Id && x.Status == Status.Active).OrderBy(x => x.Order).ToList();
                        foreach (var subPermission in subPermissions)
                        {
                            userPermissions.Add(subPermission);
                            var subPermissions2 = allPermissions.Where(x => x.RolePermissionId == subPermission.Id && x.Status == Status.Active).OrderBy(x => x.Order).ToList();
                            foreach (var subPermission2 in subPermissions2)
                            {
                                userPermissions.Add(subPermission2);
                                var subPermissions3 = allPermissions.Where(x => x.RolePermissionId == subPermission2.Id && x.Status == Status.Active).OrderBy(x => x.Order).ToList();
                                foreach (var subPermission3 in subPermissions3)
                                {
                                    userPermissions.Add(subPermission3);
                                }
                            }
                        }
                    }
                }
                session.Permissions = userPermissions.Convert<List<RolePermissionDto>>();

                user.LastSessionId = request.SessionId;
                user.LastLoginDate = DateTime.Now;
                user.WrongLoginTryCount = 0;


                session.User = userDto;
                try
                {
                    repository.Update(user, "LastSessionId", "LastLoginDate", "WrongLoginTryCount");
                    unitOfWork.SaveChanges();

                    memoryCache.Set("SessionId-" + user.Id, request.SessionId, TimeSpan.FromMinutes((request.RememberMe ? 12 : 1) * 60));

                    try
                    {
                        var userLog = new UserLog
                        {
                            MerchantId = user.MerchantId,
                            UserId = user.Id,
                            ActionType = "login",
                            Note = $"{RequestHelper.RemoteIp} logged the {user.Email} account at {DateTime.Now.ToString("HH:mm dd-MM-yyyy")}",
                            UserIp = RequestHelper.RemoteIp,
                            CreateTime = DateTime.Now,
                            CreateUserId = RequestHelper.UserId,
                        };
                        repository.Insert(userLog);
                        unitOfWork.SaveChanges();
                    }
                    catch (Exception) { }
                    return new SuccessDataResult<SessionModel>(session);
                }
                catch (Exception exp)
                {
                    logger.Error(exp.ToString());
                    return new ErrorDataResult<SessionModel>("InternalError");
                }
            }
            else
            {
                user = repository.GetQuery(u => u.Email == request.Email && u.Status == Status.Active).FirstOrDefault();
                if (user != null)
                {
                    try
                    {
                        if (WebSiteDefinitionHelper.WrongPasswordCount >= user.WrongLoginTryCount)
                        {
                            user.LastWrongLoginTryDate = DateTime.Now;
                            user.WrongLoginTryCount++;
                            user.UpdateTime = DateTime.Now;
                            repository.Update(user, "UpdateTime", "WrongLoginTryCount", "LastWrongLoginTryDate");
                            unitOfWork.SaveChanges();

                            var userBulletin = new UserBulletin
                            {
                                MerchantId = 0,
                                RoleGroupId = 0,
                                UserId = user.Id,
                                ActionType = "wrongPassword",
                                Title = "Hatalı Giriş Denemesi",
                                Message = $"{RequestHelper.RemoteIp} tried to login the {user.Email} account at {DateTime.Now.ToString("HH:mm dd-MM-yyyy")}",
                                Type = UserBulletinType.Warning,
                                Status = UserBulletinStatus.Unread,
                                StartDate = DateTime.Now,
                                ExpireDate = DateTime.Now.AddMonths(1),
                                CreateTime = DateTime.Now,
                                CreateUserId = RequestHelper.UserId,
                            };
                            repository.Insert(userBulletin);
                            unitOfWork.SaveChanges();

                            var userLog = new UserLog
                            {
                                MerchantId = user.MerchantId,
                                UserId = user.Id,
                                ActionType = "wrongPassword",
                                Note = $"{RequestHelper.RemoteIp} tried to login the {user.Email} account at {DateTime.Now.ToString("HH:mm dd-MM-yyyy")}",
                                UserIp = RequestHelper.RemoteIp,
                                CreateTime = DateTime.Now,
                                CreateUserId = RequestHelper.UserId,
                            };
                            repository.Insert(userLog);
                            unitOfWork.SaveChanges();
                        }
                    }
                    catch (Exception) { }
                }
            }
            return new ErrorDataResult<SessionModel>("InvalidUsernameOrPassword");
        }

        public UserDto GetFromCache(long Id)
        {
            var cacheKey = $"UserGetFromCache-{Id}";
            return memoryCache.GetOrCreate(cacheKey, (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                return Get(Id).Data.Convert<UserDto>();
            });
        }

        public IDataResult<User> Get(long Id, bool include = true)
        {
            var user = repository.GetById(Id);
            if (user != null)
            {
                if (include)
                {
                    user.Roles = role.GetAll();
                    user.UserRoles = userRole.GetByUserId(user.Id);
                }
                return new SuccessDataResult<User>(user);
            }
            return new ErrorDataResult<User>("RecordNotFound");
        }

        public override IDataResult<User> Add(User user)
        {
            unitOfWork.BeginTransaction();
            try
            {
                if (!repository.GetQuery(u => u.Email == user.Email).Any())
                {
                    user.CreateTime = DateTime.Now;
                    user.CreateUserId = RequestHelper.UserId;
                    user.Status = Status.Pending;

                    repository.Insert(user);
                    unitOfWork.SaveChanges();

                    var resRes = resetPasswordService.Add(user);
                    if (resRes.IsSuccess)
                    {
                        user.RoleIds = repository.GetQuery<Role>(x => x.Type == user.Type).Select(x => x.Id).ToList();
                        var userRoleId = repository.GetQuery<Role>(x => x.Type == UserType.User).Select(x => x.Id).FirstOrDefault();
                        if (!user.RoleIds.Contains(userRoleId))
                            user.RoleIds.Add(userRoleId);

                        foreach (var roleId in user.RoleIds)
                            userRole.Add(new UserRole { RoleId = roleId, UserId = user.Id });

                        unitOfWork.CommitTransaction();

                        mailHelper.SendResetPassword(user.Convert<UserDto>(), resRes.Data.Convert<ResetPasswordDto>());

                        return new SuccessDataResult<User>(user);
                    }
                    else
                    {
                        unitOfWork.RollBackTransaction();
                        return new ErrorDataResult<User>("InternalError");
                    }
                }
                else
                {
                    return new ErrorDataResult<User>("ExistsEmail");
                }
            }
            catch (Exception exp)
            {
                logger.Error(exp.ToString());
                unitOfWork.RollBackTransaction();
                return new ErrorDataResult<User>("InternalError " + exp.Message);
            }
        }

        public override IDataResult<User> Update(User user)
        {
            unitOfWork.BeginTransaction();
            try
            {
                var userRes = Get(user.Id);

                user.UpdateTime = DateTime.Now;
                user.UpdateUserId = RequestHelper.UserId;
                user.LastSessionId = "";

                userRes = base.Update(user);
                if (userRes.IsSuccess)
                {
                    userRole.DeleteByUserId(user.Id);

                    user.RoleIds = repository.GetQuery<Role>(x => x.Type == user.Type).Select(x => x.Id).ToList();
                    var userRoleId = repository.GetQuery<Role>(x => x.Type == UserType.User).Select(x => x.Id).FirstOrDefault();
                    if (!user.RoleIds.Contains(userRoleId))
                        user.RoleIds.Add(userRoleId);

                    foreach (var roleId in user.RoleIds)
                        userRole.Add(new UserRole { RoleId = roleId, UserId = user.Id });

                    unitOfWork.SaveChanges();
                    unitOfWork.CommitTransaction();

                    memoryCache.Remove("SessionId-" + user.Id);
                    memoryCache.Remove($"UserGetFromCache-{user.Id}");

                    return new SuccessDataResult<User>(user);
                }
                return userRes;
            }
            catch (Exception exp)
            {
                unitOfWork.RollBackTransaction();
                return new ErrorDataResult<User>("InternalError " + exp.Message);
            }
        }

        public Result ForgetPassword(string email)
        {
            var user = repository.GetQuery(t => t.Email == email).FirstOrDefault();
            if (user != null)
            {
                var resRes = resetPasswordService.Add(user);
                if (resRes.IsSuccess)
                {
                    mailHelper.SendResetPassword(user.Convert<UserDto>(), resRes.Data.Convert<ResetPasswordDto>());
                    return new SuccessResult();
                }
                else
                    return new ErrorResult("InternalError");
            }
            return new ErrorResult("UserNotFound");
        }

        public Result ResetPassword(long Id)
        {
            var userRes = Get(Id, false);
            if (userRes.IsSuccess)
            {
                var user = userRes.Data;

                var resRes = resetPasswordService.Add(user);
                if (resRes.IsSuccess)
                {
                    mailHelper.SendResetPassword(user.Convert<UserDto>(), resRes.Data.Convert<ResetPasswordDto>());
                    return new SuccessResult();
                }
                else
                    return new ErrorResult("InternalError");
            }
            return new ErrorResult("UserNotFound");
        }

        public Result Logout(long Id)
        {
            var user = repository.GetQuery(t => t.Id == Id).FirstOrDefault();
            if (user != null)
            {
                try
                {
                    user.LastSessionId = "";
                    Update(user, "LastSessionId");

                    memoryCache.Remove("SessionId-" + user.Id);
                    memoryCache.Remove($"UserGetFromCache-{user.Id}");

                    return new SuccessResult();
                }
                catch (Exception exp)
                {
                    logger.Error(exp.ToString());
                    return new ErrorResult("InternalError");
                }

            }
            return new ErrorResult("UserNotFound");
        }

        public Result AccountChangePassword(ChangePassword changePassword)
        {
            if (changePassword.NewPassword == changePassword.NewPasswordConfirm)
            {
                var userRes = Get(RequestHelper.UserId, false);
                if (userRes.IsSuccess)
                {
                    var user = userRes.Data;
                    user.Password = Md5Helper.Get(user.Email + ":" + changePassword.NewPassword);

                    var oldPasswords = repository.GetQuery<UserLog>(x => x.ActionType == "passwordChange")
                        .OrderByDescending(x => x.Id)
                        .Take(WebSiteDefinitionHelper.WrongPasswordCount)
                        .Select(x => x.Note).ToList();
                    if (!oldPasswords.Contains(user.Password))
                    {
                        try
                        {
                            user.LastPasswordChangeDate = DateTime.Now;
                            user.UpdateTime = DateTime.Now;
                            user.UpdateUserId = RequestHelper.UserId;
                            memoryCache.Remove("SessionId-" + user.Id);
                            memoryCache.Remove($"UserGetFromCache-{user.Id}");

                            repository.Update(user, "Password", "LastPasswordChangeDate", "UpdateTime", "UpdateUserId");
                            unitOfWork.SaveChanges();

                            var userLog = new UserLog
                            {
                                MerchantId = user.MerchantId,
                                UserId = user.Id,
                                ActionType = "passwordChange",
                                Note = user.Password,
                                UserIp = RequestHelper.RemoteIp,
                                CreateTime = DateTime.Now,
                                CreateUserId = RequestHelper.UserId,
                            };
                            repository.Insert(userLog);
                            unitOfWork.SaveChanges();

                            return new SuccessResult();
                        }
                        catch (Exception exp)
                        {
                            logger.Error(exp.ToString());
                            return new ErrorResult("InternalError");
                        }
                    }
                    else
                    {
                        return new ErrorResult("InvalidPasswordRequest");
                    }
                }
                else
                {
                    return new ErrorResult("UserNotFound");
                }
            }
            else
            {
                return new ErrorResult("NewPasswordNotEqual");
            }
        }

        public Result UpdatePassword(PasswordRequest passwordRequest)
        {
            if (passwordRequest.Password == passwordRequest.PasswordConfirm)
            {
                var resetPasswordRes = resetPasswordService.GetByToken(passwordRequest.Token);
                if (resetPasswordRes.IsSuccess
                    && resetPasswordRes.Data != null
                    && resetPasswordRes.Data.CreateTime >= DateTime.Now.AddMinutes(-30)
                    && resetPasswordRes.Data.Status == Status.New)
                {
                    var userRes = Get(resetPasswordRes.Data.UserId, false);
                    if (userRes.IsSuccess)
                    {
                        var user = userRes.Data;
                        user.Password = Md5Helper.Get(user.Email + ":" + passwordRequest.Password);

                        var oldPasswords = repository.GetQuery<UserLog>(x => x.ActionType == "passwordChange")
                            .OrderByDescending(x => x.Id)
                            .Take(WebSiteDefinitionHelper.WrongPasswordCount)
                            .Select(x => x.Note).ToList();
                        if (!oldPasswords.Contains(user.Password))
                        {
                            try
                            {
                                if (user.Status == Status.New || user.Status == Status.Pending)
                                    user.Status = Status.Active;
                                user.LastPasswordChangeDate = DateTime.Now;
                                user.UpdateTime = DateTime.Now;
                                user.UpdateUserId = RequestHelper.UserId;
                                user.LastSessionId = "";
                                user.WrongLoginTryCount = 0;
                                memoryCache.Remove("SessionId-" + user.Id);
                                memoryCache.Remove($"UserGetFromCache-{user.Id}");

                                repository.Update(user, "Password", "LastPasswordChangeDate", "UpdateTime", "UpdateUserId", "Status", "LastSessionId", "WrongLoginTryCount");
                                unitOfWork.SaveChanges();

                                resetPasswordRes.Data.Status = Status.Active;
                                var rr = resetPasswordService.Update(resetPasswordRes.Data, "Status");
                                unitOfWork.SaveChanges();

                                var userLog = new UserLog
                                {
                                    MerchantId = user.MerchantId,
                                    UserId = user.Id,
                                    ActionType = "passwordChange",
                                    Note = user.Password,
                                    UserIp = RequestHelper.RemoteIp,
                                    CreateTime = DateTime.Now,
                                    CreateUserId = RequestHelper.UserId,
                                };
                                repository.Insert(userLog);
                                unitOfWork.SaveChanges();

                                return new SuccessResult();
                            }
                            catch (Exception exp)
                            {
                                logger.Error(exp.ToString());
                                return new ErrorResult("InternalError");
                            }
                        }
                        else
                        {
                            return new ErrorResult("UsedPasswordInOldPasswords");
                        }
                    }
                    else
                    {
                        return new ErrorResult("UserNotFound");
                    }
                }
                else
                {
                    return new ErrorResult("InvalidPasswordRequest");
                }
            }
            else
            {
                return new ErrorResult("PasswordMismatch");
            }
        }

        public override IQueryable<User> BeforeSearch(IQueryable<User> req)
        {
            if (RequestHelper.MerchantId > 0)
            {
                req = req.Where(x => x.MerchantId == RequestHelper.MerchantId);
            }
            return base.BeforeSearch(req);
        }
    }

}
