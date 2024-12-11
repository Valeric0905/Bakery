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
        private const string Password1 = "PB99GbF2fC"; // Replace with your RoboKassa password1

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RedirectToRoboKassa(string name, string email, string phone, decimal amount)
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

