using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace AuthServer
{
    public class Candidate
    {
        public long id;
        public Socket socket;
        public Player player;
        public Bank playerBank;
        public string loginName;
        public string password;
        public byte[] buffer;
        public string loginToken;
        public int selectedServerIndex;
        public long connectionTime;
    }
}
