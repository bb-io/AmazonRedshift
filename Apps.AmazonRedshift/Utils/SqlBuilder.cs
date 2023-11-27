namespace Apps.AmazonRedshift.Utils;

public static class SqlBuilder
{
    public static string SelectAll(string table, string? schema)
    {
        var selectSource = string.IsNullOrWhiteSpace(schema) ? table : $"{schema}.{table}";
        return $"SELECT * FROM {selectSource}";
    }
}