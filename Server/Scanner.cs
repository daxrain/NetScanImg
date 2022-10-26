using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIA;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Protocol;

namespace Server
{
    public class Scanner
    {
        const string WIA_SCAN_COLOR_MODE = "6146";
        const string WIA_HORIZONTAL_SCAN_RESOLUTION_DPI = "6147";
        const string WIA_VERTICAL_SCAN_RESOLUTION_DPI = "6148";
        const string WIA_HORIZONTAL_SCAN_START_PIXEL = "6149";
        const string WIA_VERTICAL_SCAN_START_PIXEL = "6150";
        const string WIA_HORIZONTAL_SCAN_SIZE_PIXELS = "6151";
        const string WIA_VERTICAL_SCAN_SIZE_PIXELS = "6152";
        const string WIA_SCAN_BRIGHTNESS_PERCENTS = "6154";
        const string WIA_SCAN_CONTRAST_PERCENTS = "6155";
        const string WIA_MANUFACTURER = "3";
        const string WIA_DESCRIPTION = "4";
        const string WIA_LAMP_WARM_UP_TIME = "6161";
        const int widthA4at300dpi = 2480;
        const int heightA4at300dpi = 3508;
        const int widthA4at600dpi = 4960;
        const int heightA4at600dpi = 7016;

        static DeviceManager deviceManager = new DeviceManager();

        public static List<Protocol.Scanner> scanner_list()
        {
            List<Protocol.Scanner> scanner_list = new List<Protocol.Scanner>();
            
            foreach (DeviceInfo dev in deviceManager.DeviceInfos)
            {
                if (dev.Type == WiaDeviceType.ScannerDeviceType)
                {

                    string prop_manufacturer = (string)GetWIAProperty(dev.Properties, WIA_MANUFACTURER);
                    string prop_desc = (string)GetWIAProperty(dev.Properties, WIA_DESCRIPTION);
                    string scanner_name = prop_manufacturer + " - " + prop_desc;
                    scanner_list.Add(new Protocol.Scanner(dev.DeviceID, scanner_name));
                }

            }

            return scanner_list;
        }

        public static DeviceInfo getDevInfo(string devName)
        {
            foreach (DeviceInfo dev in deviceManager.DeviceInfos)
            {
                if (dev.DeviceID.Equals(devName))
                    return dev;
            }

            return null;
        }

