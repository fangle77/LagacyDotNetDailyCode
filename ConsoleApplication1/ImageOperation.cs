using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ConsoleApplication1
{
    class ImageOperation
    {
        public static string ReadPix(Image img)
        {
            if (img == null) return string.Empty;
            Bitmap bitmap = new Bitmap(img, 64, 64);
            int height = bitmap.Height;
            int width = bitmap.Width;

            StringBuilder sb = new StringBuilder();
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    var pix = bitmap.GetPixel(col, row);
                    sb.AppendFormat("{0:x3}{1:x3}{2:x3}{3:x3}", pix.A, pix.R, pix.G, pix.B);
                    //sb.Append(pix.ToString());
                }
            }
            bitmap.Dispose();
            return sb.ToString();
        }

        public static double SimilarImage(Image aImage, Image bImage)
        {
            string sa = ReadPix(aImage);
            string sb = ReadPix(bImage);
            Console.WriteLine(sa.Length);
            Console.WriteLine(sb.Length);
            return Similar.SimilarityBy_LD_LCS(sa, sb);
        }
        public static double SimilarImage(string aImage, string bImage)
        {
            return SimilarImage(Image.FromFile(aImage), Image.FromFile(bImage));
        }
    }

    class ImageOperationTest
    {
        public static void RunTest()
        {
            string img1 = @"C:\Users\bowen.zhang\Desktop\New folder\1.png";
            string img2 = @"C:\Users\bowen.zhang\Desktop\New folder\2.png";
            double similar = ImageOperation.SimilarImage(img1, img2);
            Console.WriteLine(similar);
        }
    }
}