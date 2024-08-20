using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UN5EventScriptEditor
{
    public class BinaryBlob : Block
    {
        public BinaryBlob(MemoryStream ms)
        {
            Type = Util.ReadUInt16(ms);
            ms.Seek(2, SeekOrigin.Current);
            Size = Util.ReadInt32(ms);
            ID = Util.ReadUInt32(ms);
            Name = TOC.objectNameList[(int)ID];
            Data = new byte[Size * 4 - 4];
            ms.Read(Data, 0, Data.Length);
        }

        public static byte[] Write(byte[] data, int index)
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(BitConverter.GetBytes(Convert.ToUInt32(0xCCCC2400)), 0, 4);
            ms.Write(BitConverter.GetBytes(Convert.ToUInt32(data.Length + 4) / 4), 0, 4);
            ms.Write(BitConverter.GetBytes(Convert.ToUInt32(index + 1)), 0, 4);
            ms.Write(data, 0, data.Length);
            return ms.ToArray();
        }
    }
}
