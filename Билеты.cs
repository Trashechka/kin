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

namespace Кинотеатр
{
    public partial class Билеты : Form
    {
        db db = new db();
        public Билеты()
        {
            InitializeComponent();
            label1.Text = Class.id.ToString();
            Bp();
        }
        public void Bp()
        {
            db.con.Open();

            string query = $"SELECT * FROM Билет where idПользователя = '{label1.Text}'";

            using (var command = new SqlCommand(query, db.con))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listBox1.Items.Add($"idЗала: {reader["idЗала"].ToString()}, Ваш Ряд: {reader["Ряд"].ToString()}, Ваше место: {reader["Место"].ToString()}, Цена: {reader["Цена"].ToString()}");
                    }
                }
            }
            db.con.Close();
        }
    }
}
