using System.Text;
using Application.Dtos.Request.Order.Momo;
using Application.Dtos.Response.Order.Momo;
using Application.ErrorHandlers;
using Application.Utils;
using ClassLibrary1.Interface.IServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebAPI.Gateway.IConfig;

namespace ClassLibrary1.Third_Parties.Service;

public class MomoService : IMomoService
{
    IMomoConfig _momoConfig;
    private IOrderService _orderService;

    public MomoService(IMomoConfig momoConfig, IOrderService orderService)
    {
        _momoConfig = momoConfig;
        _orderService = orderService;
    }

    public (string?, string?) GetLinkMomoGateway(string paymentUrl, MomoPaymentRequest momoRequest)
    {
        using HttpClient client = new HttpClient();
        var requestData = JsonConvert.SerializeObject(momoRequest, new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.Indented,
        });
        var requestContent = new StringContent(requestData, Encoding.UTF8, "application/json");

        var createPaymentLink = client.PostAsync(paymentUrl, requestContent).Result;
        if (createPaymentLink.IsSuccessStatusCode)
        {
            var responseContent = createPaymentLink.Content.ReadAsStringAsync().Result;
            var responeseData = JsonConvert.DeserializeObject<MomoPaymentResponse>(responseContent);
            // return QRcode
            if (responeseData?.resultCode == 0)
                return (responeseData.payUrl, responeseData.qrCodeUrl);
            throw new NotImplementException($"Error Momo: {responeseData?.message}");
        }

        throw new NotImplementException($"Error Momo: {createPaymentLink.ReasonPhrase}");
    }

    public string MakeSignatureMomoPayment(string accessKey, string secretKey, MomoPaymentRequest momo)
    {
        var rawHash = "accessKey=" + accessKey +
                      "&amount=" + momo.amount + "&extraData=" + momo.extraData +
                      "&ipnUrl=" + momo.ipnUrl + "&orderId=" + momo.orderId +
                      "&orderInfo=" + momo.orderInfo + "&partnerCode=" + momo.partnerCode +
                      "&redirectUrl=" + momo.redirectUrl + "&requestId=" + momo.requestId + "&requestType=" +
                      momo.requestType;
        return momo.signature = HashingUtils.HmacSha256(rawHash, secretKey);
    }

    public (string?, string?) CreatePaymentMomoAsync(int id)
    {
        var momoRequest = new MomoPaymentRequest();
        //Get order có parent id và order id vs status payment
        var order = _orderService.GetOrderByProcessing(id);

        // Lấy thông tin cho payment
        momoRequest.requestId = StringUtils.GenerateRandomNumberString(4) + "-" + order?.CustomerId;
        momoRequest.orderId = StringUtils.GenerateRandomNumberString(4) + "-" + order?.Id;
        momoRequest.amount = (long)order!.Total;
        momoRequest.redirectUrl = _momoConfig.ReturnUrl;
        momoRequest.ipnUrl = _momoConfig.IpnUrl;
        momoRequest.partnerCode = _momoConfig.PartnerCode;
        momoRequest.orderInfo = " 'HandMade Service' - You are paying for product";
        momoRequest.signature = MakeSignatureMomoPayment
            (_momoConfig.AccessKey, _momoConfig.SecretKey, momoRequest);

        // lấy link QR momo
        var result = GetLinkMomoGateway(_momoConfig.PaymentUrl, momoRequest);
        return result;
    }
}