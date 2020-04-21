using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CodeWanda.UI
{
    public sealed class AnalysisContext : INotifyPropertyChanged
    {
        private string _demo;

        public string Demo
        {
            get => _demo;
            set
            {
                if (value == _demo)
                {
                    return;
                }

                _demo = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}