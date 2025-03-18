using The_Someren.Models;
namespace The_Someren.Respository
{
    public interface IRoomRepository
    {
        List<Room> GetAll();
        Room? GetById(int roomId);
        void Add(Room room);
        void Update(Room room);
        void Delete(Room room);
    }

}
