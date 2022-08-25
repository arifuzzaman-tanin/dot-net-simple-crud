using Domain.Entites;
using Repository;
using Repository.RepositoryPattern;
using Services.Upload;
using System.Linq.Expressions;

namespace Services.Student
{
    public class AdmittedStudentService : IAdmittedStudentService, IDisposable
    {
        public readonly ApplicationDbContext _applicationDbContext;
        private readonly IRepository<AdmittedStudent> _repository;

        public AdmittedStudentService(
            IRepository<AdmittedStudent> repository,
            ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _repository = repository;
        }

        public AdmittedStudent Add(AdmittedStudent student)
        {
            try
            {
                student.Image = student.ImageFile != null ? ImageUploadService.Upload(student.ImageFile) : student.Image;
                return _repository.Add(student);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public AdmittedStudent Update(AdmittedStudent student)
        {
            try
            {
                var updateStudent = _repository.Find(student.StudentId);
                updateStudent.Name = student.Name;
                updateStudent.Age = student.Age;
                updateStudent.Sex = student.Sex;
                updateStudent.Class = student.Class;
                updateStudent.RollNo = student.RollNo;
                updateStudent.Image = student.ImageFile != null ? ImageUploadService.Upload(student.ImageFile, updateStudent.Image) : updateStudent.Image;

                return _repository.Update(updateStudent);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(AdmittedStudent student)
        {
            try
            {
                int count = _repository.Delete(student.StudentId);
                return count != 0 ? true : false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public AdmittedStudent Find(int id)
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

        public List<AdmittedStudent> GetAll()
        {
            try
            {
                List<AdmittedStudent> data = _repository.GetAll().ToList();
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose() => _applicationDbContext.Dispose();

        public List<IGrouping<int?, AdmittedStudent>> ByClass(string? name = null)
        {
            Expression<Func<AdmittedStudent, bool>> expression = x => x.Name.Contains(name == null ? "" : name);
            var data = _repository.GetAllBy(expression).ToList().GroupBy(x => x.Class);
            return data.ToList();
        }
    }
}
