using BibleBeliefs.Repository;

namespace BibleBeliefs.Database
{
    public partial class Verses
    {
        public string VerseText
        {
            get
            {
                long ch = Chapter + 1;
                long vs = VerseStart + 1;
                long ve = VerseEnd + 1;
                if (VerseEnd > VerseStart)
                { 
                    return Books.BookArray[Book] + " " + ch + ":" + vs + "-" + ve;
                }
                else
                {
                    return Books.BookArray[Book] + " " + ch + ":" + vs;
                }
            }
        }
    }
}
