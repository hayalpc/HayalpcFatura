using Microsoft.AspNetCore.Mvc;

namespace Hayalpc.Fatura.CoreApi.Filters
{
    public class AccessFilterAttribute : TypeFilterAttribute
    {
        public AccessFilterAttribute() : base(typeof(AccessFilter))
        {
        }
    }
}
