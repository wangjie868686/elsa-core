using Elsa.Workflows.Management.Entities;

namespace Elsa.Workflows.Runtime.Matches;

public record WorkflowMatch(string? CorrelationId, object? Payload);