using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using RoyalFinalApp.Models;

namespace RoyalFinalApp.Controllers
{
    public class GoogleMapController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult ContactUs(ContactUs contact)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                MailMessage mail = new MailMessage();
                // you need to enter your mail address
                mail.From = new MailAddress("nneemmoo56@hotmail.com");

                //To Email Address - your need to enter your to email address
                mail.To.Add("nuhaalsaafin2@gmail.com");

                mail.Subject = contact.Subject;

                //you can specify also CC and BCC - i will skip this
                //mail.CC.Add("");
                //mail.Bcc.Add("");

                mail.IsBodyHtml = true;

                string content = "Name : " + contact.Name;
                content += "<br/> Message : " + contact.Message;

                mail.Body = content;


                //create SMTP instant

                //you need to pass mail server address and you can also specify the port number if you required
                SmtpClient smtpClient = new SmtpClient("smtp.live.com");

                //Create nerwork credential and you need to give from email address and password
                NetworkCredential networkCredential = new NetworkCredential("nneemmoo56@hotmail.com", "");
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = networkCredential;
                smtpClient.Port =  587; // this is default port number - you can also change this
                smtpClient.EnableSsl = true; // if ssl required you need to enable it
                smtpClient.Send(mail);

                ViewBag.Message = "Mail Send";

                // now i need to create the from 
                ModelState.Clear();

            }
            catch (Exception ex)
            {
                //If any error occured it will show
                ViewBag.Message = ex.Message.ToString();
                return RedirectToAction(nameof(Index));

            }


            return RedirectToAction(nameof(Index));
        }
    }
}

    
