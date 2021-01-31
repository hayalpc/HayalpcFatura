using Hayalpc.Fatura.Panel.External.Models;
using Hayalpc.Fatura.Panel.External.Resources;
using Hayalpc.Fatura.Panel.External.Services.Interfaces;
using Hayalpc.Fatura.Common.Helpers.Interfaces;
using Hayalpc.Library.Log;

namespace Hayalpc.Fatura.Panel.External.Controllers
{
    public class TableDefinitionController : BaseController<TableDefinitionVM, ITableDefinitionService>
    {
        public TableDefinitionController(ITableDefinitionService service,LocService tr, ISessionHelper session, IHpLogger logger) : base(service,tr, session, logger)
        {
        }

    }
}
