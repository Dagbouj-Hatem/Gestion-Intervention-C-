using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;


namespace prestation
{
    public partial class Demendeur : Form
    {
        public Demendeur()
        {
            InitializeComponent();
        }

        private void Demendeur_Load(object sender, EventArgs e)
        {
            label10.Text = firstname;
            label11.Text = lastname;
        }
          

        private void button1_Click(object sender, EventArgs e)
        {
            //add prestation
            String id_demandeur = this.id_user;
            String type_materiel= geTypeMaterial();
            String description = textBox2.Text;
            String priorite = getPriorite();
            String service_dept = textBox3.Text;
            String num_inventaire = textBox4.Text;
            String salle = textBox5.Text;

            // date and heure ; 
            String dd= DateTime.Today.ToShortDateString().ToString();
            String hh = DateTime.Now.ToLocalTime().ToShortTimeString().ToString();

            // Connexion au Data Base 
            try
            {
                OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=../../../../database.accdb");
                OleDbCommand cmd = con.CreateCommand();
                con.Open(); 
                cmd.CommandText = "insert into intervention (type,description,priorité,service,num_inv,salle,add_by,date_inter,heure_inter,archive) values ('" + type_materiel + "','" + description+"','"+priorite+"','"+service_dept+"','"+num_inventaire+"','"+salle+"','"+id_user+"','"+dd+"','"+hh+"','0') ";
                cmd.Connection = con;
                int insert = cmd.ExecuteNonQuery();
                if(insert>0)
                {
                    MessageBox.Show(" Votre Intervention est bien réçu ");
                    // reset forms  
                    textBox1.Text = " ";
                    textBox2.Text = " ";
                    textBox3.Text = " ";
                    textBox4.Text = " ";
                    textBox4.Text = " ";
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                    checkBox3.Checked = false;
                    checkBox4.Checked = false;
                    checkBox5.Checked = false;
                    checkBox6.Checked = false;
                    checkBox7.Checked = false;
                    checkBox8.Checked = false; 
                }
                else
                {
                    MessageBox.Show(" Une erreur se produite lors d'ajout de votre intervention ");
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
        private String getPriorite()
        {
            // retourne la priorité de l'intervention 

            String priorite= " ";
            if(checkBox6.Checked)
            {
                priorite = "0" ; 
            }
            if (checkBox7.Checked)
            {
                priorite = "1";
            }
            if (checkBox8.Checked)
            {
                priorite = "2";
            }
            return priorite;

        }
        private String geTypeMaterial()
        {
            // routourne le type de matériel 
            String typemareriel=" ";
            if(checkBox1.Checked)
            {
                typemareriel = checkBox1.Text;
            }
            if (checkBox2.Checked)
            { 
                typemareriel += "-";
                typemareriel+=checkBox2.Text;
            }
            if (checkBox3.Checked)
            {
                typemareriel += "-";
                typemareriel += checkBox3.Text;
            }
            if (checkBox4.Checked)
            {
                typemareriel += "-";
                typemareriel += checkBox4.Text;
            }
            if (checkBox5.Checked)
            {
                typemareriel += "-";
                typemareriel += checkBox4.Text;
            }
            if(textBox1.Text!="")
            {
                typemareriel += "-";
                typemareriel += textBox1.Text; 
            }

            return typemareriel;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // logout 
            Application.Restart();

        }

       
    }
}
