using System;
using System.Collections.Generic;
using System.Text;
using TheWarehouse.Data;

namespace TheWarehouse.Service
{
    public interface IBaseEntityService
    {
        IEnumerable<BaseEntity> GetAll();
        BaseEntity GetBaseEntity(long id);
        void InsertBaseEntity(BaseEntity BaseEntity);
        void UpdateBaseEntity(BaseEntity BaseEntity);
        void DeleteBaseEntity(long id);
    }
}
