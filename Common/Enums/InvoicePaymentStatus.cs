using System;
using System.Collections.Generic;
using System.Text;

namespace Hayalpc.Fatura.Common.Enums
{
    public enum InvoicePaymentStatus
    {
        New=0,
        Active=1,
        Processing=2,
        Success=3,
        Error=4,
        Timeout=5,
    }
}
