using System.Windows;

namespace TankConcernApp.View
{
    public partial class AdminWin : Window
    {
        public AdminWin()
        {
            InitializeComponent();
        }

        private void Btn_AddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUserWin addUserWin = new AddUserWin();
            addUserWin.Show();
            this.Close();
        }

        private void Btn_EditStatus_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_CreateOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
