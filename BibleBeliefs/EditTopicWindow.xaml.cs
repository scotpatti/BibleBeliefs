using System.Windows;

namespace BibleBeliefs
{
    /// <summary>
    /// Interaction logic for EditTopicWindow.xaml
    /// </summary>
    public partial class EditTopicWindow : Window
    {
        public EditTopicWindow()
        {
            InitializeComponent();
        }

        public string TopicValue { 
            get
            {
                return tbTopic.Text;
            }
            set
            {
                tbTopic.Text = value;
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
