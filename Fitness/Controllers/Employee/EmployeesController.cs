using AutoMapper;
using Fitness.Core.Entities;
using Fitness.Core.Enums;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers;
public class EmployeesController : CommonController<EmployeesController>
{
    private readonly IMapper _mapper;
    private readonly IEmployeeService _employeeService;
    private readonly IUserService _userService;
    public EmployeesController(IMapper mapper, IEmployeeService employeeService, IUserService userService)
    {
        _mapper = mapper;
        _employeeService = employeeService;
        _userService = userService;
    }

    [HttpGet]
    [HttpGet("page/{page:int}/size/{size:int}")]
    [HttpGet("page/{page:int}/size/{size:int}/role/{roleId:int}/club/{clubId:int}/position/{positionId:int}/active/{isActiveOnly:bool}")]
    public async Task<IReadOnlyList<EmployeeDto>> GetAllAsync(int page = 1, int size = 100,  string text = "",int roleId = 0,  int clubId = 0 ,int positionId = 0, bool isActiveOnly = true)
    {
        var item = await _employeeService.GetAllAsync(page, size, roleId, clubId, positionId, isActiveOnly, text);
        var result =  _mapper.Map<IReadOnlyList<EmployeeDto>>(item);
        return result;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _employeeService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("Member Not Found");
        }
        return Ok(_mapper.Map<EmployeeDto>(item));
    }

    [HttpPost]
    public async Task<IActionResult> Add(EmployeePostDto model)
    {
        var isUsernameNoExist = await _userService.IsUsernameNoExists(0, model.UserName);
        if (isUsernameNoExist)
        {
            return BadRequest("Username Already Exist");
        }

        var isEmailExist = await _userService.IsEmailExists(UserType.Employee, 0, model.Email);
        if (isEmailExist)
        {
            return BadRequest("Email Already Exist");
        }

        var access = HttpContext.Items["Access"] as AccessDto;

        var address = _mapper.Map<Address>(model);
        address.CreatedBy = access!.UserId;

        var user = _mapper.Map<User>(model);
        user.Address = address;
        user.Type = UserType.Employee;
        user.CreatedBy = access!.UserId;

        var userResult = await _userService.AddUser(user, model.RoleId);
        if (userResult != null)
        {
            var employee = _mapper.Map<Employee>(model);
            employee.UserId = userResult.user.Id;
            employee.CreatedBy = access!.UserId;
            foreach (var clubId in model.AssignedClubIds)
            {
                EmployeeAssignedClub club = new EmployeeAssignedClub();
                club.ClubId = clubId;
                club.CreatedBy = access!.UserId;
                employee.EmployeeAssignedClubs.Add(club);
            }
            foreach (var clubId in model.AvailableClubIds)
            {
                EmployeeAvailableClub club = new EmployeeAvailableClub();
                club.ClubId = clubId;
                club.CreatedBy = access!.UserId;
                employee.EmployeeAvailableClubs.Add(club);
            }

            await _employeeService.AddAsync(employee);
            return CreatedAtAction("GetAll", new { id = employee.Id }, _mapper.Map<EmployeeDto>(employee));
        }
        return BadRequest(ModelState);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, EmployeePostDto model)
    {
        var item = await _employeeService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("Employee Not Found");
        }

        item.DepartmentId = model.DepartmentId;
        item.PositionId = model.PositionId;
        item.RoleId = model.RoleId;
        item.AddToAllClubs = model.AddToAllClubs;
        item.AssignToNewClubs = model.AssignToNewClubs;
        item.AvailableToNewClubs = model.AvailableToNewClubs;
        item.EmploymentDate = model.EmploymentDate;
        item.Citizenship = model.Citizenship;
        item.Picture = model.Picture;
        item.OnShift = model.OnShift;
        item.NoLogin = model.NoLogin;
        item.ChangePassword = model.ChangePassword;

        var access = HttpContext.Items["Access"] as AccessDto;
        item.ModifiedBy = access!.UserId;
        item.ModifiedOn = DateTime.UtcNow;

        foreach (var employeeAssignedClub in item.EmployeeAssignedClubs)
        {
            employeeAssignedClub.ModifiedBy = access!.UserId;
            employeeAssignedClub.ModifiedOn = DateTime.UtcNow;
            employeeAssignedClub.IsActive = model.AssignedClubIds.Any(x => x == employeeAssignedClub.ClubId);
            if (employeeAssignedClub.IsActive)
            {
                model.AssignedClubIds.Remove(employeeAssignedClub.ClubId);
            }
        }
        foreach (var clubId in model.AssignedClubIds)
        {
            EmployeeAssignedClub employeeAssignedClub = new EmployeeAssignedClub();
            employeeAssignedClub.CreatedBy = access!.UserId;
            employeeAssignedClub.ClubId = clubId;
            item.EmployeeAssignedClubs.Add(employeeAssignedClub);
        }

        foreach (var employeeAvailableClub in item.EmployeeAvailableClubs)
        {
            employeeAvailableClub.ModifiedBy = access!.UserId;
            employeeAvailableClub.ModifiedOn = DateTime.UtcNow;
            employeeAvailableClub.IsActive = model.AvailableClubIds.Any(x => x == employeeAvailableClub.ClubId);
            if (employeeAvailableClub.IsActive)
            {
                model.AvailableClubIds.Remove(employeeAvailableClub.ClubId);
            }
        }
        foreach (var clubId in model.AvailableClubIds)
        {
            EmployeeAvailableClub employeeAvailableClub = new EmployeeAvailableClub();
            employeeAvailableClub.CreatedBy = access!.UserId;
            employeeAvailableClub.ClubId = clubId;
            item.EmployeeAvailableClubs.Add(employeeAvailableClub);
        }

        await _employeeService.UpdateAsync(item);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _employeeService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("Employee Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        item.DeletedBy = access!.UserId;
        item.DeletedOn = DateTime.UtcNow;
        item.IsDelete = true;
        foreach (var employeeAssignedClub in item.EmployeeAssignedClubs)
        {
            employeeAssignedClub.DeletedBy = access!.UserId;
            employeeAssignedClub.DeletedOn = DateTime.UtcNow;
            employeeAssignedClub.IsDelete = true;
        }
        foreach (var employeeAvailableClub in item.EmployeeAvailableClubs)
        {
            employeeAvailableClub.DeletedBy = access!.UserId;
            employeeAvailableClub.DeletedOn = DateTime.UtcNow;
            employeeAvailableClub.IsDelete = true;
        }

        await _employeeService.UpdateAsync(item);
        return NoContent();
    }
}
