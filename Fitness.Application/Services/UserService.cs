using System.Text;
using System.Transactions;
using Fitness.Core.Entities;
using Fitness.Core.Enums;
using Fitness.Application.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Fitness.Core.Common;
using Fitness.Core.IRepositories;

namespace Fitness.Application.Services;
public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly Random _random;
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;
    private readonly IMailManagerService _mailManagerService;
    public UserService(UserManager<User> userManager, IUserRepository userRepository, Random random, IConfiguration configuration, IMailManagerService mailManagerService)
    {
        _userManager = userManager;
        _random = random;
        _configuration = configuration;
        _userRepository = userRepository;
        _mailManagerService = mailManagerService;
    }

    public async Task<IReadOnlyList<User>> GetUserList(UserType type)
    {
        return await _userRepository.GetListByType(type);
    }

    //Remove User by id
    public async Task<IdentityResult> RemoveUser(int id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
            throw new Exception("Not Found");

        return await _userManager.DeleteAsync(user);
    }

    private string GetRandomPassword(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return string.Concat("saas@") + new string(Enumerable.Repeat(chars, length)
            .Select(s => s[_random.Next(s.Length)]).ToArray());
    }

    public async Task SendMail(string email, string name, string username, string password)
    {
        try
        {
            var html = await File.ReadAllTextAsync("HtmlTemplates/Password.html");
            var builder = new StringBuilder(html);
            builder.Replace("[name]", name);
            builder.Replace("[username]", username);
            builder.Replace("[email]", email);
            builder.Replace("[password]", password);

            await _mailManagerService.SendMail(new EmailModel
            {
                Body = builder.ToString(),
                Subject = "Welcome NineRounder...!!!",
                To = email,
            });
        }
        catch (Exception e)
        {
            throw new Exception("Mail Sending Failed: " + e.Message);
        }
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _userRepository.GetByUsernameAsync(username);
    }

    public async Task<bool> IsMobileNoExists(UserType type, int userId, string val)
    {
        return await _userRepository.IsMobileNoExists(type, userId, val);
    }

    public async Task<bool> IsEmailExists(UserType type, int userId, string val)
    {
        return await _userRepository.IsEmailExists(type, userId, val);
    }

    public async Task<bool> IsUsernameNoExists(int userId, string val)
    {
        return await _userRepository.IsUsernameNoExists(userId, val);
    }

    public async Task<CreateUserResponse> AddUser(User user, int roleId)
    {
        var createUserResponse = new CreateUserResponse();
        var password = GetRandomPassword(int.Parse(_configuration["PasswordLength"] ?? "0"));

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        try
        {
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                throw new Exception("Failed to create user");

            await SendMail(user.Email!, user.LastName, user.UserName!, password);

            scope.Complete();
        }
        catch (Exception ex)
        {
            throw new Exception("Add User: " + ex.Message);
        }
        createUserResponse.password = password;
        createUserResponse.user = user;

        return createUserResponse;
    }

    public async Task UpdateAsync(User entity)
    {
        await _userRepository.UpdateAsync(entity);
    }

    public async Task<bool> UpdatePasswordAsync(User user, string currentPassword, string newPassword)
    {
        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        return result.Succeeded;
    }
}