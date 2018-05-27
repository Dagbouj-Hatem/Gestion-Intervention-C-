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
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // logout 
            Application.Restart();
        }
        private void chargeentDB(int type )
        {
            // chargement  de listes d'intervention qui  ne sont pas traité encore 
            // Connexion au Data Base 

            /*
             *     if type == 1 --> liste users
             *     if type == 2 --> liste inetrventions archivé
             **/
            try
            {
                OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=../../../../database.accdb");
                OleDbCommand cmd = con.CreateCommand();
                con.Open(); 
                if(type==1)
                {       // 
                        cmd.CommandText = "SELECT * From users  ";
                }
               if(type==2)
                {
                    cmd.CommandText = "SELECT * From intervention where archive=1 ";
                }
                
                cmd.Connection = con;
                DataSet ds = new DataSet();
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd.CommandText, cmd.Connection))
                {

                    adapter.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0];
                }
                con.Close();
            }
            catch (System.Data.OleDb.OleDbException exp)
            {
                // cas de erreur lié a la conexion  a DB
                MessageBox.Show("Erreur de Connexion : " + exp.ToString());
            }
        }
        private void Admin_Load(object sender, EventArgs e)
        {
           

            // chargement  de nom et prenom de l'utilisateur
            label10.Text = firstname;
            label11.Text = lastname;
            // chargement liste les utilisateur
            chargeentDB(1);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // update data grid view 
            dataGridView1.DataSource = null;
            chargeentDB(2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // update data grid view 
            dataGridView1.DataSource = null;
            chargeentDB(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            // ajouter un utilisateur
            AddUder add = new AddUder();
                add.Show();
        }
    }
}
