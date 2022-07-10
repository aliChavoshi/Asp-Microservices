using Application.Models;

namespace Application.Contracts.Infrastructure;

public interface IEmailService
{
    Task<bool> SendEmail(Email emailSend);
}