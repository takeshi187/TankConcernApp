using System.Windows;
using TankConcernApp.database;
using TankConcernApp.Model;

namespace TankConcernApp.View
{
    public partial class BrigadeWorkshopAssignWin : Window
    {
        public readonly TankConcernDbContext _dbContext = new TankConcernDbContext();
        public BrigadeWorkshopAssignWin()
        {
            InitializeComponent();
            LoadBrigades();
            LoadWorkshops();
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

        private void LoadWorkshops()
        {
            try
            {
                var workshops = _dbContext.Workshops.ToList();
                ComboBox_Workshops.ItemsSource = workshops;
                ComboBox_Workshops.DisplayMemberPath = "WorkshopId";
                ComboBox_Workshops.SelectedValuePath = "WorkshopId";

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке цехов: {ex.Message}");
            }
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ComboBox_Workshops.SelectedValue is long workshopId &&
                    ComboBox_Brigades.SelectedValue is long brigadeId)
                {
                    var isWorkshopAssigned = _dbContext.BrigadeWorkshopAssignments
                        .Any(w => w.WorkshopId == workshopId);

                    if (isWorkshopAssigned)
                    {
                        MessageBox.Show("Цех уже занят другой бригадой!");
                        return;
                    }

                    var isBrigadeAssigned = _dbContext.BrigadeWorkshopAssignments
                        .Any(b => b.BrigadeId == brigadeId);

                    if (isBrigadeAssigned)
                    {
                        MessageBox.Show("Бригада уже назначена на другой цех!");
                        return;
                    }

                    var newAssignment = new BrigadeWorkshopAssignment
                    {
                        WorkshopId = workshopId,
                        BrigadeId = brigadeId,
                        AssignmentDate = DateOnly.FromDateTime(DateTime.Now),
                        EndDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(1))
                    };

                    _dbContext.BrigadeWorkshopAssignments.Add(newAssignment);
                    _dbContext.SaveChanges();

                    MessageBox.Show("Бригада успешно назначена на цех!");
                }
                else
                {
                    MessageBox.Show("Выберите цех и бригаду!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при назначении бригады на цех: {ex.Message}");
            }
        }

        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
