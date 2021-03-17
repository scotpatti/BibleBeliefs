using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using BibleBeliefs.Repository;

namespace BibleBeliefs.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Private Fields
        private BindingList<TopicDTO> _Topics;
        private BindingList<BeliefDTO> _Beliefs;
        private BindingList<VerseDTO> _Verses;
        private TopicDTO _SelectedTopic;
        private BeliefDTO _SelectedBelief;
        private VerseDTO _SelectedVerse;
        #endregion

        #region Public Properties
        public BindingList<TopicDTO> Topics
        {
            get { return _Topics; }
            set
            {
                SetField<BindingList<TopicDTO>>(ref _Topics, value);
                SelectedTopic = Topics != null && Topics.Count > 0 ? Topics[0] : null;
            }
        }

        public BindingList<BeliefDTO> Beliefs
        {
            get { return _Beliefs; }
            set
            {
                SetField<BindingList<BeliefDTO>>(ref _Beliefs, value);
                SelectedBelief = Beliefs != null && Beliefs.Count > 0 ? Beliefs[0] : null;
            }
        }

        public BindingList<VerseDTO> Verses
        {
            get { return _Verses; }
            set
            {
                SetField<BindingList<VerseDTO>>(ref _Verses, value);
                SelectedVerse = Verses != null && Verses.Count > 0 ? Verses[0] : null;
            }
        }

        public TopicDTO SelectedTopic
        {
            get { return _SelectedTopic; }
            set
            {
                SetField<TopicDTO>(ref _SelectedTopic, value);
                if (_SelectedTopic != null)
                {
                    var beliefs = BibleBeliefsRepository.GetBeliefs(_SelectedTopic.Id);
                    Beliefs = beliefs != null ? beliefs : new BindingList<BeliefDTO>();
                }
            }
        }


        public BeliefDTO SelectedBelief
        {
            get { return _SelectedBelief; }
            set
            {

                SetField<BeliefDTO>(ref _SelectedBelief, value);
                if (_SelectedBelief != null)
                {
                    var verses = BibleBeliefsRepository.GetVerses(_SelectedBelief.Id);
                    Verses = verses != null ? verses : new BindingList<VerseDTO>();
                }
                else
                {
                    Verses = null;
                }
            }
        }

        public VerseDTO SelectedVerse
        {
            get { return _SelectedVerse; }
            set
            {
                SetField<VerseDTO>(ref _SelectedVerse, value);
            }
        }

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

        #region Constructor
        public MainViewModel()
        {
            Topics = BibleBeliefsRepository.GetTopics();
            SelectedTopic = Topics[0];
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
        #endregion

        #region NEW Functionality

        private void ClickNewTopic(object o)
        {
            EditTopicWindow topicDialog = new EditTopicWindow();
            topicDialog.TopicValue = "";
            topicDialog.ShowDialog();
            if (topicDialog.DialogResult == true)
            {
                TopicDTO topic = new TopicDTO()
                {
                    Topic = topicDialog.TopicValue
                };
                long newId = BibleBeliefsRepository.CreateTopic(topic);
                Topics = BibleBeliefsRepository.GetTopics();
                var t = Topics.Single(s => s.Id == newId);
                if (t != null) SelectedTopic = t;
            }
        }

        private void ClickNewBelief(object o)
        {
            EditBeliefWindow beliefDialog = new EditBeliefWindow();
            beliefDialog.BeliefValue = "";
            beliefDialog.ShowDialog();
            if (beliefDialog.DialogResult == true)
            {
                BeliefDTO belief = new BeliefDTO()
                {
                    Belief = beliefDialog.BeliefValue,
                    TopicId = SelectedTopic.Id
                };
                long newId = BibleBeliefsRepository.CreateBelief(belief);
                Beliefs = BibleBeliefsRepository.GetBeliefs(SelectedTopic.Id);
                var b = Beliefs.Single(s => s.Id == newId);
                if (b != null) SelectedBelief = b;
            }
        }

        private void ClickNewVerse(object o)
        {
            EditVerseWindow verseDialog = new EditVerseWindow();
            verseDialog.ShowDialog();
            if (verseDialog.DialogResult == true)
            {
                var context = (VerseViewModel)verseDialog.DataContext;
                VerseDTO verse = new VerseDTO()
                {
                    BeliefId = SelectedBelief.Id,
                    Book = context.BookList.IndexOf(context.SelectedBook),
                    Chapter = context.SelectedChapter,
                    VerseStart = context.SelectedVerseStart,
                    VerseEnd = context.SelectedVerseEnd
                };
                long newId = BibleBeliefsRepository.CreateVerse(verse);
                Verses = BibleBeliefsRepository.GetVerses(SelectedBelief.Id);
                var v = Verses.Single(s => s.Id == newId);
                if (v != null) SelectedVerse = v;
            }
        }

        #endregion

        #region EDIT Functionality

        private void ClickEditTopic(object o)
        {
            EditTopicWindow topicDialog = new EditTopicWindow();
            topicDialog.TopicValue = SelectedTopic.Topic;
            topicDialog.ShowDialog();
            if (topicDialog.DialogResult == true)
            {
                SelectedTopic.Topic = topicDialog.TopicValue;
                BibleBeliefsRepository.UpdateTopic(_SelectedTopic);
            }
        }

        private void ClickEditBelief(object o)
        {
            EditBeliefWindow beliefDialog = new EditBeliefWindow();
            beliefDialog.BeliefValue = SelectedBelief.Belief;
            beliefDialog.cbTopic.ItemsSource = Topics;
            beliefDialog.cbTopic.SelectedItem = SelectedTopic;
            beliefDialog.ShowDialog();
            if (beliefDialog.DialogResult == true)
            {
                SelectedBelief.TopicId = beliefDialog.TopicValue.Id;
                SelectedBelief.Belief = beliefDialog.BeliefValue;
                BibleBeliefsRepository.UpdateBelief(_SelectedBelief);
            }
            Beliefs = BibleBeliefsRepository.GetBeliefs(_SelectedTopic.Id);
        }

        private void ClickEditVerse(object o)
        {
            EditVerseWindow verseDialog = new EditVerseWindow();
            var context = (VerseViewModel)verseDialog.DataContext;
            context.SelectedBook = context.BookList[(int)(SelectedVerse.Book)];
            context.SelectedChapter = (int)SelectedVerse.Chapter; 
            context.SelectedVerseStart = (int)SelectedVerse.VerseStart; 
            context.SelectedVerseEnd = (int)SelectedVerse.VerseEnd; 
            verseDialog.ShowDialog();
            if (verseDialog.DialogResult == true)
            {
                SelectedVerse.Book = context.BookList.IndexOf(context.SelectedBook);
                SelectedVerse.Chapter = context.SelectedChapter; 
                SelectedVerse.VerseStart = context.SelectedVerseStart; 
                SelectedVerse.VerseEnd = context.SelectedVerseEnd;
                if (SelectedVerse.VerseEnd < SelectedVerse.VerseStart)
                    SelectedVerse.VerseEnd = SelectedVerse.VerseStart; //Verses must be in order.
                BibleBeliefsRepository.UpdateVerse(_SelectedVerse);
            }
            Verses = BibleBeliefsRepository.GetVerses(_SelectedVerse.BeliefId);
        }

        #endregion

        #region DELETE functionality

        private void ClickDeleteTopic(object o)
        {
            if (!BibleBeliefsRepository.DeleteTopic(_SelectedTopic))
            {
                MessageBox.Show("Topic has child beliefs. You must delete the beliefs before you can delete the topic!");
            }
            else
            {
                Topics = BibleBeliefsRepository.GetTopics();
            }
        }

        private void ClickDeleteBelief(object o)
        {
            if (!BibleBeliefsRepository.DeleteBelief(_SelectedBelief.Id))
            {
                MessageBox.Show("This belief has verses. You must delete all verses before you can delete the belief!");
            }
            else
            {
                Beliefs = BibleBeliefsRepository.GetBeliefs(_SelectedTopic.Id);
            }
        }

        private void ClickDeleteVerse(object o)
        {
            if (!BibleBeliefsRepository.DeleteVerse(_SelectedVerse.Id))
            {
                MessageBox.Show("Something when wrong! Run... run fast!");
            }
            else
            {
                Verses = BibleBeliefsRepository.GetVerses(_SelectedBelief.Id);
            }
        }

        #endregion

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
