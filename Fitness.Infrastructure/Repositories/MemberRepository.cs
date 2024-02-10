using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class MemberRepository : GenericRepository<Member>, IMemberRepository
{
    private readonly FitnessContext _context;
    public MemberRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Member>> GetAllAsync(int page, int pageSize, string searchText)
    {
        if (page == 0) page = 1;
        if (pageSize == 0) pageSize = int.MaxValue;
        int skip = (page - 1) * pageSize;

        return await _context.Members
            .Include(x => x.Club)
            .Include(x => x.User).ThenInclude(x => x.Address)
            .Where(x => !x.IsDelete && searchText == ""
                        || x.User.FirstName.Contains(searchText)
                        || x.User.LastName.Contains(searchText)
                        || x.User.Email!.Contains(searchText)
                        || x.PersonalIdentificationNumber!.Contains(searchText)
                        || x.MemberNo!.Contains(searchText)
                        || x.User.Id.ToString().Contains(searchText)
                        || x.Id.ToString().Contains(searchText)
                        || x.User.MobileNo.Contains(searchText)
                    )
            .OrderByDescending(x => x.Id)
            .Skip(skip).Take(pageSize)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Member>> GetAllByMemberTypeAsync(int page, int pageSize, bool memberType, DateTime startDate, DateTime endDate)
    {
        if (page == 0) page = 1;
        if (pageSize == 0) pageSize = int.MaxValue;
        int skip = (page - 1) * pageSize;

        return await _context.Members
            .Include(x => x.Club)
            .Include(x => x.User).ThenInclude(x => x.Address)
            .Where(x => !x.IsDelete && x.IsGuest == memberType &&
            x.CreatedOn >= startDate &&
            x.CreatedOn <= endDate)
            .OrderByDescending(x => x.Id)
            .Skip(skip).Take(pageSize)
            .ToListAsync();
    }

    async Task<Member?> IGenericRepository<Member>.GetByIdAsync(int id)
    {
        return await _context.Members
            .Include(x => x.Club)
            .Include(x => x.User).ThenInclude(x => x.Address)
            .Include(x => x.User).ThenInclude(x => x.Address).ThenInclude(x => x!.City)
            .Include(x => x.User).ThenInclude(x => x.Address).ThenInclude(x => x!.Region)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Member> GetByMobileNo(string mobileNo)
    {
        var items = await _context.Members
            .Include(x => x.User)
            .Where(x => x.User.MobileNo == mobileNo)
            .ToListAsync();
        return items.Any() ? items.First() : new Member();
    }

    public async Task<bool> IsPersonalIdentificationNumberExists(int memberId, string val)
    {
        return await _context.Members.AnyAsync(x => x.PersonalIdentificationNumber == val && x.Id != memberId && !x.IsDelete);
    }

    async Task IGenericRepository<Member>.AddAsync(Member entity)
    {
        _context.Add(entity);
        await _context.SaveChangesAsync();

        entity.MemberNo = ConstructMemberNo(entity.ClubId, entity.Id);
        _context.Update(entity);
        await _context.SaveChangesAsync();
    }

    private string ConstructMemberNo(int clubId, int memberId)
    {
        string clubText = clubId.ToString();
        while (clubText.Length < 3)
        {
            clubText = "0" + clubText;
        }

        string memberText = memberId.ToString();
        while (memberText.Length < 7)
        {
            memberText = "0" + memberText;
        }

        return clubText + memberText;
    }
}
