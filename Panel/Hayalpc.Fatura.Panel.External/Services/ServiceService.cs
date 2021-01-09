using Hayalpc.Library.Common.Helpers.Interfaces;
using Hayalpc.Library.Log;
using Hayalpc.Fatura.Panel.External.Services.Interfaces;
using Hayalpc.Library.Common.Results;
using Hayalpc.Fatura.Panel.External.Models;

namespace Hayalpc.Fatura.Panel.External.Services
{
    public class ServiceService : BaseService<ServiceVM>, IServiceService
    {
        public ServiceService(IHttpClientHelper clientHelper, IHpLogger logger) : base(clientHelper, logger,"service")
        {
        }

    }
}
