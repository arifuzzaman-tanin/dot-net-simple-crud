using Domain.Entites;

namespace Services.Student
{
    public interface IAdmittedStudentService
    {
        AdmittedStudent Add(AdmittedStudent student);
        AdmittedStudent Update(AdmittedStudent student);
        bool Delete(AdmittedStudent student);
        AdmittedStudent Find(int id);
        List<AdmittedStudent> GetAll();
        List<IGrouping<int?, AdmittedStudent>> ByClass(string? name = null);
        bool CheckDuplicateRoll(string roll);
    }
}
