using System.Windows;
using TankConcernApp.database;
using TankConcernApp.Model;

namespace TankConcernApp.View
{
    public partial class EmployeeBrigadeAssignWin : Window
    {
        public readonly TankConcernDbContext _dbContext = new TankConcernDbContext();
        public EmployeeBrigadeAssignWin()
        {
            InitializeComponent();
            LoadEmployees();
            LoadBrigades();
        }

        private void LoadEmployees()
        {
            try
            {
                var employees = _dbContext.Employees
                    .Select(e => new
                    {
                        e.EmployeeId,
                        FullName = $"Id: {e.EmployeeId} - {e.FirstName} {e.LastName}"
                    }).ToList();
                ComboBox_Employees.ItemsSource = employees;
                ComboBox_Employees.DisplayMemberPath = "FullName";
                ComboBox_Employees.SelectedValuePath = "EmployeeId";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке сотрудников: {ex.Message}");
            }
        }

        private void LoadBrigades()
        {
            try
            {
                var brigades = _dbContext.Brigades.ToList();
                ComboBox_Brigades.ItemsSource = brigades;
                ComboBox_Brigades.DisplayMemberPath = "BrigadeId";
                ComboBox_Brigades.SelectedValuePath = "BrigadeId";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке бригад: {ex.Message}");
            }
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ComboBox_Employees.SelectedValue is long employeeId &&
                    ComboBox_Brigades.SelectedValue is long brigadeId)
                {
                    var existingAssignment = _dbContext.EmployeeBrigades
                        .FirstOrDefault(eb => eb.EmployeeId == employeeId);

                    if (existingAssignment != null)
                    {
                        existingAssignment.BrigadeId = brigadeId;
                        existingAssignment.LastUpdate = DateOnly.FromDateTime(DateTime.Now);
                    }
                    else
                    {
                        var newAssignment = new EmployeeBrigade
                        {
                            EmployeeId = employeeId,
                            BrigadeId = brigadeId,
                            LastUpdate = DateOnly.FromDateTime(DateTime.Now)
                        };

                        _dbContext.EmployeeBrigades.Add(newAssignment);
                    }

                    _dbContext.SaveChanges();
                    MessageBox.Show("Сотрудник успешно назначен в бригаду!");
                }
                else
                {
                    MessageBox.Show("Выберите сотрудника и бригаду!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при назначении сотрудника в бригаду: {ex.Message}");
            }
        }

        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

