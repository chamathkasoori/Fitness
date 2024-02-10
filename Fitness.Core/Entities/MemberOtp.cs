using Fitness.Core.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness.Core.Entities;
public class MemberOtp : BaseEntity
{
    [ForeignKey("Member")]
    public int MemberId { get; set; }

    public int OTP { get; set; }

    public DateTime ExpiresOn { get; set; }

    public virtual Member Member { get; set; } = null!;
}