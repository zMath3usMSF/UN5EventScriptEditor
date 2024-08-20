using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UN5EventScriptEditor
{
    public class CCSFTXT
    {
        public static List<string[]> textList = new List<string[]>();
        public static void ReadCCSFTXT(byte[] data, string enconding)
        {
            string output = Encoding.GetEncoding(enconding).GetString(data);
            string[] outputArray = output.Split('#');
            textList.Add(new string[] { "", outputArray[0] });
            for (int i = 1; i < outputArray.Length - 1; i += 2)
            {
                textList.Add(new string[] {outputArray[i], outputArray[i + 1]});
            }
        }
    }
}
