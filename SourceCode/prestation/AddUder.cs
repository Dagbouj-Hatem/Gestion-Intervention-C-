using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prestation
{
    public partial class AddUder : Form
    {
        public AddUder()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // determination de type de compte 
            int type = listBox2.SelectedIndex;
            // récuperation des variable 
            String nom = textBox1.Text;
            String prenom = textBox2.Text;
            String mail = textBox3.Text;
            String password = textBox4.Text;    
            // Connexion au Data Base 
            try
            {
                OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=../../../../database.accdb");
                OleDbCommand cmd = con.CreateCommand();
                con.Open();
                cmd.Connection = con;
                // last inserted id 
               
                cmd.CommandText = "Select n° from users "; 
                int id=-1;
                Object[] meta = new object[10];
                OleDbDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    int x;
                    int n = read.GetValues(meta);

                    x =(int) meta[0]; 
                    if (x>id)
                    {
                        id = x; 
                    }
                }
                id++;
                read.Close();
                // insertion de nouveau utilisateur
                cmd.CommandText = "insert into users  values ("+id+",'" + nom+ "','" +prenom+ "','" + mail + "','" +password+ "'," + type + ") ";
                int insert = cmd.ExecuteNonQuery();
                if (insert > 0)
                {
                    MessageBox.Show(" Votre utilisateur est ajoutée "); 
                }
                else
                {
                    MessageBox.Show(" Une erreur se produite lors d'ajout de votre utilisateur ");
                }
                //close con
                con.Close();
            }
            catch (System.Data.OleDb.OleDbException exp)
            {
                // cas de erreur lié a la conexion  a DB
                MessageBox.Show("Erreur de Connexion : " + exp.ToString());
            }
            // cacher ce interface
            Hide();
        }
    }
}
