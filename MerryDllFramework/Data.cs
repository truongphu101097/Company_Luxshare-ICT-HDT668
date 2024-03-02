﻿using MerryTestFramework.testitem.Computer;
using MerryTestFramework.testitem.Headset;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MerryDllFramework
{
    /// <summary>
    /// 参数存放类
    /// </summary>
    internal class Data
    {    
        /// <summary>
        /// 必要，标识当前机型名
        /// </summary>
        readonly internal string _type = "HDT668";
        readonly internal string _dllpath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private ReadFile rf = new ReadFile();
        private GetHandle gh = new GetHandle();      
        internal Data()
        {
            //加载Message以及Config文件
            messagedic = rf.GetDicDataAsync($"./TestItem/{_type}/Message.txt", '=').Result;
            configdic = rf.GetDicDataAsync($"./TestItem/{_type}/Config.txt", '=').Result;          
            //加载设备PIDVID
            TXPID = configdic["TXPID"];
            TXVID = configdic["TXVID"];
            RXPID = configdic["RXPID"];
            RXVID = configdic["RXVID"];         
            devicename = configdic["devicename"];
            chemID = configdic["chemID"];
        }
        internal string devicename = null;    
        internal int lenth1 = 20;
        internal int lenth2 = 62;
        internal int lenth3 = 64;
        internal string chemID = null;      
        /// <summary>
        /// 气缸串口
        /// </summary>
        
        /// <summary>
        /// CBC串口
        /// </summary>
        internal string TXPID = null;
        internal string TXVID = null;
        internal string RXPID = null;
        internal string RXVID = null;
        internal int length1 = 515;
        internal int length2 = 64;
        internal IntPtr headsethandle1 = IntPtr.Zero;
        internal IntPtr headsethandle2 = IntPtr.Zero;
        internal IntPtr headsethandle3 = IntPtr.Zero;
        internal IntPtr headsethandle4 = IntPtr.Zero;

        internal string RFDongleBD = "";

        internal string headsetpath1 = null;
        internal string headsetpath2 = null;
        internal string headsetpath3 = null;
        internal string headsetpath4 = null;

        internal IntPtr donglehandel1 = IntPtr.Zero;
        internal IntPtr donglehandel2 = IntPtr.Zero;
        internal IntPtr donglehandel3 = IntPtr.Zero;
        internal IntPtr donglehandel4 = IntPtr.Zero;

        internal string donglepath1 = null;
        internal string donglepath2 = null;
        internal string donglepath3 = null;
        internal string donglepath4 = null;
        /// <summary>
        /// 存储message文本字典
        /// </summary>
        internal Dictionary<string, string> messagedic = new Dictionary<string, string>();
        /// <summary>
        /// 存储config文本字典
        /// </summary>
        internal Dictionary<string, string> configdic = new Dictionary<string, string>();
        /// <summary>
        /// 存储黑色bin
        /// </summary>
        internal Dictionary<string, string> blackdic = new Dictionary<string, string>();
        /// <summary>
        /// 存储蓝色bin
        /// </summary>
        internal Dictionary<string, string> bluedic = new Dictionary<string, string>();
        /// <summary>
        /// 存储白色bin
        /// </summary>
        internal Dictionary<string, string> whitedic = new Dictionary<string, string>();
        /// <summary>
        /// 存储主程序传送参数
        /// </summary>
        internal List<string> formsData = new List<string>();
        /// <summary>
        /// 存储主程序主窗体句柄
        /// </summary>
        internal IntPtr handle = IntPtr.Zero;
        internal string standaloneModel;


        public bool Mesflag { get; set; }


        /// <summary>
        /// 关闭Handel(防止某些耳机PIDVID会改变)
        /// </summary>
        internal void CloseHandel()
        {
            gh.CloseHandle();

            headsethandle1 = gh.headsethandle[0];
            headsethandle2 = gh.headsethandle[1];
            headsethandle3 = gh.headsethandle[2];
            headsethandle4 = gh.headsethandle[3];

            headsetpath1 = gh.headsetpath[0];
            headsetpath2 = gh.headsetpath[1];
            headsetpath3 = gh.headsetpath[2];
            headsetpath4 = gh.headsetpath[3];

            donglehandel1 = gh.donglehandle[0];
            donglehandel2 = gh.donglehandle[1];
            donglehandel3 = gh.donglehandle[2];
            donglehandel4 = gh.donglehandle[3];

            donglepath1 = gh.donglepath[0];
            donglepath2 = gh.donglepath[1];
            donglepath3 = gh.donglepath[2];
            donglepath4 = gh.donglepath[3];
        }
        /// <summary>
        /// 打开Handel(防止某些耳机PIDVID会改变)
        /// </summary>
        internal void OpenHandel(string RXPID, string RXVID, string TXPID, string TXVID)
        {
            gh.gethandle(RXPID, RXVID, TXPID, TXVID);

            headsethandle1 = gh.headsethandle[0];
            headsethandle2 = gh.headsethandle[1];
            headsethandle3 = gh.headsethandle[2];
            headsethandle4 = gh.headsethandle[3];

            headsetpath1 = gh.headsetpath[0];
            headsetpath2 = gh.headsetpath[1];
            headsetpath3 = gh.headsetpath[2];
            headsetpath4 = gh.headsetpath[3];

            donglehandel1 = gh.donglehandle[0];
            donglehandel2 = gh.donglehandle[1];
            donglehandel3 = gh.donglehandle[2];
            donglehandel4 = gh.donglehandle[3];

            donglepath1 = gh.donglepath[0];
            donglepath2 = gh.donglepath[1];
            donglepath3 = gh.donglepath[2];
            donglepath4 = gh.donglepath[3];
        }
    }
}
