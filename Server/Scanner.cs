using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIA;
using System.IO;
using System.Drawing;

namespace Server
{
    public class Scanner
    {
        

        public static Image scan()
        {
            var deviceManager = new DeviceManager();
            DeviceInfo firstScannerAvailable = null;
            Image myImg;

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
            var device = firstScannerAvailable.Connect();

            // Select the scanner
            var scannerItem = device.Items[1];

            // Retrieve a image in JPEG format and store it into a variable
            var imageFile = (ImageFile)scannerItem.Transfer(FormatID.wiaFormatBMP);

            myImg = (Bitmap)((new ImageConverter()).ConvertFrom(imageFile.FileData));

            return myImg;
        }
    }
}
