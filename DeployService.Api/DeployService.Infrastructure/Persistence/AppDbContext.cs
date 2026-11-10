using System.Reflection;
using DeployService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeployService.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Build> Builds { get; set; }
    public DbSet<Deployment> Deployments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureUser(modelBuilder);
        ConfigureProject(modelBuilder);
        ConfigureBuild(modelBuilder);
        ConfigureDeployment(modelBuilder);
    }

    private static void ConfigureUser(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<User>();
        entity.HasMany(u => u.Projects)
            .WithOne(p => p.OwnerUser)
            .HasForeignKey(p => p.OwnerUserId);

        entity.HasMany(u => u.TriggeredBuilds)
            .WithOne(b => b.TriggeredByUser)
            .HasForeignKey(b => b.TriggeredByUserId);

        entity.HasMany(u => u.TriggeredDeployments)
            .WithOne(d => d.TriggeredByUser)
            .HasForeignKey(d => d.TriggeredByUserId);
    }

    private static void ConfigureProject(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Project>();
        

        entity.HasMany(p => p.Builds)
            .WithOne(b => b.Project)
            .HasForeignKey(b => b.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasMany(p => p.Deployments)
            .WithOne(d => d.Project)
            .HasForeignKey(d => d.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private static void ConfigureBuild(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Build>();
        
    }

    private static void ConfigureDeployment(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Deployment>();
        
        entity.HasOne(d => d.Build)
            .WithMany()
            .HasForeignKey(d => d.BuildId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(d => d.PreviousDeployment)
            .WithMany(d => d.RollbackDeployments)
            .HasForeignKey(d => d.PreviousDeploymentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}