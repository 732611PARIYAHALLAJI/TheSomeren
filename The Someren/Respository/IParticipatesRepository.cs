using The_Someren.Models;
namespace The_Someren.Respository
{
    public interface IParticipatesRepository
    {
        List<Participates> GetAll();
        Participates? GetById(int drinkId);
        void Add(Participates participates);
        void Update(Participates participates);
        void Delete(Participates participates);
    }

}
