using Demo.BLL.DTO.DepartmentsDtos;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Factories
{
    static public class DepartmentFactory
    {
        //Get All Department
        public static DepartmentDto ToDepartmentDto(this Department D)
        {
            return new DepartmentDto()
            {
                Id = D.Id,
                Name = D.Name,
                Descriotion = D.Descriotion,
                Code = D.Code,
                DateOfCreation = DateOnly.FromDateTime(D.CreatedOn.Value),
                
            };
        }

        //Get Department By Id
        public static DepartmentDetailesDto ToDepartmentDetailsDto(this Department department)
        {
            return new DepartmentDetailesDto()
            {
                Id = department.Id,
                Name = department.Name,
                Descriotion = department.Descriotion,
                CreatedOn = DateOnly.FromDateTime(department.CreatedOn.Value),
                Code = department.Code,
                CreatedBy = department.CreatedBy,
                LastModifiedBy = department.LastModifiedBy
            };
        }
        //Add Department
        public static Department ToEntity(this CreatedDepartmentDto departmentDto)
        {
            return new Department()
            {
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Descriotion = departmentDto.Description,
                CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly())
            };
        }

        //Update Department
        public static Department ToEntity(this UpdateDepartmentDto departmentDto)
        {
            return new Department()
            {
                Id = departmentDto.Id,
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Descriotion = departmentDto.Description,
                CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly())
            };
        }
    }
}
