using Demo.DAL.Data.Repositries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data.Repositries.Classes
{
    public class UnitOfWork : IUnitOfWork  /* , IDisposable*/
    {
        private IDepartmentRepository _DepartmentRepository;
        private IEmployeeRepository _EmployeeRepository;
        private readonly AppDbContext _dbContext;

        public UnitOfWork(IDepartmentRepository departmentRepository , IEmployeeRepository employeeRepository , AppDbContext dbContext)
        {
            
            _DepartmentRepository = departmentRepository;
            _EmployeeRepository = employeeRepository;
            _dbContext = dbContext;
        }

        public IEmployeeRepository EmployeeRepository 
        {
            get
            {
                return _EmployeeRepository;
            }
            set
            {
                _EmployeeRepository = value;
            }
        }
        public IDepartmentRepository DepartmentRepository
        {
            get
            {
                return _DepartmentRepository;
            }
            set
            {
                _DepartmentRepository = value;
            }
        }
        
        public int SaveChanges()
        {
           return _dbContext.SaveChanges();
        }

        //public void Dispose()
        //{
        //   _dbContext.Dispose();
        //}
    }
}
