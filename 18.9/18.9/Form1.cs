using System.Text;

namespace _18._9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();
            this.Text = "ukol";
            this.ShowIcon = false;

            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".txt";           
            double vek;

            int soucet = 0;
            int pocet =0;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.Clear();
                string nazev = ofd.FileName;
                StreamReader sr = new StreamReader(nazev, Encoding.UTF8);
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine();
                    listBox1.Items.Add(s);
                    string[] pole = s.Split(';');

                    soucet +=Convert.ToInt32(pole[1]);
                    pocet++;

                    listBox2.Items.Add("Mesic: "+Vek(pole[2], out vek) + " - Vek: " + vek);
                }
                sr.Close();

                double prumer = (soucet / pocet);
                prumer = Math.Round(prumer, 1);

                StreamWriter sw = new StreamWriter(nazev, true);
                sw.WriteLine("\n" + prumer.ToString());
                sw.Close();
            }


           
        }

        string Vek(string x, out double vek)
        {
            int cislo = int.Parse(x) / 10000;

            int den = cislo % 100;
            cislo /= 100;
            int mes = cislo % 100;
            if (mes > 50)
            {
                mes -= 50;
            }
            int rok =2000 + cislo /100;

            DateTime narozeni = new DateTime(rok, mes, den);
            vek =Math.Round((DateTime.Now - narozeni).TotalDays / 365,2);
            
            switch(mes)
            {
                case 1: return "Leden";
                case 2: return "Únor";
                case 3: return "Bøezen";
                case 4: return "Duben";
                case 5: return "Kvìten";
                case 6: return "Èerven";
                case 7: return "Èervenec";
                case 8: return "Srpen";
                case 9: return "Záøí";
                case 10: return "Øíjen";
                case 11: return "Listopad";
                case 12: return "Prosinec";
                default: return "";
            }
        }

    }
}