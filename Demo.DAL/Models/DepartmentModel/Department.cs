using Demo.DAL.Models.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models.DepartmentModel
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } //string by default Required
        public string Code { get; set; }
        public string? Descriotion { get; set; }
        //Navigation Property => [Many]
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();//uniuque collection of data

    }
}
