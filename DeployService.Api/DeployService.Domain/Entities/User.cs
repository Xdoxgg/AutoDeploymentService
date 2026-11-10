using DeployService.Domain.Enums;

namespace DeployService.Domain.Entities;

public class User
{
    public uint Id { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public required string DisplayName { get; set; }
    public Roles Role { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<Project>? Projects { get; set; }
    public List<Build>? TriggeredBuilds { get; set; }
    public List<Deployment>? TriggeredDeployments { get; set; }
}