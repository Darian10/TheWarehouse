using System;
using System.Collections.Generic;
using TheWarehouse.Data;
using TheWarehouse.Repo;

namespace TheWarehouse.Service
{
    public class AuctionService : IBaseEntityService
    {
        private IRepository<Auction> AuctionRepository;
        public AuctionService(IRepository<Auction> AuctionRepository)
        {
            this.AuctionRepository = AuctionRepository;
        }
        public void DeleteBaseEntity(long id)
        {
            Auction auction = (Auction)this.GetBaseEntity(id);
            AuctionRepository.Remove(auction);
            AuctionRepository.SaveChanges();
        }

        public IEnumerable<BaseEntity> GetAll()
        {
            return AuctionRepository.GetAll();
        }

        public BaseEntity GetBaseEntity(long id)
        {
            return AuctionRepository.Get(id);
        }

        public void InsertBaseEntity(BaseEntity BaseEntity)
        {
            AuctionRepository.Insert((Auction)BaseEntity);
        }

        public void UpdateBaseEntity(BaseEntity BaseEntity)
        {
            AuctionRepository.Update((Auction)BaseEntity);
        }
    }
}
