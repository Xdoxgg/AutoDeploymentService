using DeployService.Domain.Enums;

namespace DeployService.Domain.Entities;

public class Build
{
    public uint Id { get; set; }
    public uint ProjectId { get; set; }
    public required Project Project { get; set; }
    public uint? TriggeredByUserId { get; set; }
    public User? TriggeredByUser { get; set; }
    public string? CommitSha { get; set; }
    public required string Branch { get; set; }
    public Status Status { get; set; }
    public TriggerReasons TriggerReason { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? FinishedAt { get; set; }
    public uint? ExitCode { get; set; }
    public string? ErrorMessage { get; set; }
    public string? LogReference { get; set; }
    public string? ArtifactReference { get; set; }
    public string? IdempotencyKey { get; set; }
    public List<Deployment>? Deployments { get; set; }
}