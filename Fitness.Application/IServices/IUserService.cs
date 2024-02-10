﻿using Fitness.Core.Common;
using Fitness.Core.Entities;
using Fitness.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace Fitness.Application.IServices;
public interface IUserService
{
    Task<IReadOnlyList<User>> GetUserList(UserType type);

    public Task<User> GetByIdAsync(int id);

    public Task<User?> GetByUsernameAsync(string userName);

    public Task<bool> IsMobileNoExists(UserType type, int userId, string val);

    public Task<bool> IsEmailExists(UserType type, int userId, string val);

    public Task<bool> IsUsernameNoExists(int userId, string val);

    Task<CreateUserResponse> AddUser(User user, int roleId);

    public Task UpdateAsync(User entity);

    public Task<bool> UpdatePasswordAsync(User user, string currentPassword, string newPassword);

    Task<IdentityResult> RemoveUser(int id);
}