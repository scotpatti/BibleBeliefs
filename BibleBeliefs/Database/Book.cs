using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BibleBeliefs.Database
{
    public partial class Book
    {
        public Book()
        {
            Verses = new HashSet<Verses>();
        }

        public long BookId { get; set; }
        public string BookTitle { get; set; }

        public virtual ICollection<Verses> Verses { get; set; }
    }
}
