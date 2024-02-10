using System.Text;
using Fitness.Core.Common;
using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;
using Fitness.Core.Enums;
using Microsoft.Extensions.Configuration;
using SelectPdf;

namespace Fitness.Application.Services;
public class InvoiceService : IInvoiceService
{
    private readonly IMailManagerService _mailManager;
    private readonly IConfiguration _configuration;
    private readonly IInvoiceRepository _invoiceRepository;
    public InvoiceService(IMailManagerService mailManager, IConfiguration configuration, IInvoiceRepository invoiceRepository)
    {
        _mailManager = mailManager;
        _configuration = configuration;
        _invoiceRepository = invoiceRepository;
    }

    public async Task<IReadOnlyList<Invoice>> GetAllAsync()
    {
        return await _invoiceRepository.GetAllAsync();
    }

    public async Task<Invoice?> GetByMemberSubscriptionIdAsync(int memberSubscriptionId)
    {
        return await _invoiceRepository.GetByMemberSubscriptionIdAsync(memberSubscriptionId);
    }

    public Task<Invoice?> GetByIdAsync(int id)
    {
        return _invoiceRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(Invoice entity)
    {
        await _invoiceRepository.AddAsync(entity);
    }

    public Task UpdateAsync(Invoice entity)
    {
        throw new NotImplementedException();
    }

    public async Task SendMail(int id, MailTypes mailType, Invoice? invoiceCreating, MemberSubscription memberSubscription)
    {
        if (!bool.Parse(_configuration["Xero:SendInvoiceEmail"] ?? false.ToString()))
            throw new Exception("Feature Deactivated");

        if (invoiceCreating != null && memberSubscription != null)
        {
            invoiceCreating.MemberSubscription = memberSubscription;
        }

        var invoice = invoiceCreating ?? await _invoiceRepository.GetByIdAsync(id);

        if (invoice == null)
            throw new Exception("Not Found");

        var converter = new HtmlToPdf();

        var html = mailType switch
        {
            MailTypes.Invoice => await CreateDoc(invoice, "HtmlTemplates/Invoice.html"),
            MailTypes.CancelInvoice => await CreateDoc(invoice, "HtmlTemplates/CancelInvoice.html"),
            _ => string.Empty
        };

        var doc = converter.ConvertHtmlString(html);

        var name = invoice.MemberSubscription.Member.User.FirstName + " " + invoice.MemberSubscription.Member.User.LastName;

        using (var ms = new MemoryStream())
        {
            doc.Save(ms);

            await _mailManager.SendMail(new EmailModel
            {
                Body = "<!DOCTYPE html>\r\n<html lang =\"en\">\r\n<head>\r\n<meta charset=\"UTF-8\">\r\n<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n<style>body, h1, h2, h3, p {margin: 0;padding: 0;} \r\n\theader {width: 100%; height:auto; background-size: 100%;background-repeat: no-repeat;background-size: cover;margin-left: auto;margin-right: auto;}main {padding: 20px;}\r\n\tfooter {width:100%; padding: 0 0 20%;padding-bottom: 0;bottom: 0;background-size: 100%;background-repeat: no-repeat;margin-left: auto;margin-right: auto;}\r\n\t.header img,.footer img {\r\nmax-width: 100%;\r\nheight: auto;\r\ndisplay: block;\r\n}\r\n\t</style>\r\n</head>\r\n<body>\r\n\t<header>\r\n\t<center>\r\n<img src='https://9roundsa.perfectgym.com/Api/Files/File/SVeNcYKCcwg$$3d?timestamp=638028202940800000' style=\"max-width: 100%; height:auto\">\r\n</center>\r\n</header>\r\n<main style=\"max-width: 100%; height:100%;\">\r\n<center>\r\n<h2 style=\"color:red;\">9Rounder " + name + ",</h2>\r\n</center>\r\n<br>\r\n<center>\r\n\t<p style=\"color:rgb(102, 150, 170);\">Attached for you in this email is a</p>\r\n</center>\r\n<center>\r\n\t<p style=\"color:red;\"> Purchase invoice </p>\r\n</center>\r\n</main>\r\n<footer>\r\n<center>\r\n<img src='https://9roundsa.perfectgym.com/Api/Files/File/Vq8$$2fKDItBqA$$3d?timestamp=638028036624430000' style=\"max-width: 100%; height:auto\"></footer></center></body></html>",
                Subject = $"Invoice for {invoice.MemberSubscription.SubscriptionPlan.Name}",
                To = invoice.MemberSubscription.Member.User.Email ?? "test@gmail.com",
                AttachmentName = invoice.InvoiceNumber.Replace("/", "_"),
                Attachment = ms.ToArray()
            });

            invoice.InvoiceSent = true;
            invoice.ModifiedOn = DateTime.UtcNow;
            invoice.MemberSubscription = null!;
            await _invoiceRepository.UpdateAsync(invoice);
        }

        doc.Close();
    }

    private async Task<string> CreateDoc(Invoice invoice, string filePath)
    {
        var html = await File.ReadAllTextAsync(filePath);
        var builder = new StringBuilder(html);

        var vat = invoice.MemberSubscription.SubscriptionPlan.MembershipFeeVat;
        var vatPercentage = vat?.Percentage ?? 15;
        var netPrice = invoice.AmountPaid * (100 - vatPercentage) / 100;// 15% Calculation
        netPrice = Math.Round(netPrice, 2, MidpointRounding.AwayFromZero);

        builder.Replace("[invoiceNo]", invoice.InvoiceNumber);
        builder.Replace("[invoiceDate]", invoice.Date.Date.ToString("dd/MM/yyyy"));
        builder.Replace("[dateOfDelivery]", invoice.Date.Date.ToString("dd/MM/yyyy"));
        builder.Replace("[dueDate]", invoice.DueDate.Date.ToString("dd/MM/yyyy"));

        builder.Replace("[name]", $"{invoice.MemberSubscription.Member.User.FirstName} {invoice.MemberSubscription.Member.User.LastName}");
        builder.Replace("[nameAR]", string.Empty);
        builder.Replace("[address]", invoice.MemberSubscription.Member.User.Address?.FullAddress);
        builder.Replace("[addressAR]", string.Empty);

        builder.Replace("[totalValue]", invoice.Total.ToString());
        builder.Replace("[vat]", (invoice.AmountPaid - netPrice).ToString());
        builder.Replace("[vatPercent]", $"{vatPercentage}%");
        builder.Replace("[netValue]", netPrice.ToString());
        builder.Replace("[netPrice]", netPrice.ToString());
        builder.Replace("[quantity]", "1");
        builder.Replace("[product]", invoice.MemberSubscription.SubscriptionPlan.Name);
        builder.Replace("[no]", "1");

        builder.Replace("[paymentMethod]", "Card");

        return builder.ToString();
    }
}
