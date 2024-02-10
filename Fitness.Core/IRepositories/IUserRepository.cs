﻿using Fitness.Core.Entities;
using Fitness.Core.Enums;

namespace Fitness.Core.IRepositories;
public interface IUserRepository
{
    Task<IReadOnlyList<User>> GetListByType(UserType type);

    public Task<User> GetByIdAsync(int id);

    public Task<User?> GetByUsernameAsync(string userName);

    public Task<bool> IsMobileNoExists(UserType type, int userId, string val);

    public Task<bool> IsEmailExists(UserType type, int userId, string val);

    public Task<bool> IsUsernameNoExists(int userId, string val);

    public Task UpdateAsync(User entity);
}
