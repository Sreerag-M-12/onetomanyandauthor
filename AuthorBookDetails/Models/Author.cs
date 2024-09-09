using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Criterion;

namespace AuthorBookDetails.Models
{
    public class Author
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual int Age { get; set; }
        public virtual IList<Book> Books { get; set; } = new List<Book>();
        public virtual AuthorDetail AuthorDetail { get; set; }= new AuthorDetail();

    }
}