using Domain.Entites;

namespace Services.SubjectExt
{
    public interface ISubjectService
    {
        Subject Add(Subject subject);
        Subject Update(Subject subject);
        bool Delete(Subject subject);
        Subject Find(int id);
        List<Subject> GetAll();
        bool CheckDuplicateLang(string name, int classFor, int language);
    }
}
