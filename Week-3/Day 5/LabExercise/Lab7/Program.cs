using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab7RepositoryAndInitializers
{
    // =========================================================================
    // 1 & 2. Entity Contract & Generic Repository
    // =========================================================================
    public interface IEntity
    {
        int Id { get; }
    }

    public interface IRepository<T> where T : class
    {
        void Add(T item);
        T? GetById(int id);
        IEnumerable<T> GetAll();
    }

    public class InMemoryRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly Dictionary<int, T> _storage = new();

        public void Add(T item)
        {
            ArgumentNullException.ThrowIfNull(item);

            if (!_storage.TryAdd(item.Id, item))
            {
                throw new ArgumentException($"An entity with ID {item.Id} already exists.", nameof(item));
            }
        }

        public T? GetById(int id) => _storage.GetValueOrDefault(id);

        public IEnumerable<T> GetAll() => _storage.Values;
    }

    // Concrete Entity for testing
    public record Article(int Id, string Title, TagList Tags) : IEntity;

    // =========================================================================
    // 3. TagList with Overloaded Add Methods
    // =========================================================================
    public class TagList : IEnumerable<string>
    {
        private readonly List<string> _tags = new();

        // Overload 1: Standard single string add
        public void Add(string tag)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(tag);
            _tags.Add(tag.Trim().ToLowerInvariant());
        }

        // Overload 2: Multi-parameter add supporting custom formatting/metadata
        public void Add(string tag, bool highlighted)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(tag);
            string formattedTag = highlighted ? $"[★ {tag.Trim().ToUpperInvariant()}]" : tag.Trim().ToLowerInvariant();
            _tags.Add(formattedTag);
        }

        public IEnumerator<string> GetEnumerator() => _tags.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    // =========================================================================
    // 4. Demonstration
    // =========================================================================
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("=== 1. Mixed Collection-Initializer Syntax on TagList ===");

            // Collection initializer calls both Add(string) and Add(string, bool)
            var tags = new TagList
            {
                "csharp",                      // Calls Add(string)
                { "dotnet", true },            // Calls Add(string, bool)
                "generics",                    // Calls Add(string)
                { "architecture", false },     // Calls Add(string, bool)
                { "featured", true }           // Calls Add(string, bool)
            };

            foreach (var tag in tags)
            {
                Console.WriteLine($"• {tag}");
            }
            Console.WriteLine();

            Console.WriteLine("=== 2. Testing Generic InMemoryRepository ===");
            var articleRepo = new InMemoryRepository<Article>();

            var article1 = new Article(101, "Deep Dive into C# Collections", tags);
            var article2 = new Article(102, "Patterns for Clean Architecture", new TagList { "patterns", { "cleancode", true } });

            articleRepo.Add(article1);
            articleRepo.Add(article2);

            var retrieved = articleRepo.GetById(101);
            if (retrieved is not null)
            {
                Console.WriteLine($"Retrieved Article #{retrieved.Id}: \"{retrieved.Title}\"");
                Console.WriteLine($"Tags: {string.Join(", ", retrieved.Tags)}");
            }
            Console.WriteLine();

            Console.WriteLine("All Articles in Repository:");
            foreach (var article in articleRepo.GetAll())
            {
                Console.WriteLine($"- [{article.Id}] {article.Title} ({string.Join(", ", article.Tags)})");
            }
        }
    }
}