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
    public partial class Form3 : Form
    {
        private readonly demo3Entities1 db = new demo3Entities1(); // Контекст базы данных
        private int partnerId; // ID редактируемого партнёра
        public Form3(int partnerId)
        {
            InitializeComponent();
            this.partnerId = partnerId;
            InitializeControls(); // Инициализация компонентов
        }
        private void InitializeControls()
        {

        }
        public void LoadPartnerData()
        {
            try
            {
                var partner = db.Partners_.Find(partnerId);
                if (partner != null)
                {
                    textBox1.Text = partner.IDTypePartner.ToString();
                    textBox2.Text = partner.NamePartner ?? string.Empty;
                    textBox3.Text = partner.Director ?? string.Empty;
                    textBox4.Text = partner.Phone ?? string.Empty;
                    textBox5.Text = partner.Rating?.ToString() ?? string.Empty;
                }
                else
                {
                    MessageBox.Show($"Партнёр с ID {partnerId} не найден в базе данных!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Введите название партнёра!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Введите название партнёра!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var newPartners = new Partners_();
            int typeId;
            if (int.TryParse(textBox1.Text, out typeId))
            {
                newPartners.IDTypePartner = typeId;
            }
            else
            {
                MessageBox.Show("Введите корректный ID типа партнёра (только числа)!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var partner = db.Partners_.Find(partnerId);
            if (partner != null)
            {
                partner.IDTypePartner = typeId;
                partner.NamePartner = textBox2.Text;
                partner.Director = textBox3.Text;
                partner.Phone = textBox4.Text;
                partner.Rating = textBox5.Text;

                db.SaveChanges();
                MessageBox.Show("Партнёр успешно обновлён!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                // Уведомляем Form2 об обновлении (если Form2 активна)
                var form2 = Application.OpenForms["Form2"] as Form2;
                form2?.RefreshData();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Удалить партнёра?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                var partner = db.Partners_.Find(partnerId);
                if (partner != null)
                {
                    db.Partners_.Remove(partner);
                    db.SaveChanges();
                    MessageBox.Show("Партнёр удалён!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    // Уведомляем Form2 об обновлении
                    var form2 = Application.OpenForms["Form2"] as Form2;
                    form2?.RefreshData();
                }
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                MessageBox.Show("Введите только цифры!", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
