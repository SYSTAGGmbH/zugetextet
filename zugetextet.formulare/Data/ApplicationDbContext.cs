namespace zugetextet.formulare.Data;

using Microsoft.EntityFrameworkCore;
using Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Forms> Forms => Set<Forms>();
    public DbSet<FormData> FormData => Set<FormData>();
    public DbSet<LoginData> LoginData => Set<LoginData>();
    public DbSet<Attachment> Attachment => Set<Attachment>();
    public DbSet<AttachmentVersion> AttachmentVersion => Set<AttachmentVersion>();

}
