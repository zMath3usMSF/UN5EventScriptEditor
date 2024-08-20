using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UN5EventScriptEditor
{
    public class Other2
    {
        public static string VerifyOther2Script(MemoryStream ms, uint cond, uint blockType2)
        {
            string result = "";
            switch (blockType2)
            {
                case 0x11A:
                    result = ReadWait(ms);
                    break;
                default:
                    ms.Seek(0x2, SeekOrigin.Current);
                    int arg1 = Util.ReadInt32(ms);
                    int arg2 = Util.ReadInt32(ms);
                    int arg3 = Util.ReadInt32(ms);
                    int arg4 = Util.ReadInt32(ms);
                    int arg5 = Util.ReadInt32(ms);
                    int arg6 = Util.ReadInt32(ms);
                    int arg7 = Util.ReadInt32(ms);
                    int arg8 = Util.ReadInt32(ms);
                    int arg9 = Util.ReadInt32(ms);
                    result = ScriptReader.Tab() + $"UNKBLOCK 0x30 0x{blockType2.ToString("X")} {arg1} {arg2} {arg3} {arg4} {arg5} {arg6} {arg7} {arg8} {arg9}";
                    break;
            }
            return result;
        }
        public static string ReadWait(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "WAIT " + $"{arg1}";
            ms.Seek(0x20, SeekOrigin.Current);
            return result;
        }
    }
}
