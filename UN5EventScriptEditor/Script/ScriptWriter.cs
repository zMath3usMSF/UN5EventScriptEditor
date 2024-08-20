using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UN5EventScriptEditor
{
    public class ScriptWriter
    {
        public static string Name;
        public static List<string> blocksNames = new List<string>();
        public static List<byte[]> blocks = new List<byte[]>();
        public static List<string[]> msgStrings = new List<string[]>();
        public static List<int[]> voiceIndexes = new List<int[]>();
        static byte[] padding = { 0xFF, 0xFF };
        static byte[] cond = { 0x00, 0x00 };
        static byte[] none = { 0xFF, 0xFF, 0xFF, 0xFF };
        static string blockName;
        static string evName;
        static int count;
        byte[] bufferHeader;
        static byte[] bufferBlock;
        public static void WriteAllScript(string[] lines)
        {
            msgStrings.Insert(0, new string[] { $"", "Não" });
            msgStrings.Insert(0, new string[] { $"", "Sim" });
            msgStrings.Insert(0, new string[] { $"", "DUMMY" });
            voiceIndexes.Insert(0, new int[] { -1, 0 });
            voiceIndexes.Insert(0, new int[] { -1, 0 });
            voiceIndexes.Insert(0, new int[] { -1, -1 });
            MemoryStream ms = new MemoryStream();
            for (int i = 0; i != lines.Length; i++)
            {
                string[] lineParts = lines[i].Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (lineParts.Length != 0)
                {
                    switch (lineParts[0])
                    {
                        case "CCSNAME":
                            Name = Util.CropString(lines[i], '"');
                            break;
                        case "BLOCKNAME":
                            blockName = Util.CropString(lines[i], '"');
                            blocksNames.Add(blockName);
                            break;
                        case "EVNAME":
                            evName = Util.CropString(lines[i], '"');
                            break;
                        case "INIT":
                            count = 1;
                            cond = BitConverter.GetBytes((ushort)0);
                            ms.Write(cond, 0, 2);
                            ms.Write(padding, 0, padding.Length);
                            for (int j = lineParts.Length; j <= 10; j++)
                            {
                                ms.Write(none, 0, none.Length);
                            }
                            break;
                        case "END":
                            count++;
                            cond = BitConverter.GetBytes((ushort)1);
                            ms.Write(cond, 0, 2);
                            ms.Write(padding, 0, padding.Length);
                            for (int j = lineParts.Length; j <= 10; j++)
                            {
                                ms.Write(none, 0, none.Length);
                            }
                            ms = WriteHeader(ms);
                            blocks.Add(ms.ToArray());
                            ms = new MemoryStream();
                            break;
                        case "IF":
                            count++;
                            cond = BitConverter.GetBytes((ushort)2);
                            ms.Write(cond, 0, 2);
                            switch (lineParts[1])
                            {
                                case "DIALOGUE":
                                    ms.Write(BitConverter.GetBytes(Convert.ToUInt16(0x24)), 0, 2);
                                    ms.Write(BitConverter.GetBytes(Convert.ToUInt16(lineParts[2])), 0, 2);
                                    ms.Write(padding, 0, padding.Length);
                                    ms.Write(BitConverter.GetBytes(Convert.ToUInt32(lineParts[4])), 0, 4);
                                    ms.Write(BitConverter.GetBytes(Convert.ToUInt32(getSignal[lineParts[3]])), 0, 4);
                                    ms.Write(BitConverter.GetBytes(Convert.ToUInt32(lineParts[4])), 0, 4);
                                    ms.Write(BitConverter.GetBytes(Convert.ToUInt32(lines[i + 1].Contains("IF") == true ? 0x1D : 0x1F)), 0, 4);
                                    break;
                                case "PROGRESS":
                                    ms.Write(BitConverter.GetBytes(Convert.ToUInt16(0x26)), 0, 2);
                                    ms.Write(BitConverter.GetBytes(Convert.ToUInt16(lineParts[2])), 0, 2);
                                    ms.Write(padding, 0, padding.Length);
                                    ms.Write(BitConverter.GetBytes(Convert.ToUInt32(lineParts[4])), 0, 4);
                                    ms.Write(BitConverter.GetBytes(Convert.ToUInt32(getSignal[lineParts[3]])), 0, 4);
                                    ms.Write(BitConverter.GetBytes(Convert.ToUInt32(lineParts[4])), 0, 4);
                                    ms.Write(BitConverter.GetBytes(Convert.ToUInt32(lines[i + 1].Contains("IF") == true ? 0x1D : 0x1F)), 0, 4);
                                    break;
                                case "ITEM":
                                    ms.Write(BitConverter.GetBytes(Convert.ToUInt16(0x28)), 0, 2);
                                    ms.Write(BitConverter.GetBytes(Convert.ToUInt16(lineParts[2])), 0, 2);
                                    ms.Write(padding, 0, padding.Length);
                                    ms.Write(BitConverter.GetBytes(Convert.ToUInt32(lineParts[4])), 0, 4);
                                    ms.Write(BitConverter.GetBytes(Convert.ToUInt32(getSignal[lineParts[3]])), 0, 4);
                                    ms.Write(BitConverter.GetBytes(Convert.ToUInt32(lineParts[4])), 0, 4);
                                    ms.Write(BitConverter.GetBytes(Convert.ToUInt32(lines[i + 1].Contains("IF") == true ? 0x1D : 0x1F)), 0, 4);
                                    break;
                                default:
                                    break;
                            }
                            for (int j = 0; j <= 4; j++)
                            {
                                ms.Write(none, 0, none.Length);
                            }
                            break;
                        case "ELSE":
                            count++;
                            cond = BitConverter.GetBytes((ushort)3);
                            ms.Write(cond, 0, 2);
                            ms.Write(padding, 0, padding.Length);
                            for (int j = lineParts.Length; j <= 10; j++)
                            {
                                ms.Write(none, 0, none.Length);
                            }
                            break;
                        case "ENDIF":
                            count++;
                            cond = BitConverter.GetBytes((ushort)4);
                            ms.Write(cond, 0, 2);
                            ms.Write(padding, 0, padding.Length);
                            for (int j = lineParts.Length; j <= 10; j++)
                            {
                                ms.Write(none, 0, none.Length);
                            }
                            break;
                        case "SWITCH":
                            count++;
                            cond = BitConverter.GetBytes((ushort)5);
                            ms.Write(cond, 0, 2);
                            if (lineParts[1] == "DIALOGUE")
                            {
                                ms.Write(BitConverter.GetBytes(Convert.ToUInt16(0x24)), 0, 2);
                                ms.Write(BitConverter.GetBytes(Convert.ToUInt16(lineParts[2])), 0, 2);
                            }
                            else
                            {
                                ms.Write(BitConverter.GetBytes(Convert.ToUInt16(0x26)), 0, 2);
                                ms.Write(BitConverter.GetBytes(Convert.ToUInt16(lineParts[2])), 0, 2);
                            }
                            ms.Write(padding, 0, padding.Length);
                            for (int j = 0; j <= 8; j++)
                            {
                                ms.Write(none, 0, none.Length);
                            }
                            break;
                        case "CASE":
                            count++;
                            cond = BitConverter.GetBytes((ushort)6);
                            ms.Write(cond, 0, 2);
                            ms.Write(BitConverter.GetBytes(Convert.ToUInt16(lineParts[2])), 0, 2);
                            ms.Write(BitConverter.GetBytes(Convert.ToUInt16(getSignal[lineParts[1]])), 0, 2);
                            ms.Write(padding, 0, padding.Length);
                            for (int j = 0; j <= 8; j++)
                            {
                                ms.Write(none, 0, none.Length);
                            }
                            break;
                        case "ENDSWITCH":
                            count++;
                            cond = BitConverter.GetBytes((ushort)7);
                            ms.Write(cond, 0, 2);
                            ms.Write(padding, 0, padding.Length);
                            for (int j = lineParts.Length; j <= 10; j++)
                            {
                                ms.Write(none, 0, none.Length);
                            }
                            break;
                        case "UNKCOND":
                            count++;
                            cond = BitConverter.GetBytes((ushort)int.Parse(lineParts[1]));
                            ms.Write(cond, 0, 2);
                            ms.Write(none, 0, none.Length);
                            ms.Write(padding, 0, padding.Length);
                            for (int j = 0; j <= 8; j++)
                            {
                                ms.Write(none, 0, none.Length);
                            }
                            break;
                        case "UNKBLOCK":
                            count++;
                            ms.Write(padding, 0, padding.Length);
                            byte[] blockType1 = BitConverter.GetBytes((short)Util.HexToDec(lineParts[1]));
                            byte[] blockType2 = BitConverter.GetBytes((short)Util.HexToDec(lineParts[2]));
                            ms.Write(blockType1, 0, 2);
                            ms.Write(blockType2, 0, 2);
                            ms.Write(padding, 0, padding.Length);
                            for (int j = 0; j < lineParts.Length - 3; j++)
                            {
                                byte[] arg = BitConverter.GetBytes(Convert.ToInt32(lineParts[3 + j]));
                                ms.Write(arg, 0, arg.Length);
                            }
                            break;
                        default:
                            VerifyScript(ms, lineParts, lines[i]);
                            break;
                    }
                }
            }
            blocksNames.Add("msg_" + Name);
            blocksNames.Add("voice_" + Name);
            WriteMsgBlock();
            WriteVoiceBlock();
            CCSF.WriteCCS(Name, blocksNames, blocks);
            blocks.Clear();
            blocksNames.Clear();
            blocksNames.Add("msg_" + Name + "txt");
            WriteMsgBlock();
            CCSF.WriteCCS(Name + "txt", blocksNames, blocks);
        }
        public static void WriteMsgBlock()
        {
            MemoryStream ms = new MemoryStream();
            byte[] countBytes = Encoding.GetEncoding("ISO-8859-1").GetBytes($"{msgStrings.Count}");
            ms.Write(countBytes, 0, countBytes.Length);
            ms.WriteByte(0x23);
            for (int i = 0; i < msgStrings.Count; i++)
            {
                byte[] string1Bytes = Encoding.GetEncoding("ISO-8859-1").GetBytes(msgStrings[i][0]);
                ms.Write(string1Bytes, 0, string1Bytes.Length);
                ms.WriteByte(0x23);
                byte[] string2Bytes = Encoding.GetEncoding("ISO-8859-1").GetBytes(msgStrings[i][1]);
                ms.Write(string2Bytes, 0, string2Bytes.Length);
                ms.WriteByte(0x23);
            }
            while (ms.Length % 4 != 0)
            {
                ms.WriteByte(0x0);
            }
            blocks.Add(ms.ToArray());
        }
        public static void WriteVoiceBlock()
        {
            MemoryStream ms = new MemoryStream();
            for (int i = 0; i < voiceIndexes.Count; i++)
            {
                ms.Write(BitConverter.GetBytes((short)voiceIndexes[i][1]), 0, 2);
                ms.Write(BitConverter.GetBytes((short)voiceIndexes[i][0]), 0, 2);
            }
            blocks.Add(ms.ToArray());
        }
        public static void VerifyScript(MemoryStream ms, string[] lineParts, string line)
        {
            count++;
            if (scriptTypes.TryGetValue(lineParts[0], out byte[] value))
            {
                ms.Write(padding, 0, padding.Length);
                ms.Write(value, 0, value.Length);
                ms.Write(padding, 0, padding.Length);
            }
            else
            {
                ms.Write(padding, 0, padding.Length);
                ms.Write(none, 0, none.Length);
                ms.Write(padding, 0, padding.Length);
            }
            switch (lineParts[0])
            {
                case "SHOW_DIALOGUE":
                    List<string> strings = Util.RemoveSpacesExceptInQuotes(line);
                    for (int i = 1; i <= 2 && i < strings.Count; i++)
                    {
                        strings[i] = Util.RemoveQuotes(strings[i]);
                    }
                    string string1 = strings[1];
                    string string2 = strings[2];
                    int voice1 = int.Parse(lineParts[lineParts.Length - 1]);
                    int voice2 = int.Parse(lineParts[lineParts.Length - 2]);
                    for (int k = 0; k < msgStrings.Count; k++)
                    {
                        if (msgStrings[k][0] == string1 && msgStrings[k][1] == string2 &&
                            voiceIndexes[k][0] == voice1 && voiceIndexes[k][1] == voice2)
                        {
                            ms.Write(BitConverter.GetBytes(Convert.ToUInt32(k + 1)), 0, 4);
                            break;
                        }
                        if (k == msgStrings.Count - 1)
                        {
                            msgStrings.Add(new string[] { string1, string2 });
                            int[] indexes = { int.Parse(lineParts[lineParts.Length - 1]), int.Parse(lineParts[lineParts.Length - 2]) };
                            voiceIndexes.Add(indexes);
                            ms.Write(BitConverter.GetBytes(Convert.ToUInt32(msgStrings.Count)), 0, 4);
                            break;
                        }
                    }
                    for (int j = 0; j <= 7; j++)
                    {
                        ms.Write(none, 0, none.Length);
                    }
                    break;
                case "SHOW_MESSAGE_BOX_TXT":
                    List<string> strings2 = Util.RemoveSpacesExceptInQuotes(line);
                    for (int i = 1; i <= 2 && i < strings2.Count; i++)
                    {
                        strings2[i] = Util.RemoveQuotes(strings2[i]);
                    }
                    string string3 = strings2[1];
                    for (int k = 0; k < msgStrings.Count; k++)
                    {
                        if (msgStrings[k][1] == string3)
                        {
                            ms.Write(BitConverter.GetBytes(Convert.ToUInt32(k + 1)), 0, 4);
                            break;
                        }
                        if (k == msgStrings.Count - 1)
                        {
                            msgStrings.Add(new string[] { "", string3 });
                            int[] indexes = { -1, -1 };
                            voiceIndexes.Add(indexes);
                            ms.Write(BitConverter.GetBytes(Convert.ToUInt32(msgStrings.Count)), 0, 4);
                            break;
                        }
                    }
                    for (int j = 0; j <= 7; j++)
                    {
                        ms.Write(none, 0, none.Length);
                    }
                    break;
                case "SHOW_ANSWER_MESSAGE_BOX":
                    List<string> strings3 = Util.RemoveSpacesExceptInQuotes(line);
                    for (int i = 1; i <= 2 && i < strings3.Count; i++)
                    {
                        strings3[i] = Util.RemoveQuotes(strings3[i]);
                    }
                    for (int k = 1; k < strings3.Count; k++)
                    {
                        for (int j = 0; j < msgStrings.Count; j++)
                        {
                            if (msgStrings[j][1] == strings3[k])
                            {
                                ms.Write(BitConverter.GetBytes(Convert.ToUInt32(j + 1)), 0, 4);
                                break;
                            }
                            if (j == msgStrings.Count - 1)
                            {
                                msgStrings.Add(new string[] { "", strings3[k] });
                                int[] indexes = { -1, -1 };
                                voiceIndexes.Add(indexes);
                                ms.Write(BitConverter.GetBytes(Convert.ToUInt32(msgStrings.Count)), 0, 4);
                                break;
                            }
                        }
                    }
                    for (int j = 0; j < 9 - (strings3.Count - 1); j++)
                    {
                        ms.Write(none, 0, none.Length);
                    }
                    break;
                case "ADD_TO_3D_BATTLE":
                    if (getAddTo3DBattleType.TryGetValue(lineParts[1], out int value4))
                    {
                        ms.Write(BitConverter.GetBytes(Convert.ToInt32(value4)), 0, 4);
                        for (int i = 0; i < lineParts.Length - 2; i++)
                        {
                            byte[] arg = BitConverter.GetBytes(Convert.ToInt32(lineParts[2 + i]));
                            ms.Write(arg, 0, arg.Length);
                        }
                        for (int i = lineParts.Length; i <= 9; i++)
                        {
                            ms.Write(none, 0, none.Length);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < lineParts.Length - 1; i++)
                        {
                            byte[] arg = BitConverter.GetBytes(Convert.ToInt32(lineParts[1 + i]));
                            ms.Write(arg, 0, arg.Length);
                        }
                        for (int i = lineParts.Length; i <= 9; i++)
                        {
                            ms.Write(none, 0, none.Length);
                        }
                    }
                    break;
                default:
                    WriteScript(ms, lineParts);
                    break;
            }
        }
        public static void WriteScript(MemoryStream ms, string[] lineParts)
        {
            for (int i = 0; i < lineParts.Length - 1; i++)
            {
                byte[] arg = BitConverter.GetBytes(Convert.ToInt32(lineParts[1 + i]));
                ms.Write(arg, 0, arg.Length);
            }
            for (int i = lineParts.Length; i <= 9; i++)
            {
                ms.Write(none, 0, none.Length);
            }
        }
        public static MemoryStream WriteHeader(MemoryStream ms)
        {
            bufferBlock = ms.ToArray();
            ms = new MemoryStream();
            ms.Write(Encoding.UTF8.GetBytes("100"), 0, Encoding.UTF8.GetBytes("100").Length);
            ms.WriteByte(0x7C);
            ms.Write(Encoding.GetEncoding("shift-jis").GetBytes(blockName), 0, Encoding.GetEncoding("shift-jis").GetBytes(blockName).Length);
            ms.WriteByte(0x7C);
            ms.Write(Encoding.GetEncoding("shift-jis").GetBytes(evName), 0, Encoding.GetEncoding("shift-jis").GetBytes(evName).Length);
            ms.WriteByte(0x7C);
            byte[] countBytes = Encoding.GetEncoding("shift-jis").GetBytes(Convert.ToString(count));
            ms.Write(countBytes, 0, countBytes.Length);
            ms.WriteByte(0x7C);
            ms.Write(bufferBlock, 0, bufferBlock.Length);
            while (ms.Length % 4 != 0)
            {
                ms.WriteByte(0);
            }
            return ms;
        }

        public static Dictionary<string, byte[]> scriptTypes = new Dictionary<string, byte[]>()
        {
            { "SWITCH_NOW_TO_STAGE", new byte[] { 0x23, 0x00, 0x37, 0x00 } },
            { "SWITCH_AFTER_TO_STAGE", new byte[] { 0x23, 0x00, 0x3A, 0x00 } },
            { "UPDATE_DIALOGUE", new byte[] { 0x24, 0x00, 0x42, 0x00 } },
            { "UPDATE_PROGRESS", new byte[] { 0x26, 0x00, 0x50, 0x00 } },
            { "SET_ITEM_TO_PLAYER", new byte[] { 0x28, 0x00, 0x5F, 0x00 } },
            { "LOCK_PLAYER_ACTIONS", new byte[] { 0x29, 0x00, 0x70, 00 } },
            { "UNLOCK_PLAYER_ACTIONS", new byte[] { 0x29, 0x00, 0x71, 0x00 } },
            { "SET_PLAYER_POSITION", new byte[] { 0x29, 0x00, 0x72, 0x00 } },
            { "SET_PLAYER_ROTATION", new byte[] { 0x29, 0x00, 0x73, 0x00 } },
            { "ROTATE_PLAYER_TO_NPC", new byte[] { 0x29, 0x00, 0x82, 0x00 } },
            { "UNLOCK_PLAYER_FROM_DIALOGUE", new byte[] { 0x29, 0x00, 0x83, 0x00 } },
            { "SWITCH_PLAYER_TO", new byte[] { 0x29, 0x00, 0x8C, 0x00 } },
            { "SPAWN_NPC", new byte[] { 0x2A, 0x00, 0x8E, 0x00 } },
            { "REMOVE_NPC", new byte[] { 0x2A, 0x00, 0x8F, 0x00 } },
            { "SET_NPC_POSITION_TO", new byte[] { 0x2A, 0x00, 0x94, 0x00 } },
            { "SET_NPC_ROTATION_TO", new byte[] { 0x2A, 0x00, 0x95, 0x00 } },
            { "SPAWN_NPC_SMOKE", new byte[] { 0x2A, 0x00, 0x9A, 0x00 } },
            { "REMOVE_NPC_SMOKE", new byte[] { 0x2A, 0x00, 0x9B, 0x00 } },
            { "ROTATE_NPC_TO_PLAYER", new byte[] { 0x2A, 0x00, 0xA4, 0x00 } },
            { "UNLOCK_NPC_TO_TALK", new byte[] { 0x2A, 0x00, 0xA5, 0x00 } },
            { "SET_INITIAL_CAMERA_POSITION_TO", new byte[] { 0x2B, 0x00, 0xAE, 0x00 } },
            { "SET_FINAL_CAMERA_POSITION_TO", new byte[] { 0x2B, 0x00, 0xAF, 0x00 } },
            { "SET_INITIAL_CAMERA_ROTATION_TO", new byte[] { 0x2B, 0x00, 0xB2, 0x00 } },
            { "SET_FINAL_CAMERA_ROTATION_TO", new byte[] { 0x2B, 0x00, 0xB3, 0x00 } },
            { "START_CAMERA_ANIMATION", new byte[] { 0x2B, 0x00, 0xB7, 0x00 } },
            { "UNLOCK_CAMERA", new byte[] { 0x2B, 0x00, 0xB8, 0x00 } },
            { "SHOW_DIALOGUE", new byte[] { 0x2C, 0x00, 0xBE, 0x00 } },
            { "CLOSE_DIALOGUE", new byte[] { 0x2C, 0x00, 0xBF, 0x00 } },
            { "SHOW_ANSWER_BOX", new byte[] { 0x2C, 0x00, 0xC0, 0x00 } },
            { "CLOSE_ANSWER_BOX", new byte[] { 0x2C, 0x00, 0xC1, 0x00 } },
            { "SHOW_OBTAINED_ITEM_MESSAGE", new byte[] { 0x2C, 0x00, 0xC2, 0x00 } },
            { "CLOSE_OBTAINED_ITEM_MESSAGE", new byte[] { 0x2C, 0x00, 0xC3, 0x00 } },
            { "SHOW_MESSAGE_BOX_TXT", new byte[] { 0x2C, 0x00, 0xC4, 0x00 } },
            { "CLOSE_MESSAGE_BOX_TXT", new byte[] { 0x2C, 0x00, 0xC5, 0x00 } },
            { "SHOW_PRINT_MESSAGE", new byte[] { 0x2C, 0x00, 0xC6, 0x00 } }, //
            { "CLOSE_PRINT_MESSAGE", new byte[] { 0x2C, 0x00, 0xC7, 0x00 } }, //
            { "START_ACT_ANIMATION", new byte[] { 0x2C, 0x00, 0xC8, 0x00 } },
            { "SHOW_GROUP_INFO", new byte[] { 0x2C, 0x00, 0xCB, 0x00 } }, //
            { "START_SCREEN_TRANSITION_EFFECT", new byte[] { 0x2D, 0x00, 0xCD, 0x00 } }, //
            { "START_2D_BATTLE", new byte[] { 0x2E, 0x00, 0xD4, 0x00 } },
            { "START_3D_BATTLE", new byte[] { 0x2E, 0x00, 0xD6, 0x00 } },
            { "ADD_TO_3D_BATTLE", new byte[] { 0x2E, 0x00, 0xD7, 0x00 } }, //
            { "START_CUTSCENE", new byte[] { 0x2E, 0x00, 0xD8, 0x00 } },
            { "SHOW_SAVE_OPTION", new byte[] { 0x2E, 0x00, 0xD9, 0x00 } },
            { "SHOW_LETTER", new byte[] { 0x2E, 0x00, 0xDA, 0x00 } },
            { "INDICATE_OBJECTIVE_ON_STAGE", new byte[] { 0x2E, 0x00, 0xDB, 0x00 } },
            { "SHOW_SPECIAL_CHAPTER_BACKGROUND", new byte[] { 0x2E, 0x00, 0xDC, 0x00 } }, //
            { "START_TIME_ATTACK", new byte[] { 0x2E, 0x00, 0xDF, 0x00 } },
            { "DISABLE_BANDITS_ON_STAGE", new byte[] { 0x2E, 0x00, 0xE1, 0x00 } },
            { "SHOW_SHOP_MENU", new byte[] { 0x2E, 0x00, 0xF2, 0x00 } }, //
            { "SHOW_RECORD_MENU", new byte[] { 0x2E, 0x00, 0xF3, 0x00 } }, //
            { "SHOW_INFOMATION", new byte[] { 0x2E, 0x00, 0xF4, 0x00 } },
            { "SHOW_MISSION_RESULT", new byte[] { 0x2E, 0x00, 0xF5, 0x00 } }, //
            { "SPAWN_TRAP", new byte[] { 0x2E, 0x00, 0xF7, 0x00 } },
            { "START_RETRY_MENU", new byte[] { 0x2E, 0x00, 0x00, 0x01 } },
            { "SHOW_EDIT_GROUP_MENU", new byte[] { 0x2E, 0x00, 0x01, 0x01 } },
            { "PLAY_VAG_SOUND", new byte[] { 0x2F, 0x00, 0x02, 0x01 } },
            { "PLAY_AFS_SOUND", new byte[] { 0x2F, 0x00, 0x04, 0x01 } },
            { "WAIT", new byte[] { 0x30, 0x00, 0x1A, 0x01 } }
        };
        public static Dictionary<string, int> getSignal = new Dictionary<string, int>()
        {
            { "==", 0x20 },
            { "<=", 0x21},
            { ">=", 0x22 }
        };

        public static Dictionary<string, int> getAddTo3DBattleType = new Dictionary<string, int>()
        {
            { "ALLY", 0x1C6 },
        };
    }
}
