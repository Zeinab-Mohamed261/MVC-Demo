using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.DTO.DepartmentsDtos
{
    public class DepartmentDetailesDto
    {
        #region Constructor Mapping
        //public DepartmentDetailesDto(Department department)
        //{
        //    Id = department.Id;
        //    Name = department.Name;
        //    Descriotion = department.Descriotion;
        //    CreatedOn = DateOnly.FromDateTime(department.CreatedOn.Value);
        //} 
        #endregion
        public int Id { get; set; } //PK
        public int CreatedBy { get; set; }  //user Id
        public DateOnly CreatedOn { get; set; } //time of creation
        public int LastModifiedBy { get; set; }  //user Id
        public bool IsDeleted { get; set; } //Soft Delete
        public string Name { get; set; } = string.Empty; //string by default Required
        public string Code { get; set; } = string.Empty;
        public string? Descriotion { get; set; }
    }
}
