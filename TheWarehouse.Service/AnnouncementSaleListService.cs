using System;
using System.Collections.Generic;
using System.Text;
using TheWarehouse.Data;
using TheWarehouse.Repo;

namespace TheWarehouse.Service
{
    public class AnnouncementSaleListService : IBaseEntityService
    {
        private IRepository<AnnouncementSaleList> AnnouncementSaleListRepository;
        public AnnouncementSaleListService(IRepository<AnnouncementSaleList> AnnouncementSaleListRepository)
        {
            this.AnnouncementSaleListRepository = AnnouncementSaleListRepository;
        }
        public void DeleteBaseEntity(long id)
        {
            AnnouncementSaleList announcementSaleList = (AnnouncementSaleList)this.GetBaseEntity(id);
            AnnouncementSaleListRepository.Remove(announcementSaleList);
            AnnouncementSaleListRepository.SaveChanges();
        }

        public IEnumerable<BaseEntity> GetAll()
        {
            return AnnouncementSaleListRepository.GetAll();
        }

        public BaseEntity GetBaseEntity(long id)
        {
            return AnnouncementSaleListRepository.Get(id);
        }

        public void InsertBaseEntity(BaseEntity BaseEntity)
        {
            AnnouncementSaleListRepository.Insert((AnnouncementSaleList)BaseEntity);
        }

        public void UpdateBaseEntity(BaseEntity BaseEntity)
        {
            AnnouncementSaleListRepository.Update((AnnouncementSaleList)BaseEntity);
        }
    }
}
