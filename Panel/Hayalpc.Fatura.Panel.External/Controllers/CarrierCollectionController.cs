using Hayalpc.Fatura.Panel.External.Models;
using Hayalpc.Fatura.Panel.External.Resources;
using Hayalpc.Fatura.Panel.External.Services.Interfaces;
using Hayalpc.Library.Common;
using Hayalpc.Library.Common.Dtos;
using Hayalpc.Library.Common.Helpers.Interfaces;
using Hayalpc.Library.Common.Results;
using Hayalpc.Library.Log;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hayalpc.Fatura.Panel.External.Controllers
{
    public class CarrierCollectionController : BaseController<CarrierCollectionVM, ICarrierCollectionService>
    {
        private readonly IBlobStorageHelper storageHelper;
        public CarrierCollectionController(ICarrierCollectionService service, LocService tr, ISessionHelper session, IHpLogger logger, IBlobStorageHelper storageHelper) : base(service, tr, session, logger)
        {
            this.storageHelper = storageHelper;
        }

        public override CarrierCollectionVM BeforeAdd(CarrierCollectionVM model)
        {
            model.FileName = " ";

            return base.BeforeAdd(model);
        }

        public override CarrierCollectionVM AfterAdd(CarrierCollectionVM model)
        {
            var formFile = HttpContext.Request.Form.Files.GetFile("file");
            if (formFile.Length > 0)
            {
                try
                {
                    BlobFileDto blobList = null;
                    var stream = new MemoryStream();
                    formFile.CopyTo(stream);

                    var guid = Guid.NewGuid();
                    var fileName = $"Collection/{model.Id}/{guid}_" + formFile.FileName;
                    var blobResult = storageHelper.Upload(stream, fileName);
                    if (blobResult.Success)
                    {
                        blobList = new BlobFileDto
                        {
                            DataId=model.Id,
                            Type = Library.Common.Enums.BlobFileType.Collection,
                            Token = guid,
                            Name = formFile.FileName,
                            FileName = blobResult.FileName,
                            BlobUrl = blobResult.BlobUrl,
                            AccountName = blobResult.AccountName,
                            AccountKey = blobResult.AccountKey,
                            Status = Library.Common.Enums.Status.Active,
                            CreateUserId = session.User.Id,
                            CreateTime = DateTime.Now
                        };
                        model.FileName = blobList.FileName;
                    }
                    if (blobList != null)
                    {
                        var result = service.Post<BlobFileDto, DataResult<BlobFileDto>>("blobFile/add", blobList);
                        if (result.IsSuccess)
                        {
                            model.BlobId = result.Data.Id;
                            model.Status = Library.Common.Enums.CarrierCollectionStatus.New;
                        }
                        Task.Run(() => service.Update(model.Id, model)).Forget();
                    }
                }
                catch(Exception exp)
                {
                    logger.Error(exp.ToString());
                }
            }
            return base.AfterAdd(model);
        }

        public override IActionResult Update(long id) => NotFound();

        public override IActionResult Update(long id, CarrierCollectionVM model) => NotFound();
        
    }
}
