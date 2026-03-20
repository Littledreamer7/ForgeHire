namespace ForgeHire.Services.IServices
{
    public interface ISmsService
    {
        Task SendSmsAsync(string mobile, string message);
    }
}
