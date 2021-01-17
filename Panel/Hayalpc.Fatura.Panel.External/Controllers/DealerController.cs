using Hayalpc.Fatura.Panel.External.Models;
using Hayalpc.Fatura.Panel.External.Resources;
using Hayalpc.Fatura.Panel.External.Services.Interfaces;
using Hayalpc.Library.Common.Dtos;
using Hayalpc.Library.Common.Helpers.Interfaces;
using Hayalpc.Library.Common.Results;
using Hayalpc.Library.Log;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

namespace Hayalpc.Fatura.Panel.External.Controllers
{
    public class DealerController : BaseController<DealerVM, IDealerService>
    {
        private readonly IBlobStorageHelper storageHelper;

        public DealerController(IDealerService service, LocService tr, ISessionHelper session, IHpLogger logger, IBlobStorageHelper storageHelper) : base(service, tr, session, logger)
        {
            this.storageHelper = storageHelper;
        }

        public override DealerVM BeforeUpdate(long id, DealerVM model)
        {
            var files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {
                var blobList = new List<BlobFileDto>();
                foreach (var formFile in files.GetFiles("files"))
                {
                    if (formFile.Length > 0)
                    {
                        var stream = new MemoryStream();
                        formFile.CopyTo(stream);

                        var guid = Guid.NewGuid();
                        var fileName = $"Merchant/{id}/{guid}_" + formFile.FileName;
                        var blobResult = storageHelper.Upload(stream, fileName);
                        if (blobResult.Success)
                        {
                            blobList.Add(new Fatura.Common.Dtos.BlobFileDto
                            {
                                DataId = id,
                                Type = Fatura.Common.Enums.BlobFileType.Dealer,
                                Token = guid,
                                Name = formFile.FileName,
                                FileName = blobResult.FileName,
                                BlobUrl = blobResult.BlobUrl,
                                AccountName = blobResult.AccountName,
                                AccountKey = blobResult.AccountKey,
                                Status = Library.Common.Enums.Status.Active,
                                CreateUserId = session.User.Id,
                                CreateTime = DateTime.Now
                            });
                        }
                    }
                }
                if (blobList.Count > 0)
                {
                    var result = service.Post<List<BlobFileDto>, Result>("blobFile/addRange", blobList);
                }
            }
            return base.BeforeUpdate(id, model);
        }

        
    }
}
