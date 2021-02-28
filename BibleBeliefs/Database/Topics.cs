using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BibleBeliefs.Database
{
    public partial class Topics
    {
        public Topics()
        {
            Beliefs = new HashSet<Beliefs>();
        }

        public long Id { get; set; }
        public string Topic { get; set; }

        public virtual ICollection<Beliefs> Beliefs { get; set; }
    }
}
