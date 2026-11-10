using DeployService.Domain.Enums;

namespace DeployService.Domain.Entities;

public class Deployment
{
    public uint Id { get; set; }
    public uint ProjectId { get; set; }
    public required Project Project { get; set; }
    public uint BuildId { get; set; }
    public required Build Build { get; set; }
    public uint? TriggeredByUserId { get; set; }
    public User? TriggeredByUser { get; set; }
    public required string Environment { get; set; }
    public Status Status { get; set; }
    public string? Version { get; set; }
    public uint? PreviousDeploymentId { get; set; }
    public Deployment? PreviousDeployment { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? FinishedAt { get; set; }
    public string? ErrorMessage { get; set; }
    public string? LogReference { get; set; }
    public bool? HealthCheckPassed { get; set; }
    public List<Deployment>? RollbackDeployments { get; set; }
    
    
}