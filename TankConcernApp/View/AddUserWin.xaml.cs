using System.Windows;
using TankConcernApp.database;
using TankConcernApp.Model;

namespace TankConcernApp.View
{
    public partial class AddUserWin : Window
    {
        private readonly TankConcernDbContext dbContext = new TankConcernDbContext();
        public AddUserWin()
        {
            InitializeComponent();
            LoadEmployees();
            LoadRoles();
        }

        private void LoadEmployees()
        {
            var employees = dbContext.Employees.Join(dbContext.EmployeePosts,
                e => e.EmployeePostId,
                p => p.EmployeePostId,
                (e, p) => new
                {
                    e.EmployeeId,
                    FullName = "id " + e.EmployeeId.ToString() + " " + e.FirstName + " " + e.LastName + " - " + p.EmployeePostName
                }).ToList();
            ComboBox_EmployeeId.ItemsSource = employees;
            ComboBox_EmployeeId.DisplayMemberPath = "FullName";
            ComboBox_EmployeeId.SelectedValuePath = "EmployeeId";
        }

        private void LoadRoles()
        {
            var roles = dbContext.UserRoles.Select(r => new
            {
                r.RoleId,
                r.RoleName
            }).ToList();
            ComboBox_RoleId.ItemsSource = roles;
            ComboBox_RoleId.DisplayMemberPath = "RoleName";
            ComboBox_RoleId.SelectedValuePath = "RoleId";
        }

        private void Btn_AddUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ComboBox_EmployeeId.SelectedValue == null || ComboBox_RoleId.SelectedValue == null)
                {
                    MessageBox.Show("Все поля должны быть заполнены!");
                    return;
                }

                long employeeId = (long)ComboBox_EmployeeId.SelectedValue;
                long roleId = (long)ComboBox_RoleId.SelectedValue;
                string login = TxtBox_Login.Text.Trim();
                string password = PassBox_Password.Password;
                string email = TxtBox_Email.Text.Trim();
                var createdDate = DateOnly.FromDateTime(DateTime.Now);

                if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
                {
                    MessageBox.Show("Все поля должны быть заполнены!");
                    return;
                }

                bool userExist = dbContext.Users.Any(u => u.EmployeeId == employeeId);
                if (userExist)
                {
                    MessageBox.Show("Сотрудник с выбранным id уже зарегистрирован!");
                    return;
                }

                var user = new User
                {
                    EmployeeId = employeeId,
                    RoleId = roleId,
                    Login = login,
                    Password = password,
                    Email = email,
                    CreatedDate = createdDate
                };

                dbContext.Users.Add(user);
                dbContext.SaveChanges();

                MessageBox.Show("Пользователь успешно добавлен!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении пользователей: {ex.Message}");
            }
        }

        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            AdminWin adminWin = new AdminWin();
            adminWin.Show();
            this.Close();
        }
    }
}
