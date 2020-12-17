﻿using System;
using System.Windows.Forms;
using System.Drawing;

namespace Reversi
{
    class Bord
    {
        // Lengte en breedte van het bord. Zou door de gebruiker kunnen worden aangepast
        public int Lengte;
        public int Breedte;

        // Variabele om te bepalen wie er aan de beurt is
        // Blauw is speler 1 en rood is speler 2
        public int Speler = 1;
        public int TegenSpeler = 2;
        public int bstenen, rstenen;

        // Hier worden de berekeningen/tests mee gedaan. 
        // Bij waarde 0 is het stuk leeg, bij waarde 1 is het stuk Rood en bij waarde 2 is het stuk blauw.
        public int[,] Velden;

        public struct Bord_Update
        {
            public int tegelflip;
            public int[,] nieuwVeld;
        }
        public Bord_Update update;

        public int KrijgVeld(int x, int y)
        {
            return Velden[x, y];
        }

        public void ZetVeld(int x, int y, int speler)
        {
            if (speler == 0 || speler == 1 || speler == 2)
                Velden[x, y] = speler;
        }

        // Maakt een leeg bord met in het midden 4 stukken van beide partijen.
        public Bord(int Breedte, int Lengte)
        {
            this.Lengte = Lengte;
            this.Breedte = Breedte;
            this.Velden = new int[Breedte, Lengte];
            for (int x = 0; x < Breedte; x++)
            {
                for (int y = 0; y < Lengte; y++)
                    ZetVeld(x, y, 0);
            }
            ZetVeld(Breedte / 2 - 1, Lengte / 2 - 1, 1);
            ZetVeld(Breedte / 2, Lengte / 2, 1);
            ZetVeld(Breedte / 2 - 1, Lengte / 2, 2);
            ZetVeld(Breedte / 2, Lengte / 2 - 1, 2);
        }

        // Tel het aantal stenen voor beide kleuren
        public void AantalStenen()
        {
            int x, y;
            bstenen = 0;
            rstenen = 0;

            for (x = 0; x < Breedte; x++)
            {
                for (y = 0; y < Lengte; y++)
                {
                    if (Velden[x, y] == 1)
                        bstenen += 1;
                    if (Velden[x, y] == 2)
                        rstenen += 1;

                }
            }
        }

        // Controleer of de zet geldig is aan de hand van de regels
        public bool BeurtValide(int x, int y)
        {
            return Velden[x, y] == 0 && Zet(x,y).tegelflip!=0;
        }


        //Controleert of het stuk een aanliggend stuk heeft
        //Bij het officiële spel mag je stukken niet schuin aanleggen. Deze methode is daarvoor geschreven, maar wordt nooit aangeroepen omdat het spel dat we maken zich niet aan de officiele regels houdt 
        private bool Aanliggend(int x, int y)
        {
            bool aanliggend = false;
            int[] burenX = { 0, 1, 0, -1 };
            int[] burenY = { 1, 0, -1, 0 };
            for (int i=0;i<4;i++)
            {
                if (0<=x + burenX[i] && x+burenX[i]<Breedte && 0 <=y+burenY[i] && y +burenY[i]<Lengte)
                {
                    if (Velden[x+burenX[i],y+burenY[i]]!=0)
                        aanliggend = true;
                }
            }
            return aanliggend;
        }
        public Bord_Update Zet(int x, int y)
        {
            update.tegelflip = 0;
            update.nieuwVeld = new int[Breedte, Lengte];
            for (int i=0;i<Lengte;i++)
            { 
                for (int j=0;j<Breedte;j++)
                    update.nieuwVeld[i,j] = Velden[i,j];
            }
            int[] burenX = { 1, 1, 1, 0, 0, -1, -1, -1 };
            int[] burenY = { 1, 0, -1, 1, -1, 1, 0, -1 };
            for (int direction=0;direction<8;direction++)
            {
                for (int distance=1;distance<Lengte;distance++)
                {
                    if (0 <= x + burenX[direction] * distance && x + burenX[direction] * distance < Breedte && 0 <= y + burenY[direction] * distance && y + burenY[direction] * distance < Lengte)
                    {
                        if (Velden[x + burenX[direction] * distance, y + burenY[direction] * distance] == 0)
                            distance = Lengte;
                        else if (Velden[x + burenX[direction] * distance, y + burenY[direction] * distance] == Speler)
                        {
                            for (int i=1;i<distance;i++)
                            {
                                if (Velden[x + burenX[direction] * i, y + burenY[direction] * i] != Speler)
                                {
                                    update.tegelflip++;
                                    update.nieuwVeld[x + burenX[direction] * i, y + burenY[direction] * i] =Speler;
                                }
                                    
                            }
                        }
                    }
                }
            }
            update.nieuwVeld[x, y] = Speler;
            return update;
        }

        public string Winnaar()
        {
            if (bstenen > rstenen)
                return "BLAUW HEEFT GEWONNEN";
            else if (bstenen < rstenen)
                return "ROOD HEEFT GEWONNEN";
            else return "HET IS GELIJKSPEL";
        }
    }
}
