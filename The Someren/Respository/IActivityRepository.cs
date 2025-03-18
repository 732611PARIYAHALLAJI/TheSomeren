using The_Someren.Models;
namespace The_Someren.Respository
{
    public interface IActivityRepository
    {
        List<Activity> GetAll();
        Activity? GetById(int activityId);
        void Add(Activity activity);
        void Update(Activity activity);
        void Delete(Activity activity);
    }

}