        public static Image scan(DeviceInfo dev, Protocol.ScannerOptions options)
        {
            ImageFile imageFile = null;
            try
            {
                // Connect to the first available scanner
                Device device = dev.Connect();
                // Select the scanner
                Item scannerItem = device.Items[1];

                AdjustScannerSettings(scannerItem, options.dpi, 0, 0, 1250, 1700, options.brightness, options.contrast, options.color_mode);

                imageFile = (ImageFile)scannerItem.Transfer(FormatID.wiaFormatBMP);
            }
            catch (COMException e)
            {
                // Convert the error code to UINT
                uint errorCode = (uint)e.ErrorCode;

                // See the error codes
                if (errorCode == 0x80210006)
                {
                    Console.WriteLine("The scanner is busy or isn't ready");
                }
                else if (errorCode == 0x80210064)
                {
                    Console.WriteLine("The scanning process has been cancelled.");
                }
                else if (errorCode == 0x8021000C)
                {
                    Console.WriteLine("There is an incorrect setting on the WIA device.");
                }
                else if (errorCode == 0x80210005)
                {
                    Console.WriteLine("The device is offline. Make sure the device is powered on and connected to the PC.");
                }
                else if (errorCode == 0x80210001)
                {
                    Console.WriteLine("An unknown error has occurred with the WIA device.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore SCANNER non pronto\n" + ex.ToString());
            }

            if(imageFile!=null)
                return ToBitmap(imageFile);
            else return null;
        }

        private static Bitmap ToBitmap(ImageFile image)
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

        private static void AdjustScannerSettings(IItem scannnerItem, int scanResolutionDPI, int scanStartLeftPixel, int scanStartTopPixel, int scanWidthPixels, int scanHeightPixels, int brightnessPercents, int contrastPercents, int colorMode)
        {
            int width;
            int height;
            switch (scanResolutionDPI)
            {
                case 150:
                    SetWIAProperty(scannnerItem.Properties, WIA_HORIZONTAL_SCAN_RESOLUTION_DPI, scanResolutionDPI);
                    SetWIAProperty(scannnerItem.Properties, WIA_VERTICAL_SCAN_RESOLUTION_DPI, scanResolutionDPI);
                    SetWIAProperty(scannnerItem.Properties, WIA_HORIZONTAL_SCAN_START_PIXEL, scanStartLeftPixel);
                    SetWIAProperty(scannnerItem.Properties, WIA_VERTICAL_SCAN_START_PIXEL, scanStartTopPixel);
                    SetWIAProperty(scannnerItem.Properties, WIA_HORIZONTAL_SCAN_SIZE_PIXELS, scanWidthPixels);
                    SetWIAProperty(scannnerItem.Properties, WIA_VERTICAL_SCAN_SIZE_PIXELS, scanHeightPixels);
                    break;
                case 300:
                    width = (int)((widthA4at300dpi / 300.0) * scanResolutionDPI);
                    height = (int)((heightA4at300dpi / 300.0) * scanResolutionDPI);
                    SetWIAProperty(scannnerItem.Properties, WIA_HORIZONTAL_SCAN_RESOLUTION_DPI, scanResolutionDPI);
                    SetWIAProperty(scannnerItem.Properties, WIA_VERTICAL_SCAN_RESOLUTION_DPI, scanResolutionDPI);
                    SetWIAProperty(scannnerItem.Properties, WIA_HORIZONTAL_SCAN_START_PIXEL, 0);
                    SetWIAProperty(scannnerItem.Properties, WIA_VERTICAL_SCAN_START_PIXEL, 0);
                    SetWIAProperty(scannnerItem.Properties, WIA_HORIZONTAL_SCAN_SIZE_PIXELS, width);
                    SetWIAProperty(scannnerItem.Properties, WIA_VERTICAL_SCAN_SIZE_PIXELS, height);
                    break;
                case 600:
                    width = (int)((widthA4at600dpi / 600.0) * scanResolutionDPI);
                    height = (int)((heightA4at600dpi / 600.0) * scanResolutionDPI);
                    SetWIAProperty(scannnerItem.Properties, WIA_HORIZONTAL_SCAN_RESOLUTION_DPI, scanResolutionDPI);
                    SetWIAProperty(scannnerItem.Properties, WIA_VERTICAL_SCAN_RESOLUTION_DPI, scanResolutionDPI);
                    SetWIAProperty(scannnerItem.Properties, WIA_HORIZONTAL_SCAN_START_PIXEL, 0);
                    SetWIAProperty(scannnerItem.Properties, WIA_VERTICAL_SCAN_START_PIXEL, 0);
                    SetWIAProperty(scannnerItem.Properties, WIA_HORIZONTAL_SCAN_SIZE_PIXELS, width);
                    SetWIAProperty(scannnerItem.Properties, WIA_VERTICAL_SCAN_SIZE_PIXELS, height);
                    break;
            }
            SetWIAProperty(scannnerItem.Properties, WIA_SCAN_BRIGHTNESS_PERCENTS, brightnessPercents);
            SetWIAProperty(scannnerItem.Properties, WIA_SCAN_CONTRAST_PERCENTS, contrastPercents);
            SetWIAProperty(scannnerItem.Properties, WIA_SCAN_COLOR_MODE, colorMode);
        }

        private static Object GetWIAProperty(IProperties properties, object propName)
        {
            Property prop = properties.get_Item(ref propName);
            return prop.get_Value();
        }

        private static void SetWIAProperty(IProperties properties, object propName, object propValue)
        {
            Property prop = properties.get_Item(ref propName);
            prop.set_Value(ref propValue);
        }
        
        /*
        public enum WiaProperty
        {
            DeviceId = 2,
            Manufacturer = 3,
            Description = 4,
            Type = 5,
            Port = 6,
            Name = 7,
            Server = 8,
            RemoteDevId = 9,
            UIClassId = 10,
            FirmwareVersion = 1026,
            ConnectStatus = 1027,
            DeviceTime = 1028,
            PicturesTaken = 2050,
            PicturesRemaining = 2051,
            ExposureMode = 2052,
            ExposureCompensation = 2053,
            ExposureTime = 2054,
            FNumber = 2055,
            FlashMode = 2056,
            FocusMode = 2057,
            FocusManualDist = 2058,
            ZoomPosition = 2059,
            PanPosition = 2060,
            TiltPostion = 2061,
            TimerMode = 2062,
            TimerValue = 2063,
            PowerMode = 2064,
            BatteryStatus = 2065,
            Dimension = 2070,
            HorizontalBedSize = 3074,
            VerticalBedSize = 3075,
            HorizontalSheetFeedSize = 3076,
            VerticalSheetFeedSize = 3077,
            SheetFeederRegistration = 3078,         // 0 = LEFT_JUSTIFIED, 1 = CENTERED, 2 = RIGHT_JUSTIFIED
            HorizontalBedRegistration = 3079,       // 0 = LEFT_JUSTIFIED, 1 = CENTERED, 2 = RIGHT_JUSTIFIED
            VerticalBedRegistraion = 3080,          // 0 = TOP_JUSTIFIED, 1 = CENTERED, 2 = BOTTOM_JUSTIFIED
            PlatenColor = 3081,
            PadColor = 3082,
            FilterSelect = 3083,
            DitherSelect = 3084,
            DitherPatternData = 3085,

            DocumentHandlingCapabilities = 3086,    // FEED = 0x01, FLAT = 0x02, DUP = 0x04, DETECT_FLAT = 0x08, 
                                                    // DETECT_SCAN = 0x10, DETECT_FEED = 0x20, DETECT_DUP = 0x40, 
                                                    // DETECT_FEED_AVAIL = 0x80, DETECT_DUP_AVAIL = 0x100
            DocumentHandlingStatus = 3087,          // FEED_READY = 0x01, FLAT_READY = 0x02, DUP_READY = 0x04, 
                                                    // FLAT_COVER_UP = 0x08, PATH_COVER_UP = 0x10, PAPER_JAM = 0x20
            DocumentHandlingSelect = 3088,          // FEEDER = 0x001, FLATBED = 0x002, DUPLEX = 0x004, FRONT_FIRST = 0x008
                                                    // BACK_FIRST = 0x010, FRONT_ONLY = 0x020, BACK_ONLY = 0x040
                                                    // NEXT_PAGE = 0x080, PREFEED = 0x100, AUTO_ADVANCE = 0x200
            DocumentHandlingCapacity = 3089,
            HorizontalOpticalResolution = 3090,
            VerticalOpticalResolution = 3091,
            EndorserCharacters = 3092,
            EndorserString = 3093,
            ScanAheadPages = 3094,                  // ALL_PAGES = 0
            MaxScanTime = 3095,
            Pages = 3096,                           // ALL_PAGES = 0
            PageSize = 3097,                        // A4 = 0, LETTER = 1, CUSTOM = 2
            PageWidth = 3098,
            PageHeight = 3099,
            Preview = 3100,                         // FINAL_SCAN = 0, PREVIEW = 1
            TransparencyAdapter = 3101,
            TransparecnyAdapterSelect = 3102,
            ItemName = 4098,
            FullItemName = 4099,
            ItemTimeStamp = 4100,
            ItemFlags = 4101,
            AccessRights = 4102,
            DataType = 4103,
            BitsPerPixel = 4104,
            PreferredFormat = 4105,
            Format = 4106,
            Compression = 4107,                     // 0 = NONE, JPG = 5, PNG = 8
            MediaType = 4108,
            ChannelsPerPixel = 4109,
            BitsPerChannel = 4110,
            Planar = 4111,
            PixelsPerLine = 4112,
            BytesPerLine = 4113,
            NumberOfLines = 4114,
            GammaCurves = 4115,
            ItemSize = 4116,
            ColorProfiles = 4117,
            BufferSize = 4118,
            RegionType = 4119,
            ColorProfileName = 4120,
            ApplicationAppliesColorMapping = 4121,
            StreamCompatibilityId = 4122,
            ThumbData = 5122,
            ThumbWidth = 5123,
            ThumbHeight = 5124,
            AudioAvailable = 5125,
            AudioFormat = 5126,
            AudioData = 5127,
            PicturesPerRow = 5128,
            SequenceNumber = 5129,
            TimeDelay = 5130,
            CurrentIntent = 6146,
            HorizontalResolution = 6147,
            VerticalResolution = 6148,
            HorizontalStartPosition = 6149,
            VerticalStartPosition = 6150,
            HorizontalExtent = 6151,
            VerticalExtent = 6152,
            PhotometricInterpretation = 6153,
            Brightness = 6154,
            Contrast = 6155,
            Orientation = 6156, // 0 = PORTRAIT, 1 = LANDSCAPE, 2 = 180°, 3 = 270°
            Rotation = 6157, // 0 = PORTRAIT, 1 = LANDSCAPE, 2 = 180°, 3 = 270°
            Mirror = 6158,
            Threshold = 6159,
            Invert = 6160,
            LampWarmUpTime = 6161,
        }*/
    }
}
