using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Кинотеатр
{
    public partial class Form1 : Form
    {
        db db = new db();
        public Form1()
        {
            InitializeComponent();
        }


        private void logins()
        {

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable Table = new DataTable();

            string query = $"select * from Пользователь where Логин='{textBox1.Text}' and Пароль='{textBox2.Text}'";

            SqlCommand command = new SqlCommand(query, db.con);

            adapter.SelectCommand = command;

            adapter.Fill(Table);

            if (Table.Rows.Count == 1)
            {
                var p = new Form1();
                MessageBox.Show("Вы успешно вошли!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                if (textBox1.Text == "admin")
                {
                    Class.name = textBox1.Text;
                    Form3 form3 = new Form3();
                    form3.ShowDialog();
                }
                else
                {
                    Class.name = textBox1.Text;
                    Form2 form2 = new Form2();
                    form2.ShowDialog();
                }

                db.con.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Close();
                db.con.Close();
            }
            else
            {
                // Отображение сообщения об ошибке
                MessageBox.Show("Неверный логин/пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void registration()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable Table = new DataTable();

            string query = $"select Логин,Пароль from Пользователь where Логин='{textBox1.Text}'";

            SqlCommand cmd = new SqlCommand(query, db.con);

            adapter.SelectCommand = cmd;

            adapter.Fill(Table);

            db.con.Open();

            if (Table.Rows.Count == 0)
            {
                SqlCommand insertCommand = new SqlCommand($"insert into Пользователь(Логин,Пароль) values ('{textBox1.Text}','{textBox2.Text}')", db.con);

                if (insertCommand.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Регистрация прошла успешно!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Hide();
                    Class.name = textBox1.Text;
                    Form2 запрос = new Form2();
                    запрос.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Такой логин/почта уже существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            db.con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {

            }
            else
            {
                logins();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {

            }
            else
            {
                registration();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
