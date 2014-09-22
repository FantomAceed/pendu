using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Pendu
{
    public partial class Form1 : Form
    {
        private string answer_accent;
        private string answer_capital;
        private int compteur;
        private int compteur2 = 7;
        private int count;
        private string[] répertoire;
        public Form1()
        {
            InitializeComponent();
        }

        private void adapte_Label()
        {
            int X = label1.Width;
            label1.Location = new System.Drawing.Point(208 - (X / 2), 21);
        }

        private void Afficher_Label()
        {
            char[] tabl = new char[answer_capital.Length];
            char a = answer_capital[0];
            char z = answer_capital[answer_capital.Length - 1];

            for (int i = 1; i < (answer_capital.Length - 1); i++)
            {
                if (answer_capital[i] == '-')
                {
                    tabl[i] = '-';
                }
                else if (answer_capital[i] == ' ')
                {
                    tabl[i] = ' ';
                }
                else if (answer_capital[i] == '\'')
                {
                    tabl[i] = '\'';
                }
                else
                {
                    tabl[i] = '_';
                }
            }
            char[] t = new char[tabl.Length * 2];
            for (int i = 0; i < tabl.Length * 2; i++)
            {
                if (i % 2 == 0)
                {
                    char currentchar = tabl[i / 2];
                    t[i] = currentchar;
                }
                else
                {
                    t[i] = '\u00a0';
                }
            }

            t[0] = a;
            t[(tabl.Length * 2) - 2] = z;
            label1.Text = new string(t);
            adapte_Label();
            this.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ValiderFermeture(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Redraw();
            Select_Word(false);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Redraw();
            Select_Word(true);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (ConvertirChaineSansAccentFOR(textBox2.Text.ToUpper()) == answer_capital)
            {
                textBox2.Text = "";
                dévoiler();
                int msg2 = (int)MessageBox.Show(string.Format("Bravo vous avez découvert le mot qui était : {0}", answer_accent), "Bravo !", MessageBoxButtons.OK);
            }
            else
            {
                richTextBox1.Text += textBox2.Text + Environment.NewLine;
                textBox2.Text = "";
                compteur2--;
                label8.Text = compteur2.ToString();
                if (compteur2 == 0)
                {
                    pictureBox1.Image = Image.FromFile("10.png");
                    button6.Enabled = false;
                    button4.Enabled = false;
                    label6.Enabled = true;
                    this.Refresh();
                    int msg = (int)MessageBox.Show("Vous êtes pendu !", "Pendu !", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button6.Enabled = false;
            button4.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            label5.Text = string.Format("Solution dévoilée : {0}", answer_accent);
            int X = label5.Width;
            label5.Location = new System.Drawing.Point(208 - (X / 2), 9);
            label5.Visible = true;
            dévoiler();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string comparateur = ConvertirChaineSansAccentFOR(textBox1.Text.ToUpper());
            char l = char.Parse(comparateur);
            int k = 0;
            char[] transfert = new char[label1.Text.Length];
            for (int j = 0; j < (label1.Text.Length); j++)
            {
                transfert[j] = label1.Text[j];
                if (j % 2 == 0)
                {
                    if (answer_capital[j / 2] == l && label1.Text[j] == '_')
                    {
                        transfert[j] = answer_capital[j / 2];
                        k++;
                    }
                }
            }
            if (k >= 1)
            {
                label1.Text = new string(transfert);
                adapte_Label();
                this.Refresh();
                int msg = (int)MessageBox.Show(string.Format("Vous avez découvert {0} lettre(s) !", k), "Bien joué !", MessageBoxButtons.OK);
                textBox1.Text = "";
            }
            else
            {
                compteur++;
                if (compteur == 10)
                {
                    label7.Text += comparateur + " ";
                    button6.Enabled = false;
                    button4.Enabled = false;
                    label6.Enabled = true;
                    pictureBox1.Image = Image.FromFile(String.Format("{0}.png", compteur));
                    this.Refresh();
                    int msg = (int)MessageBox.Show("Vous êtes pendu !", "Pendu !", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                if (compteur < 6 || compteur > 6)
                {
                    label7.Text += comparateur + " ";
                }
                if (compteur == 6)
                {
                    label7.Text += Environment.NewLine + comparateur + " ";
                }
                textBox1.Text = "";
                pictureBox1.Image = Image.FromFile(String.Format("{0}.png", compteur));
                this.Refresh();
            }
            if (label1.Text.IndexOf("_") == -1)
            {
                int msg2 = (int)MessageBox.Show(string.Format("Bravo vous avez découvert le mot qui était : {0}", answer_accent), "Bravo !", MessageBoxButtons.OK);
            }
        }

        private string ConvertirChaineSansAccentFOR(string libelle)
        {
            char[] oldChar = { 'À', 'Á', 'Â', 'Ã', 'Ä', 'Å', 'à', 'á', 'â', 'ã', 'ä', 'å', 'Ò', 'Ó', 'Ô', 'Õ', 'Ö', 'Ø', 'ò', 'ó', 'ô', 'õ', 'ö', 'ø', 'È', 'É', 'Ê', 'Ë', 'è', 'é', 'ê', 'ë', 'Ì', 'Í', 'Î', 'Ï', 'ì', 'í', 'î', 'ï', 'Ù', 'Ú', 'Û', 'Ü', 'ù', 'ú', 'û', 'ü', 'ÿ', 'Ñ', 'ñ', 'Ç', 'ç', '°' };
            char[] newChar = { 'A', 'A', 'A', 'A', 'A', 'A', 'a', 'a', 'a', 'a', 'a', 'a', 'O', 'O', 'O', 'O', 'O', 'O', 'o', 'o', 'o', 'o', 'o', 'o', 'E', 'E', 'E', 'E', 'e', 'e', 'e', 'e', 'I', 'I', 'I', 'I', 'i', 'i', 'i', 'i', 'U', 'U', 'U', 'U', 'u', 'u', 'u', 'u', 'y', 'N', 'n', 'C', 'c', ' ' };
            for (int i = 0; i < oldChar.Length; i++)
            {
                libelle = libelle.Replace(oldChar[i], newChar[i]);
            }
            return libelle;
        }

        private void dévoiler()
        {
            char[] tabl = new char[answer_capital.Length];
            for (int i = 0; i < (answer_capital.Length); i++)
            {
                tabl[i] = answer_capital[i];
            }
            char[] t = new char[tabl.Length * 2];
            for (int i = 0; i < tabl.Length * 2; i++)
            {
                if (i % 2 == 0)
                {
                    char currentchar = tabl[i / 2];
                    t[i] = currentchar;
                }
                else
                {
                    t[i] = '\u00a0';
                }
            }

            label1.Text = new string(t);
            adapte_Label();
            this.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.MinimizeBox = true;
            //this.ControlBox = false;
            //button5.Enabled = false;
            label6.Visible = false;
            label8.Text = compteur2.ToString();
            label7.Text = "";
            label5.Visible = false;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = Image.FromFile("0.png");

            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.textBox1, "Soumettre une lettre");
            System.Windows.Forms.ToolTip ToolTip2 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.textBox2, "Soumettre un mot");

            string line;
            int counter = 0;
            StreamReader reader = new StreamReader("Répertoire");
            while ((reader.ReadLine()) != null)
            {
                counter++;
            }
            reader.Close();
            répertoire = new string[counter];
            StreamReader reader2 = new StreamReader("Répertoire");
            for (int i = 0; i < counter; i++)
            {
                line = reader2.ReadLine();
                répertoire[i] = line;
            }
            reader2.Close();

            //string json = File.ReadAllText("Répertoire.json", Encoding.UTF8);
            Select_Word(true);
            adapte_Label();
        }

        private void Redraw()
        {
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            label7.Text = "";
            label5.Visible = false;
            label6.Visible = false;
            button4.Enabled = false;
            button6.Enabled = false;
            label8.Text = compteur2.ToString();
            textBox1.Text = "";
            textBox2.Text = "";
            pictureBox1.Image = Image.FromFile("0.png");
            richTextBox1.Text = "";
            compteur = 0;
            compteur2 = 7;
        }

        private void SelectRightWord()
        {
            while (répertoire[count].Length <= 4 || répertoire[count].Length > 16)
            {
                if (count == 0)
                {
                    count = répertoire.Length - 1;
                }
                else
                {
                    count--;
                }
            }
        }

        private void Select_Word(bool random)
        {
            if (random)
            {
                Random rnd = new Random();
                count = rnd.Next(0, répertoire.Length);
                //Fonction possible
                SelectRightWord();
                /*while (répertoire[count].Length <= 4 || répertoire[count].Length > 16)
                {
                    if (count == 0)
                    {
                        count = répertoire.Length - 1;
                    }
                    else
                    {
                        count--;
                    }
                }*/
            }
            else
            {
                if (count == répertoire.Length - 1)
                {
                    count = 0;
                    // Fonction possible
                    SelectRightWord();
                    /*while (répertoire[count].Length <= 4 || répertoire[count].Length > 16)
                    {
                        if (count == 0)
                        {
                            count = répertoire.Length - 1;
                        }
                        else
                        {
                            count--;
                        }
                    }*/
                }
                else
                {
                    count++;
                    while (répertoire[count].Length <= 4 || répertoire[count].Length > 16)
                    {
                        if (count == répertoire.Length - 1)
                        {
                            count = 0;
                        }
                        else
                        {
                            count++;
                        }
                    }
                }
            }

            answer_accent = répertoire[count][0].ToString().ToUpper() + répertoire[count].Substring(1).ToLower();
            answer_capital = ConvertirChaineSansAccentFOR(répertoire[count].ToUpper());
            Afficher_Label();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length < 2 && textBox1.Text.Length > 0)
            {
                button6.Enabled = true;
            }
            if (textBox1.Text.Length > 1 || textBox1.Text.Length == 0)
            {
                button6.Enabled = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 1)
            {
                button4.Enabled = true;
            }
            if (textBox2.Text.Length <= 1)
            {
                button4.Enabled = false;
            }
        }

        private void ValiderFermeture()
        {
            int msg = (int)MessageBox.Show("Etes-vous sûr de vouloir retourner au menu ? Le mot en cours ne sera pas comptabilisé. Si vous cliquez non, vous quittez le jeu.", "Retourner au menu", MessageBoxButtons.YesNoCancel);
            switch (msg)
            {
                case 6:
                    Menu menu = new Menu();
                    this.Close();
                    menu.ShowDialog();
                    return;
                case 2:
                    return;

            }
            Application.Exit();
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //ValiderFermeture();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}