using Hayalpc.Fatura.Panel.External.Services.Interfaces;
using Hayalpc.Library.Common.Dtos;
using Hayalpc.Library.Common.Helpers.Interfaces;
using Hayalpc.Library.Common.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hayalpc.Fatura.Panel.External.Controllers
{

    public class BlobFileController : Controller
    {
        private readonly IBlobFileService service;
        private readonly Hayalpc.Fatura.Common.Helpers.Interfaces.ISessionHelper session;
        private readonly IBlobStorageHelper storageHelper;

        public BlobFileController(IBlobFileService service, Hayalpc.Fatura.Common.Helpers.Interfaces.ISessionHelper session, IBlobStorageHelper storageHelper)
        {
            this.service = service;
            this.session = session;
            this.storageHelper = storageHelper;
        }

        [AllowAnonymous]
        public IActionResult Get(Guid guid)
        {
            if (session.IsAuthenticated)
            {
                var blob = service.Get<DataResult<BlobFileDto>>($"blobFile/getByGuid/{guid}");
                if (blob.IsSuccess)
                {
                    var downloadInfo = storageHelper.Download(blob.Data.BlobUrl, blob.Data.AccountName,blob.Data.AccountKey);
                    if (downloadInfo.Success)
                    {
                        return File(downloadInfo.Stream, downloadInfo.ContentType, blob.Data.FileName);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}
