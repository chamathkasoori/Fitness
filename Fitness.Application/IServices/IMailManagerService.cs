using Fitness.Core.Common;

namespace Fitness.Application.IServices;
public interface IMailManagerService
{
    public Task SendMail(EmailModel emailModel);
}
