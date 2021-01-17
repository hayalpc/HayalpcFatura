using System.Collections.Generic;

namespace Hayalpc.Fatura.Common.Dtos
{
    public class DealerDto
    {
        public long Code { get; set; }

        public string Logo { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Priorty { get; set; } = 100;

        public bool Default { get; set; } = false;

        public string Channel { get; set; }

        public string PersonName { get; set; }

        public string PersonSurname { get; set; }

        public string PersonPhone { get; set; }

        public string PersonEmail { get; set; }

        public string PersonAddress { get; set; }

        public string PersonTckNo { get; set; }

        public string PersonIban { get; set; }

        public string PersonAccountName { get; set; }

        public virtual List<BlobFileDto> BlobFiles { get; set; }

    }
}
