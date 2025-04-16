using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class BaseEntity
    {
        public int Id { get; set; } //PK
        public int CreatedBy { get; set; }  //user Id
        public DateTime? CreatedOn { get; set; } //time of creation
        public int LastModifiedBy { get; set; }  //user Id
        public DateTime? LastModifiedOn { get; set; } //time of creation [Automatically Calculated]
        public bool IsDeleted { get; set; } //Soft Delete
    }
}
