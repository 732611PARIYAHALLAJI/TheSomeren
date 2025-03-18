using The_Someren.Models;
namespace The_Someren.Respository
{
    public interface IStudentRepository
    {
        List<Student> GetAll();
        Student? GetById(int studentId);
        void Add(Student student);
        void Update(Student student);
        void Delete(Student student);
    }

}
