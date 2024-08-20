using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UN5EventScriptEditor
{
    public class Item
    {
        public static string VerifyItemScript(MemoryStream ms, uint cond, uint blockType2)
        {
            string result = "";
            if (cond == 2)
            {
                ms.Seek(0x2, SeekOrigin.Current);
                int arg1 = Util.ReadInt32(ms);
                int arg2 = Util.ReadInt32(ms);
                int arg3 = Util.ReadInt32(ms);
                int arg4 = Util.ReadInt32(ms);
                result = ScriptReader.Tab() + "IF ITEM " + $"{blockType2} {getSignal[arg2]} {arg1}";
                if (arg4 != 0x1F)
                {
                    ScriptReader.remove++;
                }
                if (arg4 == 0x1E)
                {
                    MessageBox.Show($"IF ITEM arg4 {arg4}");
                }
                ScriptReader.tabCount++;
                ms.Seek(0x14, SeekOrigin.Current);
            }
            else if (cond == 5)
            {
                ms.Seek(0x2, SeekOrigin.Current);
                int arg1 = Util.ReadInt32(ms);
                result = ScriptReader.Tab() + "SWITCH ITEM " + $"{blockType2}";
                ScriptReader.tabCount++;
                ms.Seek(0x20, SeekOrigin.Current);
            }
            else
            {
                switch (blockType2)
                {
                    case 0x5F:
                        result = ReadSetItemToPlayer(ms);
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
                        result = ScriptReader.Tab() + $"UNKBLOCK 0x28 0x{blockType2.ToString("X")} {arg1} {arg2} {arg3} {arg4} {arg5} {arg6} {arg7} {arg8} {arg9}";
                        break;
                }
            }
            return result;
        }
        public static string ReadSetItemToPlayer(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            int arg2 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "SET_ITEM_TO_PLAYER " + $"{arg1} {arg2}";
            ms.Seek(0x1C, SeekOrigin.Current);
            return result;
        }
        public static Dictionary<int, string> getSignal = new Dictionary<int, string>()
        {
            { 0x20, "==" },
            { 0x21, "<=" },
            { 0x22, ">=" }
        };
    }
}
