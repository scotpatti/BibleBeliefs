using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BibleBeliefs.Database
{
    public partial class Beliefs
    {
        public Beliefs()
        {
            Verses = new HashSet<Verses>();
        }

        public long Id { get; set; }
        public string Belief { get; set; }
        public long TopicId { get; set; }

        public virtual Topics Topic { get; set; }
        public virtual ICollection<Verses> Verses { get; set; }
    }
}
