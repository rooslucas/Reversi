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
        public int Player = 1;

        // Hier worden de berekeningen/tests mee gedaan. 
        // Bij waarde 0 is het stuk leeg, bij waarde 1 is het stuk Rood en bij waarde 2 is het stuk blauw.
        public int[,] Velden;

        // Maakt een leeg bord met in het midden 4 stukken van beide partijen.
        public Bord(int Lengte, int Breedte)
        {
            this.Lengte = Lengte;
            this.Breedte = Breedte;
            this.Velden = new int[Lengte, Breedte];
            for (int i = 0; i < Lengte; i++)
            {
                for (int j = 0; j < Breedte; j++)
                    Velden[i, j] = 0;
            }
            Velden[Lengte / 2 - 1, Breedte / 2 - 1] = 1;
            Velden[Lengte / 2, Breedte / 2] = 1;
            Velden[Lengte / 2, Breedte / 2 - 1] = 2;
            Velden[Lengte / 2 - 1, Breedte / 2] = 2;
        }

        void BeurtValide()
        {
            // TODO kleur leeg
            // TODO: Plek valide
        }
    }
}
