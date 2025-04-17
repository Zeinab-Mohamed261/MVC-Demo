using Demo.DAL.Models.EmployeeModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data.Configurations
{
    public class EmployeeConfigurations :BaseEntityConfigurations<Employee>, IEntityTypeConfiguration<Employee>
    {
        public new void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name).HasColumnType("varchar(50)");//this means max length is  50
            builder.Property(E => E.Address).HasColumnType("varchar(150)");
            builder.Property(E => E.Salary).HasColumnType("decimal(10,2)");
            //(EmployeeType , Gender)store in db as string AND treat With in application as enums
            //HasConversion
            builder.Property(E => E.Gender)
                    .HasConversion((empGender) => empGender.ToString(),//value store in db as string
                    (returnedEmpGender) => (Gender)Enum.Parse(typeof(Gender), returnedEmpGender));//retrieve from db as enum

            builder.Property(E => E.EmployeeType)
                   .HasConversion((empType) => empType.ToString(),
                    (returnedEmoType) => (EmployeeType)Enum.Parse(typeof(EmployeeType), returnedEmoType));



            base.Configure(builder);

        }
    }
}
