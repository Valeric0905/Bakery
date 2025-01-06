using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BakeryWeb.Controllers
{
    public class PaymentController : Controller
    {
        private const string RoboKassaUrl = "https://auth.robokassa.ru/Merchant/Index.aspx"; // Payment URL
        private const string MerchantLogin = "Peky_bakery"; // Replace with your RoboKassa login
        private const string Password1 = "PB99GbF2fC"; // Replace with your RoboKassa Password1
        private const string Password2 = "PB99GbF2fC"; // Replace with your RoboKassa Password2 (used for result validation)

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RedirectToRoboKassa(string name, string email, string phone, decimal amount)
        {
            // Generate unique order ID
            var orderId = Guid.NewGuid(); // Replace with your order logic

            // Prepare data for RoboKassa
            var description = $"Order payment for {name}";
            var signature = HashHelper.GetMd5Hash($"{MerchantLogin}:{amount:F2}:{orderId}:{Password1}");

            // Build query string
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["MerchantLogin"] = MerchantLogin;
            query["OutSum"] = amount.ToString("F2");
            query["InvId"] = orderId.ToString();
            query["Description"] = description;
            query["SignatureValue"] = signature;
            query["Email"] = email;
            query["Phone"] = phone;

            // Redirect to RoboKassa
            return Redirect($"{RoboKassaUrl}?{query}");
        }

        [HttpPost]
        public IActionResult Result(string OutSum, string InvId, string SignatureValue)
        {
            // Validate Signature
            var expectedSignature = HashHelper.GetMd5Hash($"{OutSum}:{InvId}:{Password2}");
            if (expectedSignature.Equals(SignatureValue, StringComparison.OrdinalIgnoreCase))
            {
                // Payment verification succeeded
                // Update order status in the database
                // Add your database logic here to mark the order as paid

                return Content("OK"); // RoboKassa requires "OK" for successful handling
            }

            // Signature verification failed
            return StatusCode(400); // Bad Request
        }

        public IActionResult Success(string OutSum, string InvId)
        {
            // Show success page
            ViewBag.Message = $"Payment succeeded. Amount: {OutSum}, Order ID: {InvId}";
            return View();
        }

        public IActionResult Fail(string OutSum, string InvId)
        {
            // Show failure page
            ViewBag.Message = $"Payment failed for Order ID: {InvId}";
            return View();
        }
    }

    public static class HashHelper
    {
        public static string GetMd5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(input);
                var hashBytes = md5.ComputeHash(inputBytes);

                var sb = new StringBuilder();
                foreach (var b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}




