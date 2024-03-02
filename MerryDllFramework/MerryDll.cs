using AWHID;
using AWOTA;
using MerryTestFramework.testitem.Computer;
using MerryTestFramework.testitem.Headset;
using MerryTestFramework.testitem.Other;
using MerryTestFramework.testitem.Utils;
using MPSample.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using MerryTestFramework.testitem;
using System.IO.Ports;
using PC_VolumeControl;
using WindowsFormsApplication1;
using System.Windows.Forms;
using MerryTest.testitem;
using SwATE_Net;
using System.Web;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace MerryDllFramework
{
    public class MerryDll : IMerryDll
    {
        /// <summary>
        /// 必要，因为反射不会自动创建构造函数
        /// </summary>
        public MerryDll()
        {

        }
        private Data data = new Data();


        //private AW_HID_RTK _AW_HID_RTK = new AW_HID_RTK();


        private AW_HID_OTA _AW_HID_RTK = new AW_HID_OTA();

        private PC_VolumeControl.VolumeControl volume = new PC_VolumeControl.VolumeControl();
        public MESDLL.MESBDA mes = new MESDLL.MESBDA();
        private SwATE mesVN = new SwATE();
        #region 通用DLL类实例化区 ：实例化MerryTestFramework.testitem.dll中帮助类
        ButtonTest bt = new ButtonTest();
        MerryTestFramework.testitem.Computer.MessageBox mb = new MerryTestFramework.testitem.Computer.MessageBox();
        GetCRC gCRC = new GetCRC();
        DataConversion conversion = new DataConversion();
        private OldButtonTest buttontest = new OldButtonTest();
        private Command command = new Command();
        private Currency currency = new Currency();
        readonly VolumeTest VolumeTestPlan = new VolumeTest();
        Form2 form = new Form2();
        Form3 ff= new Form3();  
        // readonly ControlTest ControlTestPlan = new ControlTestPlan();
        //private MessageBox messagebox = new MessageBox();

        public static string CH1 = "1";  // 原值为 "1"
        public static string CH20 = "20"; // 原值为 "20"
        public static string CH38 = "38"; // 原值为 "38"
        // 天线
        public static string ANT1 = "0"; // 天线1
        public static string ANT2 = "1"; // 天线2
        private static bool isPowerOn = true;
        #endregion

        public string[] GetDllInfo()
        {
            string dllname = "DLL 名称       ：HDT668";
            string dllfunction = "Dll功能说明 ：HDT668机型测试模块";
            string dllHistoryVersion = "历史Dll版本：无";
            string dllVersion = "当前Dll版本：23.06.09.1";
            string dllChangeInfo = "Dll改动信息：23.06.09.1";
            string dllChangeInfo2 = "无";
            string[] info = { dllname,
                dllfunction,
                dllHistoryVersion,
                dllVersion,
                dllChangeInfo, dllChangeInfo2};
            return info;
        }

        #region 主程序调用方法区
        public bool CheckType(string type)
        {
            return type == data._type;
        }

        public string GetMessage(string key)
        {
            return data.messagedic[key];
        }
    
        public bool StartRun()
        {
           
            return true;
        }
        
        string worknumber = "";
        Dictionary<string, string> bindic = new Dictionary<string, string>();
        private void sep()
        {
            if (check != "")
            {
                ff.aa = check;
                ff.ShowDialog();
            }
        }
        string check = "";
        public bool Start(List<string> formsData, IntPtr _handle)
        {
            Form2 form = new Form2();
            form.ShowDialog();
            check = form.i;
            Thread t = new Thread(sep);
            t.Start();
            return true;
        }
        public string Run(string message)
        {
            try
            {
                data.OpenHandel(data.RXPID, data.RXVID, data.TXPID, data.TXVID);
                Thread.Sleep(100);
                switch (message)
                {
                    case "SendWCP045":
                        return GetWCP045().ToString();
                    case "BootUp":
                        return BootUp().ToString();
                    case "ShutDown":
                        return ShutDown().ToString();
                    case "GetTXPidVid":
                        return GetTXPidVid();
                    case "GetRXPidVid":
                        return GetRXPidVid();
                    case "UpdateAVMode":
                        return UpdateAVMode().ToString();
                    case "GetTxAvneraFW":
                        return GetTxAvneraFW();
                   // case "GetTxMCUFW":
                   //  return GetTxMCUFW2();
                    case "GetRxAvneraFW":
                        return GetRxAvneraFW();
                    case "TxButtonTest":
                        return TxButtonTest().ToString();
                  //  case "MuteButtenTest":
                      //  return MuteButtenTest().ToString();
                    case "TestMuteButton":
                        return TestButtonMute().ToString();
                    case "TestRxRedLED":
                        return TestRxRedLED().ToString();
                    case "TestRxGreenLED":
                        return TestRxGreenLED().ToString();
                    case "TestMuteLED":
                        return TestMuteLed().ToString();
                    case "TestMuteLEDDgle":
                        return TestMuteLedDgle().ToString();
                    case "GetTxPairId":
                        return GetTxPairId();
                    case "Pair":
                        return Pair();
                    case "GetRxPairId":
                        return GetRxPairId();
                    case "GetRXGaugeICFW":
                        return GetRXGaugeICFW();
                    case "111": return a().ToString();
                    case "CheckGaugeIcFW":
                        return CheckGaugeICFW();//Repair
                    case "GetRXChemID":
                        return GetRXChemID();
                    case "GetRXGoldImage":
                        return GetRXGoldImage();
                    case "GetPower":
                        return GetPower();
                    case "GetVoltage":
                        return GetVoltage();
                    case "GetPower2":
                        return GetPower2();
                    case "GetVoltage2":
                        return CheckVoltage();
                    case "OpenSideTone":
                        return OpenSideTone().ToString();
                    case "GetGaugeIC":
                        return GetGaugeIC();
                    case "ResetGaugeIC":
                        return ResetGaugeIC().ToString();
                    case "Distinguish":
                        return Distinguish().ToString();
                    case "MICVOL": return myVolumeControl.MICVOL(100).ToString(); // 设置MIC声音100
                    case "VolumUP": return VolumUP().ToString();
                    case "VolumDown":return VolumDown().ToString();
                        //耳机HeadSET
                    case "headsetAnt1Ch1": return SetRFChannel(data.RXVID, data.RXPID, ANT1, CH1).ToString();
                    case "headsetAnt1Ch20": return SetRFChannel(data.RXVID, data.RXPID, ANT1, CH20).ToString();
                    case "headsetAnt1Ch38": return SetRFChannel(data.RXVID, data.RXPID, ANT1, CH38).ToString();
                    case "headsetAnt2Ch1": return SetRFChannel(data.RXVID, data.RXPID, ANT2, CH1).ToString();
                    case "headsetAnt2Ch20": return SetRFChannel(data.RXVID, data.RXPID, ANT2, CH20).ToString();
                    case "headsetAnt2Ch38": return SetRFChannel(data.RXVID, data.RXPID, ANT2, CH38).ToString();
                        //Dongle RF
                    case "dongleAnt1Ch1" : return SetRFChannel(data.TXVID, data.TXPID, ANT1, CH1).ToString();
                    case "dongleAnt1Ch20": return SetRFChannel(data.TXVID, data.TXPID, ANT1, CH20).ToString();
                    case "dongleAnt1Ch38": return SetRFChannel(data.TXVID, data.TXPID, ANT1, CH38).ToString();
                    case "dongleAnt2Ch1": return SetRFChannel(data.TXVID, data.TXPID, ANT2, CH1).ToString();
                    case "dongleAnt2Ch20": return SetRFChannel(data.TXVID, data.TXPID, ANT2, CH20).ToString();
                    case "dongleAnt2Ch38": return SetRFChannel(data.TXVID, data.TXPID, ANT2, CH38).ToString();
                    default: mb.JudgeBox("参数错误"); return "False";
                }
            }
            catch (Exception ex)
            {
                var result = ex;
                return "False";
            }
        }

        private string  a()
        {
            throw new NotImplementedException();
        }

        //天线选择
        public static bool SetRFChannel(string vid, string pid, string ch, string channel)
        {
            return SCPI.avnera.Main(new string[] { vid, pid, ch, channel }) == 1;
        }

        private object VolumDown()
        {
            return VolumeTestPlan.volumetest(false, "下调音量/ Vui lòng vặn giảm âm lượng");
        }

        private bool VolumUP()
        {
               return VolumeTestPlan.volumetest(true, "上调音量/Vui lòng vặn tăng âm lượng");          
        }
        #region 功能方法
        /// <summary>
        /// 开机
        /// </summary>
        private bool BootUp()
        {
            var value = "07 88 07 01";
            return command.WriteSend(value, data.lenth1, data.headsethandle1);
        }
        /// <summary>
        /// 关机
        /// </summary>
        private bool ShutDown()
        {
            var value = "07 88 07 00";
            return command.WriteSend(value, data.lenth1, data.headsethandle1);
        }
        /// <summary>
        /// 检查DonglePIDVID
        /// </summary>
        private string GetTXPidVid()
        {   
            return data.donglehandel1 != IntPtr.Zero
                ? $"P{data.TXPID}V{data.TXVID}"
                : "False";
        }

        private string GetWCP045()
        {
            return true.ToString();
        }
        /// <summary>
        /// 检查HeadsetPIDVID
        /// </summary>
        private string GetRXPidVid()
        {
            return data.headsethandle1 != IntPtr.Zero
                ? $"P{data.RXPID}V{data.RXVID}"
                : "False";
        }
        /// <summary>
        /// 进入Dongle AV模式
        /// </summary>
        private bool UpdateAVMode()
        {
            var value = "01 00 0D 00 A0 01 00 68 4A 8E 10 00 00 00 01 00 00 00";
            if (command.WriteSend(value, data.lenth2, data.donglehandel1))
            {
                for (int i = 0; i < 20; i++)
                {
                    data.CloseHandel();
                    data.OpenHandel(data.RXPID, data.RXVID, "1717", "0951");
                    if (data.donglehandel1 != IntPtr.Zero) return true;
                    Thread.Sleep(500);
                }
            }
            return false;
        }
        /// <summary>
        /// 获取Dongle AVFW
        /// </summary>
        private string GetTxAvneraFW()
        {
            var value = "06 FF BB 04";
            var returnvalue = "06 FF BB 04 04";
            var indexs = "4 5 6 7";
            if (command.WriteReturn(value, data.lenth1, returnvalue, indexs, data.donglepath3, data.donglehandel3))
            {
                return currency.GetFW(command.ReturnValue);
            }
            else
            {
                return "False";
            }
        }
        /// <summary>
        /// 获取Dongle MCUFW
        /// </summary>
        /*    private string GetTxMCUFW()
            {
                var value = "01 00 0D 00 03 01 00 23 2D B3";
                var indexs = "10 14 18 22";
                if (!command.WriteSend(value, data.lenth2, data.donglehandel1)) return "False";
                command.GetReportReturn(value, data.lenth2, data.donglehandel1, indexs);
                return currency.GetFW(command.ReturnValue);

            }*/
     /*   private string GetTxMCUFW2()
        {
            var value = "01 00 0D 00 03 01 00 23 2D B3";
            var returnvalue = "10 14 18 22";
            var indexs = "3 4 5 6 7";
            if (! command.WriteReturn(value, data.lenth2, returnvalue, indexs, data.donglepath3, data.donglehandel3))
            {
                return currency.GetFW(command.ReturnValue);
            }
            else
            {
                return "False";
            }

        }
     */
     /*   private string GetTxMCUFW2()
        {
            var value = "01 00 0D 00 03 01 00 23 2D B3";
            var indexs = "10 14 18 22";
            if (!command.WriteSend(value, data.lenth2, data.donglehandel1)) return "False";
            command.GetReportReturn(value, data.lenth2, data.donglehandel1, indexs);
            return currency.GetFW(command.ReturnValue);
        }*/
        /// <summary>
        /// 获取Headset AVFW
        /// </summary>
        private string GetRxAvneraFW()
        {
            var value = "07 88 04";
            var indexs = "3 4 5 6 7";
            var returnvalue = "07 88 04 03";
            return command.WriteReturn(value, data.lenth1, returnvalue, indexs, data.headsetpath1, data.headsethandle1)
                ? currency.GetFW(command.ReturnValue)
                : "False";
        }
        /// <summary>
        /// Dongle按键测试
        /// </summary>
        private bool TxButtonTest()
        {
            var value = "06 FF 02 01";
            return command.WriteSend(value, data.lenth2, data.donglehandel3)
                ? mb.JudgeBox(GetMessage("DongleLED"))
                : false;
        }
        /// <summary>
        /// 静音按键测试
        /// </summary>
       private bool MuteButtenTest()
        {
            var value = "07 88 02 01";
            if (command.WriteSend(value, data.lenth1, data.headsethandle1))
            {
                var redata = "170";
                var index = "3";
                return buttontest.Buttontest(redata, index, GetMessage("MuteButtonTest"), data.headsetpath1);
            }
            return false;
        }
        public string TestButtonMute()
        {

            var value = "07 88 02 01";
            var indexs = "3";
            var returnvalue = "07 88 02";
            //if (Command.WriteReturn(value, 20, returnvalue, indexs, Info.HeadsetPath, Info.HeadsetHandle))
            var flag = false;
            if (command.WriteSend(value, 20, data.headsethandle1))
            {
                return buttontest.Buttontest("aa", "3", "请按静音LED/Vui lòng nhấn nút Mute", data.headsetpath1).ToString();
            }
            else
            {
                return false.ToString();
            }

        }
        /// <summary>
        /// 测试耳机红色LED
        /// </summary>
        private bool TestRxRedLED()
        {
            var value = "07 88 03 02 01";
            if (command.WriteSend(value, data.lenth1, data.headsethandle1))
            {
                value = "07 88 03 02 00";
                return mb.JudgeBox(GetMessage("HeadsetRedLED"))
                    ? command.WriteSend(value, data.lenth1, data.headsethandle1)
                    : false;
            }
            return false;
        }
        /// <summary>
        /// 测试耳机绿色LED
        /// </summary>
        private bool TestRxGreenLED()
        {
            var value = "07 88 03 01 01";
            if (command.WriteSend(value, data.lenth1, data.headsethandle1))
            {
                value = "07 88 03 01 00";
                return mb.JudgeBox(GetMessage("HeadsetGreenLED"))
                    ? command.WriteSend(value, 20, data.headsethandle1)
                    : false;
            }
            return false;
        }
        public string TestMuteLed()
        {
            var valueOn = "07 88 03 03 01";
            var valueOff = "07 88 03 03 00";

            command.WriteSend(valueOn, 20, data.headsethandle1);
            //MessageBox.Show("1", "2");

            //Command.WriteSend("07 88 03 02 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00", 20, Info.HeadsetHandle);
            if (mb.JudgeBox(GetMessage("MuteLED")))
            {
                command.WriteSend(valueOff, 20, data.headsethandle1);
                return true.ToString();
            }

            return false.ToString();
        }
        public string TestMuteLedDgle()
        {
            var valueOn = "07 88 03 03 01";
            var valueOff = "07 88 03 03 00";
            command.WriteSend(valueOn, 62, data.donglehandel3);
            //MessageBox.Show("1", "2");
            //Command.WriteSend("07 88 03 02 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00", 20, Info.HeadsetHandle);
            if (mb.JudgeBox(GetMessage("MuteLED")))
            {
                command.WriteSend(valueOff, 62, data.donglehandel3);
                return true.ToString();
            }
            return false.ToString();
        }
        /// <summary>
        /// 获取Dongle配对ID
        /// </summary>
        private string GetTxPairId()
        {
            var value = "FF 0A 00 FD 04 00 00 05 81 D4 C0 04";
            var value2 = "FF 05 00 39";
            if (command.SetFeatureSend(value, data.lenth3, data.donglehandel4))
            {
                Thread.Sleep(50);
                var Indexes = "11 12 13 14";
                if (command.GetFeatureReturn(value, data.lenth3, data.donglehandel4, Indexes))
                {
                    return command.ReturnValue == "78 56 34 12" || command.ReturnValue == "00 00 00 00"
                          ? "False" + command.ReturnValue
                          : command.SetFeatureSend(value2, data.lenth3, data.donglehandel4) ?
                          command.ReturnValue
                          : "False" + command.ReturnValue;
                }
            }
            return "False";
        }
        /// <summary>
        /// 配对
        /// </summary>
        private string Pair()
        {
            for (int i = 0; i < 20; i++)
            {
                data.CloseHandel();
                data.OpenHandel(data.RXPID, data.RXVID, "018b", "03f0");
                if (data.donglehandel1 != IntPtr.Zero) break;
                if (i == 19) return "False";
                Thread.Sleep(500);
            }
            var value = "FF 0A 00 FD 04 00 00 05 81 D4 C0 04";
            if (command.SetFeatureSend(value, data.lenth3, data.donglehandel4))
            {
                Thread.Sleep(50);
                string Indexes = "11 12 13 14";
                if (command.GetFeatureReturn(value, data.lenth3, data.donglehandel4, Indexes))
                {
                    if (value == "78 56 34 12" || value == "00 00 00 00")
                    {
                        return "False" + command.ReturnValue;
                    }
                    else
                    {
                        Thread.Sleep(150);
                        value = "FF 05 00 39";
                        if (command.SetFeatureSend(value, data.lenth3, data.donglehandel4))
                        {
                            Thread.Sleep(150);
                            if (command.SetFeatureSend(value, data.lenth3, data.donglehandel4))
                            {
                                Thread.Sleep(150);
                                value = "07 88 01" + " " + command.ReturnValue;
                                if (command.WriteSend(value, data.lenth1, data.headsethandle1)) return command.ReturnValue;
                            }
                        }

                    }
                }
            }
            return "False" + command.ReturnValue;
        }
        /// <summary>
        /// 获取耳机配对ID
        /// </summary>
        private string GetRxPairId()
        {
            string value = "FF 0C 00 FD 04 00 00 05 81 0D B1 04";
            string Indexes = "11 12 13 14";
            var PairID = "";
            if (command.SetFeatureSend(value, data.lenth3, data.headsethandle2))
            {
                Thread.Sleep(20);
                if (command.GetFeatureReturn(value, data.lenth3, data.headsethandle2, Indexes))
                {
                    PairID = command.ReturnValue;
                    data.OpenHandel("058b", "03f0", "018b", "03f0");
                    value = "FF 0A 00 FD 04 00 00 05 81 D4 C0 04";
                    if (command.SetFeatureSend(value, data.lenth3, data.donglehandel4))
                    {
                        Thread.Sleep(50);
                        Indexes = "11 12 13 14";
                        if (command.GetFeatureReturn(value, data.lenth3, data.donglehandel4, Indexes))
                        {
                            if (value == "78 56 34 12" || value == "00 00 00 00")
                            {
                                return "False" + PairID;
                            }
                            else
                            {
                                if (PairID == command.ReturnValue)
                                {
                                    return PairID;
                                }
                            }

                        }
                    }
                }
            }
            return "False" + PairID;
        }
        private string GetGaugeIC()
        {
            Form2 form2 = new Form2();
            var value = "07 88 04";
            var indexs = "8";
            var returnvalue = "07 88 04 03";
            var result = command.WriteReturn(value, data.lenth1, returnvalue, indexs, data.headsetpath1, data.headsethandle1);
            string gauicInfo = "";
            if (result)
            {
                if (command.ReturnValue == "01")
                {
                    gauicInfo = "01-ON SEMI EPW";
                }
                
                if (command.ReturnValue == "02")          
                {
                    gauicInfo = "02–ON SEMI AEC";
                }

                if (command.ReturnValue == "03")
                {
                    gauicInfo= "03–TI Gauge";
                }

                if(command.ReturnValue == check)
                {
                    return gauicInfo;
                }else
                {
                    return "False " + gauicInfo;
                }
              //  return command.ReturnValue;
            }
            else
            {
                return "False";
            }
        }
        /// <summary>
        /// 获取耳机GaugeICFW
        /// </summary>
        private string GetRXGaugeICFW()
        {
            var value = "07 88 08 02";
            var indexs = "3 4";
            var returnvalue = "07 88 08";
            command.WriteReturn(value, data.lenth1, returnvalue, indexs, data.headsetpath1, data.headsethandle1);
            return command.ReturnValue == "10 13"
                ? "V16.19"
                : "False";
        }
        public string CheckGaugeICFW()
        {
            var value = "07 88 08 02";
            var indexs = "3 4";
            var returnvalue = "07 88 08";
            command.WriteReturn(value, 20, returnvalue, indexs, data.headsetpath1, data.headsethandle1);
            var fw = "V";
            foreach (var item in command.ReturnValue.Split(' '))
            {
                fw += $"{Convert.ToInt32(item, 16)}.";
            }
            command.GetReportSend(value, 20, data.headsethandle1);
            return (fw.Remove(fw.Length - 1, 1)).ToString();
        }
        /// <summary>
        /// 获取耳机ChemID
        /// </summary>
        private string GetRXChemID()
        {
            var value = "07 88 08 02 04 AA 3E 06";
            var indexs = "3 4";
            var returnvalue = "07 88 08 " + data.chemID;
            
            command.WriteReturn(value, data.lenth1, returnvalue, indexs, data.headsetpath1, data.headsethandle1);
            if (command.ReturnValue == "False") return command.ReturnValue;
            string[] arr = command.ReturnValue.Split(' ');
            return arr[1] + arr[0];
        }
        /// <summary>
        /// 获取GoldImage版本
        /// </summary>
        private string GetRXGoldImage()
        {
            Thread.Sleep(50);
            var value = "07 88 08 01 04 AA 3E 70";
            var index = "3";
            var returnvalue = "07 88 08";
            command.WriteReturn(value, data.lenth1, returnvalue, index, data.headsetpath1, data.headsethandle1);
            return currency.GetFW(command.ReturnValue);
        }
        /// <summary>
        /// 获取电量
        /// </summary>
        private string GetPower()
        {

            var value = "06 FF BB 02";
            var index = "7";
            var returnvalue = "06 FF BB 02 00";
            command.WriteReturn(value, data.lenth2, returnvalue, index, data.donglepath3, data.donglehandel3);
            if (command.ReturnValue == "False") return command.ReturnValue;
            return Convert.ToInt32(command.ReturnValue, 16).ToString() + "%";
        }
        /// <summary>
        /// 获取电压
        /// </summary>
        private string GetVoltage()
        {
            Thread.Sleep(50);
            var value = "06 FF BB 02";
            var index = "5 6";
            var returnvalue = "06 FF BB 02 00";
            command.WriteReturn(value, data.lenth2, returnvalue, index, data.donglepath3, data.donglehandel3);
            if (command.ReturnValue == "False") return command.ReturnValue;
            var strb = new StringBuilder(Convert.ToInt32(command.ReturnValue.Replace(" ", ""), 16).ToString());
            strb.Insert(1, '.');
            return strb.ToString();
        }
        private string Power = "";
        public string CheckVoltage()
        {
            var valueGetVol = "06 00 02 00 9A 00 00 68 4A 8E 0A 00 00 00 BB 02";
            var valueOn = "07 88 04";
            var indexs = "5 6 7";
            //var indexs2 = "7";
            var returnvalueHsVol = "0B 00 BB 02 00";
            var set = false;
           
            command.WriteReturn(valueGetVol, 62, returnvalueHsVol, indexs, data.donglepath3, data.donglehandel3);

            var fw = "";
            string[] item = command.ReturnValue.Split(' ');
            fw = item[0] + item[1];

            double voltage = Convert.ToInt32(fw, 16) * 0.001;
            //Command.WriteReturn(valueGetVol, 62, returnvalueHsVol, indexs2, Info.DonglePath, Info.DongleHandle);
            var fw2 = Convert.ToInt32(item[2], 16);
            //Command.GetReportSend(valueGetVol, 62, Info.DongleHandle);
            Power = fw2.ToString();
            return voltage.ToString();
            //if (MessageBox.Show("Disconected Headphone First!", "Notification", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{

            //}
            //else return false.ToString();

        }
        public string GetPower2()
        {
            return Power;

        }
        /// <summary>
        /// 打开SideTone
        /// </summary>
        private bool OpenSideTone()
        {
            Thread.Sleep(40);
            var value = "07 88 05 01";
            if (command.WriteSend(value, data.lenth2, data.headsethandle1))// "对麦克风说话,耳机中是否有回音"
            {
                value = "07 88 05 00";
                return mb.JudgeBox(GetMessage("SideTone"))
                    ? command.WriteSend(value, data.lenth1, data.headsethandle1)
                    : false;
            }
            else
            {
                value = "07 88 05 00";
                command.WriteSend(value, data.lenth1, data.headsethandle1);
                return false;
            }
        }
        /// <summary>
        /// 重置GaugeIC
        /// </summary>
        private bool ResetGaugeIC()
        {
            var value = "07 88 08 00 04 AA 3E 12";
            return command.WriteSend(value, data.lenth1, data.headsethandle1);
        }
        /// <summary>
        /// 环绕音测试
        /// </summary>


        /// <summary>
        /// 识别装置
        /// </summary>
        /// <returns></returns>
        private bool Distinguish()
        {
            for (int i = 0; i < 20; i++)
            {
                data.CloseHandel();
                data.OpenHandel(data.RXPID, data.RXVID, data.TXPID, data.TXVID);
                if (data.donglehandel1 != IntPtr.Zero) return true;
                Thread.Sleep(500);
            }
            return false;
        }
        #endregion
        #endregion

    }
}

