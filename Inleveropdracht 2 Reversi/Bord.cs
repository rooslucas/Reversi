using System;
using System.Windows.Forms;
using System.Drawing;

namespace Reversi
{
    class Bord
    {
        // Lengte en breedte van het bord, kan door de gebruiker aangepast worden
        public int Lengte;
        public int Breedte;

        // Variabele om te bepalen wie er aan de beurt is
        // Blauw is speler 1 en rood is speler 2
        public int Speler = 1;
        public int bstenen, rstenen;

        // Bij waarde 0 is het veld leeg, bij waarde 1 Rood en bij waarde 2 blauw
        public int[,] Velden;

        // Deze struct wordt geupdate in de Zet methode. 
        // Geeft het aantal tegels weer dat wordt geflipt en het nieuwe veld dat daardoor ontstaat bij een nieuwe zet
        public struct BordUpdate
        {
            public int tegelflip;
            public int[,] nieuwVeld;
        }
        public BordUpdate Update;

        // Geeft de waarde van het veld terug
        public int KrijgVeld(int x, int y)
        {
            return Velden[x, y];
        }

        // Past de waarde van een veld aan zodat het een kleur kan krijgen
        public void ZetVeld(int x, int y, int speler)
        {
            if (speler == 0 || speler == 1 || speler == 2 || speler == 3)
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

        // Telt het aantal stenen voor beide kleuren
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

        // Controleert of de zet geldig is aan de hand van de regels
        public bool BeurtValide(int x, int y)
        {
            return (Velden[x, y] == 0 || Velden[x, y] == 3) && Zet(x,y);
        }

        // Kijkt wat er gebeurt als er een zet op de plek (x,y) wordt gedaan
        // Hierbij wordt gekeken naar het aantal tegels dat wordt geflipt en welk nieuw veld daardoor ontstaat
        // Returned false als er 0 tegels worden geflipt, anders true
        public bool Zet(int x, int y)
        {
            Update.tegelflip = 0;
            Update.nieuwVeld = new int[Breedte, Lengte];
            for (int i = 0; i < Lengte; i++)
            { 
                for (int j = 0; j < Breedte; j++)
                    Update.nieuwVeld[i,j] = Velden[i,j];
            }
            int[] burenX = { 1, 1, 1, 0, 0, -1, -1, -1 };
            int[] burenY = { 1, 0, -1, 1, -1, 1, 0, -1 };
            for (int direction = 0; direction < 8; direction++)
            {
                for (int distance = 1; distance < Lengte; distance++)
                {
                    if (0 <= x + burenX[direction] * distance && x + burenX[direction] * distance < Breedte && 0 <= y + burenY[direction] * distance && y + burenY[direction] * distance < Lengte)
                    {
                        if (Velden[x + burenX[direction] * distance, y + burenY[direction] * distance] == 0 || Velden[x + burenX[direction] * distance, y + burenY[direction] * distance] == 3)
                            distance = Lengte;
                        else if (Velden[x + burenX[direction] * distance, y + burenY[direction] * distance] == Speler)
                        {
                            for (int i=1;i<distance;i++)
                            {
                                if (Velden[x + burenX[direction] * i, y + burenY[direction] * i] != Speler)
                                {
                                    Update.tegelflip++;
                                    Update.nieuwVeld[x + burenX[direction] * i, y + burenY[direction] * i] =Speler;
                                }
                                    
                            }
                        }
                    }
                }
            }
            Update.nieuwVeld[x, y] = Speler;
            return Update.tegelflip!=0;
        }

        // Controleert of het spel afgelopen is
        public bool Einde()
        {
            int x, y, einde;
            einde = 0;
            for (x = 0; x < Breedte; x++)
            {
                for (y = 0; y < Lengte; y++)
                {
                    if (!BeurtValide(x, y))
                        einde += 1;
                }

            }

            if (einde == Breedte * Lengte)
                return true;
            else return false;
        }

        // Controleert wie de winnaar is
        public string Winnaar()
        {
            if (bstenen > rstenen)
                return "BLAUW";
            else if (bstenen < rstenen)
                return "ROOD";
            else return "HET IS GELIJKSPEL";
        }

        // Helpt een speler met welke zetten mogelijk zijn
        public void HelpSpeler(int aan)
        {
            int x, y;

            for (x = 0; x < Breedte; x++)
            {
                for (y = 0; y < Lengte; y++)
                    if (KrijgVeld(x, y) == 3)
                        ZetVeld(x, y, 0);
            }
            if (aan == 1)
            {
                for (x = 0; x < Breedte; x++)
                {
                    for (y = 0; y < Lengte; y++)
                        if (BeurtValide(x, y))
                            ZetVeld(x, y, 3);
                }
            }
        }
    }
}
