using Microsoft.AspNetCore.Mvc;

namespace Hayalpc.Fatura.Vezne.External.Filters
{
    public class AccessFilterAttribute : TypeFilterAttribute
    {
        public AccessFilterAttribute() : base(typeof(AccessFilter))
        {
        }
    }
}
