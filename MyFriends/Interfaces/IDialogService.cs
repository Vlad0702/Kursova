using MyFriends.Data;

namespace MyFriends.ViewModels
{
    public interface IDialogService
    {
        void ShowMessage(string message);   // показ сообщения
        string FilePath { get; set; }   // путь к выбранному файлу
        bool OpenFileDialog();  // открытие файла
        bool SaveFileDialog();  // сохранение файла
        string OpenPasswordDialog(PasswordViewModel passwordViewModel);
        Friend OpenFriendListHandlerDialog(FriendListHandlerViewModel friendListHandlerViewModel);
    }
}