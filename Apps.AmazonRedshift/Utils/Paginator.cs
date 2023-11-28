namespace Apps.AmazonRedshift.Utils;

public static class Paginator
{
    public static async Task<List<T>> Paginate<T>(IAsyncEnumerator<T> enumerator)
    {
        var result = new List<T>();

        while (await enumerator.MoveNextAsync())
            result.Add(enumerator.Current);

        return result;
    }
}