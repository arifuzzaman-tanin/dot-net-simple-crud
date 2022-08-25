using Domain.Entites;

namespace Services.TeacherExt
{
    public interface ITeacherService
    {
        Teacher Add(Teacher student);
        Teacher Update(Teacher student);
        bool Delete(Teacher student);
        Teacher Find(int id);
        List<Teacher> GetAll();
        List<IGrouping<int?, Teacher>> ByClass(string? name = null);
    }
}
