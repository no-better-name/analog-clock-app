using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AnalogClockApp.ViewModels
{
    internal partial class ClockViewModel : INotifyPropertyChanged
    {
        private DateTime _dateTime = DateTime.MinValue;
        public DateTime CurrentDateTime
        {
            get { return _dateTime; }
            set
            {
                if (_dateTime != value)
                {
                    _dateTime = value;
                    OnPropertyChanged(nameof(CurrentDateTime));
                }
            }
        }

        private readonly IDispatcherTimer? _timer;
        
        private void UpdateTime(object? sender, EventArgs e)
        {
            if (sender is not null)
            {
                CurrentDateTime = DateTime.Now;
            }
        }

        public ClockViewModel()
        {
            _timer = Application.Current?.Dispatcher.CreateTimer();
            if (_timer is not null)
            {
                _timer.Interval = TimeSpan.FromMilliseconds(1);
                _timer.Tick += new EventHandler(UpdateTime);
                _timer.Start();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName]string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
