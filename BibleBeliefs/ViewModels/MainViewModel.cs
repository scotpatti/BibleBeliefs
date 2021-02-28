using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using BibleBeliefs.Database;

namespace BibleBeliefs.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {

        #region Private Fields
        private BibleBeliefsContext context;

        private BindingList<Topics> _Topics;
        private BindingList<Beliefs> _Beliefs;
        private BindingList<Verses> _Verses;
        private Topics _SelectedTopic;
        private Beliefs _SelectedBelief;
        private Verses _SelectedVerse;
        #endregion

        #region Public Properties
        public BindingList<Topics> Topics
        {
            get { return _Topics; }
            set
            {
                SetField<BindingList<Topics>>(ref _Topics, value);
                SelectedTopic = Topics != null && Topics.Count > 0 ? Topics[0] : null;
            }
        }

        public BindingList<Beliefs> Beliefs
        {
            get { return _Beliefs; }
            set
            {
                SetField<BindingList<Beliefs>>(ref _Beliefs, value);
                SelectedBelief = Beliefs != null && Beliefs.Count > 0 ? Beliefs[0] : null;
            }
        }

        public BindingList<Verses> Verses
        {
            get { return _Verses; }
            set
            {
                SetField<BindingList<Verses>>(ref _Verses, value);
                SelectedVerse = Verses != null && Verses.Count > 0 ? Verses[0] : null;
            }
        }

        public Topics SelectedTopic
        {
            get { return _SelectedTopic; }
            set
            {
                SetField<Topics>(ref _SelectedTopic, value);
                if (_SelectedTopic != null)
                {
                    var beliefs = context.Beliefs.Where(x => x.TopicId == _SelectedTopic.Id).ToList();
                    Beliefs = beliefs != null ? new BindingList<Beliefs>(beliefs) : Beliefs = new BindingList<Beliefs>();
                }
            }
        }


        public Beliefs SelectedBelief
        {
            get { return _SelectedBelief; }
            set
            {

                SetField<Beliefs>(ref _SelectedBelief, value);
                if (_SelectedBelief != null)
                {
                    var verses = context.Verses.Where(x => x.BeliefId == _SelectedBelief.Id).ToList();
                    Verses = verses != null ? new BindingList<Verses>(verses) : new BindingList<Verses>();
                }
            }
        }

        public Verses SelectedVerse
        {
            get { return _SelectedVerse; }
            set
            {
                SetField<Verses>(ref _SelectedVerse, value);
                //ToDo: Some logic here to edit a verse
            }
        }

        public DelegateCommand<object> SelectedVerseCommand { get; set; }
        public DelegateCommand<object> NewTopicCommand { get; set; }
        public DelegateCommand<object> NewBeliefCommand { get; set; }
        public DelegateCommand<object> NewVerseCommand { get; set; }
        public DelegateCommand<object> EditTopicCommand { get; set; }
        public DelegateCommand<object> EditBeliefCommand { get; set; }
        public DelegateCommand<object> EditVerseCommand { get; set; }
        public DelegateCommand<object> DeleteTopicCommand { get; set; }
        public DelegateCommand<object> DeleteBeliefCommand { get; set; }
        public DelegateCommand<object> DeleteVerseCommand { get; set; }
        #endregion

        public MainViewModel()
        {
            context = new BibleBeliefsContext();
            Topics = new BindingList<Topics>(context.Topics.OrderBy(x => x.Topic).ToList());
            SelectedTopic = Topics[0];
            SelectedVerseCommand = new DelegateCommand<object>(ClickVerse, (object o) => true);
            NewTopicCommand = new DelegateCommand<object>(ClickNewTopic, (object o) => true);
            NewBeliefCommand = new DelegateCommand<object>(ClickNewBelief, (object o) => SelectedTopic != null);
            NewVerseCommand = new DelegateCommand<object>(ClickNewVerse, (object o) => SelectedBelief != null);
            EditTopicCommand = new DelegateCommand<object>(ClickEditTopic, (object o) => true);
            EditBeliefCommand = new DelegateCommand<object>(ClickEditBelief, (object o) => SelectedTopic != null);
            EditVerseCommand = new DelegateCommand<object>(ClickEditVerse, (object o) => SelectedBelief != null);
            DeleteTopicCommand = new DelegateCommand<object>(ClickDeleteTopic, (object o) => true);
            DeleteBeliefCommand = new DelegateCommand<object>(ClickDeleteBelief, (object o) => SelectedTopic != null);
            DeleteVerseCommand = new DelegateCommand<object>(ClickDeleteVerse, (object o) => SelectedBelief != null);
        }

        private void ClickVerse(object o)
        {
            MessageBox.Show($"Did you want to Edit {SelectedVerse.VerseText}?");
        }

        private void ClickNewTopic(object o)
        {
            MessageBox.Show("So you wanted to add a new topic...?");
        }

        private void ClickNewBelief(object o)
        {
            MessageBox.Show($"So you wanted to add a new belief to {SelectedTopic.Topic}?");
        }

        private void ClickNewVerse(object o)
        {
            MessageBox.Show($"So you wanted to add a new verse to \"{SelectedBelief.Belief.Substring(0,20)}...\"?");
        }

        private void ClickEditTopic(object o)
        {
            MessageBox.Show($"Edit topic: {SelectedTopic.Topic}");
        }

        private void ClickEditBelief(object o)
        {
            MessageBox.Show($"Edit belief: \"{SelectedBelief.Belief.Substring(0, 20)}...\"");
        }

        private void ClickEditVerse(object o)
        {
            MessageBox.Show($"Edit verse: \"{SelectedVerse.VerseText}\"");
        }

        private void ClickDeleteTopic(object o)
        {
            MessageBox.Show($"Delete topic: \"{SelectedTopic.Topic}\"");
        }

        private void ClickDeleteBelief(object o)
        {
            MessageBox.Show($"Delete belief: \"{SelectedBelief.Belief.Substring(0, 20)}...\"");
        }

        private void ClickDeleteVerse(object o)
        {
            MessageBox.Show($"Delete verse: \"{SelectedVerse.VerseText}\"");
        }

        #region INPC
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
