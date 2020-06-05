using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyFriends.Data;


using System.Windows;
using System.Windows.Data;
using System.ComponentModel;
using MyFriends.Interfaces;
using System.Runtime.CompilerServices;
using MyFriends.Models;
using System.IO;

namespace MyFriends.ViewModels
{
    public class FriendsViewModel : INotifyPropertyChanged
    {
        public Friend MyFriend { get; set; }
        public ObservableCollection<Friend> FriendsList { get; set; }
        public bool IsSingIn { get; set; }
        public string[] Month { get; set; }
        public string[] Operators { get; set; }
        List<PhoneOperator> PhoneOperators;
        private readonly PasswordModel password = new PasswordModel("123");
        private Friend selectedFriend;
        IFileService fileService;
        IDialogService dialogService;
        Connection DbConnection = new Connection();



        public FriendsViewModel(IDialogService dialogService, IFileService fileService) {
            Month = new string[] { "Все", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"};
            PhoneOperators = new List<PhoneOperator>()
            {
                new PhoneOperator("Киевстар", new List<int> {039, 067, 068, 096, 097, 098, 455}),
                new PhoneOperator("Лайф", new List<int> {063, 093}),
                new PhoneOperator("МТС", new List<int> {050, 066, 095, 099})
            };
            Operators = new string[PhoneOperators.Count + 1];
            Operators[0] = "Все";
            int i = 1;
            foreach (var item in PhoneOperators)
            {
                Operators[i] = item.Name;
                i++;
            }
            this.dialogService = dialogService;
            this.fileService = fileService;
            FriendsList = DbConnection.SelectAll();
            IsSingIn = false;
        }

        private RelayCommand openCommand;
        public RelayCommand OpenCommand
        {
            get
            {
                return openCommand ??
                  (openCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (dialogService.OpenFileDialog() == true)
                          {
                              var friends = fileService.Open(dialogService.FilePath);
                              FriendsList.Clear();
                              foreach (var p in friends)
                                  FriendsList.Add(p);
                              dialogService.ShowMessage("Файл открыт");
                          }
                      }
                      catch (Exception ex)
                      {
                          dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (dialogService.SaveFileDialog() == true)
                          {
                              fileService.Save(dialogService.FilePath, FriendsList.ToList());
                              dialogService.ShowMessage("Файл сохранен");
                          }
                      }
                      catch (Exception ex)
                      {
                          dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }

        private RelayCommand addFriendCommand;
        public RelayCommand AddFriendCommand
        {
            get
            {
                return addFriendCommand ??
                  (addFriendCommand = new RelayCommand(obj =>
                  {
                      if (!IsSingIn)
                      {
                          MessageBox.Show("Необходимо ввести пароль");
                          ShowPasswordWindow();
                          if (!IsSingIn) return;
                      }
                      FriendListHandlerViewModel friendListHandlerViewModel = new FriendListHandlerViewModel();
                      Friend newFriend = dialogService.OpenFriendListHandlerDialog(friendListHandlerViewModel);
                      if (newFriend != null)
                      {
                          DbConnection.InsertOne(newFriend);
                          FriendsList.Add(newFriend);
                      }
                  }));
            }
        }

        private RelayCommand deleteFriendCommand;
        public RelayCommand DeleteFriendCommand
        {
            get
            {
                return deleteFriendCommand ??
                  (deleteFriendCommand = new RelayCommand(obj =>
                  {
                      if (!IsSingIn)
                      {
                          MessageBox.Show("Необходимо ввести пароль");
                          ShowPasswordWindow();
                          if (!IsSingIn) return;
                      }
                      var confirm = MessageBox.Show("Поссорились? Ещё не всё потерянно. Удалить?", "Подумай", MessageBoxButton.YesNo);
                      if (confirm != MessageBoxResult.Yes) return;
                    
                      Friend friend = obj as Friend;
                      if (friend == null)
                      {
                          MessageBox.Show("Выберите друга из списка");
                          return;
                      }
                      DbConnection.DeleteById(friend.Id);
                      FriendsList.Remove(friend);

                      
                  }));
            }
        }

        private RelayCommand editFriendCommand;
        public RelayCommand EditFriendCommand
        {
            get
            {
                return editFriendCommand ??
                  (editFriendCommand = new RelayCommand(obj =>
                  {
                      if (!IsSingIn)
                      {
                          MessageBox.Show("Необходимо ввести пароль");
                          ShowPasswordWindow();
                          if (!IsSingIn) return;
                      }
                      Friend friend = obj as Friend;
                      if (friend == null)
                      {
                          MessageBox.Show("Выберите друга из списка");
                          return;
                      }
                      FriendListHandlerViewModel friendListHandlerViewModel = new FriendListHandlerViewModel(friend);
                      Friend newFriend = dialogService.OpenFriendListHandlerDialog(friendListHandlerViewModel);
                      if (newFriend == null) return;
                      DbConnection.UpdateOne(newFriend);
                      FriendsList[FriendsList.IndexOf(friend)] = newFriend;
                  }));
            }
        }


        private bool isWithNumberOnly;
        public bool IsWithNumberOnly
        {
            get { return isWithNumberOnly; }
            set
            {
                if (isWithNumberOnly == value) return;

                isWithNumberOnly = value;

                OnPropertyChanged("IsWithNumberOnly");
                ShowFriendsWithNumber();
                ApplyFilters();
            }
        }

        private string selectedMonth;
        public string SelectedMonth
        {
            get { return selectedMonth; }
            set
            {
                if (selectedMonth == value) return;

                selectedMonth = value;
                OnPropertyChanged("selectedMonth");
                ApplyFilters();
            }
        }


        private string selectedOperator;
        public string SelectedOperator
        {
            get { return selectedOperator; }

            set
            {
                if (selectedOperator == value) return;

                selectedOperator = value;
                OnPropertyChanged("selectedOperator");
                ApplyFilters();
            }
        }



        private void ShowFriendsWithNumber()
        {
            if (isWithNumberOnly)
            {
                var tempList = CopyCollection(FriendsList);
                FriendsList.Clear();
                foreach (var friend in tempList)
                {
                    if (friend.Number != 0)
                    {
                        FriendsList.Add(friend);
                    }
                }
            }
           
        }

        private void ShowFriendSelectedMonth()
        {
            if (selectedMonth == "Все" || selectedMonth == null)
            {
                return;
            }
            int month;
            try
            {
                month = Convert.ToInt32(selectedMonth);
            } catch
            {
                return;
            }
            var tempList = CopyCollection(FriendsList);
            FriendsList.Clear();
            foreach (var friend in tempList)
            {
                if (friend.Month == month)
                {
                    FriendsList.Add(friend);
                }
            }
        }

        private void ShowFriendOperatorWith()
        {
            
            if (selectedOperator == "Все" || selectedOperator == null)
            {
                return;
            }

            PhoneOperator phoneOperator = null ;
            foreach (var item in PhoneOperators)
            {
                if (item.Name == selectedOperator)
                {
                    phoneOperator = item;
                }
            }
            if (phoneOperator == null) return;
            var tempList = CopyCollection(FriendsList);
            FriendsList.Clear();
            foreach (var friend in tempList)
            {
                if (phoneOperator.IsCodeContains(friend.NumberCode))
                {
                    FriendsList.Add(friend);
                }
            }
        }

        private void ApplyFilters()
        {
            FriendsList.Clear();
            FriendsList = DbConnection.SelectAll();
            ShowFriendsWithNumber();
            ShowFriendSelectedMonth();
            ShowFriendOperatorWith();
        }

        private ObservableCollection<Friend> CopyCollection(ObservableCollection<Friend> collection)
        {
            ObservableCollection<Friend> tempList = new ObservableCollection<Friend>();
            foreach (var item in collection)
            {
                tempList.Add(item);
            }

            return tempList;
        }


        private void ShowPasswordWindow()
        {
            PasswordViewModel passwordViewModel = new PasswordViewModel();
            string enteredPassword = dialogService.OpenPasswordDialog(passwordViewModel);
            if (password.CheckPassword(enteredPassword))
            {
                IsSingIn = true;
            } else if (enteredPassword == null)
            {
            } else
            {
                MessageBox.Show("Неправельный пароль!");
                ShowPasswordWindow();
            }
            
        }

        public Friend SelectedFriend
        {
            get { return selectedFriend; }
            set
            {
                selectedFriend = value;
                OnPropertyChanged("SelectedFriend");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }

}

