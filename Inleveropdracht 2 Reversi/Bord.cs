using System;
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

        // Hier worden de berekeningen/tests mee gedaan. 
        // Bij waarde 0 is het stuk leeg, bij waarde 1 is het stuk Rood en bij waarde 2 is het stuk blauw.
        private int[,] Velden;

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

        // Controleer of de zet geldig is aan de hand van de regels
        public bool BeurtValide(int x, int y)
        {
            return Velden[x, y] == 0 && Aanliggend(x, y);
        }


        //Controleert of het stuk een aanliggend stuk heeft
        private bool Aanliggend(int x, int y)
        {
            bool aanliggend = false;
            int[] burenX = { 0, 1, 0, -1 };
            int[] burenY = { 1, 0, -1, 0 };
            for (int i=0;i<4;i++)
            {
                if (0<=x + burenX[i] && x+burenX[i]<Lengte && 0 <=y+burenY[i] && y +burenY[i]<Breedte)
                {
                    if (Velden[x+burenX[i],y+burenY[i]]!=0)
                    {
                        aanliggend = true;
                    }
                }
            }
            return aanliggend;
        }
    }
}
