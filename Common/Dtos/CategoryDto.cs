using System;
using System.Collections.Generic;
using System.Text;

namespace Hayalpc.Fatura.Common.Dtos
{
    public class CategoryDto
    {
        public long Id { get; set; }
        public string Code { get; set; }

        public string Logo { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
