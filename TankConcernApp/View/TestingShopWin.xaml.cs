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
            LoadStages();
        }

        private void LoadStages()
        {
            var productStages = _dbContext.ProductStages
                .Include(s => s.ProductStageType)
                .Include(s => s.Workshop)
                .Where(s => s.ProductStageTypeId == 3 && s.Workshop.WorkshopTypeId == 2)
                .ToList();
            DGProductStages.ItemsSource = productStages;
        }

        private void Btn_AcceptStage_Click(object sender, RoutedEventArgs e)
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
                    LoadStages();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите стадию продукта!");
            }
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
