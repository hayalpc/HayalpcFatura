using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hayalpc.Fatura.Common.Dtos
{
    public class InstitutionDto
    {
        public long Id { get; set; }
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public long CategoryId { get; set; }

        public string Type { get; set; }

        public string Placeholder { get; set; }

        public string ValidationText { get; set; }
        
        public bool Reverse { get; set; }

        public string Logo { get; set; }

    }
}
