using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Wwg.Core.Entities;
using Wwg.Core.Generic;
using Wwg.Core.Interfaces;
using Wwg.Infrastructure.Data;
using Xunit;

namespace Wwg.Core.Test.UnitTests.Repository
{
	public class GenericRepositoryTest : IClassFixture<GenericRepositoryTestFixture>
	{
		private readonly IRepository<Word> repository;

		public GenericRepositoryTest(GenericRepositoryTestFixture fixture)
		{
			repository = new GenericRepository<Word>(fixture.WordContext);
		}

		[Fact]
		public void GetTest()
		{
			Assert.Equal(3, repository.Count());

			var allWords = repository.GetAll();

			Assert.Contains(allWords, w => w.Name == "school");
			Assert.DoesNotContain(allWords, w => w.Name == "school2");
		
			Assert.Equal(2, repository.GetPagedAll(0, 2).Count);
			Assert.Equal(1, repository.GetPagedAll(1, 2).Count);
		}
	}

	public class GenericRepositoryTestFixture : IDisposable
	{
		public WordContext WordContext { get; }

		public GenericRepositoryTestFixture()
		{
			var options = new DbContextOptionsBuilder<WordContext>()
				.UseInMemoryDatabase("MockDatabase")
				.Options;

			WordContext = new WordContext(options);

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

			WordContext.Words.Add(word);

			word = new Word
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

			WordContext.Words.Add(word);

			word = new Word
			{
				Name = "company",
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
								Define = "A commercial business.",
								Example = "A shipping company.",
								Synonyms = new List<Synonym>
								{
									new Synonym("firm"),
									new Synonym("business"),
									new Synonym("corporation"),
									new Synonym("house"),
									new Synonym("establishment")
								}
							},
							new Definition
							{
								Language = Language.English,
								Define = "The fact or condition of being with another or others, " + 
										 "especially in a way that provides friendship and enjoyment.",
								Example = "I could do with some company.",
								Synonyms = new List<Synonym>
								{
									new Synonym("companionship"),
									new Synonym("presence"),
									new Synonym("friendship"),
									new Synonym("fellowship"),
									new Synonym("closeness")
								}
							},
							new Definition
							{
								Language = Language.English,
								Define = "The fact or condition of being with another or others, " + 
										 "especially in a way that provides friendship and enjoyment.",
								Example = "I could do with some company.",
								Synonyms = new List<Synonym>
								{
									new Synonym("companionship"),
									new Synonym("presence"),
									new Synonym("friendship"),
									new Synonym("fellowship"),
									new Synonym("closeness"),
									new Synonym("amity")
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
								Define = "Associate with; keep company with.",
								Example = "These men which have companied with us all this time"
							}
						}
					}
				}
			};

			WordContext.Words.Add(word);

			WordContext.SaveChanges();
		}

		/// <summary>
		/// Tear down the instances.
		/// </summary>
		public void Dispose()
		{
		}
	}
}
