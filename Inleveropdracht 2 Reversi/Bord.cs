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
            
            // Controleer of de kleur leeg is
            if (Velden[x, y] == 0)
            {
                if (Aanliggend(x, y))
                    return true;
                else return false;
            }

            else
                return false;

        }

        private bool Aanliggend(int x, int y)
        {
            bool boven, onder, links, rechts, rechtsboven, linksboven, rechtsonder, linksonder;

            // Controleer of tegenspeler ernaast ligt door te controleren of een plek ernaast de juiste kleur heeft
            boven = false;
            onder = false;
            links = false;
            rechts = false;
            rechtsboven = false;
            linksboven = false;
            rechtsonder = false;
            linksonder = false;

            if (x == 0)
            {
                rechts = Velden[x + 1, y] == TegenSpeler;

                if (y == 0)
                {
                    onder = Velden[x, y + 1] == TegenSpeler;
                    rechtsonder = Velden[x + 1, y + 1] == TegenSpeler;
                }

                else if (y > 0 && y < (Lengte - 1))
                {
                    boven = Velden[x, y - 1] == TegenSpeler;
                    onder = Velden[x, y + 1] == TegenSpeler;
                    rechtsboven = Velden[x + 1, y - 1] == TegenSpeler;
                    rechtsonder = Velden[x + 1, y + 1] == TegenSpeler;
                }

                else if (y == (Lengte - 1))
                {
                    boven = Velden[x, y - 1] == TegenSpeler;
                    rechtsboven = Velden[x + 1, y - 1] == TegenSpeler;
                }
            }

            else if (x > 0 && x < (Breedte - 1))
            {
                links = Velden[x - 1, y] == TegenSpeler;
                rechts = Velden[x + 1, y] == TegenSpeler;

                if (y == 0)
                {
                    onder = Velden[x, y + 1] == TegenSpeler;
                    rechtsonder = Velden[x + 1, y + 1] == TegenSpeler;
                    linksonder = Velden[x - 1, y + 1] == TegenSpeler;
                }

                if (y > 0 && y < (Lengte - 1))
                {
                    boven = Velden[x, y - 1] == TegenSpeler;
                    onder = Velden[x, y + 1] == TegenSpeler;
                    rechtsboven = Velden[x + 1, y - 1] == TegenSpeler;
                    linksboven = Velden[x - 1, y - 1] == TegenSpeler;
                    rechtsonder = Velden[x + 1, y + 1] == TegenSpeler;
                    linksonder = Velden[x - 1, y + 1] == TegenSpeler;
                }

                if (y == (Lengte - 1))
                {
                    boven = Velden[x, y - 1] == TegenSpeler;
                    rechtsboven = Velden[x + 1, y - 1] == TegenSpeler;
                    linksboven = Velden[x - 1, y - 1] == TegenSpeler;
                }
            }

            else if (x == (Breedte - 1))
            {
                links = Velden[x - 1, y] == TegenSpeler;

                if (y == 0)
                {
                    onder = Velden[x, y + 1] == TegenSpeler;
                    linksonder = Velden[x - 1, y + 1] == TegenSpeler;
                }

                else if (y > 0 && y < (Lengte - 1))
                {
                    boven = Velden[x, y - 1] == TegenSpeler;
                    onder = Velden[x, y + 1] == TegenSpeler;
                    linksboven = Velden[x - 1, y - 1] == TegenSpeler;
                    linksonder = Velden[x - 1, y + 1] == TegenSpeler;
                }

                else if (y == (Lengte - 1))
                {
                    boven = Velden[x, y - 1] == TegenSpeler;
                    linksboven = Velden[x - 1, y - 1] == TegenSpeler;
                }
            }

            if (boven || onder || links || rechts || rechtsboven || linksboven || rechtsonder || linksonder)
                return true;

            else return false;
        }
    }
}
