using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FluentOneToMany.Models
{
    public class Order
    {
        public virtual int Id { get; set; }
        public virtual string Description { get; set; }
        public virtual Employee Employee { get; set; }
    }
}