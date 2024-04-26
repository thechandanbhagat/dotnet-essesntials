using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetEssentials.Linq.Extension;

public static class EnumerableExtensions
{
    public static void ForEachItem<T>(this IEnumerable<T> enumerables, Action<T> action)
    {
        foreach (var item in enumerables)
            action(item);
    }

    public static Task ForEachAsync<T>(this IEnumerable<T> enumerables, Func<T, Task> func) => Task.WhenAll(enumerables.Select(func).ToList());

    public static TSource EmptyIfNull<TSource>(this TSource source)
        where TSource : class, IEnumerable, new() => source ?? new TSource();

    public static IEnumerable<T> WhereIf<T>(
                this IEnumerable<T> query,
                    bool? condition,
                    Func<T, bool> predicate)
    {
        if (condition.HasValue && condition.Value)
        {
            return query.Where(predicate);
        }

        return query;
    }
}