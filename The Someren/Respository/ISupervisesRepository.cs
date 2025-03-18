using The_Someren.Models;
namespace The_Someren.Respository
{
    public interface ISupervisesRepository
    {
        List<Supervises> GetAll();
        Supervises? GetById(int supervisesId);
        void Add(Supervises supervises);
        void Update(Supervises supervises);
        void Delete(Supervises supervises);
    }

}
