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

            public Request()
            {
                MyId = request_type.EMPTY;
            }

            public Request(request_type type)
            {
                MyId = type;
            }

            public request_type MyId { get => myId; set => myId = value; }
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
            public List<string> scanners;

            public ListResponse() : base()
            {
                scanners = new List<string>();
            }

            public ListResponse(List<string> scanner_list) : base()
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
    }
}
