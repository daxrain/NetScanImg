using System;
using System.Collections.Generic;
using System.Drawing;

namespace Protocol
{
    namespace MyMessages
    {
        public enum request_type
        {
            EMPTY,
            LIST,
            OPTIONS,
            SCAN
        }
        
        [Serializable]
        public class MyMessage
        {
        }

        [Serializable]
        public class Request : MyMessage
        {
            private request_type myId;
            private Scanner device;

            public Request()
            {
                MyId = request_type.EMPTY;
            }

            public Request(request_type type)
            {
                MyId = type;
            }

            public Request(request_type myId, Scanner dev)
            {
                Device = dev;
                MyId = myId;
            }

            public request_type MyId { get => myId; set => myId = value; }
            public Scanner Device { get => device; set => device = value; }
        }

        [Serializable]
        public class Response : MyMessage
        {

        }
        
        [Serializable]
        public class EmptyResponse : Response
        {
            private string msg;

            public EmptyResponse() : base()
            {
                msg = "EMPTY MESSAGE RESPONSE";
            }

            public EmptyResponse(string my_msg) : base()
            {
                msg = my_msg;
            }
        }

        [Serializable]
        public class ListResponse : Response
        {
            public List<Scanner> scanners;

            public ListResponse() : base()
            {
                scanners = new List<Scanner>();
            }

            public ListResponse(List<Scanner> scanner_list) : base()
            {
                scanners = scanner_list;
            }
        }

        [Serializable]
        public class OptionsResponse : Response
        {
            //Options of WIA scanner 
        }
        
        [Serializable]
        public class ScanResponse : Response
        {
            public Image img;

            public ScanResponse(Image scanned_img)
            {
                img = scanned_img;
            }           
        }

        [Serializable]
        public class ScanErrorResponse : Response
        {
            public string error_msg;

            public ScanErrorResponse(string error_msg)
            {
                this.error_msg = error_msg;
            }
        }
    }
}
