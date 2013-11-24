using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuthServer
{
    public class PacketContainer
    {
        public PacketContainer (byte[] packetBuffer, int realSize, int serverID){
            buffer = new byte[realSize];
            this.size = realSize;
            Buffer.BlockCopy(packetBuffer, 0, this.buffer, 0, realSize);
            this.serverID = serverID;
            extractedPackets = new List<PacketContent>();
        }
        public PacketContainer(byte[] packetBuffer, int realSize, long clientID)
        {
            buffer = new byte[realSize];
            this.size = realSize;
            Buffer.BlockCopy(packetBuffer, 0, this.buffer, 0, realSize);
            this.clientID = clientID;
            extractedPackets = new List<PacketContent>();
        }
        public byte[] buffer;
        public int size;
        public int serverID;
        public long clientID;
        //PostProcess
        public List<PacketContent> extractedPackets;
    }
}
