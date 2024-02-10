using Fitness.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Core.Entities
{
    public class SmsNotification : BaseEntity
    {
        public string Numbers { get; set; } = null!;
        public string UserSender { get; set; } = null!;
        public string Msg { get; set; } = null!;
    }
}
