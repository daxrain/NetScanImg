using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIA;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Server
{
    public class Scanner
    {
        public Image scan()
        {
            DeviceManager deviceManager = new DeviceManager();
            DeviceInfo firstScannerAvailable = null;
            ImageFile imageFile = null;

            for (int i = 1; i <= deviceManager.DeviceInfos.Count; i++)
            {
                // Skip the device if it's not a scanner
                if (deviceManager.DeviceInfos[i].Type != WiaDeviceType.ScannerDeviceType)
                {
                    continue;
                }

                firstScannerAvailable = deviceManager.DeviceInfos[i];

                break;
            }

            // Connect to the first available scanner
            Device device = firstScannerAvailable.Connect();

            // Select the scanner
            Item scannerItem = device.Items[1];

            try
            {
                // Retrieve a image in JPEG format and store it into a variable
                imageFile = (ImageFile)scannerItem.Transfer(FormatID.wiaFormatJPEG);
                scannerItem = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore SCANNER non pronto");
            }

            //myImg = (Bitmap)((new ImageConverter()).ConvertFrom(imageFile.ARGBData));
            return ToBitmap(imageFile);
        }

        public static Bitmap ToBitmap(ImageFile image)
        {
            Bitmap result;
            byte[] data;

            data = (byte[])image.FileData.get_BinaryData();

            using (MemoryStream stream = new MemoryStream(data))
            {
                using (Image scannedImage = Image.FromStream(stream))
                {
                    result = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);

                    using (Graphics g = Graphics.FromImage(result))
                    {
                        g.Clear(Color.Transparent);
                        g.PageUnit = GraphicsUnit.Pixel;
                        g.DrawImage(scannedImage, new Rectangle(0, 0, image.Width, image.Height));
                    }
                }
            }

            return result;
        }
    }
}
