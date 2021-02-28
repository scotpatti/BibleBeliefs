using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BibleBeliefs.Database
{
    public partial class Verses
    {
        public long Id { get; set; }
        public string Verse { get; set; }
        public long BeliefId { get; set; }
        public long Book { get; set; }
        public long Chapter { get; set; }
        public long VerseStart { get; set; }
        public long VerseEnd { get; set; }

        public virtual Beliefs Belief { get; set; }
        public virtual Book BookNavigation { get; set; }
    }
}
