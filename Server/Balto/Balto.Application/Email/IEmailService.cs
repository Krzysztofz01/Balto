using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Application.Email
{
    public interface IEmailService
    {
        Task SendEmail(string address, string subject, string body);
        Task SendEmail(string[] addresses, string subject, string body);
    }
}
