using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ModuleCustomer.Models.Response
{
    public class BaseResponse : INotifyPropertyChanged
    {
        private bool success;
        private List<ErrorResponse> errors = new();

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        #endregion //INotifyPropertyChanged
        public BaseResponse()
        {
            success = true;
            Errors = new List<ErrorResponse>();
        }

        public bool Success
        {
            get => success;
            set
            {
                success = value;
                OnPropertyChanged();
            }
        }
        public List<ErrorResponse> Errors
        {
            get => errors;
            set
            {
                errors = value;
                OnPropertyChanged();
            }
        }
    }
}