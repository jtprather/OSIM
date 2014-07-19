using System;
using NHibernate;
using OSIM.Core.Entities;

namespace OSIM.Core.Persistence
{
    public interface IItemTypeRepository
    {
        int Save(ItemType itemType);
    }
    public class ItemTypeRepository : IItemTypeRepository
    {
        private ISessionFactory _sessionFactory;
        public ItemTypeRepository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }
        public int Save(ItemType itemType)
        {
            int id;
            using (var session = _sessionFactory.OpenSession())
            {
                id = (int)session.Save(itemType);
                session.Flush();
            }
            return id;
        }
        public ItemType GetById(int Id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.Get<ItemType>(Id);
            }
        }
    }
}
