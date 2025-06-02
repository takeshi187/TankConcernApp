using Microsoft.EntityFrameworkCore;
using System.Windows;
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
            LoadBrigadeWorkshops();
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

        private void LoadBrigadeWorkshops()
        {
            try
            {
                var brigadeWorkshops = _dbContext.BrigadeWorkshopAssignments.ToList();
                DGBrigadeWorkshops.ItemsSource = brigadeWorkshops;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке бригад для цехов: {ex.Message}");
            }
        }

        private void Btn_AssignEmployee_Click(object sender, RoutedEventArgs e)
        {
            EmployeeBrigadeAssignWin employeeBrigadeAssignWin = new EmployeeBrigadeAssignWin();
            employeeBrigadeAssignWin.ShowDialog();
        }

        private void Btn_AssignBrigade_Click(object sender, RoutedEventArgs e)
        {
            BrigadeWorkshopAssignWin brigadeWorkshopAssignWin = new BrigadeWorkshopAssignWin();
            brigadeWorkshopAssignWin.ShowDialog();
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn_Update_Click(object sender, RoutedEventArgs e)
        {
            LoadEmployeeBrigades();
            LoadBrigadeWorkshops();
        }
    }
}
