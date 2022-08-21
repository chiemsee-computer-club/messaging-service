using MessagingServer.Dto;

namespace MessagingServer.Services;

public interface IMessagingService
{
    public Task<SendResult> SendAsync(SendRequest request);
}