using System.Collections.Generic;
using Wwg.Core.Entities;
using Wwg.Core.Interfaces;
using Wwg.Services.Mappers;
using Wwg.Services.Models;

namespace Wwg.Services
{
	public class WordService : IService
	{
		private IUnitOfWork unitOfWork;
		private IRepository<Word> repo;

		public WordService(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
			repo = unitOfWork.GetRepository<Word>();
		}

		public void AddWord(WordModel word)
		{
			var wordEntity = WordMapper.ConvertModelToEntity(word);

			repo.Add(wordEntity);
			
			unitOfWork.Commit();
		}

		public void UpdateWord(WordModel word)
		{
			var found = repo.Single(w => w.Name == word.Name);

			WordMapper.UpdateEntityWithModel(word, found);

			repo.Update(found);

			unitOfWork.Commit();
		}

		public WordModel GetWord(string word)
		{
			var found = repo.SingleOrDefault(w => w.Name == word);

			return found == default ? null : WordMapper.ConvertEntityToModel(found);
		}

		public bool Remove(string word)
		{
			var found = repo.SingleOrDefault(w => w.Name == word);

			if (found == default)
				return false;

			repo.Remove(found);

			unitOfWork.Commit();

			return true;
		}

		public IReadOnlyList<WordModel> GetPagedAll(int page, int pageSize, string filter)
		{
			var wordEntites = repo.GetPagedAll(page, pageSize);

			var list = new List<WordModel>();

			foreach (var wordEntity in wordEntites)
				list.Add(WordMapper.ConvertEntityToModel(wordEntity));

			return list;
		}
	}
}
