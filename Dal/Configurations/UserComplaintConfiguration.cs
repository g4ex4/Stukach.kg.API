using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dal.Configurations;

public class UserComplaintConfiguration : IEntityTypeConfiguration<UserComplaint>
{
    public void Configure(EntityTypeBuilder<UserComplaint> builder)
    {
        builder.HasKey(x => new { x.UserId, x.ComplaintId });
        
    }
}