using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using AuthorBookDetails.Models;
using FluentNHibernate.Mapping;

namespace AuthorBookDetails.Mappings
{
    public class AuthorMap:ClassMap<Author>
    {
        public AuthorMap()
        {
            Table("Authors");
            Id(a=>a.Id).GeneratedBy.Identity();
            Map(a => a.Name);
            Map(a => a.Email);
            Map(a => a.Age);
            HasOne(a=>a.AuthorDetail).PropertyRef(ad=>ad.Author).Cascade.All();
            HasMany(a => a.Books).Inverse().Cascade.All();
        }
    }
}