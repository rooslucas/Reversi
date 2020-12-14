using System.Windows.Forms;
using System.Drawing;

namespace Reversi
{
    class Reversi : Form
    {
        
        // Dit zijn alle buttons op het reversibord.
        Button[,] KlikVelden;

        // Variabele om bord in op te slaan
        Bord bord;
        

        public Reversi()
        {
            bord = new Bord(6, 6); // Initalizeer het model

            this.ClientSize = new Size(bord.Lengte * 60, bord.Breedte * 60);
            this.Text = "Reversi";
            GenereerKlikveld();
            
        }
        //Maakt klik_veld en voegt hem aan de Form toe
        void GenereerKlikveld()
        {
            KlikVelden = new Button[bord.Lengte, bord.Breedte];
            for (int i = 0; i < bord.Lengte; i++)
            {
                for (int j = 0; j < bord.Breedte; j++)
                {
                    KlikVelden[i,j] = new Button();
                    KlikVelden[i,j].Location = new Point(i * 60, j * 60);
                    KlikVelden[i, j].Text = "";
                    KlikVelden[i, j].Size = new Size(60, 60);
                    KlikVelden[i, j].MouseClick += new MouseEventHandler(Beurt);
                    this.Controls.Add(KlikVelden[i, j]);
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
                    KlikVelden[i, j].BackColor = kleur[bord.Velden[i,j]];
                    //gr.FillEllipse(steen[Bord[i, j]], i, j, 10, 10);
                }
            }
        }

        // Speler die aan de beurt is plaatst steen van zijn kleur
        void Beurt(object sender, MouseEventArgs e)
        {
            Button b = (Button)sender;
            if (bord.Speler % 2 != 0)
                b.BackColor = Color.Red;
            // Add BLAUW!

            else
                b.BackColor = Color.Blue;
            // Add ROOD!

            sender = (object)b;
            bord.Speler += 1;

        }
    }
}
