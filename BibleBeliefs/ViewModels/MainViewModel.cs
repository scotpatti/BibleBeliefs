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
            MessageBox.Show($"So you wanted to add a new belief to {SelectedTopic.Topic}?");
        }

        private void ClickNewVerse(object o)
        {
            MessageBox.Show($"So you wanted to add a new verse to \"{SelectedBelief.Belief.Substring(0,20)}...\"?");
        }

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
            MessageBox.Show($"Edit belief: \"{SelectedBelief.Belief.Substring(0, 20)}...\"");
        }

        private void ClickEditVerse(object o)
        {
            MessageBox.Show($"Edit verse: \"{SelectedVerse.VerseText}\"");
        }

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
            MessageBox.Show($"Delete Belief: \"{SelectedBelief.Belief}\"");

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
