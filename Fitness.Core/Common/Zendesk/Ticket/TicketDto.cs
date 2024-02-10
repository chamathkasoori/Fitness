using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Core.Common.Zendesk.Ticket
{
    public class TicketDto
    {
        public required Ticket Ticket { get; set; }
    }

    public class Ticket
    {
        public Comment? Comment { get; set; }
        public string? Priority { get; set; }
        public string? Subject { get; set; }
        public int ExternalId { get; set; }
        public RequesterDetails? Requester { get; set; }
        public CustomField[]? CustomFields { get; set; }
    }

    public class Comment
    {
        public string? Body { get; set; }
    }

    public class CustomField
    {
        public long Id { get; set; }
        public object? Value { get; set; }
    }

    public class RequesterDetails
    {
        public int LocaleId { get; set; } = 1;
        public string? Name { get; set; }
        public string? Email { get; set; }
    }

}
