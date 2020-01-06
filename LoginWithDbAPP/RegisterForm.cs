using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginWithDbAPP
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {

        }

        private void ButtonRegister_Click(object sender, EventArgs e)
        {
            if (userName2Field.Text == "")
            {
                MessageBox.Show("Введи никнейм чудак!");
                return;
            }

            if (loginField2.Text == "")
            {
                MessageBox.Show("Введи логин чудак!");
                return;
            }

            if (passwordField2.Text == "")
            {
                MessageBox.Show("Введи пароль чудак!");
                return;
            }
            
                if (passwordField3.Text == "")
            {
                MessageBox.Show("Повтори пароль чудак!");
                return;
            }

            if (passwordField3.Text != passwordField2.Text) 
            {
                MessageBox.Show("Пароли не совпадают! ты норм?");
                return;
            }

            if (isUserExist())
                return;

            MessageBox.Show($"{userName2Field.Text}, введи свои данные. ");

            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `userbase` (`login`, `pass`, `username`) VALUES (@login, @password, @username)" , db.getConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = loginField2.Text;
            command.Parameters.Add("@password", MySqlDbType.VarChar).Value = passwordField2.Text;
            command.Parameters.Add("@username", MySqlDbType.VarChar).Value = userName2Field.Text;

            db.OpenConnection();

            if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("Аккаунт был создан!");
            else
                MessageBox.Show("Аккаунт не был создан! (лох) ");

            db.CloseConnection();
        }

        public Boolean isUserExist()
        {
            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `userbase` WHERE `login` = @uL", db.getConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginField2.Text;
            
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такой логин уже есть!");
                return true;
            }
            else
                return false;
                
            
        }

        private void LoginLabel_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 loginForm = new Form1();
            loginForm.Show();
        }
    }
}
