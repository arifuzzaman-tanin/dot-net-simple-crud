using Domain.Entites;
using Repository;
using Repository.RepositoryPattern;
using System.Linq.Expressions;

namespace Services.SubjectExt
{
    public class SubjectService : ISubjectService, IDisposable
    {
        public readonly ApplicationDbContext _applicationDbContext;
        private readonly IRepository<Subject> _repository;

        public SubjectService(
            IRepository<Subject> repository,
            ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _repository = repository;
        }

        public Subject Add(Subject subject)
        {
            try
            {
                return _repository.Add(subject);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(Subject subject)
        {
            try
            {
                int count = _repository.Delete(subject.SubjectId);
                return count != 0 ? true : false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Subject Find(int id)
        {
            try
            {
                return _repository.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Subject> GetAll()
        {
            try
            {
                List<Subject> data = _repository.GetAll().ToList();
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Subject Update(Subject subject)
        {
            try
            {
                var updateSubject = _repository.Find(subject.SubjectId);
                updateSubject.Name = subject.Name;
                updateSubject.Class = subject.Class;
                updateSubject.SubjectLanguage = subject.SubjectLanguage;

                return _repository.Update(updateSubject);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose() => _applicationDbContext.Dispose();

        public bool CheckDuplicateLang(string name, int classFor, int language)
        {
            try
            {
                Expression<Func<Subject, bool>> expression = x => x.Name == name && x.Class == classFor && (int)x.SubjectLanguage == language;
                var data = _repository.GetAllBy(expression).FirstOrDefault();
                return data != null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
