using Microsoft.Win32;
using MyFriends.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MyFriends.Data;

namespace MyFriends.ViewModels
{
    public class DefaultDialogService : IDialogService
    {
        public string FilePath { get; set; }
  

        public bool OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                return true;
            }
            return false;
        }

        public bool SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;
                return true;
            }
            return false;
        }

        public string OpenPasswordDialog(PasswordViewModel passwordViewModel)
        {
            PasswordWindow passwordWindow = new PasswordWindow(passwordViewModel);
            return passwordWindow.ShowDialog() == true ? passwordViewModel.EnteredPassword : null;
        }

        public Friend OpenFriendListHandlerDialog(FriendListHandlerViewModel friendListHandlerViewModel)
        {
            FriendsListHandlerWindow friendsListHandlerWindow = new FriendsListHandlerWindow(friendListHandlerViewModel);
            return friendsListHandlerWindow.ShowDialog() == true ? friendListHandlerViewModel.GetFriend() : null;
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
