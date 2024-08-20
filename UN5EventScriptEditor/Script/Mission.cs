using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UN5EventScriptEditor
{
    public class Mission
    {
        public static string VerifyMissionScript(MemoryStream ms, uint cond, uint blockType2)
        {
            string result = "";
            switch (blockType2)
            {
                case 0xD4:
                    result = ReadStart2DBattle(ms);
                    break;
                case 0xD6:
                    result = ReadStart3DBattle(ms);
                    break;
                case 0xD7:
                    result = ReadAddTo3DBattle(ms);
                    break;
                case 0xD8:
                    result = ReadStartCutscene(ms);
                    break;
                case 0xD9:
                    result = ReadShowSaveOption(ms);
                    break;
                case 0xDA:
                    result = ReadShowLetter(ms);
                    break;
                case 0xDB:
                    result = ReadIndicateObjectiveOnStage(ms);
                    break;
                case 0xDC:
                    result = ReadShowSpecialChapterBackground(ms);
                    break;
                case 0xDF:
                    result = ReadStartTimeAttack(ms);
                    break;
                case 0xE1:
                    result = ReadDisableBanditsOnStage(ms);
                    break;
                case 0xF2:
                    result = ReadShowShopMenu(ms);
                    break;
                case 0xF3:
                    result = ReadShowRecordMenu(ms);
                    break;
                case 0xF4:
                    result = ReadShowMissionInfo(ms);
                    break;
                case 0xF5:
                    result = ReadShowMissionResult(ms);
                    break;
                case 0xF7:
                    result = ReadSpawnTrap(ms);
                    break;
                case 0x100:
                    result = ReadStartRetryMenu(ms);
                    break;
                case 0x101:
                    result = ReadShowEditGroupMenu(ms);
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
                    result = ScriptReader.Tab() + $"UNKBLOCK 0x2E 0x{blockType2.ToString("X")} {arg1} {arg2} {arg3} {arg4} {arg5} {arg6} {arg7} {arg8} {arg9}";
                    break;
            }
            return result;
        }
        public static string ReadStart2DBattle(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            int arg2 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "START_2D_BATTLE " + $"{arg1} {arg2}";
            ms.Seek(0x1C, SeekOrigin.Current);
            return result;
        }
        public static string ReadStart3DBattle(MemoryStream ms)
        {
            string result = ScriptReader.Tab() + "START_3D_BATTLE";
            ms.Seek(0x26, SeekOrigin.Current);
            return result;
        }
        public static string ReadAddTo3DBattle(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            string result = "";
            int arg1 = Util.ReadInt32(ms);
            int arg2 = Util.ReadInt32(ms);
            int arg3 = Util.ReadInt32(ms);
            int arg4 = Util.ReadInt32(ms);
            int arg5 = Util.ReadInt32(ms);
            int arg6 = Util.ReadInt32(ms);
            switch (arg1)
            {
                case 0x1C6:
                    result = ScriptReader.Tab() + "ADD_TO_3D_BATTLE " + $"{getAddTo3DBattleType[arg1]} {arg2} {arg3} {arg4} {arg5} {arg6}";
                    break;
                default:
                    result = ScriptReader.Tab() + "ADD_TO_3D_BATTLE " + $"{arg1} {arg2} {arg3} {arg4} {arg5} {arg6}";
                    break;
            }
            ms.Seek(0xC, SeekOrigin.Current);
            return result;
        }
        public static string ReadStartCutscene(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            int arg2 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "START_CUTSCENE " + $"{arg1} {arg2}";
            ms.Seek(0x1C, SeekOrigin.Current);
            return result;
        }
        public static string ReadShowSaveOption(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            int arg2 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "SHOW_SAVE_OPTION " + $"{arg1} {arg2}";
            ms.Seek(0x1C, SeekOrigin.Current);
            return result;
        }
        public static string ReadShowLetter(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            int arg2 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "SHOW_LETTER " + $"{arg1} {arg2}";
            ms.Seek(0x1C, SeekOrigin.Current);
            return result;
        }
        public static string ReadIndicateObjectiveOnStage(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "INDICATE_OBJECTIVE_ON_STAGE " + $"{arg1}";
            ms.Seek(0x20, SeekOrigin.Current);
            return result;
        }
        public static string ReadShowSpecialChapterBackground(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            int arg2 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "SHOW_SPECIAL_CHAPTER_BACKGROUND " + $"{arg1} {arg2}";
            ms.Seek(0x1C, SeekOrigin.Current);
            return result;
        }
        public static string ReadStartTimeAttack(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            int arg2 = Util.ReadInt32(ms);
            int arg3 = Util.ReadInt32(ms);
            int arg4 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "START_TIME_ATTACK " + $"{arg1} {arg2} {arg3} {arg4}";
            ms.Seek(0x14, SeekOrigin.Current);
            return result;
        }
        public static string ReadDisableBanditsOnStage(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            int arg2 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "DISABLE_BANDITS_ON_STAGE " + $"{arg1} {arg2}";
            ms.Seek(0x1C, SeekOrigin.Current);
            return result;
        }
        public static string ReadShowShopMenu(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            int arg2 = Util.ReadInt32(ms);
            int arg3 = Util.ReadInt32(ms);
            int arg4 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "SHOW_SHOP_MENU " + $"{arg1} {arg2} {arg3} {arg4}";
            ms.Seek(0x14, SeekOrigin.Current);
            return result;
        }
        public static string ReadShowRecordMenu(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            int arg2 = Util.ReadInt32(ms);
            int arg3 = Util.ReadInt32(ms);
            int arg4 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "SHOW_RECORD_MENU " + $"{arg1} {arg2} {arg3} {arg4}";
            ms.Seek(0x14, SeekOrigin.Current);
            return result;
        }
        public static string ReadShowMissionInfo(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "SHOW_INFOMATION " + $"{arg1}";
            ms.Seek(0x20, SeekOrigin.Current);
            return result;
        }
        public static string ReadShowMissionResult(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            int arg2 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "SHOW_MISSION_RESULT " + $"{arg1} {arg2}";
            ms.Seek(0x1C, SeekOrigin.Current);
            return result;
        }
        public static string ReadSpawnTrap(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            int arg2 = Util.ReadInt32(ms);
            int arg3 = Util.ReadInt32(ms);
            int arg4 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "SPAWN_TRAP " + $"{arg1} {arg2} {arg3} {arg4}";
            ms.Seek(0x14, SeekOrigin.Current);
            return result;
        }
        public static string ReadStartRetryMenu(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            int arg2 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "START_RETRY_MENU " + $"{arg1} {arg2}";
            ms.Seek(0x1C, SeekOrigin.Current);
            return result;
        }
        public static string ReadShowEditGroupMenu(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            int arg2 = Util.ReadInt32(ms);
            int arg3 = Util.ReadInt32(ms);
            int arg4 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "SHOW_EDIT_GROUP_MENU " + $"{arg1} {arg2} {arg3} {arg4}";
            ms.Seek(0x14, SeekOrigin.Current);
            return result;
        }
        public static Dictionary<int, string> getAddTo3DBattleType = new Dictionary<int, string>()
        {
            { 0x1C6, "ALLY" },
            { 0x21, "<=" },
            { 0x22, ">=" }
        };
    }
}
