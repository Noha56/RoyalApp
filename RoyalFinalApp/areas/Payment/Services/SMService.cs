using Microsoft.Extensions.Options;
using RoyalFinalApp.areas.Payment.Helpers;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace RoyalFinalApp.Areas.Payment.Services
{
    public class SMService : ISMService
    {
        private readonly TwilioSettings _twilio;
        public SMService(IOptions<TwilioSettings> twilio)
        {
            _twilio = twilio.Value;
        }
        public MessageResource Send(string mobileNumber, string body)
        {
            TwilioClient.Init(_twilio.AccountSID, _twilio.AuthToken);

            var result = MessageResource.Create(
                body: body,
                from: new Twilio.Types.PhoneNumber(_twilio.TwilioPhoneNumber),
                to: mobileNumber
                );
            return result;
        }
    }
}