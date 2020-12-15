﻿using System.Windows.Forms;
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
        

        public Reversi()
        {
            bord = new Bord(6, 6); // Initalizeer het model

            this.ClientSize = new Size(bord.Breedte * 60, bord.Lengte * 60);
            this.Text = "Reversi";
            GenereerKlikveld();
            
        }
        //Maakt klik_veld en voegt hem aan de Form toe
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

        //De buttons in klik_veld worden geupdate met de waardes in bord 
        void RenderBord()
        {
            Color[] kleur = { Color.White, Color.Blue, Color.Red };
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
                // Controleer wie er aan de beurt is en verander de steen van kleur
                if (bord.Speler == 1)
                {
                    bord.ZetVeld(x, y, 1);
                    bord.Speler = 2;
                    bord.TegenSpeler = 1;
                    // Text --> rood is aan de beurt
                }
                else if (bord.Speler == 2)
                {
                    bord.ZetVeld(x, y, 2);
                    bord.Speler = 1;
                    bord.TegenSpeler = 2;
                    // Text --> blauw is aan de beurt
                }
            }

            // else : deze zet is niet geldig
            
            RenderBord();

        }
    }
}
