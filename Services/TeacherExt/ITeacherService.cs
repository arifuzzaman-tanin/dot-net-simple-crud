using Domain.Entites;

namespace Services.TeacherExt
{
    public interface ITeacherService
    {
        Teacher Add(Teacher teacher);
        Teacher Update(Teacher teacher);
        bool Delete(Teacher teacher);
        Teacher Find(int id);
        List<Teacher> GetAll();
    }
}
