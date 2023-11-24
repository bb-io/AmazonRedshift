using Amazon.RedshiftDataAPIService.Model;
using Apps.AmazonRedshift.Invocables;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.AmazonRedshift.Actions;

[ActionList]
public class DatabaseActions : AmazonRedshiftInvocable
{
    public DatabaseActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    private async Task<GetStatementResultResponse> ExecuteStatement(string db, string workgroup, string cluster,
        string sql)
    {
        var statement = await DataClient.ExecuteStatementAsync(new()
        {
            Database = db,
            WorkgroupName = workgroup,
            ClusterIdentifier = cluster,
            Sql = sql
        });

        return await DataClient.GetStatementResultAsync(new()
        {
            Id = statement.Id
        });
    }
}