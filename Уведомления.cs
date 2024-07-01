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
    public partial class Уведомления : Form
    {
        db db = new db();
        public Уведомления()
        {
            InitializeComponent();
            Bp();
        }
        public void Bp()
        {
            db.con.Open();

            string query = $"SELECT * FROM Уведомления";

            using (var command = new SqlCommand(query, db.con))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listBox1.Items.Add($"idЗала: {reader["idЗала"].ToString()}, Оповещение: {reader["Текст"].ToString()}");
                    }
                }
            }
            db.con.Close();
        }
    }
}
