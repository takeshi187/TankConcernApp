using Microsoft.EntityFrameworkCore;
using System.Windows;
using TankConcernApp.database;
using TankConcernApp.Model;

namespace TankConcernApp.View
{
    public partial class ProdManagerWin : Window
    {
        private readonly TankConcernDbContext _dbContext = new TankConcernDbContext();
        private readonly long _managerId;
        private readonly long _brigadeId;
        private readonly long _workshopId;
        public ProdManagerWin(long managerId, long brigadeId, long workshopId)
        {
            InitializeComponent();
            _managerId = managerId;
            _brigadeId = brigadeId;
            _workshopId = workshopId;

            TxtBox_ManagerId.Text = _managerId.ToString();
            TxtBox_BrigadeId.Text = _brigadeId.ToString();
            TxtBox_WorkshopId.Text = _workshopId.ToString();

            LoadProductStages();
            LoadProductStageTypes();
        }

        private void LoadProductStages()
        {
            try
            {
                var productStages = _dbContext.ProductStages
                    .Include(s => s.ProductStageType)
                    .Include(s => s.Workshop)
                    .Where(s => s.WorkshopId == _workshopId && s.ProductStageTypeId != 4)
                    .ToList();
                DGProductStages.ItemsSource = productStages;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке стадий: {ex.Message}");
            }
        }

        private void LoadProductStageTypes()
        {
            try
            {
                var statuses = _dbContext.ProductStageTypes.ToList();
                ComboBox_ProductStageTypes.ItemsSource = statuses;
                ComboBox_ProductStageTypes.DisplayMemberPath = "ProductStageTypeName";
                ComboBox_ProductStageTypes.SelectedValuePath = "ProductStageTypeId";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке статусов стадий: {ex.Message}");
            }
        }

        private void Btn_EditStatus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DGProductStages.SelectedItem is ProductStage selectedStage &&
                ComboBox_ProductStageTypes.SelectedItem is ProductStageType selectedType)
                {
                    var stage = _dbContext.ProductStages
                        .Include(s => s.Workshop)
                        .FirstOrDefault(s => s.ProductStageId == selectedStage.ProductStageId);

                    if (stage == null)
                    {
                        MessageBox.Show("Стадия не найдена!");
                        return;
                    }

                    if (stage.ProductStageTypeId == selectedType.ProductStageTypeId)
                    {
                        MessageBox.Show("Этот статус уже установлен!");
                        return;
                    }

                    if (selectedType.ProductStageTypeId == 4 && stage.Workshop.WorkshopTypeId != 3)
                    {
                        MessageBox.Show("Статус 'Завершен' можно применить только в цеху тестирования!");
                        return;
                    }

                    stage.ProductStageTypeId = selectedType.ProductStageTypeId;
                    _dbContext.SaveChanges();

                    if (selectedType.ProductStageTypeId == 4)
                    {
                        var order = _dbContext.Orders.FirstOrDefault(o => o.OrderId == stage.OrderId);
                        if (order == null)
                        {
                            MessageBox.Show($"Заказ не найден!");
                            return;
                        }
                        order.OrderStatusId = 3;
                        _dbContext.SaveChanges();
                    }

                    var log = new ProductionLog
                    {
                        WorkshopId = _workshopId,
                        BrigadeId = _brigadeId,
                        OrderId = stage.OrderId,
                        ProductStageId = stage.ProductStageId,
                        ProductStageTypeId = selectedType.ProductStageTypeId,
                        Date = DateOnly.FromDateTime(DateTime.Now)
                    };

                    _dbContext.ProductionLogs.Add(log);
                    _dbContext.SaveChanges();

                    MessageBox.Show("Статус успешно изменен, отчет сформирован!");
                    LoadProductStages();
                }
                else
                {
                    MessageBox.Show("Выберите стадию и новый статус!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при изменении статуса стадии: {ex.Message}");
            }

        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
