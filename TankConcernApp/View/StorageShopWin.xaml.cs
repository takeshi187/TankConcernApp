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
            catch(Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке инвентаря: {ex.Message}");
            }
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
