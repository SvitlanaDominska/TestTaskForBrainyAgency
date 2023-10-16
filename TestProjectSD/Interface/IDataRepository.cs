
namespace TestProjectSD.Interface
{
    public interface IDataRepository
    {
        public bool SaveChanges();
        public void AddEntity<T>(T entityToAdd);
        public void AddEntities <T>(List<T> entitiesToAdd);
        public void RemoveEntity<T>(T entityToRemove);
    }
}
