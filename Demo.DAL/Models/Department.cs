using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class Department:BaseEntity
    {
        public string Name { get; set; } //string by default Required
        public string Code { get; set; }
        public string? Descriotion { get; set; }

    }
}
