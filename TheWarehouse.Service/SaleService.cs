using System;
using System.Collections.Generic;
using System.Text;
using TheWarehouse.Data;
using TheWarehouse.Repo;

namespace TheWarehouse.Service
{
    public class SaleService : IBaseEntityService

    {
        private IRepository<Sale> SaleRepository;
        public SaleService(IRepository<Sale> SaleRepository)
        {
            this.SaleRepository = SaleRepository;
        }
        public void DeleteBaseEntity(long id)
        {
            Sale sale = (Sale)this.GetBaseEntity(id);
            SaleRepository.Remove(sale);
            SaleRepository.SaveChanges();
        }

        public IEnumerable<BaseEntity> GetAll()
        {
            return SaleRepository.GetAll();
        }

        public BaseEntity GetBaseEntity(long id)
        {
            return SaleRepository.Get(id);
        }

        public void InsertBaseEntity(BaseEntity BaseEntity)
        {
            SaleRepository.Insert((Sale)BaseEntity);
        }

        public void UpdateBaseEntity(BaseEntity BaseEntity)
        {
            SaleRepository.Update((Sale)BaseEntity);
        }
    }
}
