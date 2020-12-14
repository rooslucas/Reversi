using System.Windows.Forms;
using System.Drawing;

namespace Reversi
{
    class Reversi : Form
    {
        
        // Dit zijn alle buttons op het reversibord.
        Button[,] klik_veld;

        // Variabele om bord in op te slaan
        Bord bord;
        

        public Reversi()
        {
            bord = new Bord(6, 6); // Initalizeer het model

            this.ClientSize = new Size(bord.Lengte * 60, bord.Breedte * 60);
            this.Text = "Reversi";
            genereer_klikveld();
            
        }
        //Maakt klik_veld en voegt hem aan de Form toe
        void genereer_klikveld()
        {
            klik_veld = new Button[bord.Lengte, bord.Breedte];
            for (int i = 0; i < bord.Lengte; i++)
            {
                for (int j = 0; j < bord.Breedte; j++)
                {
                    klik_veld[i,j] = new Button();
                    klik_veld[i,j].Location = new Point(i * 60, j * 60);
                    klik_veld[i, j].Text = "";
                    klik_veld[i, j].Size = new Size(60, 60);
                    klik_veld[i, j].MouseClick += new MouseEventHandler(Beurt);
                    this.Controls.Add(klik_veld[i, j]);
                }
            }
            RenderBord();
        }

        //De buttons in klik_veld worden geupdate met de waardes in bord 
        void RenderBord()
        {
            // Graphics gr = pea.Graphics;
            Color[] kleur = { Color.White, Color.Blue, Color.Red };
            // SolidBrush[] steen = { new SolidBrush(Color.White), new SolidBrush(Color.Red), new SolidBrush(Color.Blue) };
            for (int i = 0; i < bord.Lengte; i++)
            {
                for (int j = 0; j < bord.Breedte; j++)
                {
                    klik_veld[i, j].BackColor = kleur[bord.Velden[i,j]];
                    //gr.FillEllipse(steen[Bord[i, j]], i, j, 10, 10);
                }
            }
        }

        // Speler die aan de beurt is plaatst steen van zijn kleur
        void Beurt(object sender, MouseEventArgs e)
        {
            Button b = (Button)sender;
            if (bord.Player % 2 != 0)
                b.BackColor = Color.Red;
            // Add BLAUW!

            else
                b.BackColor = Color.Blue;
            // Add ROOD!

            sender = (object)b;
            bord.Player += 1;

        }
    }
}
