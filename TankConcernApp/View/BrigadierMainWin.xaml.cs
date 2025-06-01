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
    public partial class BrigadierMainWin : Window
    {
        private readonly TankConcernDbContext _dbContext = new TankConcernDbContext();
        public BrigadierMainWin()
        {
            InitializeComponent();
            LoadEmployeeBrigades();
        }

        private void LoadEmployeeBrigades()
        {
            try
            {
                var employeeBrigades = _dbContext.EmployeeBrigades
                    .Include(eb => eb.Employee)
                    .Select(eb => new
                    {
                        eb.Ebid,
                        FullName = eb.Employee.EmployeeId.ToString() + " " + eb.Employee.FirstName.ToString() + " " + eb.Employee.LastName.ToString(),
                        eb.BrigadeId,
                        eb.LastUpdate
                    }).ToList();
                DGEmployeeBrigades.ItemsSource = employeeBrigades;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке бригад и сотрудников: {ex.Message}");
            }
        }

        private void Btn_AssignEmployee_Click(object sender, RoutedEventArgs e)
        {
            EmployeeBrigadeAssignWin employeeBrigadeAssignWin = new EmployeeBrigadeAssignWin();
            employeeBrigadeAssignWin.ShowDialog();
        }

        private void Btn_AssignBrigade_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn_Update_Click(object sender, RoutedEventArgs e)
        {
            LoadEmployeeBrigades();
        }
    }
}
