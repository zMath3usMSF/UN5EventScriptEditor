using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UN5EventScriptEditor
{
    public class AlphanumComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x == null || y == null) return string.Compare(x, y);

            var regex = new Regex(@"(\d+|\D+)");
            var xParts = regex.Split(x);
            var yParts = regex.Split(y);

            int i = 0;
            int j = 0;

            while (i < xParts.Length && j < yParts.Length)
            {
                int comparison;
                if (int.TryParse(xParts[i], out int xNum) && int.TryParse(yParts[j], out int yNum))
                {
                    comparison = xNum.CompareTo(yNum);
                }
                else
                {
                    comparison = string.Compare(xParts[i], yParts[j], StringComparison.OrdinalIgnoreCase);
                }

                if (comparison != 0)
                    return comparison;

                i++;
                j++;
            }

            return xParts.Length.CompareTo(yParts.Length);
        }
    }
    public class ScriptReader
    {
        public static int tabCount = 0;
        public static List<string> scriptNameList = new List<string>();
        public static List<string> scriptDescriptionList = new List<string>();
        public static List<int> scriptBlockCount = new List<int>();
        public static List<string> result = new List<string>();
        public static List<int[]> voiceIndexes = new List<int[]>();
        public static int remove;

        public static List<string> ReadAllScript(CCSF ccs, bool isNA2)
        {
            ccs.blocks.RemoveAt(0);
            ccs.blocks.RemoveAt(0);
            var listaOrdenada = TOC.objectNameList
            .Select((item, index) => new { Item = item, Index = index })
            .OrderBy(x => x.Item, new AlphanumComparer())
            .ToList();

            // Mapeando índices originais para índices ordenados
            var novoIndice = listaOrdenada
            .Select((x, index) => new { OriginalIndex = x.Index, NewIndex = index })
            .ToDictionary(x => x.OriginalIndex, x => x.NewIndex);

            // Aplicando a ordenação à lista de valores
            var listaValoresOrdenada = ccs.blocks
                .Select((value, index) => new { Value = value, Index = index })
                .OrderBy(x => novoIndice[x.Index])
                .Select(x => x.Value)
                .ToList();

            ccs.blocks = listaValoresOrdenada;

            byte[] DataI = ccs.blocks[ccs.blocks.Count - 1].Data;
            for (int j = 0; j < DataI.Length / 4; j++)
            {
                int[] voice = { BitConverter.ToInt16(DataI, 0 + j * 4), BitConverter.ToInt16(DataI, 2 + j * 4) };
                voiceIndexes.Add(voice);
            }
            result.Add($"CCSNAME \"{Header.Name}\"\n");
            for(int i = 0; i < ccs.blocks.Count; i++)
            {
                int ID = (int)ccs.blocks[i].ID;
                string Name = TOC.objectNameList[ID];
                if(Name.Contains("BIN_msg_") && isNA2 == true)
                {
                    byte[] DataM = ccs.blocks[i].Data;
                    CCSFTXT.ReadCCSFTXT(DataM, "shift-jis");
                }
                if (!Name.Contains("BIN_msg_") &&
                    !Name.Contains("BIN_voice_") && Name != "")
                {
                    byte[] Data = ccs.blocks[i].Data;
                    string Header = Util.ReadFixedLenString(Data, 0, Data.Length);

                    scriptNameList.Add(Header.Split('|')[1]);
                    result.Add("BLOCKNAME " + $"\"{Header.Split('|')[1]}\"");
                    scriptDescriptionList.Add(Header.Split('|')[2]);
                    result.Add("EVNAME " + $"\"{Header.Split('|')[2]}\"");
                    scriptBlockCount.Add(int.Parse(Header.Split('|')[3]));

                    MemoryStream ms = new MemoryStream(Data);
                    byte[] byteArray = Encoding.GetEncoding("shift-jis").GetBytes(Header);
                    ms.Position = byteArray.Length;
                    ReadScriptAllBlock(ms, int.Parse(Header.Split('|')[3]));
                }
            }

            return result;
        }

        public static void ReadScriptAllBlock(MemoryStream ms, int count)
        {
            while(true)
            {
                uint cond = Util.ReadUInt16(ms);
                uint blockType1 = Util.ReadUInt16(ms);
                uint blockType2 = Util.ReadUInt16(ms);
                switch (cond)
                {
                    case 0:
                        tabCount = 1;
                        result.Add(Tab() + "INIT");
                        tabCount++;
                        break;
                    case 1:
                        tabCount--;
                        result.Add(Tab() + "END\n");
                        break;
                    case 2: //IF
                        break;
                    case 3:
                        ScriptReader.tabCount--;
                        result.Add(Tab() + "ELSE");
                        ScriptReader.tabCount++;
                        break;
                    case 4:
                        ScriptReader.tabCount--;
                        result.Add(Tab() + "ENDIF");
                        tabCount -= remove;
                        if(remove != 0)
                        {
                            remove = 0;
                        }
                        break;
                    case 5: //SWITCH
                        break;
                    case 6: //CASE
                        break;
                    case 7: //ENDSWITCH
                        ScriptReader.tabCount -= 2;
                        result.Add(Tab() + "ENDSWITCH");
                        break;
                    case 0xFFFF:
                        break;
                    default:
                        result.Add(ScriptReader.Tab() + $"UNKCOND {cond}");
                        break;
                }
                if (cond == 1)
                {
                    break;
                }
                string line;
                switch (blockType1)
                {
                    case 0x23:
                        line = Stage.VerifyStageScript(ms, cond, blockType2);
                        result.Add(line);
                        break;
                    case 0x24:
                        line = MCDDialogue.ReadIFScript(ms, cond, blockType1, blockType2);
                        result.Add(line);
                        break;
                    case 0x26:
                        line = MCDProgress.VerifyMCDScript(ms, cond, blockType2);
                        result.Add(line);
                        break;
                    case 0x28:
                        line = Item.VerifyItemScript(ms, cond, blockType2);
                        result.Add(line);
                        break;
                    case 0x29:
                        line = Player.VerifyPlayerScript(ms, cond, blockType2);
                        result.Add(line);
                        break;
                    case 0x2A:
                        line = NPC.VerifyNPCScript(ms, cond, blockType2);
                        result.Add(line);
                        break;
                    case 0x2B:
                        line = Camera.VerifyCameraScript(ms, cond, blockType2);
                        result.Add(line);
                        break;
                    case 0x2C:
                        line = Text.VerifyTextScript(ms, cond, blockType2);
                        result.Add(line);
                        break;
                    case 0x2D:
                        line = Other1.VerifyOther1Script(ms, cond, blockType2);
                        result.Add(line);
                        break;
                    case 0x2E:
                        line = Mission.VerifyMissionScript(ms, cond, blockType2);
                        result.Add(line);
                        break;
                    case 0x2F:
                        line = Sound.VerifySoundScript(ms, cond, blockType2);
                        result.Add(line);
                        break;
                    case 0x30:
                        line = Other2.VerifyOther2Script(ms, cond, blockType2);
                        result.Add(line);
                        break;
                    case 0xFFFF:
                        ms.Seek(0x26, SeekOrigin.Current);
                        break;
                    default:
                        if(cond == 6)
                        {
                            ms.Seek(0x2, SeekOrigin.Current);
                            if (result[result.Count - 1].Contains("SWITCH") == false)
                            {
                                tabCount--;
                            }
                            result.Add(Tab() + "CASE " + $"{getSignal[(int)blockType2]} {blockType1}");
                            tabCount++;
                            ms.Seek(0x24, SeekOrigin.Current);
                        }
                        else
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
                            int arg9 = Util.ReadInt32(ms);
                            result.Add(Tab() + $"UNKBLOCK 0x{blockType1.ToString("X")} 0x{blockType2.ToString("X")} {arg1} {arg2} {arg3} {arg4} {arg5} {arg6} {arg7} {arg8} {arg9}");
                        }
                        break;
                }
            }
            while (true)
            {
                if (ms.Position % 4 != 0)
                {
                    ms.Seek(1, SeekOrigin.Current);
                }
                else
                {
                    break;
                }
            }
        }
        public static string Tab()
        {
            return new string('\t', tabCount);
        }

        public static Dictionary<uint, string> scriptType = new Dictionary<uint, string>
        {
        { 0x002D0026, "CHECK_AOI_PENDANT" },
        { 0x00500026, "WRITE_TO_MEMORY_CARD" },
        { 0x00820029, "PLAYER_ROTATES_TO_NPC" },
        { 0x00830029, "UNLOCK_PLAYER_FROM_DIALOGUE" },
        { 0x00700029, "LOCK_PLAYER_ACTIONS" },
        { 0x00710029, "UNLOCK_PLAYER_ACTIONS" },
        { 0x00720029, "SET_PLAYER_POSITION_TO" },
        { 0x00730029, "SET_PLAYER_ROTATION_TO" },
        { 0x008C0029, "SWITCH_TO_CHAR" },
        { 0x005F0028, "SET_ITEM_PLAYER" },
        { 0x003A0023, "SWITCH_TO_STAGE" },
        { 0x00370023, "SWITCH_IMMEDIATELY_TO_STAGE" },
        { 0x00F4002E, "SHOW_MESSAGE_SCREEN" },
        { 0x00DB002E, "INDICATE_OBJECTIVE_TO_STAGE" },
        { 0x00D8002E, "START_THIS_CUTSCENE" },
        { 0x00D7002E, "START_THIS_3D_BATTLE" },
        { 0x00D4002E, "START_THIS_2D_BATTLE" },
        { 0x00DF002E, "START_TIME_ATTACK" },
        { 0x00C8002C, "SHOW_THIS_ADVTITLE_ANM" },
        { 0x00C2002C, "SHOW_MESSAGE_BOX_ITEM" },
        { 0x00C3002C, "CLOSE_MESSAGE_BOX_ITEM" },
        { 0x00C0002C, "SHOW_ANSWER_MESSAGE_BOX" },
        { 0x00C1002C, "CLOSE_ANSWER_MESSAGE_BOX" },
        { 0x00C4002C, "SHOW_THIS_MESSAGE_BOX_TXT" },
        { 0x00C5002C, "CLOSE_MESSAGE_BOX_TXT" },
        { 0x00BE002C, "START_THIS_DIALOGUE" },
        { 0x00BF002C, "CLOSE_DIALOGUE" },
        { 0x00AE002B, "SET_INITIAL_CAMERA_POSITION_TO" },
        { 0x00AF002B, "SET_FINAL_CAMERA_POSITION_TO" },
        { 0x00B2002B, "SET_INITIAL_CAMERA_ROTATION_TO" },
        { 0x00B3002B, "SET_FINAL_CAMERA_ROTATION_TO" },
        { 0x00B7002B, "START_CAMERA_COMMANDS" },
        { 0x00B8002B, "UNLOCK_FREE_CAMERA" },
        { 0x008E002A, "SPAWN_THIS_NPC" },
        { 0x008F002A, "UNSPAWN_THIS_NPC" },
        { 0x009A002A, "SPAWN_THIS_NPC_WITH_SMOKE_EFF" },
        { 0x009B002A, "UNSPAWN_THIS_NPC_WITH_SMOKE_EFF" },
        { 0x00A4002A, "ROTATE_THIS_NPC_TO_CHAR" },
        { 0x00A5002A, "UNLOCK_THIS_NPC_TO_TALK" },
        { 0x00CD002D, "SET_SCREEN_TRANSITION_EFFECT" },
        { 0x01100030, "----- IF_YOU_LOSE_START." },
        { 0x01160030, "HIDE_HUD" },
        { 0x011A0030, "WAIT_TO_RUN_THE_NEXT_SCRIPT" },
        { 0x01390032, "SET_THIS_CHAR_TO_TEAM" },
        { 0x0100002E, "START_RETRY_MENU" },
        { 0xFFFFFFFF, "" }
           // Adicione mais entradas conforme necessário
        };

        public static Dictionary<uint, string> buscarscriptType { get => scriptType; set => scriptType = value; }

        public static Dictionary<int, string> getSignal = new Dictionary<int, string>()
        {
            { 0x20, "==" },
            { 0x21, "<=" },
            { 0x22, ">=" }
        };
    }
}
