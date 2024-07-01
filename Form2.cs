using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Кинотеатр
{
    public partial class Form2 : Form
    {
        db db = new db();
        public Form2()
        {
            InitializeComponent();
            listBox1.Items[0] = Class.name;
            ID();
            Ticket();
            Фильм();
        }

        public void ID()
        {
            db.con.Open();

            string query = $"SELECT id FROM Пользователь where Логин = '{listBox1.Items[0].ToString()}'";

            using (var command = new SqlCommand(query, db.con))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Class.id = reader["id"].ToString();
                    }
                }
            }
            db.con.Close();
        }

        public void Ticket()
        {
            db.con.Open();

            string query = $"SELECT НомерЗала FROM Зал";

            using (var command = new SqlCommand(query, db.con))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader["НомерЗала"].ToString());
                    }
                }
            }
            db.con.Close();
        }

        public void Фильм()
        {
            db.con.Open();

            string query = $"SELECT id,Фильм FROM Зал where НомерЗала = '{comboBox1.Text}'";

            using (var command = new SqlCommand(query, db.con))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        textBox5.Text = reader["id"].ToString();
                        comboBox2.Items.Add(reader["Фильм"].ToString());
                    }
                }
            }
            db.con.Close();
        }

        public void Фильм2()
        {
            db.con.Open();

            string query = $"SELECT * FROM Зал where Фильм = '{comboBox2.Text}'";

            using (var command = new SqlCommand(query, db.con))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        textBox1.Text = reader["ВремяНачала"].ToString();
                    }
                }
            }
            db.con.Close();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == 1)
            {
                Уведомления form1 = new Уведомления();
                form1.ShowDialog();
            }
            if (listBox1.SelectedIndex == 2)
            {
                Билеты form1 = new Билеты();
                form1.ShowDialog();
            }
            if (listBox1.SelectedIndex == 3)
            {
                this.Hide();
                Form1 form1 = new Form1();
                form1.ShowDialog();
            }
        }

        public void Message()
        {
            string query = $"insert into Билет(idЗала,idПользователя,Ряд,Место,Цена) values ('{textBox5.Text}','{Class.id}','{textBox2.Text}','{textBox3.Text}','{textBox4.Text}')";

            SqlCommand command = new SqlCommand(query, db.con);

            try
            {
                db.con.Open();
                int rowsAffected = command.ExecuteNonQuery();
                MessageBox.Show("Данные занесены.", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);

                db.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
                db.con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Message();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Фильм2();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox4.Text = "600";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            Фильм();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 8) //Если символ, введенный с клавы - не цифра (IsDigit),
            {
                e.Handled = true;// то событие не обрабатывается. ch!=8 (8 - это Backspace)
            }
        }
    }
}
