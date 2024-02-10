using Fitness.Core.Entities;

namespace Fitness.Core.Common;
public class CreateUserResponse
{
    public string password { get; set; } = string.Empty;

    public User user { get; set; } = null!;
}
