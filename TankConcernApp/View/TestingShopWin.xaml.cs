using Microsoft.EntityFrameworkCore;
using System.Windows;
using TankConcernApp.database;
using TankConcernApp.Model;

namespace TankConcernApp.View
{
    public partial class TestingShopWin : Window
    {
        private readonly TankConcernDbContext _dbContext = new TankConcernDbContext();
        private readonly long _WorkshopId;
        public TestingShopWin(long workshopId)
        {
            InitializeComponent();
            _WorkshopId = workshopId;
            TxtBox_WorkshoId.Text = workshopId.ToString();
            LoadProductStages();
        }

        private void LoadProductStages()
        {
            try
            {
                var productStages = _dbContext.ProductStages
                    .Include(s => s.ProductStageType)
                    .Include(s => s.Workshop)
                    .Where(s => s.ProductStageTypeId == 3 && s.Workshop.WorkshopTypeId == 2)
                    .ToList();
                DGProductStages.ItemsSource = productStages;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке стадий: {ex.Message}");
            }
        }

        private void Btn_AcceptStage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DGProductStages.SelectedItem is ProductStage selectedProductStage)
                {
                    var productStage = _dbContext.ProductStages.FirstOrDefault(p => p.ProductStageId == selectedProductStage.ProductStageId);
                    if (productStage != null)
                    {
                        productStage.ProductStageTypeId = 1;
                        productStage.WorkshopId = _WorkshopId;
                        _dbContext.SaveChanges();
                        MessageBox.Show("Стадия продукта успешно принята!");
                        LoadProductStages();
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите стадию продукта!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при принятии стадии: {ex.Message}");
            }
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            LogInWin logInWin = new LogInWin();
            logInWin.Show();
            this.Close();
        }
    }
}
