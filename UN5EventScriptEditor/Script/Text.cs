using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UN5EventScriptEditor
{
    public class Text
    {
        public static string VerifyTextScript(MemoryStream ms, uint cond, uint blockType2)
        {
            string result = "";
            switch(blockType2)
            {
                case 0xBE:
                    result = ReadShowDialogue(ms);
                    break;
                case 0xBF:
                    result = ReadCloseDialogue(ms);
                    break;
                case 0xC0:
                    result = ReadShowAnswerMessageBox(ms);
                    break;
                case 0xC1:
                    result = ReadCloseAnswerMessageBox(ms);
                    break;
                case 0xC2:
                    result = ReadShowObtainedItem(ms);
                    break;
                case 0xC3:
                    result = ReadCloseMessageBoxItem(ms);
                    break;
                case 0xC4:
                    result = ReadShowMessageBoxTXT(ms);
                    break;
                case 0xC5:
                    result = ReadCloseMessageBoxTXT(ms);
                    break;
                case 0xC6:
                    result = ReadShowPrintMessage(ms);
                    break;
                case 0xC7:
                    result = ReadClosePrintMessage(ms);
                    break;
                case 0xC8:
                    result = ReadShowActAnimation(ms);
                    break;
                case 0xCB:
                    result = ReadShowGroupInfo(ms);
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
                    result = ScriptReader.Tab() + $"UNKBLOCK 0x2C 0x{blockType2.ToString("X")} {arg1} {arg2} {arg3} {arg4} {arg5} {arg6} {arg7} {arg8} {arg9}";
                    break;
            }
            return result;
        }
        public static string ReadShowDialogue(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            string ok = $"\"{CCSFTXT.textList[arg1][0]}\" \"{CCSFTXT.textList[arg1][1]}\" {ScriptReader.voiceIndexes[arg1 - 1][0]} {ScriptReader.voiceIndexes[arg1 - 1][1]}";
            string result = ScriptReader.Tab() + "SHOW_DIALOGUE " + ok;
            ms.Seek(0x20, SeekOrigin.Current);
            return result;
        }
        public static string ReadCloseDialogue(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            string result = ScriptReader.Tab() + "CLOSE_DIALOGUE";
            ms.Seek(0x24, SeekOrigin.Current);
            return result;
        }
        public static string ReadShowAnswerMessageBox(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            List<string> options = new List<string>();
            for(int i = 0; i < 9; i++)
            {
                int arg = Util.ReadInt32(ms);
                if(arg > -0)
                {
                    string option = $"\"{CCSFTXT.textList[arg][1]}\"";
                    options.Add(option);
                }
            }
            string result = ScriptReader.Tab() + "SHOW_ANSWER_MESSAGE_BOX " + string.Join(" ", options);
            return result;
        }
        public static string ReadCloseAnswerMessageBox(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            int arg2 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "CLOSE_ANSWER_MESSAGE_BOX " + $"{arg1} {arg2}";
            ms.Seek(0x1C, SeekOrigin.Current);
            return result;
        }
        public static string ReadShowObtainedItem(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "SHOW_OBTAINED_ITEM " + $"{arg1}";
            ms.Seek(0x20, SeekOrigin.Current);
            return result;
        }
        public static string ReadCloseMessageBoxItem(MemoryStream ms)
        {
            string result = ScriptReader.Tab() + "CLOSE_MESSAGE_BOX_ITEM";
            ms.Seek(0x26, SeekOrigin.Current);
            return result;
        }
        public static string ReadShowMessageBoxTXT(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "SHOW_MESSAGE_BOX_TXT " + $"\"{CCSFTXT.textList[arg1][1]}\"";
            ms.Seek(0x20, SeekOrigin.Current);
            return result;
        }
        public static string ReadCloseMessageBoxTXT(MemoryStream ms)
        {
            string result = ScriptReader.Tab() + "CLOSE_MESSAGE_BOX_TXT";
            ms.Seek(0x26, SeekOrigin.Current);
            return result;
        }
        public static string ReadShowPrintMessage(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "SHOW_PRINT_MESSAGE " + $"{arg1}";
            ms.Seek(0x20, SeekOrigin.Current);
            return result;
        }
        public static string ReadClosePrintMessage(MemoryStream ms)
        {
            string result = ScriptReader.Tab() + "CLOSE_PRINT_MESSAGE";
            ms.Seek(0x26, SeekOrigin.Current);
            return result;
        }
        public static string ReadShowActAnimation(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "SHOW_ACT_ANIMATION " + $"{arg1}";
            ms.Seek(0x20, SeekOrigin.Current);
            return result;
        }
        public static string ReadShowGroupInfo(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            int arg2 = Util.ReadInt32(ms);
            int arg3 = Util.ReadInt32(ms);
            int arg4 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "SHOW_GROUP_INFO " + $"{arg1} {arg2} {arg3} {arg4}";
            ms.Seek(0x14, SeekOrigin.Current);
            return result;
        }
    }
}
