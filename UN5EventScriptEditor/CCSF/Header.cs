using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UN5EventScriptEditor
{
    public class Header : Block
    {
        public static string Name;

        public Header(MemoryStream ms)
        {
            Type = Util.ReadUInt16(ms);
            ms.Seek(2, SeekOrigin.Current);
            Size = Util.ReadInt32(ms);
            Data = new byte[Size * 4];
            ms.Read(Data, 0, Data.Length);

            Name = Util.ReadFixedLenString(Data, 4, 0x1E);
        }

        public static byte[] Write(string Name)
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(BitConverter.GetBytes(Convert.ToUInt32(0xCCCC0001)), 0, 4);
            ms.Write(BitConverter.GetBytes(Convert.ToUInt32(0x0D)), 0, 4);
            byte[] nameBytes = Encoding.UTF8.GetBytes("CCSF" + Name);
            ms.Write(nameBytes, 0, nameBytes.Length);
            while(ms.Length < 0x2C)
            {
                ms.WriteByte(0x0);
            }
            ms.Write(BitConverter.GetBytes(Convert.ToUInt32(0x123)), 0, 4);
            ms.Write(BitConverter.GetBytes(Convert.ToUInt32(0x0)), 0, 4);
            ms.Write(BitConverter.GetBytes(Convert.ToUInt32(0x1)), 0, 4);
            ms.Write(BitConverter.GetBytes(Convert.ToUInt32(0x0)), 0, 4);
            return ms.ToArray();
        }
    }
}
