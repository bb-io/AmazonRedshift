using Apps.AmazonRedshift.Invocables;
using Apps.AmazonRedshift.Utils;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.AmazonRedshift.DataSourceHandlers;

public class WorkGroupDataHandler : AmazonRedshiftInvocable, IAsyncDataSourceHandler
{
    public WorkGroupDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var clusterEnumerator =
            ServerlessClient.Paginators.ListWorkgroups(new()).Workgroups.GetAsyncEnumerator(cancellationToken);
        var clusters = await Paginator.Paginate(clusterEnumerator);

        return clusters
            .Where(x => context.SearchString is null ||
                        x.WorkgroupName.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(x => x.WorkgroupName, x => x.WorkgroupName);
    }
}