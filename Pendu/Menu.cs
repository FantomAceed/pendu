using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pendu
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Visible = true;
            label2.Visible = true;
            label1.Text = "Pseudo du joueur 1 :";
            adapte_Label(label1);
            panel1.Location = new System.Drawing.Point(0, 107);
            this.Height = 259;
            this.Refresh();
        }

        private void adapte_Label(Label label)
        {
            label.Location = new System.Drawing.Point((this.Width / 2) - (label.Width / 2) - 8, 26);
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            textBox2.Visible = false;
            label2.Visible = false;
            radioButton1.Checked = true;
            panel1.Location = new System.Drawing.Point(0, 65);
            this.Height = 226;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Visible = false;
            label2.Visible = false;
            label1.Text = "Votre pseudo :";
            adapte_Label(label1);
            panel1.Location = new System.Drawing.Point(0, 65);
            this.Height = 226;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.Hide();
            //Application.Run(new Form1());
            Form1 UnJoueur = new Form1();
            UnJoueur.Show();
            this.Hide();
        }
    }
}
