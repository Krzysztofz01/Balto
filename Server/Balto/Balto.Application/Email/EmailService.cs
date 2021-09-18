using FluentEmail.Core;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balto.Application.Email
{
    public class EmailService : IEmailService
    {
        private readonly IFluentEmailFactory _fluentEmailFactory;

        public EmailService(IFluentEmailFactory fluentEmailFactory)
        {
            _fluentEmailFactory = fluentEmailFactory ??
                throw new ArgumentNullException(nameof(fluentEmailFactory));
        }

        public async Task SendEmail(string address, string subject, string body)
        {
            await _fluentEmailFactory
                .Create()
                .To(address)
                .Subject(subject)
                .Body(body)
                .SendAsync();
        }

        public async Task SendEmail(string[] addresses, string subject, string body)
        {
            var addressSb = new StringBuilder();
            foreach(var address in addresses)
            {
                addressSb.Append(address.Trim());

                if (address != addresses.Last()) addressSb.Append(";");
            }

            await _fluentEmailFactory
                .Create()
                .To(addressSb.ToString())
                .Subject(subject)
                .Body(body)
                .SendAsync();
        }
    }
}
