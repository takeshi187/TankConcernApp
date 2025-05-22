using System.Net;
using System.Windows;
using TankConcernApp.database;

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
            var employeePosts = dbContext.EmployeePosts.Select(p => new
            {
                p.EmployeePostId,
                p.EmployeePostName
            }).ToList(); 

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

        }

        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            AdminWin adminWin = new AdminWin();
            adminWin.Show();
            this.Close();
        }
    }
}
