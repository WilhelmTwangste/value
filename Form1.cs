using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using WindowsFormsApp3;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form, IUserForm
    {
        private readonly demo3Entities1 db = new demo3Entities1();
        private int editingCardID = 0;
        private FlowLayoutPanel flowLayoutPanel1;

        public Form1()
        {
            InitializeComponent();
            InitializeFlowLayoutPanel();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCards();
        }

        public void Delete(int ID)
        {
            var result = MessageBox.Show("Удалить партнера?", "Подтверждение", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                var user = db.Partners_.Find(ID);
                if (user != null)
                {
                    db.Partners_.Remove(user);
                    db.SaveChanges();
                    LoadCards();
                }
            }
        }
        private void InitializeFlowLayoutPanel()
        {
            flowLayoutPanel1 = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };
            this.Controls.Add(flowLayoutPanel1);
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
    
            // Создаём новый объект PARTNER
            var newPartner = new Partners_
            {
                IDTypePartner = typeId,
                NamePartner = textBox2.Text,
                Director = textBox3.Text,
                Phone = textBox4.Text,
                Rating = textBox5.Text
            };

            // Добавляем в контекст Entity Framework
            db.Partners_.Add(newPartner);

            // Сохраняем изменения в базе данных
            db.SaveChanges();

            // Обновляем отображение карточек в Form1
            LoadCards();

            // Очищаем поля после сохранения
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();

            // Передаём обновлённый список партнёров в Form2
            List<Partners_> partners = db.Partners_.ToList();
            Form2 form2 = new Form2(partners);
            form2.Show();
            this.Hide();

            MessageBox.Show("Партнёр успешно добавлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoadCards()
        {
            flowLayoutPanel1.Controls.Clear();
            foreach (var PARTNER in db.Partners_.ToList())
            {
                UserControl1 card = new UserControl1();
                card.SetData(
                    PARTNER.IDTypePartner,
                    PARTNER.NamePartner,
                    PARTNER.Director,
                    PARTNER.Phone,
                    PARTNER.Rating?.ToString() ?? "0"
                );
                card.Tag = PARTNER.IDPartner;
                flowLayoutPanel1.Controls.Add(card);
            }
        }

        public void FillFields(int PARTNER_ID)
        {
            var PARTNER = db.Partners_.Find(PARTNER_ID);
            if (PARTNER != null)
            {
                textBox1.Text = PARTNER.IDTypePartner.ToString();
                textBox2.Text = PARTNER.NamePartner;
                textBox3.Text = PARTNER.Director;
                textBox4.Text = PARTNER.Phone;
                textBox5.Text = PARTNER.Rating?.ToString() ?? "0";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Partners_> partners = db.Partners_.ToList();
            Form2 form2 = new Form2(partners);
            form2.Show();
            this.Hide();
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Отменяем ввод
                MessageBox.Show("Введите только цифры!", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}