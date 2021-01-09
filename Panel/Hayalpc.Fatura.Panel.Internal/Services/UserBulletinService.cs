using Hayalpc.Fatura.Data;
using Hayalpc.Fatura.Data.Models;
using Hayalpc.Fatura.Panel.Internal.Services.Interfaces;
using Hayalpc.Library.Common.Helpers;
using Hayalpc.Library.Common.Results;
using Hayalpc.Library.Log;
using Hayalpc.Library.Repository.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;

namespace Hayalpc.Fatura.Panel.Internal.Services
{
    public class UserBulletinService : BaseService<UserBulletin>, IUserBulletinService
    {
        public UserBulletinService(IRepository<UserBulletin> repository, IHpLogger logger, IHpUnitOfWork<HpDbContext> unitOfWork, IMemoryCache memoryCache) :
            base(repository, logger, unitOfWork, memoryCache)
        {
        }

        public IResult ReadAll()
        {
            var query = repository.GetQuery(x=>
                     x.Status == Library.Common.Enums.UserBulletinStatus.Unread
                     && x.UserId == RequestHelper.UserId
                 );

            var list = query.ToList();
            if (list.Count > 0)
            {
                foreach (var notif in list)
                {
                    notif.Status = Library.Common.Enums.UserBulletinStatus.Read;
                    notif.ReadDate = DateTime.Now;
                    notif.UpdateTime = DateTime.Now;
                    repository.Update(notif, "ReadDate", "UpdateTime", "Status");
                }
                unitOfWork.SaveChanges();
            }
            return new SuccessResult();
        }

        public IResult Read(long Id)
        {
            //userId ekle
            try
            {
                var model = repository.Get(x => x.Id == Id);
                model.ReadDate = DateTime.Now;
                model.UpdateTime = DateTime.Now;
                model.Status = Library.Common.Enums.UserBulletinStatus.Read;

                repository.Update(model, "ReadDate", "UpdateTime", "Status");
                unitOfWork.SaveChanges();
            }
            catch(Exception exp)
            {
                logger.Error(exp.ToString());
            }
            return new SuccessResult();
        }

        public override IQueryable<UserBulletin> BeforeSearch(IQueryable<UserBulletin> req)
        {
            req = req.Where(x => x.UserId == RequestHelper.UserId);
            return base.BeforeSearch(req);
        }
    }
}
