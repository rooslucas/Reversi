using System.Windows.Forms;
using System.Drawing;
using System;

namespace Reversi
{
    class Reversi : Form
    {
        
        // Dit zijn alle buttons op het reversibord.
        Button[,] KlikVelden;

        // Variabele om bord in op te slaan
        Bord bord;

        // Dit zijn de tekst en buttons die op onder het bord komen te staan
        Label aanzet, blauw, rood;
        Button help, nieuwspel;

        // Controleert of er geklikt is
        bool geklikt = false;


        public Reversi()
        {
            Font lettertype;
            bord = new Bord(6, 6); // Initalizeer het model

            aanzet = new Label();
            blauw = new Label();
            rood = new Label();
            help = new Button();
            nieuwspel = new Button();
            lettertype = new Font("Microsoft Sans Serif", 10);

            // Laat zien wie er begint
            aanzet.Location = new Point(30, bord.Lengte * 60 + 50);
            aanzet.Size = new Size(120, 35);
            aanzet.Text = "Blauw begint";
            aanzet.ForeColor = Color.Navy;
            aanzet.Font = lettertype;
            this.Controls.Add(aanzet);

            // Laat het aantal stenen per speler zien
            blauw.Location = new Point(205, bord.Lengte * 60 + 50);
            blauw.Size = new Size(150, 20);
            blauw.Text = "2 stenen";
            blauw.Font = lettertype;
            rood.Location = new Point(205, bord.Lengte * 60 + 70);
            rood.Size = new Size(150, 20);
            rood.Text = "2 stenen";
            rood.Font = lettertype;
            this.Controls.Add(blauw);
            this.Controls.Add(rood);
            this.Paint += this.TekenStenen;

            // Toon de help en nieuwspel button
            help.Location = new Point(205, bord.Lengte * 60 + 120);
            help.Size = new Size(80, 30);
            help.Text = "Help";
            help.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
            help.BackColor = Color.LavenderBlush;
            help.Click += this.Help; 
            nieuwspel.Size = new Size(80, 30);
            nieuwspel.Location = new Point(75, bord.Lengte * 60 + 120);
            nieuwspel.Text = "Nieuw Spel";
            nieuwspel.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
            nieuwspel.BackColor = Color.LavenderBlush;
            nieuwspel.Click += this.Herstart;
            this.Controls.Add(help);
            this.Controls.Add(nieuwspel);

            // Maak de spelomgeving
            this.ClientSize = new Size(bord.Breedte * 60, bord.Lengte * 60 + 200);
            this.BackColor = Color.AliceBlue;
            this.Text = "Reversi";
            
            GenereerKlikveld();
            
        }

        // Tekent de stenen in de spelomgeving
        void TekenStenen(object sender, PaintEventArgs pea)
        {
            Graphics gr = pea.Graphics;

            gr.FillRectangle(Brushes.Navy, 180, bord.Lengte * 60 + 50, 18, 18);
            gr.FillRectangle(Brushes.Crimson, 180, bord.Lengte * 60 + 70, 18, 18);

        }

        // Maakt de klik velden en voegt hem aan de Form toe
        void GenereerKlikveld()
        {
            KlikVelden = new Button[bord.Breedte, bord.Lengte];
            for (int x = 0; x < bord.Breedte; x++)
            {
                for (int y = 0; y < bord.Lengte; y++)
                {
                    Button button = new Button();
                    button.Location = new Point(x * 60, y * 60);
                    button.Text = "";
                    button.Size = new Size(60, 60);
                    button.MouseClick += new MouseEventHandler(Beurt);

                    // Maak een tag zodat deze later gebruikt kan worden om een zet te doen
                    button.Tag = $"{x} {y}";
                    this.KlikVelden[x, y] = button;
                    this.Controls.Add(KlikVelden[x, y]);
                }
            }
            RenderBord();
        }

        // De buttons in klikvelden worden gkleurd met de waardes van het veld 
        void RenderBord()
        {
            Color[] kleur = { Color.White, Color.Navy, Color.Crimson, Color.Silver };
            for (int x = 0; x < bord.Breedte; x++)
            {
                for (int y = 0; y < bord.Lengte; y++)
                {
                    KlikVelden[x, y].BackColor = kleur[bord.KrijgVeld(x,y)];
                }
            }
        }

        // Speler die aan de beurt is plaatst steen van zijn kleur
        void Beurt(object sender, MouseEventArgs e)
        {
            int x, y;
            string [] tag;
            Button b = (Button)sender;

            // Gebruik de eerder gemaakte tag om te kijken waar de zet gedaan wordt
            tag = (b.Tag.ToString()).Split(' ');
            x = int.Parse(tag[0]);
            y = int.Parse(tag[1]);

            // Controleer of de zet valide is
            if (bord.BeurtValide(x, y))
            {
                bord.Velden = bord.Update.nieuwVeld;

                // Controleer wie er aan de beurt is
                if (bord.Speler == 1)
                {
                    bord.Speler = 2;
                    this.aanzet.Text = "Rood is aan zet";
                    this.aanzet.ForeColor = Color.Crimson;
                }
                else if (bord.Speler == 2)
                {
                    bord.Speler = 1;
                    this.aanzet.Text = "Blauw is aan zet";
                    this.aanzet.ForeColor = Color.Navy;
                }

                // Controleer of het spel afgelopen is
                if (bord.Einde())
                {
                    this.aanzet.Text = $"{bord.Winnaar()} HEEFT GEWONNEN";
                    this.aanzet.ForeColor = Color.Black;
                    this.aanzet.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
 
                }
            }

            else if(bord.Einde())
            {
                this.aanzet.Text = $"{bord.Winnaar()} heeft echt gewonnen...";
            }

            // Controleer of het spel is afgelopen
            else
                this.aanzet.Text = $"DEZE ZET IS NIET GELDIG";

            // Toon het aantal stenen
            bord.AantalStenen();
            this.blauw.Text = $"{bord.bstenen} stenen";
            this.rood.Text = $"{bord.rstenen} stenen";

            // Controleer of de help functie aan staat
            if (geklikt)
                bord.HelpSpeler(1);
            
            RenderBord();
        }

        // Start een nieuw spel op
        void Herstart(object sender, EventArgs e)
        {
            Application.Restart();
        }

        // Helpt met welke zetten mogelijk zijn
        void Help(object sender, EventArgs e)
        {
            if (!geklikt)
            {
                bord.HelpSpeler(1);
                geklikt = true;
            }

            else
            {
                bord.HelpSpeler(0);
                geklikt = false;
            }

            RenderBord();
        }
    }
}
