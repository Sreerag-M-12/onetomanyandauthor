using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using FluentOneToMany.Models;

namespace FluentOneToMany.Mappings
{
    public class OrderMap:ClassMap<Order>
    {
        public OrderMap()
        {
            Table("Orders");
            Id(o=>o.Id);
            Map(o=>o.Description);
            References(o=>o.Employee).Column("empId").Cascade.None().Nullable();
        }
    }
}