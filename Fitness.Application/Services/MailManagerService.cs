using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using Fitness.Core.Common;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class MailManagerService : IMailManagerService
{
    private readonly string _email;
    private readonly string _password;
    private readonly IConfiguration _configuration;
    public MailManagerService(IConfiguration configuration)
    {
        _configuration = configuration;
        _email = configuration["Email:Email"] ?? string.Empty;
        _password = configuration["Email:Password"] ?? string.Empty;
    }

    public async Task SendMail(EmailModel model)
    {
        try
        {
            using var ms = new MemoryStream();
            using var message = new MailMessage();

            var from = new MailAddress(_email);
            message.From = from;
            message.To.Add(model.To);
            message.Subject = model.Subject;
            message.Body = model.Body;
            message.IsBodyHtml = true;

            if (model.Attachment != null)
            {
                ms.Write(model.Attachment, 0, model.Attachment.Length);
                ms.Position = 0;
                var attachment = new Attachment(ms, $"{model.AttachmentName!.Replace(" ", "-")}.pdf", "application/pdf");
                message.Attachments.Add(attachment);
            }

            using var client = new SmtpClient();
            client.Host = _configuration["Email:Host"] ?? string.Empty;
            client.EnableSsl = true;
            var credential = new NetworkCredential(_email, _password);
            client.UseDefaultCredentials = false;
            client.Credentials = credential;
            client.Port = int.Parse(_configuration["Email:Port"] ?? "587");

            await client.SendMailAsync(message);
        }
        catch (Exception ex)
        {
            throw new Exception($"SendMail - {ex.Message}");
        }
    }
}
