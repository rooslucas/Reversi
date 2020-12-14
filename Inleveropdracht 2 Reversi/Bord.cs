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
        public int Speler = 1;

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
            ZetVeld(Lengte / 2 - 1, Breedte / 2 - 1, 1);
            ZetVeld(Lengte / 2, Breedte / 2, 1);
            ZetVeld(Lengte / 2, Breedte / 2 - 1, 2);
            ZetVeld(Lengte / 2 - 1, Breedte / 2, 2);
        }

        public bool BeurtValide(int x, int y)
        {
            // Controleer of de kleur leeg is
            if (Velden[x, y] == 0)
                return true;

            // Controleer of de zet geldig is
            // TODO: zet geldig controleren

            else
                return false;

        }
    }
}
