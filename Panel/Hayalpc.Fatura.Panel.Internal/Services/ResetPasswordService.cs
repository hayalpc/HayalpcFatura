using Hayalpc.Library.Common.Results;
using Hayalpc.Library.Log;
using Hayalpc.Library.Repository.Interfaces;
using Hayalpc.Fatura.Data;
using Hayalpc.Fatura.Data.Models;
using System;
using System.Linq;
using Hayalpc.Library.Common.Helpers;

namespace Hayalpc.Fatura.Panel.Internal.Services
{
    public class ResetPasswordService : IResetPasswordService
    {
        private readonly IRepository<ResetPassword> repository;
        private readonly IHpLogger logger;
        private readonly IHpUnitOfWork<HpDbContext> unitOfWork;

        public ResetPasswordService(IRepository<ResetPassword> repository, IHpLogger logger, IHpUnitOfWork<HpDbContext> unitOfWork)
        {
            this.repository = repository;
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        public IDataResult<ResetPassword> Add(ResetPassword resetPassword)
        {
            try
            {
                resetPassword.CreateTime = DateTime.Now;
                resetPassword.CreateUserId = RequestHelper.UserId;

                repository.Insert(resetPassword);
                unitOfWork.SaveChanges();
                return new SuccessDataResult<ResetPassword>(resetPassword);
            }
            catch (Exception exp)
            {
                logger.Error(exp.ToString());
                return new ErrorDataResult<ResetPassword>(500, exp.Message);
            }
        }

        public IDataResult<ResetPassword> Add(User user)
        {
            var resetPassword = new ResetPassword();
            resetPassword.Token = Guid.NewGuid();
            resetPassword.UserId = user.Id;
            return Add(resetPassword);
        }

        public IDataResult<ResetPassword> GetByToken(Guid token)
        {
            var res = repository.GetQuery(x => x.Token == token && x.Status == Hayalpc.Library.Common.Enums.Status.New).FirstOrDefault();
            if (res != null)
                return new SuccessDataResult<ResetPassword>(res);
            else
                return new ErrorDataResult<ResetPassword>("RecordNotFound");
        }

        public IDataResult<ResetPassword> Update(ResetPassword model)
        {
            try
            {
                model.UpdateTime = DateTime.Now;
                model.UpdateUserId = RequestHelper.UserId;

                repository.Update(model);

                unitOfWork.SaveChanges();
                return new SuccessDataResult<ResetPassword>(model);
            }
            catch (Exception exp)
            {
                return new ErrorDataResult<ResetPassword>("InternalError " + exp.Message);
            }
        }

        public IDataResult<ResetPassword> Update(ResetPassword model, params string[] fields)
        {
            try
            {
                model.UpdateTime = DateTime.Now;
                model.UpdateUserId = RequestHelper.UserId;

                Array.Resize(ref fields, fields.Length + 2);
                fields[fields.Length - 1] = "UpdateTime";
                fields[fields.Length - 2] = "UpdateUserId";

                repository.Update(model, fields);
                return new SuccessDataResult<ResetPassword>(model);
            }
            catch (Exception exp)
            {
                return new ErrorDataResult<ResetPassword>("InternalError " + exp.Message);
            }
        }
    }
}
