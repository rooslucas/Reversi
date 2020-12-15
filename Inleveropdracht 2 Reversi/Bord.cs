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
            ZetVeld(Lengte / 2 - 1, Breedte / 2 - 1, 1);
            ZetVeld(Lengte / 2, Breedte / 2, 1);
            ZetVeld(Lengte / 2, Breedte / 2 - 1, 2);
            ZetVeld(Lengte / 2 - 1, Breedte / 2, 2);
        }

        // Controleer of de zet geldig is aan de hand van de regels
        public bool BeurtValide(int x, int y)
        {
            bool aanliggend, boven, onder, links, rechts, rechtsboven, linksboven, rechtsonder, linksonder;
            
            // Controleer of de kleur leeg is
            if (Velden[x, y] == 0)
            {
                // Controleer of tegenspeler ernaast ligt door te controleren of een plek ernaast de juiste kleur heeft
                aanliggend = false;

                if (x == 0)
                {
                    rechts = Velden[x + 1, y] == TegenSpeler;

                    if (y == 0)
                    {
                        onder = Velden[x, y + 1] == TegenSpeler;
                        rechtsonder = Velden[x + 1, y + 1] == TegenSpeler;

                        if (onder || rechts || rechtsonder)
                            aanliggend = true;
                    }

                    else if (y > 0 && y < 6)
                    {
                        boven = Velden[x, y - 1] == TegenSpeler;
                        onder = Velden[x, y + 1] == TegenSpeler;
                        rechtsboven = Velden[x + 1, y - 1] == TegenSpeler;
                        rechtsonder = Velden[x + 1, y + 1] == TegenSpeler;

                        if (boven || onder || rechts || rechtsonder || rechtsboven)
                            aanliggend = true;
                    }

                    else if (y == 6)
                    {
                        boven = Velden[x, y - 1] == TegenSpeler;
                        rechtsboven = Velden[x + 1, y - 1] == TegenSpeler;

                        if (boven || rechts || rechtsboven)
                            aanliggend = true;
                    }
                }

                else if (x > 0 && x < 6)
                {
                    if (y == 0)
                    {
                        onder = Velden[x, y + 1] == TegenSpeler;
                        rechtsonder = Velden[x + 1, y + 1] == TegenSpeler;
                        linksonder = Velden[x - 1, y - 1] == TegenSpeler;

                        if (onder || rechtsonder || linksonder)
                            aanliggend = true;
                    }

                    if (y > 0 && y < 6)
                    {
                        boven = Velden[x, y - 1] == TegenSpeler;
                        onder = Velden[x, y + 1] == TegenSpeler;
                        links = Velden[x - 1, y] == TegenSpeler;
                        rechts = Velden[x + 1, y] == TegenSpeler;
                        rechtsboven = Velden[x + 1, y - 1] == TegenSpeler;
                        linksboven = Velden[x - 1, y - 1] == TegenSpeler;
                        rechtsonder = Velden[x + 1, y + 1] == TegenSpeler;
                        linksonder = Velden[x - 1, y - 1] == TegenSpeler;

                        if (boven || onder || links || rechts || rechtsboven || linksboven || rechtsonder || linksonder)
                            aanliggend = true;
                    }

                    if (y == 6)
                    {
                        boven = Velden[x, y - 1] == TegenSpeler;
                        rechtsboven = Velden[x + 1, y - 1] == TegenSpeler;
                        linksboven = Velden[x - 1, y - 1] == TegenSpeler;

                        if (boven || rechtsboven || linksboven)
                            aanliggend = true;
                    }
                }

                else if (x == 6)
                {
                    links = Velden[x - 1, y] == TegenSpeler;

                    if (y == 0)
                    {
                        onder = Velden[x, y + 1] == TegenSpeler;
                        linksonder = Velden[x - 1, y - 1] == TegenSpeler;

                        if (onder || links || linksonder)
                            aanliggend = true;
                    }

                    else if (y > 0 && y < 6)
                    {
                        boven = Velden[x, y - 1] == TegenSpeler;
                        onder = Velden[x, y + 1] == TegenSpeler;
                        linksboven = Velden[x - 1, y - 1] == TegenSpeler;
                        linksonder = Velden[x - 1, y - 1] == TegenSpeler;

                        if (boven || onder || links || linksonder || linksboven)
                            aanliggend = true;
                    }

                    else if (y == 6)
                    {
                        boven = Velden[x, y - 1] == TegenSpeler;
                        linksboven = Velden[x - 1, y - 1] == TegenSpeler;

                        if (boven || links || linksboven)
                            aanliggend = true;
                    }
                }

                if (aanliggend)
                    return true;
                else
                    // Controleer of spel afgelopen
                    return false;

            }

            else
                // controleer of spel afgelopen
                return false;

        }
    }
}
