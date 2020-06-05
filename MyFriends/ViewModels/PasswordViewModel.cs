using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyFriends.ViewModels
{
    public class PasswordViewModel : INotifyPropertyChanged
    {
        public EventHandler CloseHandler;

        public string EnteredPassword { get; set; }

        public PasswordViewModel() { }

        private RelayCommand acceptClick;
        public RelayCommand AcceptClick
        {
            get
            {
                return acceptClick ??
                    (acceptClick = new RelayCommand(action =>
                    {
                        var handler = CloseHandler;
                        if (handler != null) handler.Invoke(this, EventArgs.Empty);
                    }, canExecute => true));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
