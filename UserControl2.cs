using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class UserControl2 : UserControl
    {
        public UserControl2()
        {
            InitializeComponent();
            this.Width = 250; // Фиксированная ширина карточки
            this.MinimumSize = new Size(250, 150); // Минимальная высота для всех данных
            this.AutoSize = false; // Отключаем автосайз, чтобы контролировать размер
            this.BorderStyle = BorderStyle.None;
            this.BackColor = Color.WhiteSmoke;

            // Настраиваем Label'ы
            label1.AutoSize = true;
            label2.AutoSize = true;
            label3.AutoSize = true;
            label4.AutoSize = true;
            label5.AutoSize = true;

        }
        public void SetData(int idSale, string article, string partner, string count, string date)
        {
            label1.Text = idSale.ToString();
            label2.Text = article;
            label3.Text = partner;
            label4.Text = count;
            label5.Text = date;
        }

        private void UserControl2_Click(object sender, EventArgs e)
        {

        }

        private void UserControl2_Load(object sender, EventArgs e)
        {

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
    }
}
