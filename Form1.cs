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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            employee();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            SqlConnection sqlConnection = SqlConnectionHelper.GetSqlConnection();
            SqlCommand selectSotr = new SqlCommand("select*from Сотрудники", sqlConnection);
            selectSotr.CommandTimeout = 30;
            SqlDataAdapter sotrDA = new SqlDataAdapter();
            sotrDA.SelectCommand = selectSotr;

            sqlConnection.Open();
            DataSet ds = new DataSet();
            ds.Clear();
            sotrDA.Fill(ds, "Таблица");
            sqlConnection.Close();

            ds.Tables["Таблица"].Columns.Add("fio", typeof(string), "Фамилия+' '+Имя+' '+Отчество");
            comboBox1.DataSource = ds.Tables["Таблица"];
            comboBox1.DisplayMember = "fio";
            comboBox1.ValueMember = "Сотрудник_ID";
            comboBox1.SelectedIndex = -1;
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
           e.Handled = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Back)
                return;
            }
            if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Enter)
                    employee();
            }
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
            {
                return;
            }
            e.Handled = true;
        }
        private void employee()
        {
            SqlConnection sqlConnection = SqlConnectionHelper.GetSqlConnection();
            if (comboBox1.Text.Length == 0)
            {
                MessageBox.Show("Поля не должны быть пустыми");
            }
            else
            {
                SqlCommand selectSotr = new SqlCommand("SELECT * FROM Сотрудники WHERE Сотрудники.Сотрудник_ID = " + comboBox1.SelectedValue.ToString() + " AND Сотрудники.Пароль = " + textBox1.Text + "", sqlConnection);
                selectSotr.CommandTimeout = 30;
                sqlConnection.Open();
                if (textBox1.Text.Length == 0 ^ comboBox1.Text.Length == 0)
                {
                    MessageBox.Show("Поля не должны быть пустыми");
                }
                else if (selectSotr.ExecuteScalar() == null)
                {
                    MessageBox.Show("Не верный пользователь или пароль");
                    // Действия, осуществляемые при неудачном входе
                }
                else
                {
                    MessageBox.Show("Вход выполнен успешно");
                    // Действия, осуществляемые при удачном входе
                    Form2 form2 = new Form2();
                    this.Hide();
                    form2.Show();
                }
            }
            sqlConnection.Close();
        }
    }
}
