using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UN5EventScriptEditor
{
    public class MCDDialogue
    {
        public static string ReadIFScript(MemoryStream ms, uint cond, uint blockType1, uint blockType2)
        {
            string result = "";
            switch(cond)
            {
                case 2:
                    ms.Seek(0x2, SeekOrigin.Current);
                    int aarg1 = Util.ReadInt32(ms);
                    int aarg2 = Util.ReadInt32(ms);
                    int aarg3 = Util.ReadInt32(ms);
                    int aarg4 = Util.ReadInt32(ms);
                    result = ScriptReader.Tab() + "IF DIALOGUE " + $"{blockType2} {getSignal[aarg2]} {aarg1}";
                    if(aarg4 != 0x1F)
                    {
                        ScriptReader.remove++;
                        MessageBox.Show($"{aarg4}");
                    }
                    if(aarg4 == 0x1E)
                    {
                        MessageBox.Show($"IF PROGRESS arg4 {aarg4}");
                    }
                    ScriptReader.tabCount++;

                    ms.Seek(0x14, SeekOrigin.Current);
                    break;
                case 5:
                    ms.Seek(0x2, SeekOrigin.Current);
                    result = ScriptReader.Tab() + "SWITCH DIALOGUE " + $"{blockType2}";
                    ScriptReader.tabCount++;
                    ms.Seek(0x24, SeekOrigin.Current);
                    break;
                default:
                    switch(blockType2)
                    {
                        case 0x42:
                            result = ReadUpdateDialogue(ms);
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
                            result = ScriptReader.Tab() + $"UNKBLOCK 0x24 0x{blockType2.ToString("X")} {arg1} {arg2} {arg3} {arg4} {arg5} {arg6} {arg7} {arg8} {arg9}";
                            break;
                    }
                    break;
            }
            return result;
        }
        public static string ReadUpdateDialogue(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            int arg2 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "UPDATE_DIALOGUE " + $"{arg1} {arg2}";
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
