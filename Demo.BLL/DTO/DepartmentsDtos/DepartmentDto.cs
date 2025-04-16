using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.DTO.DepartmentsDtos
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } //string by default Required
        public string Code { get; set; }
        public string? Descriotion { get; set; }
        [Display(Name = "Date Of Creation")]
        public DateOnly? DateOfCreation { get; set; }
    }
}
