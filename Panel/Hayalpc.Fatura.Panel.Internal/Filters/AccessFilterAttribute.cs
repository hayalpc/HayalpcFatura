using Microsoft.AspNetCore.Mvc;

namespace Hayalpc.Fatura.Panel.Internal.Filters
{
    public class AccessFilterAttribute : TypeFilterAttribute
    {
        public AccessFilterAttribute() : base(typeof(AccessFilter))
        {
        }
    }
}
