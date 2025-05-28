using Microsoft.EntityFrameworkCore;
using System.Windows;
using TankConcernApp.database;

namespace TankConcernApp.View
{
    public partial class AdminWin : Window
    {
        private readonly TankConcernDbContext _dbContext = new TankConcernDbContext();
        public AdminWin()
        {
            InitializeComponent();
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            var employees = _dbContext.Employees
                .Include(e => e.EmployeePost)
                .Include(e => e.EmployeeStatus)
                .Select(e => new
                {
                    e.EmployeeId,
                    e.FirstName,
                    e.LastName,
                    EmployeePostName = e.EmployeePost.EmployeePostName,
                    EmployeeStatusName = e.EmployeeStatus.EmployeeStatusName
                }).ToList();
            DGEmployees.ItemsSource = employees;
        }

        private void Btn_AddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUserWin addUserWin = new AddUserWin();
            addUserWin.Show();
            this.Close();
        }

        private void Btn_EditStatus_Click(object sender, RoutedEventArgs e)
        {
            EditEmployeeStatusWin editEmployeeStatusWin = new EditEmployeeStatusWin();
            editEmployeeStatusWin.Show();
            this.Close();
        }

        private void Btn_CreateOrder_Click(object sender, RoutedEventArgs e)
        {
            CreateOrderWin createOrderWin = new CreateOrderWin();
            createOrderWin.Show();
            this.Close();
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
