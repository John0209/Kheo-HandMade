using Application.Dtos.Request.Order.Momo;

namespace ClassLibrary1.Third_Parties.Service;

public interface IMomoService
{
    (string?, string?) GetLinkMomoGateway(string paymentUrl, MomoPaymentRequest momoRequest);
    string MakeSignatureMomoPayment(string accessKey, string secretKey, MomoPaymentRequest momo);
    public (string?, string?) CreatePaymentMomoAsync(int id);
}