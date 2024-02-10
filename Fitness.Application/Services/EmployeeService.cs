using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmployeeAssignedClubRepository _employeeAssignedClubRepository;
    private readonly IEmployeeAvailableClubRepository _employeeAvailableClubRepository;
    public EmployeeService(IEmployeeRepository EmployeeRepository, IEmployeeAssignedClubRepository employeeAssignedClubRepository, IEmployeeAvailableClubRepository employeeAvailableClubRepository)
    {
        _employeeRepository = EmployeeRepository;
        _employeeAssignedClubRepository = employeeAssignedClubRepository;
        _employeeAvailableClubRepository = employeeAvailableClubRepository;
    }

    Task<IReadOnlyList<Employee>> IGenericService<Employee>.GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<Employee>> GetAllAsync(int page, int pageSize, int roleId, int clubId, int positionId, bool isActiveOnly, string text)
    {
        return await _employeeRepository.GetAllAsync(page, pageSize, roleId, clubId, positionId, isActiveOnly, text);
    }

    public async Task<List<int>> GetAssignedClulbIdsAsync(int employeeId)
    {
        return await _employeeAssignedClubRepository.GetClulbIdsAsync(employeeId);
    }


    async Task<Employee?> IGenericService<Employee>.GetByIdAsync(int id)
    {
        return await _employeeRepository.GetByIdAsync(id);
    }

    public async Task<Employee?> GetByUserIdAsync(int userId)
    {
        return await _employeeRepository.GetByUserIdAsync(userId);
    }

    async Task IGenericService<Employee>.AddAsync(Employee entity)
    {
        await _employeeRepository.AddAsync(entity);
    }

    async Task IGenericService<Employee>.UpdateAsync(Employee entity)
    {
        await _employeeRepository.UpdateAsync(entity);
    }

    async Task IEmployeeService.AddNewClubAsync(int clubId, int createdBy)
    {
        var assignToNewClubEmployeeList = await _employeeRepository.GetAllAssignToNewClubsAsync();
        if (assignToNewClubEmployeeList.Any())
        {
            List<EmployeeAssignedClub> items = new List<EmployeeAssignedClub>();
            foreach (var employeeId in assignToNewClubEmployeeList)
            {
                EmployeeAssignedClub item = new EmployeeAssignedClub();
                item.EmployeeId = employeeId;
                item.ClubId = clubId;
                item.CreatedBy = createdBy;
                items.Add(item);
            }
            await _employeeAssignedClubRepository.SaveAsync(items);
        }

        var avaialbleToNewClubEmployeeList = await _employeeRepository.GetAllAvaialbleInNewClubsAsync();
        if (avaialbleToNewClubEmployeeList.Any())
        {
            List<EmployeeAvailableClub> items = new List<EmployeeAvailableClub>();
            foreach (var employeeId in avaialbleToNewClubEmployeeList)
            {
                EmployeeAvailableClub item = new EmployeeAvailableClub();
                item.EmployeeId = employeeId;
                item.ClubId = clubId;
                item.CreatedBy = createdBy;
                items.Add(item);
            }
            await _employeeAvailableClubRepository.SaveAsync(items);
        }
    }
}
