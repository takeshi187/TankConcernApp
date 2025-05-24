using System.Windows;
using TankConcernApp.database;
using TankConcernApp.View;

namespace TankConcernApp
{
    public partial class LogInWin : Window
    {
        private readonly TankConcernDbContext dbContext = new TankConcernDbContext();
        public LogInWin()
        {
            InitializeComponent();
        }

        private void Btn_Entry_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = TxtBox_Login.Text.Trim();
                string password = PassBox_Password.Password.Trim();

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Введите логин и пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var user = dbContext.Users.FirstOrDefault(u => u.Login == username && u.Password == password);
                if (user != null)
                {
                    var roleId = user.RoleId;
                    var employee = dbContext.Employees.FirstOrDefault(e => e.EmployeeId == user.EmployeeId);
                    if (employee.EmployeeStatusId == 3)
                        MessageBox.Show("Вы уволены и не можете авторизоваться в системе!");
                    else
                    {
                        user.LastLogin = DateOnly.FromDateTime(DateTime.Now);
                        dbContext.SaveChanges();
                        switch (roleId)
                        {
                            case 1:
                                MessageBox.Show($"Добро пожаловать! Администратор: {employee.LastName}");
                                AdminWin adminWin = new AdminWin();
                                adminWin.Show();
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                            case 4:
                                MessageBox.Show($"Добро пожаловать! Менеджер по производству: {employee.LastName}");
                                ProdManagerWin prodManagerWin = new ProdManagerWin();
                                prodManagerWin.Show();
                                break;
                            case 5:
                                break;
                            default:
                                MessageBox.Show("Такой роли не существует!");
                                break;
                        }
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при авторизации: ", ex.Message);
            }
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn_Clean_Click(object sender, RoutedEventArgs e)
        {
            TxtBox_Login.Clear();
            PassBox_Password.Clear();
        }
    }
}
