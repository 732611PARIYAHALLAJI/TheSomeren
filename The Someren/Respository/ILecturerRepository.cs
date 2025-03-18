using The_Someren.Models;
namespace The_Someren.Respository
{
    public interface ILecturerRepository
    {
        List<Lecturer> GetAll();
        Lecturer? GetById(int lectureId);
        void Add(Lecturer lecturer);
        void Update(Lecturer lecturer);
        void Delete(Lecturer lecturer);
    }

}
