using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp2;

namespace WindowsFormsApp3
{
    public partial class UserControl1 : UserControl
    {
        private Form3 editForm;
        private Partners_ partnerData;

        public UserControl1()
        {
            InitializeComponent();
            this.Width = 250; // Фиксированная ширина карточки
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.BorderStyle = BorderStyle.None; // Убираем стандартную рамку
            this.BackColor = Color.WhiteSmoke;
        }

        public void SetData(int PARTNER_TYPE, string PARTNER_NAME, string DIRECTOR, string DIRECTOR_PHONE, string RATING)
        {
            label1.Text = PARTNER_TYPE.ToString();
            label2.Text = PARTNER_NAME;
            label3.Text = DIRECTOR;
            label4.Text = DIRECTOR_PHONE;
            label5.Text = RATING;

        }

        private void panel1_Click(object sender, EventArgs e)
        {
            int partnerId = (int)this.Tag; // ID партнёра из Tag
            OpenEditForm(partnerId);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int partnerId = (int)this.Tag; // ID партнёра из Tag
            OpenEditForm(partnerId);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Определяем размеры внешнего и внутреннего прямоугольников
            Rectangle outerRect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            Rectangle innerRect = new Rectangle(2, 2, this.Width - 5, this.Height - 5);

            // Рисуем внешнюю рамку
            ControlPaint.DrawBorder(e.Graphics, outerRect, Color.Black, ButtonBorderStyle.Solid);

            // Рисуем внутреннюю рамку
            ControlPaint.DrawBorder(e.Graphics, innerRect, Color.Black, ButtonBorderStyle.Solid);
        }

        private void OpenEditForm(int partnerId)
        {
            if (editForm == null || editForm.IsDisposed)
            {
                editForm = new Form3(partnerId);
                editForm.FormClosed += (s, args) => editForm = null;
                editForm.LoadPartnerData(); // Автоматическая загрузка данных при открытии
            }
            editForm.ShowDialog();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}