using System.Linq.Expressions;

namespace DotnetEssentials.Linq.Extension
{
	public static class QueryableExtensions
	{
		public static IQueryable<T> WhereIf<T>(
				this IQueryable<T> query,
					bool? condition,
					Expression<Func<T, bool>> predicate)
		{
			if (condition.HasValue && condition.Value)
			{
				return query.Where(predicate);
			}

			return query;
		}

		public static IQueryable<T> WhereIfElse<T>(
				this IQueryable<T> query,
					bool? condition,
					Expression<Func<T, bool>> truepredicate,
							Expression<Func<T, bool>> falsePredicate)
		{
			if (condition.HasValue)
			{
				if (condition.Value)
					return query.Where(truepredicate);
				else
					return query.Where(falsePredicate);
			}

			return query;
		}

		public static void WhereIfQuery<T>(
			this IQueryable<T> query,
				bool? condition,
				Expression<Func<T, bool>> predicate)
		{
			if (condition.HasValue && condition.Value)
			{
				query = query.Where(predicate);
			}
		}
	}
}