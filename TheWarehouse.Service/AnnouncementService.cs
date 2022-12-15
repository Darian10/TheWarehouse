using System;
using System.Collections.Generic;
using System.Text;
using TheWarehouse.Data;
using TheWarehouse.Repo;

namespace TheWarehouse.Service
{
    public class AnnouncementService : IBaseEntityService
    {
        private IRepository<Announcement> AnnouncementRepository;
        public AnnouncementService(IRepository<Announcement> AnnouncementRepository)
        {
            this.AnnouncementRepository = AnnouncementRepository;
        }
        public void DeleteBaseEntity(long id)
        {
            Announcement announcement = (Announcement)this.GetBaseEntity(id);
            AnnouncementRepository.Remove(announcement);
            AnnouncementRepository.SaveChanges();
        }

        public IEnumerable<BaseEntity> GetAll()
        {
            return AnnouncementRepository.GetAll();
        }

        public BaseEntity GetBaseEntity(long id)
        {
            return AnnouncementRepository.Get(id);
        }

        public void InsertBaseEntity(BaseEntity BaseEntity)
        {
            AnnouncementRepository.Insert((Announcement)BaseEntity);
        }

        public void UpdateBaseEntity(BaseEntity BaseEntity)
        {
            AnnouncementRepository.Update((Announcement)BaseEntity);
        }
    }
}
