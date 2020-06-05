using MyFriends.Data;
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
    public class FriendListHandlerViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public string Name { get; set; }
        int Id { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public string Address { get; set; }
        public int NumberCode { get; set; }
        public int Number { get; set; }
        bool isOk = false;
        string error = String.Empty;
        public EventHandler CloseHandler;
        public string NumberCodeStr { get; set; }
        public string NumberStr { get; set; }

    public FriendListHandlerViewModel() {
            NumberCodeStr = String.Empty;
            NumberStr = String.Empty;
        }

        public FriendListHandlerViewModel(Friend friend)
        {
            NumberCodeStr = String.Empty;
            NumberStr = String.Empty;
            Id = friend.Id;
            Name = friend.Name;
            Surname = friend.Surname;
            Patronymic = friend.Patronymic;
            Year = friend.Year;
            Month = friend.Month;
            Day = friend.Day;
            Address = friend.Address;
            NumberCode = friend.NumberCode;
            Number = friend.Number;
        }

        public Friend GetFriend()
        {
            return new Friend(Id, Name, Surname, Patronymic, Year, Month, Day, Address, NumberCode, Number);
        }

        private RelayCommand acceptClick;
        public RelayCommand AcceptClick
        {
            get
            {
                return acceptClick ??
                    (acceptClick = new RelayCommand(action =>
                    {
                        if (string.IsNullOrEmpty(Name?.Trim()) || string.IsNullOrEmpty(Surname?.Trim()) || string.IsNullOrEmpty(Patronymic?.Trim()) || string.IsNullOrEmpty(Address?.Trim()))
                        {
                            MessageBox.Show("Все поля обязательны, кроме номера тф. Не ленись");
                            return;
                        }

                    if (Year == 0 || Month == 0 || Day == 0)
                        {
                            MessageBox.Show("Введи дату. Ещё чуть-чуть");
                            return;
                        }

                        try
                        {
                            new DateTime(Year, Month, Day);
                        }  catch (Exception ex)
                        {
                            MessageBox.Show("Такой даты нет! Исправь");
                            return;
                        }
                        if (!string.IsNullOrEmpty(error)) return;
                        var handler = CloseHandler;
                        if (handler != null) handler.Invoke(this, EventArgs.Empty);

                    }, canExecute => true));
            }
        }
        
        public string this[string columnName]
        {
            
            get
            {
                error = String.Empty;
                switch (columnName)
                {
                    case "Year":
                        if ((Year < 1900) && (Year != 0))
                        {
                            error = "Ваш друг - динозавр? Думаю, нет";
                        }
                        if ((Year > DateTime.Now.Year))
                        {
                            error = "Наконец-то изобрели машину времени!";
                        }
                        break;
                    case "Day":
                        if (Day < 0)
                        {
                            error = "Хотелось бы там побывать...";
                        }
                        if (Day > 31)
                        {
                            error = "Бесконечный месяц? Надеюсь, на улице лето";
                        }
                        break;
                    case "Month":
                        if (Month > 12)
                        {
                            error = "Да, и мне не 17 лет. Мне 15 лет и 30 месяцев... Кого мы обманываем...";
                        }
                        if (Month < 0)
                        {
                            error = "Это тот самый секретный -1 месяц года?";
                        }
                        break;
                    case "NumberCodeStr":
                        if (String.IsNullOrEmpty(NumberCodeStr)) break;
                        if (NumberCodeStr.Trim().Length != 3) 
                        {
                            error = "Код должен состоять из 3 цифр";
                        }
                        try
                        {
                            NumberCode = Convert.ToInt32(NumberCodeStr);
                        }
                        catch
                        {
                            error = "Только цифры";
                        }
                        break;
                    case "NumberStr":
                        if (String.IsNullOrEmpty(NumberStr)) break;
                        if (NumberStr.Trim().Length != 7)
                        {
                            error = "Номер должен состоять из 7 цифр";
                        }
                        try
                        {
                            Number = Convert.ToInt32(NumberStr);
                        } catch
                        {
                            error = "Только цифры";
                        }
                        break;
                }
                return error;
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    

    public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
