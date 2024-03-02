using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MerryDllFramework
{
    public class RTK_Helper
    {
        const CharSet charset = CharSet.Ansi;
        const CallingConvention calling_convention = CallingConvention.Cdecl;
        const string path = @".\TestItem\HDT647\RvcLib.dll";
        // GetDevFirmwareInfo
        [DllImport(path, EntryPoint = "GetDevFirmwareInfo",
                CharSet = charset, CallingConvention = calling_convention)]
        public static extern bool GetDevFirmwareInfo(ushort wdevVID, ushort wdevPID,
            out fw fwVer);
        public struct fw
        {
            public uint ver1;
            public uint ver2;
            public uint ver3;
            public uint ver4;
            public uint ver5;
        }
    }
}