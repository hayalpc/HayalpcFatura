using Hayalpc.Library.Common.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hayalpc.Fatura.Common.Helpers 
{
    public class RequestHelper : Hayalpc.Library.Common.Helpers.RequestHelper
    {
        [ThreadStatic]
        public static long DealerId = 0;
      
    }
}
