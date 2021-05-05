using Balto.Repository;
using Balto.Service.Dto;
using Balto.Service.Handlers;
using Balto.Service.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Service
{
    public class EmailService : IEmailService
    {
        private readonly IObjectiveService objectiveService;
        private readonly SMTPSettings smtpSettings;

        public EmailService(
            IObjectiveRepository objectiveRepository,
            IOptions<SMTPSettings> smtpSettings)
        {
            this.objectiveService = objectiveService ??
                throw new ArgumentNullException(nameof(objectiveService));

            this.smtpSettings = smtpSettings.Value ??
                throw new ArgumentNullException(nameof(smtpSettings));
        }

        public async Task ObjectiveReminderDay()
        {
            var result = await objectiveService.IncomingObjectivesDay();
            if (result.Status() != ResultStatus.Sucess) return;

            var messages = GenerateMessagesForObjectivesV1(result.Result());

            using (var client = new SmtpClient())
            {
                //Connect to the SMTP Server
                await client.ConnectAsync(smtpSettings.SmtpServer, smtpSettings.Port, true);

                if(client.IsConnected)
                {
                    //Authentication
                    await client.AuthenticateAsync(smtpSettings.Username, smtpSettings.Password);
                    
                    if(client.IsAuthenticated)
                    {
                        foreach(var message in messages)
                        {
                            await client.SendAsync(message);
                        }
                    }

                    await client.DisconnectAsync(true);
                }
            }
            return;
        }

        public async Task ObjectiveReminderWeek()
        {
            var result = await objectiveService.IncomingObjectivesWeek();
            if (result.Status() != ResultStatus.Sucess) return;

            var messages = GenerateMessagesForObjectivesV1(result.Result());

            using (var client = new SmtpClient())
            {
                //Connect to the SMTP Server
                await client.ConnectAsync(smtpSettings.SmtpServer, smtpSettings.Port, true);

                if (client.IsConnected)
                {
                    //Authentication
                    await client.AuthenticateAsync(smtpSettings.Username, smtpSettings.Password);

                    if (client.IsAuthenticated)
                    {
                        foreach (var message in messages)
                        {
                            await client.SendAsync(message);
                        }
                    }

                    await client.DisconnectAsync(true);
                }
            }
            return;
        }

        private IEnumerable<MimeMessage> GenerateMessagesForObjectivesV1(IEnumerable<ObjectiveDto> objectives)
        {
            var messages = new List<MimeMessage>();
            var senderAddress = new MailboxAddress("Balto", smtpSettings.Sender);

            foreach (var objective in objectives)
            {
                var message = new MimeMessage();

                message.From.Add(senderAddress);
                message.To.Add(new MailboxAddress(objective.User.Name, objective.User.Email));
                message.Subject = $"Balto! { objective.Name } deadline in { objective.EndingDate - objective.StartingDate } days!";

                messages.Add(message);
            }

            return messages;
        }
    }
}
