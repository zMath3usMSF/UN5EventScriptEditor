using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UN5EventScriptEditor
{
    public class Player
    {
        public static string VerifyPlayerScript(MemoryStream ms, uint cond, uint blockType2)
        {
            string result = "";
            switch (blockType2)
            {
                case 0x70:
                    result = ReadLockPlayerActions(ms);
                    break;
                case 0x71:
                    result = ReadUnlockPlayerActions(ms);
                    break;
                case 0x72:
                    result = ReadSetPlayerPosition(ms);
                    break;
                case 0x73:
                    result = ReadSetPlayerRotation(ms);
                    break;
                case 0x82:
                    result = ReadRotatePlayerToNPC(ms);
                    break;
                case 0x83:
                    result = ReadUnlockPlayerFromDialogue(ms);
                    break;
                case 0x8C:
                    result = ReadSwitchPlayerTo(ms);
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
                    result = ScriptReader.Tab() + $"UNKBLOCK 0x29 0x{blockType2.ToString("X")} {arg1} {arg2} {arg3} {arg4} {arg5} {arg6} {arg7} {arg8} {arg9}";
                    break;
            }
            return result;
        }
        public static string ReadLockPlayerActions(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            string result = ScriptReader.Tab() + "LOCK_PLAYER_ACTIONS";
            ms.Seek(0x24, SeekOrigin.Current);
            return result;
        }
        public static string ReadUnlockPlayerActions(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            string result = ScriptReader.Tab() + "UNLOCK_PLAYER_ACTIONS";
            ms.Seek(0x24, SeekOrigin.Current);
            return result;
        }
        public static string ReadSetPlayerPosition(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            int arg2 = Util.ReadInt32(ms);
            int arg3 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "SET_PLAYER_POSITION " + $"{arg1} {arg2} {arg3}";
            ms.Seek(0x18, SeekOrigin.Current);
            return result;
        }
        public static string ReadSetPlayerRotation(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            int arg2 = Util.ReadInt32(ms);
            int arg3 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "SET_PLAYER_ROTATION " + $"{arg1} {arg2} {arg3}";
            ms.Seek(0x18, SeekOrigin.Current);
            return result;
        }
        public static string ReadRotatePlayerToNPC(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "ROTATE_PLAYER_TO_NPC " + $"{arg1}";
            ms.Seek(0x20, SeekOrigin.Current);
            return result;
        }
        public static string ReadUnlockPlayerFromDialogue(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            string result = ScriptReader.Tab() + "UNLOCK_PLAYER_FROM_DIALOGUE ";
            ms.Seek(0x24, SeekOrigin.Current);
            return result;
        }
        public static string ReadSwitchPlayerTo(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "SWITCH_PLAYER_TO " + $"{arg1}";
            ms.Seek(0x20, SeekOrigin.Current);
            return result;
        }
    }
}
