namespace Cryptocop.Software.API.Repositories.Helpers
{
    public class PaymentCardHelper
    {
        public string MaskPaymentCard(string paymentCardNumber)
        {
            return paymentCardNumber
                .Substring(0, 6) + "******" + paymentCardNumber
                .Substring(12, 4);
        }
    }
}