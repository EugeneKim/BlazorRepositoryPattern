using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Wwg.Core.Entities;
using Wwg.Core.Interfaces;

namespace Wwg.Core
{
	public class WordRepository : IRepository<Word>
	{
		private readonly DbSet<Word> dbSet;

		public WordRepository(IDbContext dbContext) => dbSet = dbContext.Set<Word>();

		public void Add(Word entity) => dbSet.Add(entity);

		public int Count(Expression<Func<Word, bool>> predicate = null) =>
			(predicate == null ? dbSet : dbSet.Where(predicate)).Count();

		public IReadOnlyList<Word> GetAll(Expression<Func<Word, bool>> predicate = null) =>
			throw new NotImplementedException();	

		public IReadOnlyList<Word> GetPagedAll(int page, int pageSize, Expression<Func<Word, bool>> predicate = null) =>
			(predicate == null ? dbSet : dbSet.Where(predicate))
			.Skip(page * pageSize).Take(pageSize)
			.ToList();

		public void Remove(Word entity) => dbSet.Remove(entity);

		public Word Single(Expression<Func<Word, bool>> predicate) =>
			IncludeAll(predicate).Single(predicate);

		public Word SingleOrDefault(Expression<Func<Word, bool>> predicate, Word defaultValue = null) =>
			IncludeAll(predicate).SingleOrDefault(predicate) ?? default;

		public void Update(Word entity) => dbSet.Update(entity);

		private IQueryable<Word> IncludeAll(Expression<Func<Word, bool>> predicate) =>
			(predicate == null ? dbSet : dbSet.Where(predicate))
				.Include(w => w.Meanings).ThenInclude(m => m.Definitions).ThenInclude(m => m.Synonyms)
				.Include(w => w.Meanings).ThenInclude(m => m.Definitions).ThenInclude(m => m.Antonyms);
	}
}
