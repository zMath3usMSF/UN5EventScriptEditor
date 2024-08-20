using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UN5EventScriptEditor
{
    public class Util
    {
        public static UInt16 ReadUInt16(MemoryStream ms)
        {
            byte[] buffer = new byte[2];
            ms.Read(buffer, 0, 2);
            return BitConverter.ToUInt16(buffer, 0);
        }
        public static UInt32 ReadUInt32(MemoryStream ms)
        {
            byte[] buffer = new byte[4];
            ms.Read(buffer, 0, 4);
            return BitConverter.ToUInt32(buffer, 0);
        }
        public static Int32 ReadInt32(MemoryStream ms)
        {
            byte[] buffer = new byte[4];
            ms.Read(buffer, 0, 4);
            return BitConverter.ToInt32(buffer, 0);
        }

        public static string ReadFixedLenString(byte[] buff, int pos, int len)
        {
            int i = 0;
            List<byte> fileStrBytes = new List<byte>();

            while (buff[pos] != 0 && i < len)
            {
                fileStrBytes.Add(buff[pos++]);
                i++;
            }
            return Encoding.GetEncoding("shift-jis").GetString(fileStrBytes.ToArray());
        }

        public static int HexToDec(string stringValue)
        {
            if (stringValue.StartsWith("0x"))
            {
                stringValue = stringValue.Substring(2);
            }
            return Convert.ToInt32(stringValue, 16);
        }
        public static string CropString(string stringValue, char charValue)
        {
            int init = stringValue.IndexOf('"') + 1;
            int end = stringValue.LastIndexOf('"');
            return stringValue.Substring(init, end - init);
        }
        public static string CropString2(string texto, int index)
        {
            Regex regex = new Regex("\"([^\"]*)\"");
            MatchCollection matches = regex.Matches(texto);

            if (index >= 0 && index < matches.Count)
            {
                return matches[index].Groups[1].Value;
            }
            else
            {
                return null;
            }
        }
        public static void WriteString(MemoryStream ms, string stringValue, int length)
        {
            byte[] stringBytes = Encoding.GetEncoding("shift-jis").GetBytes(stringValue);
            ms.Write(stringBytes, 0, stringBytes.Length);
            for (int i = stringBytes.Length; i < length; i++)
            {
                ms.WriteByte(0x0);
            }
        }
        public static List<string> RemoveSpacesExceptInQuotes(string input)
        {
            List<string> result = new List<string>();
            StringBuilder currentPart = new StringBuilder();
            bool insideQuotes = false;

            foreach (char c in input)
            {
                if (c == '"')
                {
                    insideQuotes = !insideQuotes;
                    currentPart.Append(c);
                }
                else if (char.IsWhiteSpace(c) && !insideQuotes)
                {
                    if (currentPart.Length > 0)
                    {
                        result.Add(currentPart.ToString().Trim());
                        currentPart.Clear();
                    }
                }
                else
                {
                    if (currentPart.Length == 0 && char.IsWhiteSpace(c) && !insideQuotes)
                    {
                        continue;
                    }

                    currentPart.Append(c);
                }
            }

            if (currentPart.Length > 0)
            {
                result.Add(currentPart.ToString().Trim());
            }

            return result;
        }
        public static string RemoveQuotes(string part)
        {
            if (part.Length > 1 && part[0] == '"' && part[part.Length - 1] == '"')
            {
                return part.Substring(1, part.Length - 2);
            }
            return part;
        }
    }
}
