using Hayalpc.Library.Common.Helpers.Interfaces;
using Hayalpc.Library.Log;
using Hayalpc.Fatura.Panel.External.Services.Interfaces;
using Hayalpc.Fatura.Panel.External.Models;

namespace Hayalpc.Fatura.Panel.External.Services
{
    [ServiceTypeAttr]
    public class SmsService : BaseService<SmsVM>, ISmsService
    {
        public SmsService(IHttpClientHelper clientHelper, IHpLogger logger) : base(clientHelper, logger,"sms")
        {
        }
      
    }
}
