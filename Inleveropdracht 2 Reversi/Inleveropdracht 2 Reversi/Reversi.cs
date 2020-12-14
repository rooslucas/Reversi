using System.Windows.Forms;
using System.Drawing;

namespace Reversi
{
    class Reversi : Form
    {
        // Lengte en breedte van het bord. Zou door de gebruiker kunnen worden aangepast
        int Lengte = 6;
        int Breedte = 6;

        // Variabele om te bepalen wie er aan de beurt is
        int Player = 1;
        
        // Dit zijn alle buttons op het reversibord.
        Button[,] klik_veld;
        
        // Hier worden de berekeningen/tests mee gedaan. 
        // Bij waarde 0 is het stuk leeg, bij waarde 1 is het stuk Rood en bij waarde 2 is het stuk blauw.
        int[,] Bord;

        public Reversi()
        {
            this.ClientSize = new Size(Lengte * 60, Breedte * 60);
            this.Text = "Reversi";
            Bord = start_bord(Lengte,Breedte);
            this.Paint += this.genereer_klikveld;
            
        }
        //Maakt klik_veld en voegt hem aan de Form toe
        void genereer_klikveld(object sender, PaintEventArgs pea)
        {
            klik_veld = new Button[Lengte,Breedte];
            for (int i = 0; i < Lengte; i++)
            {
                for (int j = 0; j < Breedte; j++)
                {
                    klik_veld[i,j] = new Button();
                    klik_veld[i,j].Location = new Point(i * 60, j * 60);
                    klik_veld[i, j].Text = "";
                    klik_veld[i, j].Size = new Size(60, 60);
                    klik_veld[i, j].MouseClick += new MouseEventHandler(Beurt);
                    this.Controls.Add(klik_veld[i, j]);
                }
            }
            RenderBord(Bord, pea);
        }

        // Maakt een leeg bord met in het midden 4 stukken van beide partijen.
        static int[,] start_bord(int Lengte,int Breedte)
        {
            int[,] start = new int[Lengte, Breedte];
            for (int i = 0; i < Lengte; i++)
            {
                for (int j = 0; j < Breedte; j++)
                    start[i, j] = 0;
            }
            start[Lengte / 2 - 1, Breedte / 2 - 1] = 1;
            start[Lengte / 2, Breedte / 2] = 1;
            start[Lengte / 2, Breedte / 2 - 1] = 2;
            start[Lengte / 2 - 1, Breedte / 2] = 2;
            return start;
        }

        //De buttons in klik_veld worden geupdate met de waardes in bord 
        void RenderBord(int[,] Bord, PaintEventArgs pea)
        {
            Graphics gr = pea.Graphics;
            Color[] kleur = { Color.White, Color.Blue, Color.Red };
            SolidBrush[] steen = { new SolidBrush(Color.White), new SolidBrush(Color.Red), new SolidBrush(Color.Blue) };
            for (int i = 0; i < Lengte; i++)
            {
                for (int j = 0; j < Breedte; j++)
                {
                    klik_veld[i, j].BackColor = kleur[Bord[i,j]];
                    //gr.FillEllipse(steen[Bord[i, j]], i, j, 10, 10);
                }
            }
        }

        // Speler die aan de beurt is plaatst steen van zijn kleur
        void Beurt(object sender, MouseEventArgs e)
        {
            Button b = (Button)sender;
            if (Player % 2 != 0)
                b.BackColor = Color.Red;
            // Add BLAUW!

            else
                b.BackColor = Color.Blue;
            // Add ROOD!

            sender = (object)b;
            Player += 1;

        }

        void BeurtValide()
        {
            // TODO kleur leeg
            // TODO: Plek valide
        }
    }
}
