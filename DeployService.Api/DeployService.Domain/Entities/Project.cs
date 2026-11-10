namespace DeployService.Domain.Entities;

public class Project
{
    public uint Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string RepositoryUrl { get; set; }
    public required string DefaultBranch { get; set; }
    public uint OwnerUserId { get; set; }
    public required User OwnerUser { get; set; }
    public string? BuildConfiguration { get; set; }
    public string? RepositorySecretReference { get; set; }
    public bool IsArchived { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public List<Build>? Builds { get; set; }
    public List<Deployment>? Deployments { get; set; }
}