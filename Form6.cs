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
    public partial class Form6 : Form
    {
        public Form6()
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
            con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Магазин2.mdb;Persist Security Info=False;";
            Query = "INSERT INTO Продажи (Товар, Количество, Клиент_ID, Сотрудник_ID, Номер_продажи, Дата_продажи) VALUES ('" + comboBox2.ValueMember + "','" + textBox2.Text + "','" + comboBox3.ValueMember + "','" + comboBox1.ValueMember + "','" + textBox6.Text + "','" + dateTimePicker1.Text + "');";
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

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '1') && (e.KeyChar <= '9'))
            {
                return;
            }
            if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Enter)
                    textBox6.Focus();
                return;
            }
            e.Handled = true;
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            SqlConnection connection = SqlConnectionHelper.GetSqlConnection();
            SqlCommand command = new SqlCommand("select*from Товары", connection);
            command.CommandTimeout = 30;
            SqlDataAdapter sotrDA = new SqlDataAdapter();
            sotrDA.SelectCommand = command;

            connection.Open();
            DataTable tbl1 = new DataTable();
            sotrDA.Fill(tbl1);
            comboBox2.DataSource = tbl1;
            comboBox2.DisplayMember = "Наименование";
            comboBox2.ValueMember = "Товар_ID";
            connection.Close();
            comboBox2.SelectedIndex = -1;

            var query = "select*from Клиенты";
            var connect = SqlConnectionHelper.GetSqlConnection();
            try
            {
                connect.Open();
                MessageBox.Show("Соединение прошло успешно");
            }
            catch { MessageBox.Show("Ошибка соединения"); }
            DataSet ds1 = new DataSet();
            ds1.Clear();
            SqlConnectionHelper.ExecuteButtonHandle(query, "Клиенты", ds1, connect);
            ds1.Tables["Клиенты"].Columns.Add("fio", typeof(string), "Фамилия+' '+Имя+' '+Отчество");
            connect.Close();
            comboBox3.DataSource = ds1.Tables["Клиенты"];
            comboBox3.DisplayMember = "fio";
            comboBox3.ValueMember = "Код";
            comboBox3.SelectedIndex = -1;

            query = "select*from Сотрудники";
            try
            {
                connect.Open();
                MessageBox.Show("Соединение прошло успешно");
            }
            catch { MessageBox.Show("Ошибка соединения"); }
            DataSet ds2 = new DataSet();
            ds2.Clear();
            SqlConnectionHelper.ExecuteButtonHandle(query, "Сотрудники", ds2, connect);
            ds2.Tables["Сотрудники"].Columns.Add("fio", typeof(string), "Фамилия+' '+Имя+' '+Отчество");
            connect.Close();
            comboBox1.DataSource = ds2.Tables["Сотрудники"];
            comboBox1.DisplayMember = "fio";
            comboBox1.ValueMember = "Сотрудник_ID";
            comboBox1.SelectedIndex = -1;
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '1') && (e.KeyChar <= '9'))
            {
                return;
            }
            if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Enter)
                    textBox2.Focus();
                return;
            }
            e.Handled = true;
        }
    }
}
