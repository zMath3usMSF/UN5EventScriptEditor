using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UN5EventScriptEditor
{
    public partial class Form1 : Form
    {
        public static List<CCSF> ccsList = new List<CCSF>();
        public Form1()
        {
            InitializeComponent();
        }

        private void openCCSToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open a CCS File";
            ofd.Filter = "CyberConnect Streaming File (*.ccs)|*.ccs";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                bool isNA2 = false;
                richTextBox1.Text = "";
                pictureBox1.Visible = false;
                ccsList.Clear();
                CCSF ccs = new CCSF();
                CCSFTXT.textList.Clear();
                ScriptReader.scriptNameList.Clear();
                ScriptReader.scriptDescriptionList.Clear();
                ScriptReader.scriptBlockCount.Clear();
                ScriptReader.result.Clear();
                ScriptReader.voiceIndexes.Clear();
                ScriptWriter.blocksNames.Clear();
                ScriptWriter.blocks.Clear();
                ScriptWriter.msgStrings.Clear();
                ScriptWriter.voiceIndexes.Clear();

                string directory = Path.GetDirectoryName(ofd.FileName);
                string fileName = Path.GetFileNameWithoutExtension(ofd.FileName);
                if(File.Exists(Path.Combine(directory, fileName) + "TXT.CCS"))
                {
                    ccs.OpenCCS(Path.Combine(directory, fileName) + "TXT.CCS");
                    ccsList.Add(ccs);
                    CCSFTXT.ReadCCSFTXT(ccsList[0].blocks[3].Data, "ISO-8859-1");
                }
                else
                {
                    ccsList.Add(ccs);
                    isNA2 = true;
                }

                ccs = new CCSF();
                ccs.OpenCCS(ofd.FileName);
                ccsList.Add(ccs);
                richTextBox1.Text = string.Join(Environment.NewLine, ScriptReader.ReadAllScript(ccsList[1], isNA2));
            }
        }

        private void saveCCSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScriptWriter.WriteAllScript(richTextBox1.Lines);
        }

        private void exportToTXTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), Header.Name + ".txt"), richTextBox1.Text);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("UN5 ADV Script Editor, version 1.0.\n\nMade by zMath3usMSF.");
        }
    }
}
