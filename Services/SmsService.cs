using ForgeHire.Helpers;
using ForgeHire.Services.IServices;
using System.Text;
using System.Text.Json;

namespace ForgeHire.Services
{
    public class SmsService : ISmsService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _http;

        public SmsService(IConfiguration config, HttpClient http)
        {
            _config = config;
            _http = http;
        }

        public async Task SendSmsAsync(string mobile, string message)
        {
            if (!PhoneHelper.IsValidIndianMobile(mobile))
                throw new Exception("Invalid mobile number");

            var normalizedMobile = PhoneHelper.NormalizeMobile(mobile);

            Console.WriteLine($"OTP DEBUG -> {normalizedMobile}: {message}");

            var apiKey = _config["Sms:ApiKey"];
            var senderId = _config["Sms:SenderId"];

            var payload = new
            {
                sender = senderId,
                route = "4",
                country = "91",
                sms = new[]
                {
                    new
                    {
                        message = message,
                        to = new[] { normalizedMobile }
                    }
                }
            };

            var request = new HttpRequestMessage(
                HttpMethod.Post,
                "https://api.msg91.com/api/v2/sendsms"
            );

            request.Headers.Add("authkey", apiKey);

            request.Content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _http.SendAsync(request);

            var result = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"MSG91 RESPONSE: {result}");
        }
    }
}