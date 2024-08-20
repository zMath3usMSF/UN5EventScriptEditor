using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UN5EventScriptEditor
{
    public class DefaultBlock : Block
    {
        public DefaultBlock(MemoryStream ms)
        {
            Type = Util.ReadUInt16(ms);
            ms.Seek(2, SeekOrigin.Current);
            Size = Util.ReadInt32(ms);
            ID = Util.ReadUInt32(ms);
            Data = new byte[Size * 4 - 4];
            ms.Read(Data, 0, Data.Length);
        }
    }
}
