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
    public partial class Technicien : Form
    {
        public Technicien()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // logout 
            Application.Restart();
        }
        private void chargeentDB()
        {
            // chargement  de listes d'intervention qui  ne sont pas traité encore 
            // Connexion au Data Base 
            try
            {
                OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=../../../../database.accdb");
                OleDbCommand cmd = con.CreateCommand();
                con.Open();
                Object[] meta = new object[100];
                // cmd.CommandText = "SELECT type,description,priorité,service,num_inv,salle,add_by,date_inter,heure_inter From intervention where archive = '0' ";
                cmd.CommandText = "SELECT N°,type,description,priorité,service,num_inv,salle,date_inter,heure_inter From intervention where archive=0 ";
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

        private void Technicien_Load(object sender, EventArgs e)
        {
            // ajouter le nom et le prenom 
            label10.Text = firstname;
            label11.Text = lastname;

            // lancer le chargement DB de la liste d'intervention 
            chargeentDB(); 


        }

        private void button1_Click(object sender, EventArgs e)
        {
            // recupération  des valeur du  champs  ; 
            String duree = textBox1.Text;
            String tache = textBox2.Text;
            String observation = textBox3.Text;
            // date and heure ; 
            String dd = DateTime.Today.ToShortDateString().ToString();
            String hh = DateTime.Now.ToLocalTime().ToShortTimeString().ToString();
            // etat 
            String etat =" ";
            if(checkBox1.Checked)
            {
                etat = "0";
            }
            if (checkBox2.Checked)
            {
                etat = "1";
            }
            if (checkBox3.Checked)
            {
                etat = "2";
            }


            // mise a jours Data Base 


            if (dataGridView1.SelectedRows.Count > 0)
            {
                // l'utulisteur est  bien  selectionner un  ligne 
                        // id de l'intervention selectionnée 
                        int id= (int) dataGridView1.SelectedRows[0].Cells[0].Value ;

                // Data base Update 
            
                try
                {
                    OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=../../../../database.accdb");
                    OleDbCommand cmd = con.CreateCommand();
                    con.Open();
                    cmd.CommandText = "UPDATE intervention SET id_tech='" + id_user+ "' , date_reparation='" + dd + "' , heure_reparation='" + hh + "' , duree='" + duree + "' , tache='" + tache + "' , etat='" + etat + "' , observation='" + observation + "' , archive=1  WHERE N°=" + id;
                    cmd.Connection = con;
                    int insert = cmd.ExecuteNonQuery();
                    if (insert > 0)
                    {
                        // Message 
                        MessageBox.Show(" Mise à jours avec succée .");

                        // update data grid view 
                        dataGridView1.DataSource = null; 
                        // lancer le chargement DB de la liste d'intervention 
                        chargeentDB();
                    }
                    else
                    {
                        MessageBox.Show(" Une erreur se produite lors de la mise à jours de l'intervention ");
                    }
                    //close con
                    con.Close();
                }
                catch (System.Data.OleDb.OleDbException exp)
                {
                    // cas de erreur lié a la conexion  a DB
                    MessageBox.Show("Erreur de Connexion : " + exp.ToString());
                }
                
            }
            else
            {   // l'utulisteur n'est  bien  selectionner un  ligne 
                MessageBox.Show("SVP selectionner un ligne avant de faire la mise à jours");
            }

           
           

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
