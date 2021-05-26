using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace AscII_art_Lesson
{
    class Program
    {
        private const double WIDTH_OFFSET = 1.5;

        [STAThread]
        static void Main(string[] args)
        {

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            
            
            var openFiledialog = new OpenFileDialog
            {
                Filter = "Images | *.bmp;*.png;*.jpg;*.jpeg"
            };
            

            while (true)
            {
                Console.ReadLine();
                if (openFiledialog.ShowDialog() != DialogResult.OK)
                    continue;

                Console.Clear();

                var bimap = new Bitmap(openFiledialog.FileName);
                bimap = ResizeBitmap(bimap);
                             
                bimap.ToGrayscale();

                var converter = new BitmapAsciiConvertor(bimap);
                var rows = converter.Convert();

                foreach (var row in rows)
                     Console.WriteLine(row);
                  
                var rowNegativ = converter.ConvertNegativ();
                File.WriteAllLines("image.txt",rowNegativ.Select(r => new string(r)));

                Console.SetCursorPosition(0, 0);
            }
        }

        private static Bitmap ResizeBitmap(Bitmap bitmap)
        {
            var maxWidth = 474;
            var newHeight = bitmap.Height / WIDTH_OFFSET * maxWidth / bitmap.Width;
            if (bitmap.Width > maxWidth || bitmap.Height > newHeight)
                bitmap = new Bitmap(bitmap,new Size(maxWidth,(int)newHeight));

            return bitmap;
        }
    }
}
