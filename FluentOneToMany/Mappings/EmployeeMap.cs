using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using FluentOneToMany.Models;

namespace FluentOneToMany.Mappings
{
    public class EmployeeMap:ClassMap<Employee>
    {
        public EmployeeMap()
        {
            Table("Employees");
            Id(e=>e.Id).GeneratedBy.Identity();
            Map(e => e.Name);
            HasMany(e => e.Orders).Inverse().Cascade.All();
        }
    }
}