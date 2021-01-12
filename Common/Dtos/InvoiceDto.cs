namespace Hayalpc.Fatura.Common.Dtos
{
    public class InvoiceDto
    {
        public long InstutionId { get; set; }
        public string SubscriberNo { get; set; }
        public string InstutionName { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceOwner { get; set; }
        public decimal Amount { get; set; }
        public decimal DelayAmount { get; set; }
        public decimal Fee { get; set; }
        public decimal TotalAmount { get; set; }
    }

}