using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FluentOneToMany.Models
{
    public class Employee
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Order> Orders { get; set; } = new List<Order>();
    }
}