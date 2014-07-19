using System;
using FluentNHibernate.Mapping;
using OSIM.Core.Entities;

namespace OSIM.Core.Persistence.Mappings
{
    public class ItemTypeMap : ClassMap<ItemType>
    {
        public ItemTypeMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
        }
    }
}
