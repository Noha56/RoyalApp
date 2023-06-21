using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoyalFinalApp.Areas.Payment.Dtos;
using RoyalFinalApp.Areas.Payment.Services;
using Stripe;
using Twilio.TwiML.Voice;

namespace RoyalFinalApp.Areas.Payment.Controllers
{
    [Area("Payment")]
    [Authorize(Roles = "User")]
    public class SMSController : Controller
    {

        private readonly ISMService _smsService;
        public SMSController(ISMService smsService)
        {
            _smsService= smsService;
        }

        public IActionResult Send()
        {
            SendSMSDto dto = new SendSMSDto
            {
                MobileNumber="+962791955168",
                Body="Thank you for choosing Royal Booking system!"
            };
            //areas\Payment\Views\SMS\Send.cshtml

            var result = _smsService.Send(dto.MobileNumber, dto.Body);

            if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                return Content("Error");
            }
            return View();

        }
    }
}
