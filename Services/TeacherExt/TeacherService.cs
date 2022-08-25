using Domain.Entites;
using Repository;
using Repository.RepositoryPattern;
using Services.Upload;

namespace Services.TeacherExt
{
    public class TeacherService : ITeacherService, IDisposable
    {
        public readonly ApplicationDbContext _applicationDbContext;
        private readonly IRepository<Teacher> _repository;

        public TeacherService(
            IRepository<Teacher> repository,
            ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _repository = repository;
        }

        public Teacher Add(Teacher teacher)
        {
            try
            {
                teacher.Image = teacher.ImageFile != null ? ImageUploadService.Upload(teacher.ImageFile) : teacher.Image;
                return _repository.Add(teacher);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(Teacher teacher)
        {
            try
            {
                int count = _repository.Delete(teacher.TeacherId);
                return count != 0 ? true : false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Teacher Find(int id)
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

        public List<Teacher> GetAll()
        {
            try
            {
                List<Teacher> data = _repository.GetAll().ToList();
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Teacher Update(Teacher teacher)
        {
            try
            {
                var updateTeacher = _repository.Find(teacher.TeacherId);
                updateTeacher.Name = teacher.Name;
                updateTeacher.Age = teacher.Age;
                updateTeacher.Sex = teacher.Sex;
                updateTeacher.Image = teacher.ImageFile != null ? ImageUploadService.Upload(teacher.ImageFile, teacher.Image) : updateTeacher.Image;

                return _repository.Update(updateTeacher);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose() => _applicationDbContext.Dispose();
    }
}
