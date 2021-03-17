using BibleBeliefs.Repository;
using System;
using System.ComponentModel;

namespace BibleBeliefs.ViewModels
{
    /// <summary>
    /// Note: Database data inserted into SelectedX refers to the index of the 
    /// value. Hence, in a strict equating of the database values to the DTOs, 
    /// we would have the following:
    /// SelectedChapter = 0 --> Genesis 1
    /// SelectedVerseStart = SelectedVerseEnd = 0 --> Genesis 1:1
    /// 
    /// </summary>
    public class VerseViewModel : BaseDTO
    {
        private BibleRepository repo;

        private BindingList<string> _BookList;
        private BindingList<int> _ChapterList;
        private BindingList<int> _VerseList;
        private string _SelectedBook;
        private int _SelectedChapter;
        private int _SelectedVerseStart;
        private int _SelectedVerseEnd;

        public BindingList<string> BookList { get { return _BookList; } set { SetField<BindingList<String>>(ref _BookList, value); } }
        public BindingList<int> ChapterList { get { return _ChapterList; } set { SetField<BindingList<int>>(ref _ChapterList, value); } }
        public BindingList<int> VerseList { get { return _VerseList; } set { SetField<BindingList<int>>(ref _VerseList, value); } }
        public string SelectedBook { get { return _SelectedBook; } set { ChangeBook(value); } }
        public int SelectedChapter { get { return _SelectedChapter; } set { ChangeChapter(value); } }
        public int SelectedVerseStart { get { return _SelectedVerseStart; } set { SetField<int>(ref _SelectedVerseStart, value); } }
        public int SelectedVerseEnd { get { return _SelectedVerseEnd; } set { SetField<int>(ref _SelectedVerseEnd, value); } }

        public VerseViewModel()
        {
            repo = new BibleRepository(); //using the default kjv version
            SetBookList(); //This will cause the other lists to be populated.
            SelectedChapter = 0;
            SelectedVerseStart = 0;
            SelectedVerseEnd = 0;
        }

        private void SetBookList()
        {
            BookList = repo.GetBooks();
            SelectedBook = BookList[0];
        }

        private void SetChapterList()
        {
            ChapterList = repo.GetChapters(SelectedBook);
            SelectedChapter = ChapterList[0];
        }

        private void SetVerseList()
        {
            VerseList = repo.GetVerses(SelectedBook, SelectedChapter);
            if (SelectedVerseStart > VerseList.Count)
            {
                SelectedVerseStart = VerseList[0];
                SelectedVerseEnd = VerseList[0];
            }
        }

        private void ChangeBook(string book)
        {
            if (SetField<string>(ref _SelectedBook, book, "SelectedBook"))
            {
                SetChapterList();
            }
        }

        private void ChangeChapter(int chapter)
        {
            if (SetField<int>(ref _SelectedChapter, chapter, "SelectedChapter"))
            {
                SetVerseList();
            }
        }
    }
}
