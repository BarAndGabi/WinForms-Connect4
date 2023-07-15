using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_Connect4
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application
        /// </summary>
        [STAThread]
        static void Main()
        {
            createCheckerPieces();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        static void createCheckerPieces()
        {
            //create png files for red and black checker pieces
            //red checker piece
            System.Drawing.Bitmap redChecker = new System.Drawing.Bitmap(100, 100);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(redChecker);
            g.FillEllipse(System.Drawing.Brushes.Red, 0, 0, 100, 100);
            redChecker.Save("redChecker.png", System.Drawing.Imaging.ImageFormat.Png);
            //black checker piece
            System.Drawing.Bitmap blackChecker = new System.Drawing.Bitmap(100, 100);
            g = System.Drawing.Graphics.FromImage(blackChecker);
            g.FillEllipse(System.Drawing.Brushes.Black, 0, 0, 100, 100);
            blackChecker.Save("blackChecker.png", System.Drawing.Imaging.ImageFormat.Png);

		}   
    }
}
