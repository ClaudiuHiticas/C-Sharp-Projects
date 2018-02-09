using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Cabinet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int x, y, l, L;
        void dimensiune(GroupBox a)
        {
            //redimensioneaza si pozitioneaza controalele pe forma
            a.Left = x;
            a.Top = y;
            a.Width = L;
            a.Height = l;

        }

        private void inregistrareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox4.Visible = false;
            groupBox5.Visible = false;
            groupBox6.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cabinetDataSet4.Beneficiar' table. You can move, or remove it, as needed.
            this.beneficiarTableAdapter1.Fill(this.cabinetDataSet4.Beneficiar);
            // TODO: This line of code loads data into the 'cabinetDataSet3.Beneficiar' table. You can move, or remove it, as needed.
            this.beneficiarTableAdapter.Fill(this.cabinetDataSet3.Beneficiar);
            // TODO: This line of code loads data into the 'cabinetDataSet1.Pacienti' table. You can move, or remove it, as needed.
            this.pacientiTableAdapter.Fill(this.cabinetDataSet1.Pacienti);
            // TODO: This line of code loads data into the 'cabinetDBDataSet1.consultatii' table. You can move, or remove it, as needed.

            x = 15; y = 30; l = 400; L = 790;
            dimensiune(groupBox1);
            dimensiune(groupBox2);
            dimensiune(groupBox3);
            dimensiune(groupBox4);
            dimensiune(groupBox5);
            dimensiune(groupBox6);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            SqlConnection sqlcon = new SqlConnection();
            sqlcon.ConnectionString = Properties.Settings.Default.CabinetConnectionString;

            SqlCommand sqlcom = new SqlCommand();
            sqlcom.Connection = sqlcon;

            sqlcom.Parameters.AddWithValue("nume", textBox1.Text);
            sqlcom.Parameters.AddWithValue("localitate", textBox4.Text);
            sqlcom.Parameters.AddWithValue("detalii", textBox5.Text);
            sqlcom.Parameters.AddWithValue("data", this.dateTimePicker1.Text);

            sqlcom.CommandText = "insert into Pacienti(Nume, Localitate, Detalii, Data) values(@nume, @localitate, @detalii, @data)";

            sqlcon.Open();
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
            MessageBox.Show("Adaugat cu succes!");
            Application.Restart();

        }

        private void consultatiiBindingSource_AddingNew(object sender, AddingNewEventArgs e)
        {
            //this.PacientiTableAdapter.Update(this.cabinetDataSet1);
        }

        private void vizualizareConsultatiiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = true;
            groupBox3.Visible = false;
            groupBox4.Visible = false;
            groupBox5.Visible = false;
            groupBox6.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection sqlcon = new SqlConnection();
            sqlcon.ConnectionString = Properties.Settings.Default.CabinetConnectionString;

            SqlDataAdapter sqlda = new SqlDataAdapter("select * from Pacienti where data between '" + dateTimePicker2.Value.ToString() + " ' and ' " + dateTimePicker3.Value.ToString() + "'", sqlcon);
            DataTable dt = new DataTable();
            sqlda.Fill(dt);
            dataGridView2.DataSource = dt;

        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            //Cand dam click pe combobox ne incarca numele din Baza de date
            comboBox1.Items.Clear();

            SqlConnection sqlcon = new SqlConnection();
            sqlcon.ConnectionString = Properties.Settings.Default.CabinetConnectionString;

            SqlCommand sqlcom = new SqlCommand();
            sqlcom.Connection = sqlcon;

            sqlcom.CommandText = "select * from Pacienti";
            sqlcon.Open();

            SqlDataReader sdr = sqlcom.ExecuteReader();
            
            while (sdr.Read())
                comboBox1.Items.Add(sdr[1].ToString());
            sqlcon.Close();

        }

        private void beneficiatoriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = true;
            groupBox4.Visible = false;
            groupBox5.Visible = false;
            groupBox6.Visible = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection sqlcon = new SqlConnection();
            sqlcon.ConnectionString = Properties.Settings.Default.CabinetConnectionString;

            SqlCommand sqlcom = new SqlCommand();
            sqlcom.Connection = sqlcon;

            sqlcom.CommandText = "select * from Pacienti where Nume = '" + comboBox1.Text + "'";
            sqlcon.Open();

            SqlDataReader reader = sqlcom.ExecuteReader();
            reader.Read();

            label11.Text = "Descriere:   " + reader["Detalii"].ToString();
            label9.Text = "Localitate:   " + reader["Localitate"].ToString();
            label12.Text = "Data:   " + reader["Data"].ToString();
        }

        private void vizualizareConsultatiiToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
            groupBox1.Visible = false;
            groupBox3.Visible = false;
            groupBox4.Visible = false;
            groupBox5.Visible = false;
            groupBox6.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection sqlcon = new SqlConnection();
            sqlcon.ConnectionString = Properties.Settings.Default.CabinetConnectionString;

            SqlCommand sqlcom = new SqlCommand();
            sqlcom.Connection = sqlcon;

            sqlcom.Parameters.AddWithValue("nume", comboBox1.Text);
            sqlcom.Parameters.AddWithValue("detalii", textBox2.Text);
            sqlcom.Parameters.AddWithValue("data", this.dateTimePicker4.Text);

            sqlcom.CommandText = "insert into Beneficiar(Nume, Detalii, Data) values(@nume, @detalii, @data)";

            sqlcon.Open();
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
            MessageBox.Show("Benefeciar adaugat cu succes!");
            Application.Restart();
        }

        private void urmarireMedicalaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox4.Visible = true;
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox5.Visible = false;
            groupBox6.Visible = false;
        }

        private void raportZilnicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox4.Visible = false;
            groupBox5.Visible = true;
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox6.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection sqlcon = new SqlConnection();
            sqlcon.ConnectionString = Properties.Settings.Default.CabinetConnectionString;

            SqlDataAdapter sqlda = new SqlDataAdapter("select * from Pacienti where data = '" + dateTimePicker5.Value.ToString()  + "'", sqlcon);
            DataTable dt = new DataTable();
            sqlda.Fill(dt);
            dataGridView4.DataSource = dt;
        }

        private void urmarireMedicalaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            groupBox6.Visible = true;
            groupBox4.Visible = false;
            groupBox5.Visible = false;
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
        }

        private void comboBox2_MouseClick(object sender, MouseEventArgs e)
        {
            comboBox2.Items.Clear();

            SqlConnection sqlcon = new SqlConnection();
            sqlcon.ConnectionString = Properties.Settings.Default.CabinetConnectionString;

            SqlCommand sqlcom = new SqlCommand();
            sqlcom.Connection = sqlcon;

            sqlcom.CommandText = "select Nume from Beneficiar";
            sqlcon.Open();

            SqlDataReader reader = sqlcom.ExecuteReader();

            while (reader.Read())
                comboBox2.Items.Add(reader[0].ToString());
            sqlcon.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection sqlcon = new SqlConnection();
            sqlcon.ConnectionString = Properties.Settings.Default.CabinetConnectionString;

            SqlCommand sqlcom = new SqlCommand();
            sqlcom.Connection = sqlcon;

            sqlcom.CommandText = "select * from Pacienti where Nume = '" + comboBox2.Text + "'";
            sqlcon.Open();

            SqlDataReader reader = sqlcom.ExecuteReader();
            reader.Read();
            richTextBox1.Text = reader[3].ToString();
            sqlcon.Close();


            SqlConnection sqlcon2 = new SqlConnection();
            sqlcon2.ConnectionString = Properties.Settings.Default.CabinetConnectionString;

            SqlCommand sqlcom2 = new SqlCommand();
            sqlcom2.Connection = sqlcon2;

            sqlcom2.CommandText = "select * from Beneficiar where Nume = '" + comboBox2.Text + "'";
            sqlcon2.Open();

            SqlDataReader reader2 = sqlcom2.ExecuteReader();
            reader2.Read();
            richTextBox2.Text = reader2[2].ToString();
            sqlcon2.Close();


        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
