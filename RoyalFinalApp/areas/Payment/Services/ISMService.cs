using Twilio.Rest.Api.V2010.Account;

namespace RoyalFinalApp.Areas.Payment.Services
{
    public interface ISMService
    {
        MessageResource Send(string mobileNumber, string body);

    }
}
