using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hayalpc.Fatura.Vezne.External.Models
{
    public class BaseResponse
    {
        public bool IsSuccess { get => ResultCode == 0; }
        public int ResultCode { get; set; }
        public string ResultDescription { get; set; }
        public string Url { get; set; }

    }
}
