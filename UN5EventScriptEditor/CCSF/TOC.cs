using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UN5EventScriptEditor
{
    public class TOC : Block
    {
        public static int fileCount;
        public static int objectCount;
        public static List<string> fileNameList;
        public static List<string> objectNameList;
        public static List<int> indexesList;

        public TOC(MemoryStream ms)
        {
            Type = Util.ReadUInt16(ms);
            ms.Seek(2, SeekOrigin.Current);
            Size = Util.ReadInt32(ms);
            Data = new byte[Size * 4];
            ms.Read(Data, 0, Data.Length);

            fileCount = BitConverter.ToInt32(Data, 0);
            objectCount = BitConverter.ToInt32(Data, 4);

            fileNameList = new List<string>();
            for (int i = 0; i < fileCount; i++)
            {
                fileNameList.Add(Util.ReadFixedLenString(Data, i * 0x8 + 0x20, 0x20));
            }

            objectNameList = new List<string>();
            indexesList = new List<int>();
            int ok = fileCount * 0x20;
            for (int i = 0; i < objectCount * 0x20; i+=0x20)
            {
                objectNameList.Add(Util.ReadFixedLenString(Data, i + 0x8 + ok, 0x1E));
                indexesList.Add(BitConverter.ToInt16(Data, i + 0x1E + 0x8 + ok));
            }
        }

        public static byte[] Write(List<string> blockNames)
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(BitConverter.GetBytes(Convert.ToUInt32(0xCCCC0002)), 0, 4);
            ms.Write(BitConverter.GetBytes(Convert.ToUInt32(((blockNames.Count + 2) * 0x20 + 8) / 4)), 0, 4);
            ms.Write(BitConverter.GetBytes(Convert.ToUInt32(0x01)), 0, 4);
            ms.Write(BitConverter.GetBytes(Convert.ToUInt32(blockNames.Count + 1)), 0, 4);
            for(int i = 0; i != 0x40; i++)
            {
                ms.WriteByte(0x0);
            }
            for(int i = 0; i < blockNames.Count; i++)
            {
                Util.WriteString(ms, "BIN_" + blockNames[i].ToLower(), 0x1E);
                ms.Write(BitConverter.GetBytes((ushort)0x0), 0, 2);
            }
            return ms.ToArray();
        }
    }
}

