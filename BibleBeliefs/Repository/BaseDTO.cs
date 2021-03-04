using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BibleBeliefs.Repository
{
    public class BaseDTO : INotifyPropertyChanged
    {

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
