using TestProjectSD.Interface;

namespace TestProjectSD.Repositories
{
    public class DataRepository : IDataRepository
    {
       protected DataBaseContext _dataContext;
        public DataRepository(DataBaseContext context) 
        { 
            _dataContext = context;
        }

        public bool SaveChanges()
        { 
           return _dataContext.SaveChanges() > 0 ;
        }
        public void AddEntity<T>(T entityToAdd)
        {
            if(entityToAdd != null)
            {
                _dataContext.Add(entityToAdd);
            }            
        }

        public void AddEntities<T>(List<T> entitiesToAdd)
        {
            if (entitiesToAdd != null && entitiesToAdd.Any())
            {
                _dataContext.AddRange(entitiesToAdd);
            }

        }

        public void RemoveEntity<T>(T entityToRemove)
        {
            if (entityToRemove != null)
            {
                _dataContext.Remove(entityToRemove);
            }
        }
    }
}
