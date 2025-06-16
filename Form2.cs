using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp3;

namespace WindowsFormsApp2
{
    public partial class Form2 : Form
    {
        private List<Partners_> partners = new List<Partners_>();
        private readonly demo3Entities1 db = new demo3Entities1();
        private int editingCardID = 0;


        public Form2(List<Partners_> partners)
        {
            InitializeComponent();
            this.partners = partners;
            InitializeFlowLayoutPanel();
            LoadPartnerCards();
        }
        private void InitializeFlowLayoutPanel()
        {
            flowLayoutPanel1 = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,          // Занимает всю форму
                AutoScroll = true,              // Включаем прокрутку
                FlowDirection = FlowDirection.TopDown, // Карточки сверху вниз
                WrapContents = false,           // Отключаем перенос на следующую строку
                Padding = new Padding(10)       // Отступы внутри панели
            };
            this.Controls.Add(flowLayoutPanel1);
        }

        private void LoadPartnerCards()
        {
            flowLayoutPanel1.Controls.Clear();
            foreach (var partner in partners)
            {
                UserControl1 card = new UserControl1();
                card.SetData(
                    partner.IDTypePartner,
                    partner.NamePartner,
                    partner.Director,
                    partner.Phone,
                    partner.Rating?.ToString() ?? "0"
                );
                card.Tag = partner.IDPartner;
                card.Margin = new Padding(10);
                flowLayoutPanel1.Controls.Add(card);
            }
        }
        public void RefreshData()
        {
            partners = db.Partners_.ToList(); // Перезагружаем данные из базы
            LoadPartnerCards(); // Обновляем карточки
        }
        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Sale_> sales = db.Sale_.ToList();
            Form4 form4 = new Form4(sales);
            form4.Show();
            this.Hide();
        }
    }
}