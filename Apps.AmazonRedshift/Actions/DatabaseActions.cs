using Amazon.RedshiftDataAPIService.Model;
using Apps.AmazonRedshift.Invocables;
using Apps.AmazonRedshift.Models.Request.Database;
using Apps.AmazonRedshift.Models.Response;
using Apps.AmazonRedshift.Utils;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.AmazonRedshift.Actions;

[ActionList("Database entries")]
public class DatabaseActions : AmazonRedshiftInvocable
{
    public DatabaseActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    [Action("Get entries", Description = "Get all database table entries")]
    public async Task<SelectResponse> SelectValues(
        [ActionParameter] TableRequest table,
        [ActionParameter] SelectRequest input)
    {
        var sql = SqlBuilder.SelectAll(table.Table, input.Schema, input.Where, input.Limit, input.Offset);
        var response = await ExecuteStatement(table.Database, table.Workgroup, table.Cluster, sql);

        return new(response);
    }

    [Action("Add entry", Description = "Add a new table entry")]
    public async Task InsertValue(
        [ActionParameter] TableRequest tableRequest,
        [ActionParameter] InsertRequest input)
    {
        var table = await DataClient.DescribeTableAsync(new()
        {
            Database = tableRequest.Database,
            WorkgroupName = tableRequest.Workgroup,
            ClusterIdentifier = tableRequest.Cluster,
            Table = tableRequest.Table
        });

        var insertValues = input.Values.ToArray();
        for (var i = 0; i < insertValues.Length; i++)
        {
            var columnType = table.ColumnList[i].TypeName;

            if (IsStringType(columnType, insertValues[i]))
                insertValues[i] = $"'{insertValues[i]}'";
        }

        var sql = SqlBuilder.Insert(tableRequest.Table, input.Schema, insertValues);
        await ExecuteStatement(tableRequest.Database, tableRequest.Workgroup, tableRequest.Cluster, sql);
    }

    [Action("Update entries", Description = "Update entries of the table")]
    public async Task UpdateValues(
        [ActionParameter] TableRequest tableRequest,
        [ActionParameter] UpdateRequest input)
    {
        var updateColumns = input.Columns.ToArray();
        var updateValues = input.Values.ToArray();

        if (updateColumns.Length != updateValues.Length)
            throw new("Count of columns and values should be the same");

        var table = await DataClient.DescribeTableAsync(new()
        {
            Database = tableRequest.Database,
            WorkgroupName = tableRequest.Workgroup,
            ClusterIdentifier = tableRequest.Cluster,
            Table = tableRequest.Table
        });

        for (var i = 0; i < updateColumns.Length; i++)
        {
            var columnType = table.ColumnList.FirstOrDefault(x => x.Name == updateColumns[i])?.TypeName;

            if (columnType is null)
                throw new($"Column {updateColumns[i]} does not exist in the table {tableRequest.Table}");

            if (IsStringType(columnType, updateValues[i]))
                updateValues[i] = $"'{updateValues[i]}'";
        }

        var sql = SqlBuilder.Update(tableRequest.Table, input.Schema, input.Where, updateColumns, updateValues);
        await ExecuteStatement(tableRequest.Database, tableRequest.Workgroup, tableRequest.Cluster, sql);
    }

    [Action("Delete entries", Description = "Delete entries from the table")]
    public Task DeleteValues(
        [ActionParameter] TableRequest table,
        [ActionParameter] DeleteRequest input)
    {
        var sql = SqlBuilder.Delete(table.Table, input.Schema, input.Where);
        return ExecuteStatement(table.Database, table.Workgroup, table.Cluster, sql);
    }

    [Action("Query database", Description = "Execute SQL query for a specific database")]
    public async Task<SelectResponse> QueryDatabase(
        [ActionParameter] TableRequest table,
        [ActionParameter] QueryDatabaseRequest input)
    {
        var response = await ExecuteStatement(table.Database, table.Workgroup, table.Cluster, input.Sql);
        return new(response);
    }

    private async Task<GetStatementResultResponse?> ExecuteStatement(string db, string? workgroup, string? cluster,
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

        while (statementResponse?.Status != "FINISHED" && statementResponse?.Status != "FAILED" &&
               statementResponse?.Status != "ABORTED")
        {
            statementResponse = await AmazonHandler.Execute(() => DataClient.DescribeStatementAsync(new()
            {
                Id = response.Id
            }));
        }

        if (!string.IsNullOrWhiteSpace(statementResponse.Error))
            throw new(statementResponse.Error);

        if (statementResponse.ResultSize == 0)
            return null;

        return await AmazonHandler.Execute(() => DataClient.GetStatementResultAsync(new()
        {
            Id = response.Id
        }));
    }

    private bool IsStringType(string type, string value)
        => !type.StartsWith("int") && !type.StartsWith("float") && type != "bool" &&
           !value.Equals("null", StringComparison.OrdinalIgnoreCase);
}