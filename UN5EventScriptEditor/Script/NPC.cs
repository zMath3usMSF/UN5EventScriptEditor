using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UN5EventScriptEditor
{
    public class NPC
    {
        public static string VerifyNPCScript(MemoryStream ms, uint cond, uint blockType2)
        {
            string result = "";
            switch (blockType2)
            {
                case 0x8E:
                    result = ReadSpawnNPC(ms);
                    break;
                case 0x8F:
                    result = ReadRemoveNPC(ms);
                    break;
                case 0x94:
                    result = ReadSetNPCPositionTo(ms);
                    break;
                case 0x95:
                    result = ReadSetNPCRotationTo(ms);
                    break;
                case 0x9A:
                    result = ReadSpawnNPCSmoke(ms);
                    break;
                case 0x9B:
                    result = ReadRemoveNPCSmoke(ms);
                    break;
                case 0xA4:
                    result = ReadRotateNPCToPlayer(ms);
                    break;
                case 0xA5:
                    result = ReadUnlockNPCToTalk(ms);
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
                    result = ScriptReader.Tab() + $"UNKBLOCK 0x2A 0x{blockType2.ToString("X")} {arg1} {arg2} {arg3} {arg4} {arg5} {arg6} {arg7} {arg8} {arg9}";
                    break;
            }
            return result;
        }
        public static string ReadSpawnNPC(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            int arg2 = Util.ReadInt32(ms);
            int arg3 = Util.ReadInt32(ms);
            int arg4 = Util.ReadInt32(ms);
            int arg5 = Util.ReadInt32(ms);
            int arg6 = Util.ReadInt32(ms);
            int arg7 = Util.ReadInt32(ms);
            int arg8 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "SPAWN_NPC " + $"{arg1} {arg2} {arg3} {arg4} {arg5} {arg6} {arg7} {arg8}";
            ms.Seek(0x4, SeekOrigin.Current);
            return result;
        }
        public static string ReadRemoveNPC(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            int arg2 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "REMOVE_NPC " + $"{arg1} {arg2}";
            ms.Seek(0x1C, SeekOrigin.Current);
            return result;
        }
        public static string ReadSetNPCPositionTo(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            int arg2 = Util.ReadInt32(ms);
            int arg3 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "SET_NPC_POSITION_TO " + $"{arg1} {arg2} {arg3}";
            ms.Seek(0x18, SeekOrigin.Current);
            return result;
        }
        public static string ReadSetNPCRotationTo(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            int arg2 = Util.ReadInt32(ms);
            int arg3 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "SET_NPC_ROTATION_TO " + $"{arg1} {arg2} {arg3}";
            ms.Seek(0x18, SeekOrigin.Current);
            return result;
        }
        public static string ReadSpawnNPCSmoke(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            int arg2 = Util.ReadInt32(ms);
            int arg3 = Util.ReadInt32(ms);
            int arg4 = Util.ReadInt32(ms);
            int arg5 = Util.ReadInt32(ms);
            int arg6 = Util.ReadInt32(ms);
            int arg7 = Util.ReadInt32(ms);
            int arg8 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "SPAWN_NPC_SMOKE " + $"{arg1} {arg2} {arg3} {arg4} {arg5} {arg6} {arg7} {arg8}";
            ms.Seek(0x4, SeekOrigin.Current);
            return result;
        }
        public static string ReadRemoveNPCSmoke(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            int arg2 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "REMOVE_NPC_SMOKE " + $"{arg1} {arg2}";
            ms.Seek(0x1C, SeekOrigin.Current);
            return result;
        }
        public static string ReadRotateNPCToPlayer(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "ROTATE_NPC_TO_PLAYER " + $"{arg1}";
            ms.Seek(0x20, SeekOrigin.Current);
            return result;
        }
        public static string ReadUnlockNPCToTalk(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "UNLOCK_NPC_TO_TALK " + $"{arg1}";
            ms.Seek(0x20, SeekOrigin.Current);
            return result;
        }
    }
}
