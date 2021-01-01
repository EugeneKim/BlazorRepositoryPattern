using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Diagnostics;
using Wwg.Core.Entities;
using Wwg.Core.Interfaces;

namespace Wwg.Infrastructure.Data
{
	public class WordContext : DbContext, IDbContext
	{
		public DbSet<Word> Words { get; set; }
		public WordContext() { }

		public WordContext(DbContextOptions<WordContext> options) : base(options) { }
	}

	public class WordContextFactory : IDesignTimeDbContextFactory<WordContext>
	{
		// The design-time DB context is required to create the DbContext in a class library.
		// https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli
		public WordContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<WordContext>();

			if (args.Length == 2 && args[0] == "--path")	
				optionsBuilder.UseSqlite($"Data Source={args[1]}");

			return new WordContext(optionsBuilder.Options);
		}
	}
}
