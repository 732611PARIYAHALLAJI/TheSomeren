using The_Someren.Models;
namespace The_Someren.Respository
{
    public interface IDrinkRepository
    {
        List<Drink> GetAll();
        Drink? GetById(int drinkId);
        void Add(Drink drink);
        void Update(Drink drink);
        void Delete(Drink drinkId);
    }

}
