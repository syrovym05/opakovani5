using System.Text;

namespace _18._9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();
            this.Text = "ukol 5";
            this.ShowIcon = false;

            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".txt";
            ofd.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);

            double vek;
            int soucet = 0;
          
            
            List<string> jmena = new List<string>();
            List<int> znamky = new List<int>();
            List<long> rodne_cisla = new List<long>();


            if (ofd.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.Clear();
                string nazev = ofd.FileName;
                StreamReader sr = new StreamReader(nazev, Encoding.UTF8);
                int i = 0;
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine();
                    listBox1.Items.Add(s);
                    string[] pole = s.Split(';');

                    jmena.Add(pole[0]);
                    znamky.Add(Convert.ToInt32(pole[1]));
                    rodne_cisla.Add(Convert.ToInt64(pole[2]));

                    soucet += znamky[i];           
                    i++;                    
                }
                sr.Close();

                double prumer = ((double)soucet / (double)i);
                prumer = Math.Round(prumer, 2);

                StreamWriter sw = new StreamWriter(nazev, false);

                for(int j = 0; j < jmena.Count;j++)
                {
                    string mesic = Vek(rodne_cisla[j].ToString(), out vek);
                    listBox2.Items.Add("Mesic: " + mesic+ " - Vek: " + vek);
                    //MessageBox.Show(mesic);
                    if (mesic == "Prosinec" && label1.Text == "") label1.Text = "Prvni clovek narozeny v prosinci je: " + jmena[j];

                    sw.WriteLine(jmena[j] + ";" + znamky[j] + ";" + rodne_cisla[j] + ";" + vek);                   
                }
            
                sw.WriteLine("\nPrumer: " + prumer.ToString());
                sw.Close();

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);

                if (sfd.ShowDialog() == DialogResult.OK && sfd.FileName != "")
                {
                    string file = sfd.FileName;

                    StreamWriter swr = new StreamWriter(file, false);
                    int l = 0;
                    foreach(int znamka in znamky)
                    {
                        if(znamka < 3)
                        {
                            string mesic = Vek(rodne_cisla[l].ToString(), out vek);
                            string s = jmena[l] + ";" + vek + ";" + mesic;
                            swr.WriteLine(s);
                            listBox3.Items.Add(s);

                        }
                        l++;
                    }                        
                }

              
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
          
            switch (mes)
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

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}