using The_Someren.Models;
using The_Someren.Respository;

public class ParticipatesRepository : IParticipatesRepository
    {
        private readonly string? connectionString;

        public ParticipatesRepository(IConfiguration configuration)
        {
            connectionString = "Server=tcp:testserverprojectpariya.database.windows.net,1433;Initial Catalog=TheSomeren;Persist Security Info=False;User ID=testadmin;Password=Pariya@4548;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }

    public void Add(Participates participates)
    {
        throw new NotImplementedException();
    }

    public void Delete(Participates participates)
    {
        throw new NotImplementedException();
    }

    public List<Participates> GetAll()
    {
        throw new NotImplementedException();
    }

    public Participates? GetById(int drinkId)
    {
        throw new NotImplementedException();
    }

    public void Update(Participates participates)
    {
        throw new NotImplementedException();
    }
}

