using Amazon.RedshiftDataAPIService.Model;
using Apps.AmazonRedshift.Invocables;
using Apps.AmazonRedshift.Models.Request.Database;
using Apps.AmazonRedshift.Models.Response;
using Apps.AmazonRedshift.Utils;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.AmazonRedshift.Actions;

[ActionList]
public class DatabaseActions : AmazonRedshiftInvocable
{
    public DatabaseActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    [Action("Get entries", Description = "Get all database entries")]
    public async Task<SelectResponse> SelectValues(
        [ActionParameter] TableRequest table,
        [ActionParameter] SelectRequest input)
    {
        var sql = SqlBuilder.SelectAll(table.Table, input.Schema);
        var response = await ExecuteStatement(table.Database, table.Workgroup, table.Cluster, sql);

        return new(response);
    }

    private async Task<GetStatementResultResponse> ExecuteStatement(string db, string? workgroup, string? cluster,
        string sql)
    {
        var response = await AmazonHandler.Execute(() => DataClient.ExecuteStatementAsync(new()
        {
            Database = db,
            WorkgroupName = workgroup,
            ClusterIdentifier = cluster,
            Sql = sql
        }));

        DescribeStatementResponse? statementResponse = default;

        while (statementResponse?.Status != "FINISHED" && statementResponse?.Status != "FAILED" && statementResponse?.Status != "ABORTED")
        {
            statementResponse = await AmazonHandler.Execute(() => DataClient.DescribeStatementAsync(new()
            {
                Id = response.Id
            }));
        }

        if (!string.IsNullOrWhiteSpace(statementResponse.Error))
            throw new(statementResponse.Error);

        return await AmazonHandler.Execute(() => DataClient.GetStatementResultAsync(new()
        {
            Id = response.Id
        }));
    }
}