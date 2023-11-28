using Amazon.RedshiftDataAPIService.Model;

namespace Apps.AmazonRedshift.Models.Response;

public class SelectResponse
{
    public IEnumerable<RowResponse>? Rows { get; set; }

    public SelectResponse()
    {
    }

    public SelectResponse(GetStatementResultResponse? response)
    {
        Rows = response?.Records.Select(x => new RowResponse()
        {
            Values = x.Select((y, ind) =>
            {
                var column = response.ColumnMetadata[ind];
                return new ValueResponse(column.Name, GetRowValue(column, y));
            })
        });
    }

    private string? GetRowValue(ColumnMetadata column, Field field)
    {
        if (field.IsNull)
            return null;

        var columnType = column.TypeName;

        switch (columnType)
        {
            case var type when type.Contains("int"):
                return field.LongValue.ToString();
            case var type when type.Contains("float"):
                return field.DoubleValue.ToString();
            case "bool":
                return field.BooleanValue.ToString();
            default:
                return field.StringValue;
        }
    }
}