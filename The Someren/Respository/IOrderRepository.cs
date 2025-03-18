using The_Someren.Models;
namespace The_Someren.Respository
{
    public interface IOrderRepository
    {
        List<Order> GetAll();
        Order? GetById(int orderId);
        void Add(Order order);
        void Update(Order order);
        void Delete(Order order);
    }

}
