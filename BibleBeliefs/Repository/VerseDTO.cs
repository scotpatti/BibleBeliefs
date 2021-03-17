namespace BibleBeliefs.Repository
{
    public class VerseDTO : BaseDTO
    {
        private long _Id;
        private string _VerseText;
        private long _Book;
        private long _Chapter;
        private long _VerseStart;
        private long _VerseEnd;
        private long _BeliefId;

        public long Id
        {
            get { return _Id; }
            set { SetField<long>(ref _Id, value); }
        }

        public string VerseText
        {
            get { return _VerseText; }
            set { SetField<string>(ref _VerseText, value); }
        }

        public long Book
        {
            get { return _Book; }
            set { SetField<long>(ref _Book, value); }
        }

        public long Chapter
        {
            get { return _Chapter; }
            set { SetField<long>(ref _Chapter, value); }
        }

        public long VerseStart
        {
            get { return _VerseStart; }
            set { SetField<long>(ref _VerseStart, value); }
        }

        public long VerseEnd
        {
            get { return _VerseEnd; }
            set { SetField<long>(ref _VerseEnd, value); }
        }

        public long BeliefId
        {
            get { return _BeliefId; }
            set { SetField<long>(ref _BeliefId, value); }
        }

        // TODO: DELETE THIS OVERRIDE
        //public override string ToString()
        //{
        //    if (VerseEnd>VerseStart)
        //    {
        //        return Books.BookArray[Book] + " " + Chapter + ":" + VerseStart + "-" + VerseEnd;
        //    }
        //    else
        //    {
        //        return Books.BookArray[Book] + " " + Chapter + ":" + VerseStart;
        //    }
        //}
    }
}
