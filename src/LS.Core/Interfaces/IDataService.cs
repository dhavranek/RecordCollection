using System.Collections.Generic;

namespace RC.Core.Interfaces
{
    public interface IDataService<TEntity, TPK> where TEntity : class, IEntity<TPK>
    {
        ServiceProcessingResult<List<TEntity>> GetAll();

        ServiceProcessingResult<TEntity> Get(TPK id);

        ServiceProcessingResult<TEntity> Upsert(TEntity entity);

        ServiceProcessingResult Delete(TPK id);
    }
}
