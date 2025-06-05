using Microsoft.EntityFrameworkCore;
using System.Windows;
using TankConcernApp.database;

namespace TankConcernApp.View
{
    public partial class StorageShopWin : Window
    {
        private readonly TankConcernDbContext _dbContext = new TankConcernDbContext();
        private readonly long _WorkshopId;
        public StorageShopWin(long workshopId)
        {
            InitializeComponent();
            _WorkshopId = workshopId;
            TxtBox_WorkshoId.Text = _WorkshopId.ToString();
            LoadInventory();
        }

        private void LoadInventory()
        {
            try
            {
                var inventory = _dbContext.PartsInventories
                    .Include(p => p.PartType)
                    .Include(p => p.TankPart)
                    .Select(i => new
                    {
                        i.InventoryId,
                        i.TankPart.TankPartName,
                        i.Count,
                        i.LastUpdate,
                        i.PartType.PartTypeName
                    }).ToList();
                DGPartsInventory.ItemsSource = inventory;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке инвентаря: {ex.Message}");
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
