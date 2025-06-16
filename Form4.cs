using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp3;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp2
{
    public partial class Form4 : Form
    {
        private List<Sale_> sales = new List<Sale_>();
        private readonly demo3Entities1 db = new demo3Entities1();
        private FlowLayoutPanel flowLayoutPanel1;
        private List<Sale_> originalSales;

        public Form4(List<Sale_> sales)
        {
            InitializeComponent();
            this.originalSales = sales ?? new List<Sale_>();
            this.sales = sales ?? new List<Sale_>(); // Предотвращаем null
            InitializeFlowLayoutPanel();
            LoadSaleCards(); // Переименуем метод для ясности
        }

        private void InitializeFlowLayoutPanel()
        {
            flowLayoutPanel1 = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(10)
            };
            this.Controls.Add(flowLayoutPanel1);
        }

        private void LoadSaleCards()
        {
            flowLayoutPanel1.Controls.Clear();
            if (sales == null || !sales.Any())
            {
                MessageBox.Show("Список заказов пуст!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            MessageBox.Show($"Загружаю {sales.Count} заказов.", "Отладка"); // Проверка количества
            foreach (var sale in sales)
            {
                UserControl2 card = new UserControl2();
                card.SetData(
                    sale.IDSale,
                    sale.ProductArticle?.ToString() ?? "0",
                    sale.Partner?.ToString() ?? "0",
                    sale.CountProduct?.ToString() ?? "0",
                    sale.SaleDate
                );
                card.Tag = sale.IDSale;
                card.Margin = new Padding(10);
                flowLayoutPanel1.Controls.Add(card);
            }
        }
        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Partners_> partners = db.Partners_.ToList();
            Form2 form2 = new Form2(partners);
            form2.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                // Сброс к исходному списку
                sales = originalSales.ToList();
                LoadSaleCards();
                MessageBox.Show("Список продаж сброшен к исходному состоянию.", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (int.TryParse(textBox1.Text, out int partnerId))
            {
                var sortedSales = sales
                    .Where(s => s.Partner != null && s.Partner.Value == partnerId)
                    .OrderBy(s => s.SaleDate)
                    .ToList();
                if (!sortedSales.Any())
                {
                    MessageBox.Show($"Продажи для партнёра с ID {partnerId} не найдены! Попробуйте сбросить список!", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                sales = sortedSales;
                LoadSaleCards();
            }
            else
            {
                MessageBox.Show("Введите корректный номер партнёра (целое число)!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
