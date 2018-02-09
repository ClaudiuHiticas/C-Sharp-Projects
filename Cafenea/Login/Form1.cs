using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Login
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

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet4.Comenzi' table. You can move, or remove it, as needed.
            this.comenziTableAdapter1.Fill(this.database1DataSet4.Comenzi);
            // TODO: This line of code loads data into the 'database1DataSet3.Comenzi' table. You can move, or remove it, as needed.
            this.comenziTableAdapter.Fill(this.database1DataSet3.Comenzi);
            // TODO: This line of code loads data into the 'database1DataSet2.produse' table. You can move, or remove it, as needed.
            this.produseTableAdapter.Fill(this.database1DataSet2.produse);
            // TODO: This line of code loads data into the 'database1DataSet1.tabel' table. You can move, or remove it, as needed.
            this.tabelTableAdapter.Fill(this.database1DataSet1.tabel);

            x = 15; y = 30; l = 400; L = 700;
            dimensiune(groupBox1);
            dimensiune(groupBox2);
            dimensiune(groupBox3);
            dimensiune(groupBox4);
            dimensiune(groupBox5);
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            //Adaugam inregistrarile angajatiilor in baza de date
            SqlConnection sqlcon = new SqlConnection();
            sqlcon.ConnectionString = Properties.Settings.Default.Database1ConnectionString;

            SqlCommand sqlcom = new SqlCommand();
            sqlcom.Connection = sqlcon;

            SqlDataReader reader;
            sqlcon.Open();

            sqlcom.Parameters.AddWithValue("nume", textBox5.Text);

            sqlcom.CommandText = "select Parola from tabel where(@nume = UserName)";

            reader = sqlcom.ExecuteReader();
            if(reader.Read())
            {
                if (reader[0].ToString() == textBox6.Text)
                {
                    groupBox1.Visible = false;
                    groupBox2.Visible = false;
                    groupBox3.Visible = true;
                    groupBox4.Visible = false;
                    groupBox5.Visible = false;
                }
                else

                    MessageBox.Show("Parola gresita!");
            }else
                MessageBox.Show("Utilizator inexistent!");


         }

        

        

        private void inregistrareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox4.Visible = false;
            groupBox5.Visible = false;
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
               
            //Inregistrare
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                SqlConnection sqlcon = new SqlConnection();
                sqlcon.ConnectionString = Properties.Settings.Default.Database1ConnectionString;

                SqlCommand sqlcom = new SqlCommand();
                sqlcom.Connection = sqlcon;

                sqlcom.CommandText = "SELECT * from tabel WHERE UserName='" + textBox1.Text + "'";
                sqlcon.Open();
                SqlDataReader reader = sqlcom.ExecuteReader();
                if (reader.Read())
                {
                    MessageBox.Show("Din pacate acest nume de utilizator este deja folosit.\nVa rog sa alegeti altul!");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                }
                else
                    if (textBox3.Text == textBox4.Text)
                    {
                        sqlcom.Parameters.AddWithValue("user", textBox1.Text);
                        sqlcom.Parameters.AddWithValue("nume", textBox2.Text);
                        sqlcom.Parameters.AddWithValue("parola", textBox3.Text);

                        sqlcom.CommandText = "insert into tabel (UserName, Nume, Parola) values(@user, @nume, @parola)";

                        sqlcon.Close();
                        sqlcon.Open();
                        sqlcom.ExecuteNonQuery();
                        sqlcon.Close();
                        Application.Restart();
                    }
                    else
                        MessageBox.Show("Parolele nu corespund!");
            }
            else
                MessageBox.Show("Va rugam introduceti datele!");
        }

        private void logareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = true;
            groupBox3.Visible = false;
            groupBox4.Visible = false;
            groupBox5.Visible = false;
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Selectam poza dorita pe care sa o incarcam in baza de date
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JPG (*.jpg)|*.jpg|PNG (*.png)|*.png|All Files(*.*)|*.*";
            ofd.Title = "Alege Poza";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(ofd.FileName);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Incarcam produsele in baza de date.
            SqlConnection sqlcon = new SqlConnection();
            sqlcon.ConnectionString = Properties.Settings.Default.Database1ConnectionString;

            SqlCommand sqlcom = new SqlCommand();
            sqlcom.Connection = sqlcon;

            byte[] imgData;
            imgData = File.ReadAllBytes(pictureBox1.ImageLocation);

            sqlcom.Parameters.AddWithValue("nume", textBox7.Text);
            sqlcom.Parameters.AddWithValue("pret", textBox8.Text);
            sqlcom.Parameters.AddWithValue("calorii", textBox9.Text);
            sqlcom.Parameters.AddWithValue("descriere", textBox10.Text);
            sqlcom.Parameters.AddWithValue("poza", imgData);

            sqlcom.CommandText = "insert into produse(Nume, Pret, Calorii, Descriere, Poza) values(@nume, @pret, @calorii, @descriere, @poza)";

            sqlcon.Open();
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
            Application.Restart();

        }

        private void iesireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
             
        }

        private void clientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox4.Visible = true;
            groupBox5.Visible = false;
        }

        int pret;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Dupa selectarea produsului dorit afisam informatiile disponibile
            SqlConnection sqlcon = new SqlConnection();
            sqlcon.ConnectionString = Properties.Settings.Default.Database1ConnectionString;

            SqlCommand sqlcom = new SqlCommand();
            sqlcom.Connection = sqlcon;
            sqlcom.CommandText = "Select * from produse where Nume = '" + comboBox1.Text + "'";
            sqlcon.Open();
            SqlDataReader reader = sqlcom.ExecuteReader();
            reader.Read();

            byte[] poza = reader["poza"]as byte[] ?? null;
            if (poza != null)
            {
                MemoryStream ms = new MemoryStream(poza);
                Bitmap bmp = new Bitmap(ms);
                pictureBox2.Image = bmp;
            }
            else
            {
                string adresa = Application.StartupPath + "/cafe.png";
                pictureBox2.ImageLocation = adresa; 
            }


            richTextBox1.Visible = true;
            label14.Visible = true;
            label9.Visible = true;
            label5.Visible = true;
            label15.Visible = true;
            numericUpDown2.Visible = true;
            numericUpDown1.Visible = true;
            button5.Visible = true;
            richTextBox1.Text = "Descriere: " +  reader["Descriere"].ToString();
            label9.Text =  "Kcal: " + reader["Calorii"].ToString();
            label14.Text = "Pret: " + reader["Pret"].ToString();
            pret = int.Parse(reader["pret"].ToString());
            
            sqlcon.Close();

        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            comboBox1.Items.Clear();
            SqlConnection sqlcon = new SqlConnection();
            sqlcon.ConnectionString = Properties.Settings.Default.Database1ConnectionString;

            SqlCommand sqlcom = new SqlCommand();
            sqlcom.Connection = sqlcon;

            
            sqlcom.CommandText = "Select Nume from produse";
            sqlcon.Open();
            SqlDataReader reader = sqlcom.ExecuteReader();
            while (reader.Read())
                comboBox1.Items.Add(reader[0].ToString());
            sqlcon.Close();


        }

        private void comenziToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox4.Visible = false;
            groupBox5.Visible = true;
        }
        

        private void button5_Click(object sender, EventArgs e)
        {
            //Completam tabela de comenzi inregistrte
            SqlConnection sqlcon = new SqlConnection();
            sqlcon.ConnectionString = Properties.Settings.Default.Database1ConnectionString;

            SqlCommand sqlcom = new SqlCommand();
            sqlcom.Connection = sqlcon;

            pret *= (int)numericUpDown1.Value;

            sqlcom.Parameters.AddWithValue("Nume", comboBox1.Text);
            sqlcom.Parameters.AddWithValue("Masa", numericUpDown2.Value.ToString());
            sqlcom.Parameters.AddWithValue("Cantitate", numericUpDown1.Value.ToString());
            sqlcom.Parameters.AddWithValue("Pret", pret.ToString());
            sqlcom.CommandText = "insert into Comenzi(Nume, Masa, Cantitate, Pret) values(@Nume, @Masa, @Cantitate, @Pret)";

            sqlcon.Open();
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
            MessageBox.Show("Cumparat cu succes!");
            Application.Restart();

        }

       
    }
}
