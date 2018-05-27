using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prestation
{
    public partial class Prestation : Form
    {

        // difinition des attribut mail (user) et  password 
        private String user ; // mail 
        private string pass; 

        public Prestation()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)

        {
            // recupération de l'email et le mot de passe
            user = mail.Text; //mail 
            pass = password.Text; 
            
            // les Tests de saisie 
            if (user == "" && pass != "")
            {
                MessageBox.Show("SVP saisissez votre E-mail");
            }
            if (user != "" && pass == "")
            {
                MessageBox.Show("SVP saisissez votre Mot de Passe");
            }
            if (user == "" && pass == "")
            {
                MessageBox.Show("SVP saisissez votre E-mail et Mot de Passe");
            }
            // Connexion au Data Base 
            try
            {
                OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=../../../../database.accdb");
                OleDbCommand cmd = con.CreateCommand();
                con.Open();
                Object[] meta = new object[10];
                cmd.CommandText = "SELECT * From users  where mail='" + mail.Text + "' and password='" + password.Text + "'";
                cmd.Connection = con;
                OleDbDataReader read = cmd.ExecuteReader();

                while(read.Read())
                {
                     
                        int n = read.GetValues(meta); // le nombre de ligne dans DB
                        // si il ya un  utilisateur avec mail et password saisie 
                        if(n>0)
                        {
                                //les tests sur le type de User authentifié 

                                if(meta[5].ToString()=="0")
                                {
                                        // demandeur 
                                        Demendeur d = new Demendeur(); 
                                       // passer les données aux fenetre 
                                        d.firstname= meta[1].ToString();
                                        d.lastname = meta[2].ToString();
                                        d.id_user = meta[0].ToString();
                                       // affichage 
                                                 d.Show();
                                                    Hide();
                                }
                                if (meta[5].ToString() == "1")
                                {
                                            // technicien 
                                            Technicien t = new Technicien();

                                                // passer les données aux fenetre 
                                                t.firstname = meta[1].ToString();
                                                t.lastname = meta[2].ToString();
                                                t.id_user = meta[0].ToString();
                                        
                                        // affichage    
                                        t.Show();
                                                Hide();
                                }
                                if (meta[5].ToString() == "2")
                                {
                                        // admin  
                                                Admin a = new Admin();
                                        // passer les données aux fenetre 
                                        a.firstname = meta[1].ToString();
                                        a.lastname = meta[2].ToString();
                                        a.id_user = meta[0].ToString();
                                        // affichage
                                        a.Show();
                                            Hide();
                                }
                         } 
                     
                }
                con.Close();
            }
            catch(System.Data.OleDb.OleDbException exp)
            {
                // cas de erreur lié a la conexion  a DB
                MessageBox.Show("Erreur de Connexion : "+ exp.ToString());
            }
 
        }

     
 
    }
}
