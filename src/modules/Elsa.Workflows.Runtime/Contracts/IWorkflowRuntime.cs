using Elsa.Workflows.Core.State;
using Elsa.Workflows.Runtime.Entities;
using Elsa.Workflows.Runtime.Filters;

namespace Elsa.Workflows.Runtime.Contracts;

/// <summary>
/// Represents a workflow runtime that can start, resume and find workflows.
/// </summary>
public interface IWorkflowRuntime
{
    /// <summary>
    /// Returns a value whether or not the specified workflow definition can create a new instance.
    /// </summary>
    Task<CanStartWorkflowResult> CanStartWorkflowAsync(string definitionId, StartWorkflowRuntimeOptions options, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new workflow instance of the specified definition ID and executes it.
    /// </summary>
    /// <param name="definitionId">The workflow definition ID to run.</param>
    /// <param name="options"></param>
    /// <param name="cancellationToken"></param>
    Task<WorkflowExecutionResult> StartWorkflowAsync(string definitionId, StartWorkflowRuntimeOptions options, CancellationToken cancellationToken = default);

    /// <summary>
    /// Starts all workflows with triggers matching the specified activity type and bookmark payload.
    /// </summary>
    /// <returns></returns>
    Task<ICollection<WorkflowExecutionResult>> StartWorkflowsAsync(
        string activityTypeName,
        object bookmarkPayload,
        TriggerWorkflowsOptions options,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Tries to start a workflow and returns the result if successful.
    /// </summary>
    Task<WorkflowExecutionResult?> TryStartWorkflowAsync(string definitionId, StartWorkflowRuntimeOptions options, CancellationToken cancellationToken = default);

    /// <summary>
    /// Resumes an existing workflow instance.
    /// </summary>
    /// <param name="workflowInstanceId">The ID of the workflow instance to resume.</param>
    /// <param name="options"></param>
    /// <param name="cancellationToken"></param>
    Task<WorkflowExecutionResult?> ResumeWorkflowAsync(string workflowInstanceId, ResumeWorkflowRuntimeOptions options, CancellationToken cancellationToken = default);

    /// <summary>
    /// Resumes all workflows that are bookmarked on the specified activity type. 
    /// </summary>
    Task<ICollection<WorkflowExecutionResult>> ResumeWorkflowsAsync(string activityTypeName, object bookmarkPayload, TriggerWorkflowsOptions options, CancellationToken cancellationToken = default);

    /// <summary>
    /// Starts all workflows and resumes existing workflow instances based on the specified activity type and bookmark payload.
    /// </summary>
    Task<TriggerWorkflowsResult> TriggerWorkflowsAsync(string activityTypeName, object bookmarkPayload, TriggerWorkflowsOptions options, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes a pending workflow.
    /// </summary>
    /// <param name="match">A workflow match to execute.</param>
    /// <param name="input"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WorkflowExecutionResult> ExecuteWorkflowAsync(WorkflowMatch match, IDictionary<string, object>? input = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds all the workflows that can be started or resumed based on a query model.
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<WorkflowMatch>> FindWorkflowsAsync(WorkflowsFilter filter, CancellationToken cancellationToken = default);

    /// <summary>
    /// Exports the <see cref="WorkflowState"/> of the specified workflow instance.
    /// </summary>
    Task<WorkflowState?> ExportWorkflowStateAsync(string workflowInstanceId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Imports the specified <see cref="WorkflowState"/>.
    /// </summary>
    Task ImportWorkflowStateAsync(WorkflowState workflowState, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds and removes bookmarks based on the provided bookmarks diff.
    /// </summary>
    Task UpdateBookmarksAsync(UpdateBookmarksRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the specified bookmark.
    /// </summary>
    /// <param name="bookmark">The bookmark to update.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task UpdateBookmarkAsync(StoredBookmark bookmark, CancellationToken cancellationToken = default);

    /// <summary>
    /// Counts the number of workflow instances based on the provided query args.
    /// </summary>
    Task<long> CountRunningWorkflowsAsync(CountRunningWorkflowsRequest request, CancellationToken cancellationToken = default);
}