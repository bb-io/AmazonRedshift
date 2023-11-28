namespace Apps.AmazonRedshift.Utils;

public static class SqlBuilder
{
    public static string SelectAll(string table, string? schema, string? where, int? limit, int? offset)
    {
        var selectSource = string.IsNullOrWhiteSpace(schema) ? table : $"{schema}.{table}";
        var result = $"SELECT * FROM {selectSource}";

        if (where is not null)
            result = $"{result} WHERE {where}";

        if (limit is not null)
            result = $"{result} LIMIT {limit}";

        if (offset is not null)
            result = $"{result} OFFSET {offset}";

        return result;
    }

    public static string Insert(string table, string? schema, IEnumerable<string> values)
    {
        var selectSource = string.IsNullOrWhiteSpace(schema) ? table : $"{schema}.{table}";
        return $"INSERT INTO {selectSource} VALUES ({string.Join(", ", values)})";
    }

    public static string Delete(string table, string? schema, string? where)
    {
        var selectSource = string.IsNullOrWhiteSpace(schema) ? table : $"{schema}.{table}";
        var result = $"DELETE FROM {selectSource}";

        if (where is not null)
            result = $"{result} WHERE {where}";

        return result;
    }

    public static string Update(string table, string? schema, string? where, IEnumerable<string> columns,
        IEnumerable<string> values)
    {
        var selectSource = string.IsNullOrWhiteSpace(schema) ? table : $"{schema}.{table}";

        var setQuery = columns.Zip(values).Select(x => $"{x.First} = {x.Second}");
        var result = $"UPDATE {selectSource} SET {string.Join(", ", setQuery)}";

        if (where is not null)
            result = $"{result} WHERE {where}";

        return result;
    }
}