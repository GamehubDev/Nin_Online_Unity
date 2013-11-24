using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuthServer
{
    public enum PacketType { Send, Recv };

    public class PacketsProcessor
    {

        public PacketsProcessor()
        {
            this._packetType = 0;
            this._msgType = 0;
            this._packetLength = 0;
        }

        public PacketsProcessor(byte[] data, int size)
        {
            UnpackPacket(data, size);
        }
        public PacketsProcessor(int msgType)
        {
            PrePacketPacking(msgType);
        }
        //Converts the Data structure into an array of bytes
        public byte[] getContent()
        {
            return _content;
        }

        private void UnpackPacket(byte[] data, int size)
        {
            int currentOffset = 0;
            //size of packet
            this._packetLength = BitConverter.ToInt32(data, currentOffset) + 4;// +4 first 4 bytes are seize of the packet
            currentOffset += 4;

            this._msgType = BitConverter.ToInt32(data, currentOffset);
            currentOffset += 4;

            //Actual content
            _contentLength = _packetLength - currentOffset;
            this._content = new byte[_contentLength];
            Buffer.BlockCopy(data, currentOffset, this._content, 0, _contentLength);
        }
        //Reading
        public byte ReadByte()
        {
            if (readOffset > _contentLength) return 0;
            byte ret = _content[readOffset];
            readOffset += 1;
            return ret;
        }
        public Int16 ReadInteger()
        {
            if (readOffset > _contentLength) return -1;
            Int16 ret = BitConverter.ToInt16(_content, readOffset);
            readOffset += 2;
            return ret;
        }
        public int ReadLong()
        {
            if (readOffset > _contentLength) return -1;
            int ret = BitConverter.ToInt32(_content, readOffset);
            readOffset += 4;
            return ret;
        }
        public string ReadString()
        {
            if (readOffset > _contentLength) return "";
            int len = ReadLong();
            string ret = Encoding.ASCII.GetString(_content, readOffset, len);
            readOffset += len;
            return ret;
        }
        public string ReadUnicode(int i)
        {
            if (readOffset > _contentLength) return "";
            string ret = Encoding.Unicode.GetString(_content, readOffset, i * 2);
            readOffset += i * 2;
            return ret;
        }
        public bool ReadFlag()
        {
            if (readOffset > _contentLength) return true;
            bool ret = ReadLong() == 0 ? false : true;
            return ret;
        }

        //Writing
        private void PrePacketPacking(int msgType)
        {
            sendPacket.AddRange(BitConverter.GetBytes((int)msgType));
            writeoffset += 4;
        }
        public void FinishPacking()
        {
            sendPacket.InsertRange(0, BitConverter.GetBytes(writeoffset));
        }
        public void WriteByte(byte data)
        {
            sendPacket.Add(data);
            writeoffset += 1;
        }
        public void WriteBytes(byte[] data)
        {
            sendPacket.AddRange(data);
            writeoffset += data.Length;
        }
        public void WriteInteger(int data)
        {
            sendPacket.AddRange(BitConverter.GetBytes((Int16)data));
            writeoffset += 2;
        }
        public void WriteLong(int data)
        {
            byte[] test = BitConverter.GetBytes(data);
            sendPacket.AddRange(test);
            writeoffset += 4;

        }
        public void WriteString(string data)
        {
            if (data == null) return;
            WriteLong(data.Length);
            sendPacket.AddRange(Encoding.ASCII.GetBytes(data.ToCharArray(), 0, data.Length));
            writeoffset += data.Length;

        }
        public void WriteFlag(bool flag)
        {
            WriteLong(flag ? 1 : 0);
        }
        public byte[] GetSendBytes()
        {
            byte[] test = sendPacket.ToArray();
            return sendPacket.ToArray();
        }
        public void DebuffPlayer(ref Player playerToFill)
        {

            playerToFill.PlayerName = ReadUnicode(Constants.PLAYER_NAME_LENGTH);
            playerToFill.sex = (byte)ReadLong();
            playerToFill.playerClass = ReadLong();

            playerToFill.sprite = ReadLong();
            playerToFill.level = (byte)ReadLong();
            playerToFill.exp = ReadLong();
            playerToFill.access = ReadByte();
            playerToFill.isPK = ReadByte();
            readOffset += 2;

            playerToFill.hair = ReadLong();
            playerToFill.eyes = ReadLong();

            playerToFill.ryo = ReadLong();
            playerToFill.vitals[(int)Vitals.HP] = ReadLong();
            playerToFill.vitals[(int)Vitals.MP] = ReadLong();

            for (int i = 0; i < (int)Stats.counter; i++)
            {
                playerToFill.stats[i] = ReadInteger();
            }
            readOffset += 2;

            playerToFill.points = ReadLong();
            //Inventory
            for (int i = 0; i < (int)Equipment.counter; i++)
            {
                playerToFill.equipment[i] = ReadLong();
            }

            for (int i = 0; i < Constants.MAX_INV; i++)
            {
                playerToFill.inventory[i].id = ReadLong();
                playerToFill.inventory[i].amount = ReadLong();
            }
            //Spells
            for (int i = 0; i < Constants.MAX_PLAYER_SPELLS; i++)
            {
                playerToFill.spells[i] = ReadLong();
            }

            //Hotbar
            for (int i = 0; i < Constants.MAX_HOTBAR; i++)
            {
                playerToFill.hotbar[i].slot = ReadLong();
                playerToFill.hotbar[i].sType = (byte)ReadLong();
            }

            //Map
            playerToFill.currentMap = ReadLong();
            playerToFill.x = ReadByte();
            playerToFill.y = ReadByte();
            playerToFill.dir = ReadByte();

            //Switches
            for (int i = 0; i < Constants.MAX_SWITCHES; i++)
            {
                playerToFill.switches[i] = ReadByte();
            }

            //Variables
            for (int i = 0; i < Constants.MAX_VARIABLES; i++)
            {
                playerToFill.variables[i] = ReadLong();
            }
            //PlayerQuests
            for (int i = 0; i < Constants.MAX_PLAYER_QUESTS; i++)
            {
                playerToFill.quests[i].currentObjective = ReadByte();

                playerToFill.quests[i].completed[0] = ReadByte();
                playerToFill.quests[i].completed[1] = ReadByte();
                playerToFill.quests[i].completed[2] = ReadByte();

                playerToFill.quests[i].id = ReadLong();

                playerToFill.quests[i].counter[0] = ReadLong();
                playerToFill.quests[i].counter[1] = ReadLong();
                playerToFill.quests[i].counter[2] = ReadLong();
            }
            //Completed Quest
            for (int i = 0; i < Constants.MAX_QUESTS; i++)
            {
                playerToFill.completedQuest[i] = ReadLong();
            }
            //Guild
            playerToFill.guild = ReadLong();

            playerToFill.minutes = ReadLong();
            playerToFill.hours = ReadLong();
            playerToFill.attackAnimation = ReadLong();

            //Friends
            for (int i = 0; i < Constants.MAX_PLAYER_FRIENDS; i++)
            {
                playerToFill.friends[i].name = ReadUnicode(Constants.PLAYER_NAME_LENGTH);
            }
            //Elements
            for (int i = 0; i < Constants.MAX_ELEMENTS; i++)
            {
                playerToFill.elements[i] = ReadByte();
            }

            playerToFill.isMuted = ReadByte();
            playerToFill.IsDonor = ReadByte();

            readOffset += 1;

            //HasChar
            Int16 hasChar = ReadInteger();
            playerToFill.HasChar = (hasChar == (-1) ? true : false);

        }
        public void DebuffBank(ref Bank bankToFill)
        {
            for (int i = 0; i < Constants.MAX_BANK; i++)
            {
                bankToFill.items[i].id = ReadLong();
                bankToFill.items[i].amount = ReadLong();
            }
        }
        public void BufferPlayer(ref Player player)
        {

            WriteBytes(Encoding.Unicode.GetBytes(player.PlayerName));
            WriteLong(player.sex);
            WriteLong(player.playerClass);
            WriteLong(player.sprite);
            WriteLong(player.level);
            WriteLong(player.exp);
            WriteByte(player.access);
            WriteByte(player.isPK);
            WriteBytes(new byte[] { 0, 0 });//padding, yp to 4 bytes

            WriteLong(player.hair);
            WriteLong(player.eyes);

            WriteLong(player.ryo);
            WriteLong(player.vitals[(int)Vitals.HP]);
            WriteLong(player.vitals[(int)Vitals.MP]);

            for (int i = 0; i < (int)Stats.counter; i++)
            {
                WriteInteger(player.stats[i]);
            }
            WriteBytes(new byte[] { 0, 0 });//padding?

            WriteLong(player.points);
            //Inventory
            for (int i = 0; i < (int)Equipment.counter; i++)
            {
                WriteLong(player.equipment[i]);
            }

            for (int i = 0; i < Constants.MAX_INV; i++)
            {
                WriteLong(player.inventory[i].id);
                WriteLong(player.inventory[i].amount);
            }
            //Spells
            for (int i = 0; i < Constants.MAX_PLAYER_SPELLS; i++)
            {
                WriteLong(player.spells[i]);
            }

            //Hotbar
            for (int i = 0; i < Constants.MAX_HOTBAR; i++)
            {
                WriteLong(player.hotbar[i].slot);
                WriteLong(player.hotbar[i].sType);
            }

            //Map
            WriteLong(player.currentMap);
            WriteByte(player.x);
            WriteByte(player.y);
            WriteByte(player.dir);

            //Switches
            for (int i = 0; i < Constants.MAX_SWITCHES; i++)
            {
                WriteByte(player.switches[i]);
            }

            //Variables
            for (int i = 0; i < Constants.MAX_VARIABLES; i++)
            {
                WriteLong(player.variables[i]);
            }
            //PlayerQuests
            for (int i = 0; i < Constants.MAX_PLAYER_QUESTS; i++)
            {
                WriteByte(player.quests[i].currentObjective);

                WriteByte(player.quests[i].completed[0]);
                WriteByte(player.quests[i].completed[1]);
                WriteByte(player.quests[i].completed[2]);

                WriteLong(player.quests[i].id);

                WriteLong(player.quests[i].counter[0]);
                WriteLong(player.quests[i].counter[1]);
                WriteLong(player.quests[i].counter[2]);
            }
            //Completed Quest
            for (int i = 0; i < Constants.MAX_QUESTS; i++)
            {
                WriteLong(player.completedQuest[i]);
            }
            //Guild
            WriteLong(player.guild);

            WriteLong(player.minutes);
            WriteLong(player.hours);
            WriteLong(player.attackAnimation);

            //Friends
            //Completed Quest
            for (int i = 0; i < Constants.MAX_PLAYER_FRIENDS; i++)
            {
                WriteBytes(Encoding.Unicode.GetBytes(player.friends[i].name));
            }
            //Elements
            for (int i = 0; i < Constants.MAX_ELEMENTS; i++)
            {
                WriteByte(player.elements[i]);
            }

            WriteByte(player.isMuted);
            WriteByte(player.IsDonor);

            //Empty Byte for alignement
            WriteByte(0);

            //HasChar?
            WriteInteger(player.HasChar ? -1 : 0);
            string test = BitConverter.ToString(sendPacket.ToArray(), 0);
        }

        public void BufferPlayerBank(ref Bank playerBank)
        {
            for (int i = 0; i < Constants.MAX_BANK; i++)
            {
                WriteLong(playerBank.items[i].id);
                WriteLong(playerBank.items[i].amount);
            }
        }

        public void ExtractPackets(PacketContainer cont, ref Fragmentation f)
        {
            bool stillReading = true;
            int packetLen;

            //Extreme case
            const int PACKET_SIZE_HEADER = 4; // 4 bytes

            if (f.pendingFragment)
            {

                if (f.sizeSplitted)
                {
                    f.sizeSplitted = false;
                    Buffer.BlockCopy(cont.buffer, 0, f.sizeBuffer, f.sizeAlreadyFilledBytes, f.sizeMissingBytes);
                    readOffset += f.sizeMissingBytes;
                    f.missingBytes = BitConverter.ToInt32(f.sizeBuffer, 0);

                    f.sizeAlreadyFilledBytes = 0;
                    f.sizeMissingBytes = 0;

                }
                if (f.missingBytes + readOffset <= cont.size)
                {
                    Buffer.BlockCopy(cont.buffer, readOffset, f.buffer, f.alreadyFilledBytes, f.missingBytes);
                    readOffset += f.missingBytes;
                    cont.extractedPackets.Add(new PacketContent(ref f.buffer, 0, f.alreadyFilledBytes + f.missingBytes));
                    f.pendingFragment = false;
                    f.alreadyFilledBytes = 0;
                    f.missingBytes = 0;
                }
                else
                {
                    f.missingBytes -= cont.size;
                    f.alreadyFilledBytes += cont.size;
                    Buffer.BlockCopy(cont.buffer, readOffset, f.buffer, f.alreadyFilledBytes, cont.size);
                    readOffset += cont.size;
                }



            }
            while (stillReading && readOffset < cont.size)
            {
                //size of single packet
                if (cont.size < 8192)
                {
                    if (1 == 1)
                    {
                    }
                }
                packetLen = BitConverter.ToInt32(cont.buffer, readOffset);
                readOffset += 4;

                if (packetLen + readOffset > cont.size) // fill Fragmentation
                {
                    f.pendingFragment = true;
                    f.alreadyFilledBytes = cont.size - readOffset;
                    f.missingBytes = packetLen - f.alreadyFilledBytes;
                    if (f.alreadyFilledBytes == 0) break;
                    Buffer.BlockCopy(cont.buffer, readOffset, f.buffer, 0, f.alreadyFilledBytes);
                    readOffset += f.alreadyFilledBytes;
                    break;
                }
                else // just read it
                {
                    cont.extractedPackets.Add(new PacketContent(ref cont.buffer, readOffset, packetLen));
                    readOffset += packetLen;
                }


                if ((cont.size - readOffset) <= 0)
                {

                    stillReading = false;
                    break;
                }

                //Extreme case - packet header splitted between packets
                if ((cont.size - readOffset) < PACKET_SIZE_HEADER)
                {
                    f.pendingFragment = true;
                    f.sizeSplitted = true;
                    f.sizeAlreadyFilledBytes = cont.size - readOffset;
                    f.sizeMissingBytes = PACKET_SIZE_HEADER - f.sizeAlreadyFilledBytes;
                    Buffer.BlockCopy(cont.buffer, readOffset, f.sizeBuffer, 0, f.sizeAlreadyFilledBytes);
                    stillReading = false;
                }
            }



        }

        //Packet Processor
        private int readOffset = 0;
        //====OLD======
        //Manipulation
        private int writeoffset = 0;

        //Packing / Unpacking
        private PacketType _packetType;
        private int _msgType;
        private byte[] _content;
        private int _packetLength;
        private int _contentLength;

        //Packing send packet
        List<byte> sendPacket = new List<byte>();

        public PacketType PacketType
        {
            get { return _packetType; }
            set { _packetType = value; }
        }
        public int MsgType
        {
            get { return _msgType; }
            set { _msgType = value; }
        }
        public int PacketLength
        {
            get { return _packetLength; }
            set { _packetLength = value; }
        }
        public int ContentLength
        {
            get { return _contentLength; }
            set { _contentLength = value; }
        }
    }
}
