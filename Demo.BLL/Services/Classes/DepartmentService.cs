using Demo.BLL.DTO.DepartmentsDtos;
using Demo.BLL.Factories;
using Demo.BLL.Services.Intrfaces;
using Demo.DAL.Data.Repositries.Classes;
using Demo.DAL.Data.Repositries.Interfaces;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services.Classes
{
    //primary constructor
    public class DepartmentService(IUnitOfWork unitOfWork) : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        //private readonly IDepartmentRepository _departmentRepository = departmentRepository;

        //Get All Departments
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            #region 1.Manual Mapping 
            //Mapping(Manual) =>To Convert From Department To DepartmentDTO
            //var departmentsToReturn = departments.Select(D => new DepartmentDto()
            //{
            //    Id = D.Id,
            //    Name = D.Name,
            //    Descriotion = D.Descriotion,
            //    Code = D.Code,
            //    DateOfCreation = DateOnly.FromDateTime(D.CreatedOn.Value)
            //});
            //return departmentsToReturn; 
            #endregion
            #region 2.Extension Meyhod
            return departments.Select(D => D.ToDepartmentDto());
            #endregion
        }

        //Get DepartmentById
        public DepartmentDetailesDto? GetDepartmentById(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            //if (department is null) return null;
            //else
            //{
            //    var departmentToReturn = new DepartmentDetailesDto()
            //    {
            //        Id = id,
            //        Name = department.Name,
            //        Code = department.Code,
            //        Descriotion = department.Descriotion,
            //        CreatedOn = DateOnly.FromDateTime(department.CreatedOn.Value),

            //    };
            //    return departmentToReturn;
            //}

            //Manual Mapping  //no
            //Auto Mapper  //big project
            //Constructor Mapping  //no
            //Extension Method  //small Project


            #region 1.Manual Mapping
            //1.Manual Mapping
            //return department is null ? null : new DepartmentDetailesDto(department)
            //{
            //    //Id = department.Id,
            //    //Name = department.Name,
            //    //Descriotion = department.Descriotion,
            //    //Code = department.Code,
            //    //CreatedOn = DateOnly.FromDateTime(department.CreatedOn.Value)
            //}; 
            #endregion

            #region 2.Extension Method
            return department is null ? null : department.ToDepartmentDetailsDto();
            #endregion
        }

        //Add Department
        public int AddDepartment(CreatedDepartmentDto departmentDto)
        {
            var department = departmentDto.ToEntity();
             _unitOfWork.DepartmentRepository.Add(department);
            return _unitOfWork.SaveChanges();

        }

        //Update Department
        public int UpdateDepartment(UpdateDepartmentDto departmentDto)
        {

             _unitOfWork.DepartmentRepository.Update(departmentDto.ToEntity());
            return _unitOfWork.SaveChanges();
        }

        //Delete Department
        public bool DeleteDepartment(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            if (department is null) return false;
            else
            {
                _unitOfWork.DepartmentRepository.Delete(department);
                return _unitOfWork.SaveChanges() > 0 ? true : false;
            }
        }
    }
}
