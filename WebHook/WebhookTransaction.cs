namespace VpnWebHook.WebHook
{
    using System;

    public class WebhookTransaction
    {
        public string TransactionType { get; set; }
        public Guid TransactionId { get; set; }
        public string TransactionStatus { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
        public string TerminalName { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string OrderId { get; set; }
        public string OrderDescription { get; set; }
        public decimal Commission { get; set; }
        public DateTime PaymentTime { get; set; }
        public string Email { get; set; }
    }
}
