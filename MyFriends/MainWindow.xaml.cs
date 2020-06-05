using MyFriends.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyFriends
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Data.Friend MyFriend { get; set; }
        public ObservableCollection<Data.Friend> FriendsList { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModels.FriendsViewModel(new DefaultDialogService(), new JsonFileService());
        }
    }


    public class Phone
    {
        public string Title { get; set; }
        public string Company { get; set; }
        public int Price { get; set; }
    }
}
