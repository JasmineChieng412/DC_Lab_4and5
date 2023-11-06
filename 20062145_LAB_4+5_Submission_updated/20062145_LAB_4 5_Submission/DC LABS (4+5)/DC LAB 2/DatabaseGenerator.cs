using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DC_LAB_2
{
    internal class DatabaseGenerator
    {
        Random random = new Random();

        private string GetFirstname()
        {
            string[] firstNames = { "Alice", "Bob", "Charlie", "David", "Eve", "Frank", "Grace" };
            return firstNames[random.Next(firstNames.Length)];
        }

        private string GetLastname()
        {
            string[] lastNames = { "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller" };
            return lastNames[random.Next(lastNames.Length)];
        }

        private uint GetPIN()
        {
            return (uint)random.Next(1000, 10000); // 4-digit PIN
        }

        private uint GetAcctNo()
        {
            return (uint)random.Next(1000000, 10000000); // 7-digit account number
        }

        private int GetBalance()
        {
            return random.Next(-10000, 1000000); // Random balance between -10000 and 999999
        }

        private string GetImage()
        {
            // Code to generate a random image (replace with your image generation logic)
            /* int width = 100;
             int height = 100;

             Bitmap image = new Bitmap(width, height);

             Random random = new Random();

             Color startColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
             Color endColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));

             for (int i = 0; i < width; i++)
             {
                 int r = Interpolate(startColor.R, endColor.R, width, i);
                 int g = Interpolate(startColor.G, endColor.G, width, i);
                 int b = Interpolate(startColor.B, endColor.B, width, i);

                 Color gradientColor = Color.FromArgb(r, g, b);

                 for (int j = 0; j < height; j++)
                 {
                     image.SetPixel(i, j, gradientColor);
                 }
             }
            return image;*/
            string[] images = { "dog.jpg", "balloons.jpg", "ice_cream.jpg", "chair.jpg" };
            string randomImage = images[random.Next(images.Length)];

            // Provide the full path to the image file
            string imagePath = @"C:\Users\User\Downloads\20062145_LAB_4+5_Submission_updated\20062145_LAB_4 5_Submission\DC LABS (4+5)\Async_Client\" + randomImage;

            if (System.IO.File.Exists(imagePath))
            {
                // using (System.Drawing.Image img = System.Drawing.Image.FromFile(imagePath))
                //{
                //   image = new Bitmap(img);
                Console.WriteLine("Image found: " + imagePath);
                // }
            }
            else
            {
                Console.WriteLine("Image not found: " + imagePath);
            }

            return imagePath;
        }

       /* private int Interpolate(int start, int end, int steps, int step)
        {
            double delta = (double)(end - start) / steps;
            return (int)(start + delta * step);
        }*/

        public void GetNextAccount(out uint pin, out uint acctNo, out string firstName, out string lastName, out int balance, out string imagepath)
        {
            pin = GetPIN();
            acctNo = GetAcctNo();
            firstName = GetFirstname();
            lastName = GetLastname();
            balance = GetBalance();
            imagepath = GetImage();
        }

        Bitmap image;
        private Bitmap LoadImage(string imageName)
        {
            
            try
            {
                // Provide the full path to the image file
                string imagePath = @"C:\Users\User\Desktop\DC LABS\Lab2_Client\" + imageName;

                // Load the image using Bitmap class
                 //image = new Bitmap(imagePath);
                //image = (Bitmap)System.Drawing.Image.FromFile(imagePath, true);
                if (System.IO.File.Exists(imagePath))
                {
                   // using (System.Drawing.Image img = System.Drawing.Image.FromFile(imagePath))
                    //{
                     //   image = new Bitmap(img);
                        Console.WriteLine("Image found: " + imagePath);
                        return image;
                   // }
                }
                else
                {
                    Console.WriteLine("Image not found: " + imagePath);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., file not found, invalid format, etc.)
                Console.WriteLine("Error loading image: " + ex.Message);

            }
            return image;
        }

    }
}
