using System;
using System.Windows.Forms;
using System.Drawing;

namespace Reversi
{
    class Program
    {
        //Er wordt nu direct het spel Reversi opgestart. Een interface waar je de regels kan lezen/aanpassen zou handig kunnen zijn.
        static void Main()
        {
            Reversi scherm = new Reversi();
            Application.Run(scherm);
        }
    }
}
