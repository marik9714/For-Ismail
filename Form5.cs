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
    public partial class Form5 : Form
    {
        int i = 0;
        int n = 0;
        public Form5()
        {
            InitializeComponent();
        }

        private void tabLichDan()
        {
            string Query, con;
            con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Магазин2.mdb;Persist Security Info=False;";
            Query = "SELECT Сотрудники.Сотрудник_ID, Сотрудники.Фамилия, Сотрудники.Имя, Сотрудники.Отчество, Отделы.Отдел, Сотрудники.Адрес, Сотрудники.Дата_приема, Сотрудники.Пароль FROM Отделы INNER JOIN Сотрудники ON Отделы.Отдел_ID = Сотрудники.Отдел_ID;";
            var connect = SqlConnectionHelper.GetSqlConnection();
            try
            {
                connect.Open();
                MessageBox.Show("Соединение прошло успешно");
            }
            catch { MessageBox.Show("Ошибка соединения"); }
            DataSet ds = new DataSet();
            ds.Clear();
            SqlConnectionHelper.ExecuteButtonHandle(Query, "Сотрудники", ds, connect);
            textBox6.Text = ds.Tables["Сотрудники"].Rows[i][0].ToString();
            textBox1.Text = ds.Tables["Сотрудники"].Rows[i][1].ToString();
            textBox2.Text = ds.Tables["Сотрудники"].Rows[i][2].ToString();
            textBox3.Text = ds.Tables["Сотрудники"].Rows[i][3].ToString();
            comboBox1.Text = ds.Tables["Сотрудники"].Rows[i][4].ToString();
            textBox5.Text = ds.Tables["Сотрудники"].Rows[i][5].ToString();
            dateTimePicker1.Text = ds.Tables["Сотрудники"].Rows[i][6].ToString();
            textBox4.Text = ds.Tables["Сотрудники"].Rows[i][7].ToString();
            connect.Close();
            n = ds.Tables["Сотрудники"].Rows.Count;
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
            Query = "INSERT INTO Сотрудники (Фамилия, Имя, Отчество, Адрес, Отдел_ID, Дата_приема, Пароль) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox5.Text + "','" + comboBox1.SelectedValue.ToString() + "','" + dateTimePicker1.Text + "','" + textBox4.Text + "');";
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

        private void Form5_Load(object sender, EventArgs e)
        {

            SqlConnection connection = SqlConnectionHelper.GetSqlConnection();
            SqlCommand command = new SqlCommand("select*from Отделы", connection);
            command.CommandTimeout = 30;
            SqlDataAdapter sotrDA = new SqlDataAdapter();
            sotrDA.SelectCommand = command;

            connection.Open();
            DataTable tbl = new DataTable();
            sotrDA.Fill(tbl);
         
            comboBox1.DataSource = tbl;
            comboBox1.DisplayMember = "Отдел";
            comboBox1.ValueMember = "Отдел_ID";
            connection.Close();
            comboBox1.SelectedIndex = -1;
            tabLichDan();
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox4.MaxLength = 3;
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
            {
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

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection connection = SqlConnectionHelper.GetSqlConnection();
            SqlCommand command = new SqlCommand($"Delete from Сотрудники where Сотрудник_ID = {textBox6.Text}", connection);
            command.CommandTimeout = 30;
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (i < n - 1)
            {
                i++;
                tabLichDan();
            }

            else
            {
                MessageBox.Show("Конец списка");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (i > 0)
            {
                i--;
                tabLichDan();
            }

            else
            {
                MessageBox.Show("Начало списка");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlConnection connection = SqlConnectionHelper.GetSqlConnection();
            SqlCommand comUpdate = new SqlCommand(
                $@"Update Сотрудники set Фамилия = {textBox1.Text}, 
                                         Имя = {textBox2.Text}, 
                                        Отчество = {textBox3.Text}, 
                                        Адрес = { textBox5.Text},
                                        Отдел_ID = { comboBox1.SelectedValue}, 
                                        Дата_приема = {dateTimePicker1.Value}, 
                                        Пароль = {textBox4.Text} where Сотрудник_ID = {textBox6.Text}", connection);
            comUpdate.CommandTimeout = 30;
            connection.Open();
            comUpdate.ExecuteNonQuery();
            connection.Close();
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
  
}
