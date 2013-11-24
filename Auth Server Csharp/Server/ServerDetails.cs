using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace AuthServer
{
    public class ServerDetails
    {
            public int index;
            public int serverIndex;
            public string name;
            public string ip;
            public int port;
            public string location;
            public bool donorOnly;
            public Socket socket;
            public byte[] buffer;
            public bool registered = false;

            //Packets longer than 8192 bytes.
            public byte[] partBuffer;
            public int partBufferSize;
    }
}
