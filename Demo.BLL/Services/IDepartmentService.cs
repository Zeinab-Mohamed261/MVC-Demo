using Demo.BLL.DTO.DepartmentsDtos;

namespace Demo.BLL.Services
{
    public interface IDepartmentService
    {
        int AddDepartment(CreatedDepartmentDto departmentDto);
        bool DeleteDepartment(int id);
        IEnumerable<DepartmentDto> GetAllDepartments();
        DepartmentDetailesDto? GetDepartmentById(int id);
        int UpdateDepartment(UpdateDepartmentDto departmentDto);
    }
}