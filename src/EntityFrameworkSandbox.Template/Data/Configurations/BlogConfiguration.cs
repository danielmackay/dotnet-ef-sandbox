using EntityFrameworkSandbox.Template.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkSandbox.Template.Data.Configurations;

internal class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        // NOTE: Custom model configuration goes here
    }
}
