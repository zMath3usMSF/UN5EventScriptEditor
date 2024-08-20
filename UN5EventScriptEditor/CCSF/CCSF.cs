using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UN5EventScriptEditor
{
    public class CCSF
    {
        public List<Block> blocks = new List<Block>();
        
        public void OpenCCS(string filePath)
        {
            MemoryStream ms = new MemoryStream(File.ReadAllBytes(filePath));
            
            byte[] buffer = new byte[4];
            ms.Read(buffer, 0, 4);
            int magic = BitConverter.ToInt32(buffer, 0);
            ms.Position = 0;
            if (magic == 0x08088B1F)
            {
                GZipStream gzipStream = new GZipStream(new MemoryStream(File.ReadAllBytes(filePath)), CompressionMode.Decompress);
                ms = new MemoryStream();
                gzipStream.CopyTo(ms);
                ms.Position = 0;
            }
            Block.ReadAllBlocks(ms, blocks);
        }

        public static void WriteCCS(string Name, List<string> blocksNames, List<byte[]> blocks)
        {
            MemoryStream ms = new MemoryStream();
            byte[] headerData = Header.Write(Name);
            ms.Write(headerData, 0, headerData.Length);
            byte[] tocData = TOC.Write(blocksNames);
            ms.Write(tocData, 0, tocData.Length);
            byte[] setupData = Setup.Write();
            ms.Write(setupData, 0, setupData.Length);
            for (int i = 0; i < blocks.Count; i++)
            {
                byte[] binaryData = BinaryBlob.Write(blocks[i], i);
                ms.Write(binaryData, 0, binaryData.Length);
            }
            byte[] endSetupBytes = Setup.WriteEnd();
            ms.Write(endSetupBytes, 0, endSetupBytes.Length);
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"{Name}.ccs".ToUpper());

            byte[] ccsGzip = Compress(ms.ToArray(), Name);
            File.WriteAllBytes(path, ccsGzip);
        }

        public static byte[] Compress(byte[] data, string Name)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream gs = new GZipStream(ms, CompressionMode.Compress))
                {
                    gs.Write(data, 0, data.Length);
                    byte[] nameBytes = Encoding.UTF8.GetBytes(Name + ".tmp");
                    ms.Seek(0xA, SeekOrigin.Begin);
                    ms.Write(nameBytes, 0, nameBytes.Length);
                    ms.WriteByte(0x0);
                }
                data = ms.ToArray();
                data[3] = 0x8;
                return data;
            }
        }
    }
}
