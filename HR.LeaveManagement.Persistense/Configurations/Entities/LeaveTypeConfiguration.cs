using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Persistense.Configurations.Entities
{
    public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.HasData(
             new LeaveType
             {
                 Id = 1,
                 Name = "Vacation",
                 DefaultDays = 10,
                 CreatedBy = "System",
                 LastModifiedBy = "System",


             },
            new LeaveType
            {
                Id = 2,
                Name = "Sick Leave",
                DefaultDays = 5,
                CreatedBy = "System",
                LastModifiedBy = "System",
            },
            new LeaveType
            {
                Id = 3,
                Name = "Maternity Leave",
                DefaultDays = 90,
                CreatedBy = "System",
                LastModifiedBy = "System",
            },
            new LeaveType
            {
                Id = 4,
                Name = "Paternity Leave",
                DefaultDays = 14,
                CreatedBy = "System",
                LastModifiedBy = "System",
            },
            new LeaveType
            {
                Id = 5,
                Name = "Unpaid Leave",
                DefaultDays = 0,
                CreatedBy = "System",
                LastModifiedBy = "System",
            });
        }
    }
}
