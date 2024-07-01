using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Кинотеатр
{
    public partial class Form3 : Form
    {
        db db = new db();
        public Form3()
        {
            InitializeComponent();
            Фильм();
            Фильм2(); 
            List();
            Lists();
        }
        public void Message()
        {
            string query = $"insert into Зал(НомерЗала,Фильм,ВремяНачала) values ('{textBox1.Text}','{textBox2.Text}','{dateTimePicker1.Text}')";
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

        public void Фильм()
        {
            db.con.Open();

            string query = $"SELECT НомерЗала,Фильм,ВремяНачала FROM Зал";

            using (var command = new SqlCommand(query, db.con))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listBox1.Items.Add($"{reader["НомерЗала"].ToString()}, {reader["Фильм"].ToString()}, {reader["ВремяНачала"].ToString()}");
                    }
                }
            }
            db.con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            Message();
            Фильм();
        }




        public void Message2()
        {
            string query = $"insert into Уведомления(idЗала,Текст) values ('{textBox4.Text}','{textBox3.Text}')";
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

        public void Фильм2()
        {
            db.con.Open();

            string query = $"SELECT НомерЗала FROM Зал";

            using (var command = new SqlCommand(query, db.con))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox1.Items.Add($"{reader["НомерЗала"].ToString()}");
                    }
                }
            }
            db.con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            Message2();
            List();
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public void Фильм3()
        {
            db.con.Open();

            string query = $"SELECT * FROM Зал where НомерЗала = '{comboBox1.Text}'";

            using (var command = new SqlCommand(query, db.con))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        textBox4.Text = reader["id"].ToString();
                    }
                }
            }
            db.con.Close();
        }


        public void List()
        {
            db.con.Open();

            string query = $"SELECT * FROM Уведомления";

            using (var command = new SqlCommand(query, db.con))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listBox2.Items.Add($"{reader["idЗала"].ToString()}, {reader["Текст"].ToString()}");
                    }
                }
            }
            db.con.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Фильм3();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();

            printDocument.PrintPage += PrintDocument_PrintPage;

            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;

            string header = "Отчет";
            Font headerFont = new Font("Yu Gothic", 14, FontStyle.Bold);
            graphics.DrawString(header, headerFont, Brushes.Black, 100, 100);

            int y = 150;
            foreach (string item in listBox3.Items)
            {
                StringFormat stringFormat = new StringFormat();
                stringFormat.LineAlignment = StringAlignment.Near;
                stringFormat.Alignment = StringAlignment.Near;
                stringFormat.Trimming = StringTrimming.Word;

                graphics.DrawString(item, new Font("Yu Gothic", 12), Brushes.Black, 100, y, stringFormat);

                y += 20;
            }
        }


        public void Lists()
        {
            db.con.Open();

            string query = $"SELECT * FROM Билет";

            using (var command = new SqlCommand(query, db.con))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listBox3.Items.Add($"{reader["idЗала"].ToString()}, {reader["idПользователя"].ToString()} , {reader["Ряд"].ToString()} , {reader["Место"].ToString()} , {reader["Цена"].ToString()}");
                    }
                }
            }
            db.con.Close();
        }
    }
}
