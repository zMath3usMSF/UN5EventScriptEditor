using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UN5EventScriptEditor
{
    public class Camera
    {
        public static string VerifyCameraScript(MemoryStream ms, uint cond, uint blockType2)
        {
            string result = "";
            switch (blockType2)
            {
                case 0xAE:
                    result = ReadSetInitialCameraPositionTo(ms);
                    break;
                case 0xAF:
                    result = ReadSetFinalCameraPositionTo(ms);
                    break;
                case 0xB2:
                    result = ReadSetInitialCameraRotationTo(ms);
                    break;
                case 0xB3:
                    result = ReadSetFinalCameraRotationTo(ms);
                    break;
                case 0xB7:
                    result = ReadStartCameraAnimation(ms);
                    break;
                case 0xB8:
                    result = ReadUnlockCamera(ms);
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
                    result = ScriptReader.Tab() + $"UNKBLOCK 0x2B 0x{blockType2.ToString("X")} {arg1} {arg2} {arg3} {arg4} {arg5} {arg6} {arg7} {arg8} {arg9}";
                    break;
            }
            return result;
        }
        public static string ReadSetInitialCameraPositionTo(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            int arg2 = Util.ReadInt32(ms);
            int arg3 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "SET_INITIAL_CAMERA_POSITION_TO " + $"{arg1} {arg2} {arg3}";
            ms.Seek(0x18, SeekOrigin.Current);
            return result;
        }
        public static string ReadSetFinalCameraPositionTo(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            int arg2 = Util.ReadInt32(ms);
            int arg3 = Util.ReadInt32(ms);
            int arg4 = Util.ReadInt32(ms);
            int arg5 = Util.ReadInt32(ms);
            int arg6 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "SET_FINAL_CAMERA_POSITION_TO " + $"{arg1} {arg2} {arg3} {arg4} {arg5} {arg6}";
            ms.Seek(0xC, SeekOrigin.Current);
            return result;
        }
        public static string ReadSetInitialCameraRotationTo(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            int arg2 = Util.ReadInt32(ms);
            int arg3 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "SET_INITIAL_CAMERA_ROTATION_TO " + $"{arg1} {arg2} {arg3}";
            ms.Seek(0x18, SeekOrigin.Current);
            return result;
        }
        public static string ReadSetFinalCameraRotationTo(MemoryStream ms)
        {
            ms.Seek(0x2, SeekOrigin.Current);
            int arg1 = Util.ReadInt32(ms);
            int arg2 = Util.ReadInt32(ms);
            int arg3 = Util.ReadInt32(ms);
            int arg4 = Util.ReadInt32(ms);
            int arg5 = Util.ReadInt32(ms);
            int arg6 = Util.ReadInt32(ms);
            string result = ScriptReader.Tab() + "SET_FINAL_CAMERA_ROTATION_TO " + $"{arg1} {arg2} {arg3} {arg4} {arg5} {arg6}";
            ms.Seek(0xC, SeekOrigin.Current);
            return result;
        }
        public static string ReadStartCameraAnimation(MemoryStream ms)
        {
            string result = ScriptReader.Tab() + "START_CAMERA_ANIMATION";
            ms.Seek(0x26, SeekOrigin.Current);
            return result;
        }
        public static string ReadUnlockCamera(MemoryStream ms)
        {
            string result = ScriptReader.Tab() + "UNLOCK_CAMERA";
            ms.Seek(0x26, SeekOrigin.Current);
            return result;
        }
    }
}
