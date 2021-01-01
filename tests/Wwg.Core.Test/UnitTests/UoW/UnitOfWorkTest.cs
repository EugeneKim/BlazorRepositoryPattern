using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Wwg.Core.Entities;
using Wwg.Core.Generic;
using Wwg.Core.Interfaces;
using Wwg.Infrastructure.Data;
using Xunit;
using Xunit.Abstractions;

namespace Wwg.Core.Test.UnitTests.UoW
{
	public class UnitOfWorkTest : IClassFixture<UnitOfWorkTestFixture>
	{
		private readonly UnitOfWork uow;
		private readonly IRepository<Word> wordRepo;
		private readonly ITestOutputHelper output;

		public UnitOfWorkTest(UnitOfWorkTestFixture fixture, ITestOutputHelper output)
		{
			this.output = output;

			uow = fixture.UnitOfWork;
			wordRepo = fixture.WordRepo;
		}

		[Fact]
		public void AddTest()
		{
			var word = new Word
			{
				Name = "run",
				Level = Level.A1,
				Meanings = new List<Meaning>()
				{
					new Meaning
					{
						PartOfSpeech = PartOfSpeech.Verb,
						Definitions = new List<Definition>()
						{
							new Definition
							{
								Language = Language.English,
								Define = "Move at a speed faster than a walk, " +
										 "never having both or all the feet on the ground at the same time.",
								Example = "The dog ran across the road",
								Synonyms = new List<Synonym>
								{
									new Synonym("sprint"),
									new Synonym("race"),
									new Synonym("dart"),
									new Synonym("rush"),
									new Synonym("dash")
								}
							}
						}
					}
				}
			};

			wordRepo.Add(word);
			uow.Commit();

			var entity = wordRepo.SingleOrDefault(w => w.Name == "run");
			Assert.NotNull(entity);
		}

		[Fact]
		public void UpdateTest()
		{
			var entity = wordRepo.Single(w => w.Name == "school");

			Assert.Equal(2, entity.Meanings.Count);

			entity.Meanings.RemoveAt(1);
			wordRepo.Update(entity);

			uow.Commit();

			entity = wordRepo.Single(w => w.Name == "school");
			Assert.Single(entity.Meanings);
		}

		[Fact]
		public void RemoveTest()
		{
			wordRepo.Remove(wordRepo.Single(w => w.Name == "run"));
			uow.Commit();

			var entity = wordRepo.SingleOrDefault(w => w.Name == "run");
			Assert.Null(entity);
		}
	}

	public class UnitOfWorkTestFixture : IDisposable
	{
		public WordContext WordContext { get; }
		public UnitOfWork UnitOfWork { get; }
		public IRepository<Word> WordRepo { get; }

		/// <summary>
		/// Set up new instances.
		/// </summary>
		public UnitOfWorkTestFixture()
		{
			var options = new DbContextOptionsBuilder<WordContext>()
				.UseInMemoryDatabase("MockUnitOfWorkDatabase")
				.Options;

			WordContext = new WordContext(options);
			UnitOfWork = new UnitOfWork(WordContext);

			WordRepo = UnitOfWork.GetRepository<Word>();

			var word = new Word
			{
				Name = "school",
				Level = Level.A1,
				Meanings = new List<Meaning>()
				{
					new Meaning
					{
						PartOfSpeech = PartOfSpeech.Noun,
						Definitions = new List<Definition>()
						{
							new Definition
							{
								Language = Language.English,
								Define = "An institution for educating children.",
								Example = "Ryder's children did not go to school at all.",
								Synonyms = new List<Synonym>
								{
									new Synonym("educational institution"),
									new Synonym("centre of learning")
								}
							},
							new Definition
							{
								Language = Language.English,
								Define = "Any institution at which instruction is given in a particular discipline.",
								Example = "A dancing school"
							},
							new Definition
							{
								Language = Language.English,
								Define = "A group of people, particularly writers, artists, or philosophers, " +
										 "sharing the same or similar ideas, methods, or style.",
								Example = "The Frankfurt school of critical theory",
								Synonyms = new List<Synonym>
								{
									new Synonym("group"),
									new Synonym("set"),
									new Synonym("circle"),
									new Synonym("clique"),
									new Synonym("faction"),
									new Synonym("sect")
								}
							}
						}
					},
					new Meaning()
					{
						PartOfSpeech = PartOfSpeech.Verb,
						Definitions = new List<Definition>()
						{
							new Definition
							{
								Language = Language.English,
								Define = "Send to school; educate.",
								Example = "he was schooled in Boston.",
								Synonyms = new List<Synonym>
								{
									new Synonym("educate"),
									new Synonym("teach"),
									new Synonym("instruct")
								}
							}
						}
					}
				}
			};

			WordRepo.Add(word);
			UnitOfWork.Commit();
		}

		/// <summary>
		/// Tear down the instances.
		/// </summary>
		public void Dispose()
		{
		}
	}
}
