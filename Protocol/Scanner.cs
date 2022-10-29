using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol
{
    [Serializable]
    public class Scanner
    {
        public string ID;
        public string Name;
        public ScannerOptions Options;

        public Scanner(string ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }

        public Scanner(string iD, string name, ScannerOptions options) : this(iD, name)
        {
            Options = options;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    [Serializable]
    public class ScannerOptions
    {
        public int color_mode = 0;
        public int dpi = 150;
        public int contrast = 0;
        public int brightness = 0;
        public bool adf = false;
        public bool duplex = false;

        public ScannerOptions()
        {

        }

        public ScannerOptions(int color_mode, int dpi, int contrast, int brightness, bool adf, bool duplex)
        {
            this.color_mode = color_mode;
            this.dpi = dpi;
            this.contrast = contrast;
            this.brightness = brightness;
            this.adf = adf;
            this.duplex = duplex;
        }   
    }
}
