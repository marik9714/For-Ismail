using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Windows.Forms;
using WindowsFormsApplication4.Helpers;
using System.Data.SqlClient;

namespace WindowsFormsApplication4
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
            Form9 form9 = new Form9();
            form9.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Query, con;
            //"SELECT Товары.Товар_ID, Товары.Наименование, [Товары]![Количество]+[Поступления]![Кол-во] AS Количество, Товары.Цена, Товары.Поставщик_ID, Товары.Отдел_ID FROM Товары INNER JOIN Поступления ON Товары.Товар_ID = Поступления.Товар_ID;"
            con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Магазин2.mdb;Persist Security Info=False;";
            Query = "INSERT INTO Поступления (Дата, Товар_ID, Количество , Цена) VALUES ('" + dateTimePicker1.Text + "','" + comboBox1.SelectedValue + "','" + textBox3.Text + "','" + textBox4.Text + "');";
            var connect = SqlConnectionHelper.GetSqlConnection();
            try
            {
                connect.Open();
                MessageBox.Show("Соединение прошло успешно");
            }
            catch { MessageBox.Show("Ошибка соединения"); }
            DataSet ds = new DataSet();
            ds.Clear();
            SqlConnectionHelper.ExecuteButtonHandle(Query, "Добавление", ds, connect);
            this.Close();
            connect.Close();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox3.MaxLength = 1;
            if ((e.KeyChar >= '1') && (e.KeyChar <= '4'))
            {
                return;
            }
            if (e.KeyChar == '.')
            {
                e.KeyChar = ',';
            }

            if (e.KeyChar == ',')
            {
                if (textBox3.Text.IndexOf(',') != -1)
                {
                    e.Handled = true;
                }
                return;
            }

            if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Enter)
                    textBox4.Focus();
                return;
            }
            e.Handled = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
            {
                return;
            }
            if (e.KeyChar == '.')
            {
                e.KeyChar = ',';
            }

            if (e.KeyChar == ',')
            {
                if (textBox4.Text.IndexOf(',') != -1)
                {
                    e.Handled = true;
                }
                return;
            }

            if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Enter)
                    textBox4.Focus();
                return;
            }
            e.Handled = true;
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            SqlConnection connection = SqlConnectionHelper.GetSqlConnection();
            SqlCommand command = new SqlCommand("select*from Товары", connection);
            command.CommandTimeout = 30;
            SqlDataAdapter sotrDA = new SqlDataAdapter();
            sotrDA.SelectCommand = command;

            connection.Open();
            DataTable tbl = new DataTable();
            sotrDA.Fill(tbl);

            comboBox1.DataSource = tbl;
            comboBox1.DisplayMember = "Наименование";
            comboBox1.ValueMember = "Товар_ID";
            connection.Close();
            comboBox1.SelectedIndex = -1;
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
