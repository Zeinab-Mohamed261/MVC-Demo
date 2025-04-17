using Demo.DAL.Data.Repositries.Interfaces;
using Demo.DAL.Models.DepartmentModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data.Repositries.Classes
{
    //primary constructor   .Net8 c#12
    public class DepartmentRepositpry(AppDbContext dbContext) : GenericRepository<Department>(dbContext), IDepartmentRepository
    {
        
    }
}
