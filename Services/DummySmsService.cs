using ForgeHire.Services.IServices;

namespace ForgeHire.Services
{
    public class DummySmsService : ISmsService
    {
        public Task SendSmsAsync(string mobile, string message)
        {
            Console.WriteLine("=================================");
            Console.WriteLine($"SMS to: {mobile}");
            Console.WriteLine($"Message: {message}");
            Console.WriteLine("=================================");
            return Task.CompletedTask;
        }
    }
}
