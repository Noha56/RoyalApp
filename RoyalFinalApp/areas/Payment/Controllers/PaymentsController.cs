using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace RoyalFinalApp.areas.Payment.Controllers
{
    [Area("Payment")]
    [Authorize(Roles = "User")]
    public class PaymentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CheckOut()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CheckOut(string stripeEmail, string stripeToken)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email= stripeEmail,
                Source=stripeToken,
            });

            var charge = charges.Create(new ChargeCreateOptions
            {

                Amount=5,
                Description="Test Payment",
                Currency="usd",
                Customer=customer.Id,
            });
            if (charge.Status=="succeeded")
            {
                string BalanceTransactionId = charge.BalanceTransactionId;
                return RedirectToAction("Send", "SMS");//, new {area="Payment"}
            }
            else
            {
                return View();

            }
        }
    }
}
