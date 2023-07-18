using System;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using Microsoft.Win32;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        private string connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=MAIN.mdb;jet oledb:database password=LW;Data Source=C:\Program Files (x86)\巡检管理系统A1.0+\MAIN.MDB";//名称；密码；地址

        private OleDbConnection conn = null;
        private OleDbDataAdapter adapter = null;
        private DataTable dt = null;

        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 连接数据库并显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            // 连接数据库，需要传递连接字符串
            conn = new OleDbConnection(connStr);
            // 打开数据库连接
            conn.Open();

            // "Select * from .."为SQL语句，意思是从数据库中选择叫做“lwnhdd”的表，“conn”为连接
            adapter = new OleDbDataAdapter("Select * from lwnhdd", conn);

            label1.Text = "lwnhdd";
            // CommandBuilder对应的是数据适配器，需要传递参数
            var cmd = new OleDbCommandBuilder(adapter);

            // 在内存中创建一个DataTable，用来存放、修改数据库表
            dt = new DataTable();
            // 通过适配器把表的数据填充到内存dt
            adapter.Fill(dt);
            // 把数据显示到界面
            dataGridView1.DataSource = dt.DefaultView;
        }
        private void label1_Click(object sender, EventArgs e)
        {
            label1.Text = "lwnhdd";
        }
        private void FormDatabase_Load(object sender, EventArgs e)
        {

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>  
        /// 修改程序在注册表中的键值  
        /// </summary>  
        /// <param name="isAuto">true:开机启动,false:不开机自启</param> 
        private void AutoStart(bool isAuto, bool showinfo)
        {
            try
            {
                if (isAuto == true)
                {
                    RegistryKey R_local = Registry.CurrentUser;//RegistryKey R_local = Registry.CurrentUser;
                    RegistryKey R_run = R_local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                    R_run.SetValue("WindowsFormsApp", Application.ExecutablePath);
                    R_run.Close();
                    R_local.Close();
                }
                else if (isAuto == false)
                {
                    RegistryKey R_local = Registry.CurrentUser;//RegistryKey R_local = Registry.CurrentUser;
                    RegistryKey R_run = R_local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                    R_run.DeleteValue("WindowsFormsApp", false);
                    R_run.Close();
                    R_local.Close();
                }
            }
            catch (Exception)
            {
                if (showinfo)
                    MessageBox.Show("您需要管理员权限修改", "提示");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            AutoStart(true, true);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            AutoStart(false, true);
        }
    }
}
