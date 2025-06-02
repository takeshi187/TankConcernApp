using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using TankConcernApp.database;

namespace TankConcernApp.View
{
    public partial class EmployeeWin : Window
    {
        private readonly TankConcernDbContext _dbContext = new TankConcernDbContext();
        private readonly long _employeeId;
        public EmployeeWin(long employeeId)
        {
            InitializeComponent();
            _employeeId = employeeId;
            TxtBox_EmployeeId.Text = employeeId.ToString();
            LoadEmployeeInfo();
            LoadBrigadeEmployees();
        }

        private void LoadEmployeeInfo()
        {
            try
            {
                var employees = _dbContext.Employees
                    .Include(e => e.EmployeePost)
                    .Include(e => e.EmployeeStatus)
                    .Where(e => e.EmployeeId == _employeeId)
                    .ToList();
                DGEmployeeInfo.ItemsSource = employees;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных сотрудника: {ex.Message}");
            }
        }

        private void LoadBrigadeEmployees()
        {
            try
            {
                var brigadeId = _dbContext.EmployeeBrigades
                    .Where(eb => eb.EmployeeId == _employeeId)
                    .Select(eb => eb.BrigadeId)
                    .FirstOrDefault();
                
                if(brigadeId == null)
                {
                    MessageBox.Show("Сотрудник не состоит в бригаде!");
                    return;
                }

                var employeesBrigades = _dbContext.EmployeeBrigades
                    .Include(eb => eb.Employee)
                    .Where(eb => eb.BrigadeId == brigadeId)
                    .Select(eb => new
                    {
                        eb.Ebid,
                        FullName = eb.Employee.EmployeeId.ToString() + " " + eb.Employee.FirstName.ToString() + " " + eb.Employee.LastName.ToString(),
                        eb.BrigadeId,
                        eb.LastUpdate
                    }).ToList();
                DGBrigadeEmployees.ItemsSource = employeesBrigades;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке бригад и сотрудников: {ex.Message}");
            }
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
