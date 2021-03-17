using BibleBeliefs.Repository;
using System.Windows;

namespace BibleBeliefs
{
    /// <summary>
    /// Interaction logic for EditBeliefWindow.xaml
    /// </summary>
    public partial class EditBeliefWindow : Window
    {
        public EditBeliefWindow()
        {
            InitializeComponent();
        }

        public string BeliefValue
        {
            get
            {
                return tbBeliefText.Text;
            }
            set
            {
                tbBeliefText.Text = value;
            }
        }

        public TopicDTO TopicValue
        {
            get
            {
                return (TopicDTO)cbTopic.SelectedItem;
            }
            set
            {
                cbTopic.SelectedItem = value;
            }
        }

        private void btOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
