using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuthServer
{

    public class PacketContent
    {
        private int _msgType;
        public byte[] _content;
        public int contenLen;

        public int readOffset = 4;
        public PacketContent(ref byte[] content,int srcStartOffset,  int size)
        {
            contenLen = size;
            _content = new byte[contenLen];
            Buffer.BlockCopy(content, srcStartOffset, this._content, 0, contenLen);
            
        }
        //Reading
        public byte ReadByte()
        {
            if (readOffset > contenLen) return 0;
            byte ret = _content[readOffset];
            readOffset += 1;
            return ret;
        }
        public Int16 ReadInteger()
        {
            if (readOffset > contenLen) return -1;
            Int16 ret = BitConverter.ToInt16(_content, readOffset);
            readOffset += 2;
            return ret;
        }
        public int ReadLong()
        {
            if (readOffset > contenLen) return -1;
            int ret = BitConverter.ToInt32(_content, readOffset);
            readOffset += 4;
            return ret;
        }
        public string ReadString()
        {
            if (readOffset > contenLen) return "";
            int len = ReadLong();
            string ret = Encoding.ASCII.GetString(_content, readOffset, len);
            readOffset += len;
            return ret;
        }
        public string ReadUnicode(int i)
        {
            if (readOffset > contenLen) return "";
            string ret = Encoding.Unicode.GetString(_content, readOffset, i * 2);
            readOffset += i * 2;
            return ret;
        }
        public bool ReadFlag()
        {
            if (readOffset > contenLen) return true;
            bool ret = ReadLong() == 0 ? false : true;
            return ret;
        }
        public void ResetReadHead(){
            readOffset = 4;
        }
        public int GetMsgType(){
            _msgType = BitConverter.ToInt32(_content, 0);
            return _msgType;
        }
    }
}
