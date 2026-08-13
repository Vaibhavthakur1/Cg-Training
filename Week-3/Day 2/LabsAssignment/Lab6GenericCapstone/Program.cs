public class CacheEntryOptions
{
    public string Label { get; set; } = string.Empty;
    public bool Pinned { get; set; }
}


public class TypedCache<TKey, TValue> where TKey : notnull
{
    private readonly Dictionary<TKey, TValue> _store = new();
    private static int _totalInstances;

    public TypedCache() {
        _totalInstances++;
    }


    // Indexer
    public TValue this[TKey key]
    {
        get
        {
            if (!_store.ContainsKey(key))
            {
                throw new KeyNotFoundException(
                    $"The given key '{key}' was not present in the cache."
                );
            }

            return _store[key];
        }

        set
        {
            _store[key] = value;
        }
    }

    public int Count => _store.Count;

    // Static read-only property
    public static int TotalInstances => _totalInstances;

    // Add method
    public void Add(
        TKey key,
        TValue value,
        CacheEntryOptions? options = null)
    {
        _store[key] = value;

        // Metadata can be used here.
        if (options != null)
        {
            Console.WriteLine(
                $"Added '{key}' - Label: {options.Label}, Pinned: {options.Pinned}"
            );
        }
    }

    public static void PrintGlobalStats()
    {
        Console.WriteLine(
            $"Global TypedCache<{typeof(TKey).Name},{typeof(TValue).Name}> instances created: {_totalInstances}"
        );
    }



}

class Program
{
    static void Main()
    {
        TypedCache<string, int> cache1 =
            new TypedCache<string, int>();

        TypedCache<string, int> cache2 =
            new TypedCache<string, int>();

        cache1.Add(
            "a",
            1,
            new CacheEntryOptions
            {
                Label = "First value",
                Pinned = true
            });

        cache1.Add(
            "b",
            2,
            new CacheEntryOptions
            {
                Label = "Second value",
                Pinned = false
            });

        cache2.Add(
           "x",
           100,
           new CacheEntryOptions
           {
               Label = "Cache 2 value",
               Pinned = true
           });

        Console.WriteLine($"cache1[\"a\"] = {cache1["a"]}");
        Console.WriteLine($"cache1.Count = {cache1.Count}");
        try
        {
            Console.WriteLine(cache1["z"]);
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine($"Missing key caught: {ex.Message}");
        }

        TypedCache<string, int>.PrintGlobalStats();
    }
}
