using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPSample.Model
{
    public class AW_LEVEL_CAL
    {
        public byte led_level_count = 0;

        bool led1On = false;
        bool led2On = false;
        bool led3On = false;
        
        byte led_enable_pwn = 0;

        byte[] led1_levels = null;
        byte[] led2_levels = null;
        byte[] led3_levels = null;

        byte[] led1_pwms = null;
        byte[] led2_pwms = null;
        byte[] led3_pwms = null;


        double led1_max_current = 0;
        double led2_max_current = 0;
        double led3_max_current = 0;

        Dictionary<int, List<double>> dic_target_curs = new Dictionary<int, List<double>>();
        public static AW_LEVEL_CAL Load(byte[] data)
        {

            AW_LEVEL_CAL awlelvel = new AW_LEVEL_CAL();

            byte[] tmp = new byte[data.Length];
            Array.Copy(data.ToArray(), 0, tmp, 0, data.Length);


            CheckDataFormat(tmp);


            awlelvel.led1On = Convert.ToBoolean(tmp[0] & 0x01);
            awlelvel.led2On = Convert.ToBoolean(tmp[0] & 0x02);
            awlelvel.led3On = Convert.ToBoolean(tmp[0] & 0x04);

            awlelvel.led_level_count = tmp[1];         //led level count
            
            awlelvel.led_enable_pwn = tmp[2];

  

            awlelvel.led1_levels = new byte[awlelvel.led_level_count];
            awlelvel.led2_levels = new byte[awlelvel.led_level_count];
            awlelvel.led3_levels = new byte[awlelvel.led_level_count];

            int sel = 3;
            Array.Copy(tmp, sel, awlelvel.led1_levels, 0, awlelvel.led_level_count);
            sel += awlelvel.led_level_count;
            Array.Copy(tmp, sel, awlelvel.led2_levels, 0, awlelvel.led_level_count);
            sel += awlelvel.led_level_count;
            Array.Copy(tmp, sel, awlelvel.led3_levels, 0, awlelvel.led_level_count);


            awlelvel.led1_pwms = new byte[awlelvel.led_level_count];
            awlelvel.led2_pwms = new byte[awlelvel.led_level_count];
            awlelvel.led3_pwms = new byte[awlelvel.led_level_count];

            sel += awlelvel.led_level_count;
            Array.Copy(tmp, sel, awlelvel.led1_pwms, 0, awlelvel.led_level_count);
            sel += awlelvel.led_level_count;
            Array.Copy(tmp, sel, awlelvel.led2_pwms, 0, awlelvel.led_level_count);
            sel += awlelvel.led_level_count;
            Array.Copy(tmp, sel, awlelvel.led3_pwms, 0, awlelvel.led_level_count);
            sel += awlelvel.led_level_count;

            // Console.WriteLine("size:" + sel);

            return awlelvel;


        }

        private static void CheckDataFormat(byte[] tmp)
        {

            if (tmp[0] == 0 || tmp[0] > 7)
            {
                throw new Exception("Format error. LED number." + tmp[0].ToString("X"));
            }

            //check total size       
            int level_count = tmp[1];
            if(level_count == 0)
            {
                throw new Exception("Format error. level count can't be zero.");
            }

            int totalsize = 3 + 6*level_count;
            if (tmp.Length != totalsize)
            {
                throw new Exception("mp data size(" + tmp.Length + ") size is not unexpected size(" + totalsize + ").");
            }

            if (tmp[2] != 1) //enable pwm
            {
                throw new Exception("Format error. LED enable pwm." + tmp[2]);
            }

            int sel = 3;
            for ( sel = 3; sel < 3 * level_count; sel++)
            {
                if(tmp[sel] > 15)
                {
                    throw new Exception("Format error. Level out of range.(" + tmp[sel]+")");
                }
            }

            for (; sel < 6 * level_count; sel++)
            {
                if (tmp[sel] > 128)
                {
                    throw new Exception("Format error. PWM out of range.(" + tmp[sel]+")");
                }
            }

        }

        public double CALMaxCurrent(int led)
        {

            double res = 0;
            int max = this.led_level_count - 1;

            double maxcurrent = dic_target_curs[led][max];
            if (led == 1)
            {
                res = GetLED15LevelPWM(this.led1_levels[max], this.led1_pwms[max], maxcurrent);
            }
            else if (led == 2)
            {
                res = GetLED15LevelPWM(this.led2_levels[max], this.led2_pwms[max], maxcurrent);
            }
            else if (led == 3)
            {
                res = GetLED15LevelPWM(this.led3_levels[max], this.led3_pwms[max], maxcurrent);
            }
            else
            {
                throw new Exception("Led is out of range." + led);
            }

            return res;
        }
        private double GetLED15LevelPWM(double maxlevel, int maxpwm, double target_cur)
        {
            //find max current 2.8 level and pwm

            //double target_cur = 1.5;
            double A = maxlevel;
            int P = maxpwm;
            double Max15 = (target_cur * 15 * 128) / (A * P);

            return Max15;


            //double MAX_CURRENT = 2.8;
            //KeyValuePair<byte, byte> res = LEDLevelCAL.get_target_level(MAX_CURRENT, Max15, 15);

            //Console.WriteLine("res:" + res.Key + "," + res.Value);

            //return res;
        }
        public byte[] build()
        {
            List<byte> bytes = new List<byte>();

            byte led_lvl_count = (byte)this.dic_target_curs.ElementAt(0).Value.Count;
            bool led1 = this.led1On;
            bool led2 = this.led2On;
            bool led3 = this.led3On;


            List<KeyValuePair<byte, byte>> l1s = null;
            List<KeyValuePair<byte, byte>> l2s = null;
            List<KeyValuePair<byte, byte>> l3s = null;

            if (led1)
            {
                l1s = gen_targets(1, this.led1_max_current);

            }
            if (led2)
            {
                l2s = gen_targets(2, this.led2_max_current);
            }
            if (led3)
            {
                l3s = gen_targets(3, this.led3_max_current);
            }

            byte[] res = create_led_cal(led_lvl_count, led1, led2, led3, l1s, l2s, l3s);

            //Console.WriteLine(BitConverter.ToString(res));

            return res;

        }

        private List<KeyValuePair<byte, byte>> gen_targets(int led, double current)
        {
            List<KeyValuePair<byte, byte>> res = new List<KeyValuePair<byte, byte>>();

            var list_curs = dic_target_curs[led];
            foreach (var target in list_curs)
            {
                res.Add(get_target_level(target, current, 15));
            }
            return res;
        }

        internal void set_targets_current(Dictionary<int, List<double>> dicttarts)
        {
            dic_target_curs.Clear();
            foreach (var item in dicttarts)
            {
                dic_target_curs.Add(item.Key, item.Value);
            }

        }

        internal void set_led_max_target(int led, double max_current)
        {
            if (led == 1) this.led1_max_current = max_current;
            else if (led == 2) this.led2_max_current = max_current;
            else if (led == 3) this.led3_max_current = max_current;
            else throw new Exception("UnExpect LED:" + led);
        }

        internal void cal_maximum_current()
        {

            if (led1On)
            {
                double max1 = CALMaxCurrent(1);
                set_led_max_target(1, max1);
            }
            if (led2On)
            {
                double max2 = CALMaxCurrent(2);
                set_led_max_target(2, max2);
            }
            if (led3On)
            {
                double max3 = CALMaxCurrent(3);
                set_led_max_target(3, max3);
            }


        }
        public static byte[] create_led_cal(byte led_level_count, bool led1, bool led2, bool led3,
                                            List<KeyValuePair<byte, byte>> led1_lvl, List<KeyValuePair<byte, byte>> led2_lvl = null,
                                                List<KeyValuePair<byte, byte>> led3_lvl = null)
        {

            List<byte> rets = new List<byte>();

            byte led_active_mask = 0;
            //byte led_array_max = 0;              //multiple of 4(4,8,12,16)
            //led_level_count = 0;            //led level count, must less than led_array_max
            byte led_enable_pwn = 1;


            if (led1)
            {
                led_active_mask |= 0x01;
            }
            if (led2)
            {
                led_active_mask |= 0x02;
            }
            if (led3)
            {
                led_active_mask |= 0x04;
            }
            rets.Add(led_active_mask);

            rets.Add(led_level_count);
            rets.Add(led_enable_pwn);

            byte[] led1_levels = new byte[led_level_count];
            byte[] led2_levels = new byte[led_level_count];
            byte[] led3_levels = new byte[led_level_count];


            if (led1_lvl != null)
            {
                int i = 0;
                foreach (var item1 in led1_lvl)
                {
                    led1_levels[i] = item1.Key;
                    i++;
                }
            }

            if (led2_lvl != null)
            {
                int i = 0;
                foreach (var item1 in led2_lvl)
                {
                    led2_levels[i] = item1.Key;
                    i++;
                }
            }

            if (led3_lvl != null)
            {
                int i = 0;
                foreach (var item1 in led3_lvl)
                {
                    led3_levels[i] = item1.Key;
                    i++;
                }
            }


            rets.AddRange(led1_levels);
            rets.AddRange(led2_levels);
            rets.AddRange(led3_levels);


            byte[] led1_pwm = new byte[led_level_count];
            byte[] led2_pwm = new byte[led_level_count];
            byte[] led3_pwm = new byte[led_level_count];

            if (led1_lvl != null)
            {
                int i = 0;
                foreach (var item1 in led1_lvl)
                {
                    led1_pwm[i] = item1.Value;
                    i++;
                }
            }

            if (led2_lvl != null)
            {
                int i = 0;
                foreach (var item1 in led2_lvl)
                {
                    led2_pwm[i] = item1.Value;
                    i++;
                }
            }

            if (led3_lvl != null)
            {
                int i = 0;
                foreach (var item1 in led3_lvl)
                {
                    led3_pwm[i] = item1.Value;
                    i++;
                }
            }

            rets.AddRange(led1_pwm);
            rets.AddRange(led2_pwm);
            rets.AddRange(led3_pwm);


            return rets.ToArray();
        }
        public static KeyValuePair<byte, byte> get_target_level(double target, double mes_current, int mes_lvl)
        {

            double lelunit = mes_current / mes_lvl;


            //    measure_led15     target
            //
            //         mes_lvl      target_level
            //
            int target_level = (int)Math.Ceiling(target / lelunit);

            if (target_level > 15) throw new Exception("Level 15 is out of range:" + target_level +".(Prediction max current:"+ mes_current + "). Target current:" + target);


            double y = target_level * lelunit;

            //target_pwm     target
            //
            //  128          y
            //

            int target_pwm = (int)(128 * target / y);
            if (target_pwm == 0)
            {
                target_pwm = 1;
            }

            return new KeyValuePair<byte, byte>((byte)target_level, (byte)target_pwm);

        }

        internal void set_led_enable(int led, bool on)
        {
            if (led == 1) this.led1On = on;
            else if (led == 2) this.led2On = on;
            else if (led == 3) this.led3On = on;
            else throw new Exception("UnExpect LED:" + led);
        }

        internal void set_led_maximum_current(int led, double current)
        {
            set_led_max_target(led, current);
        }

        internal static AW_LEVEL_CAL Load_max_current(byte[] data)
        {

            if (data == null) throw new Exception("data is empty");

            AW_LEVEL_CAL awlelvel = new AW_LEVEL_CAL();

            byte[] tmp = new byte[data.Length];
            Array.Copy(data.ToArray(), 0, tmp, 0, data.Length);


            CheckMaxCurrentFormat(tmp);


            int max_cur_size = 4;
            int sel = 1;
            byte[] tmpmax1 = new byte[max_cur_size];
            byte[] tmpmax2 = new byte[max_cur_size];
            byte[] tmpmax3 = new byte[max_cur_size];
            Array.Copy(tmp, sel, tmpmax1, 0, max_cur_size);
            sel += 4;
            Array.Copy(tmp, sel, tmpmax2, 0, max_cur_size);
            sel += 4;
            Array.Copy(tmp, sel, tmpmax3, 0, max_cur_size);
            sel += 4;

            awlelvel.led1On = Convert.ToBoolean(tmp[0] & 0x01);
            awlelvel.led2On = Convert.ToBoolean(tmp[0] & 0x02);
            awlelvel.led3On = Convert.ToBoolean(tmp[0] & 0x04);

            UInt32 u1 =  BitConverter.ToUInt32(tmpmax1, 0);
            UInt32 u2 = BitConverter.ToUInt32(tmpmax2, 0);
            UInt32 u3 = BitConverter.ToUInt32(tmpmax3, 0);

            awlelvel.led1_max_current = Decimal.ToDouble((Decimal)u1 / 1000);
            awlelvel.led2_max_current = Decimal.ToDouble((Decimal)u2 / 1000);
            awlelvel.led3_max_current = Decimal.ToDouble((Decimal)u3 / 1000);


            return awlelvel;

        }

        private static void CheckMaxCurrentFormat(byte[] data)
        {
            if(data.Length != 13)
            {
                throw new Exception("mp data size(" + data.Length + ") size is not unexpected size(" + 13 + ").");
            }

            if (data[0] == 0 || data[0] > 7)
            {
                throw new Exception("Format error. LED number." + data[0].ToString("X"));
            }
        }

        internal double get_led_max_current(int led)
        {
            double max_current = 0;
            if (led == 1) max_current = this.led1_max_current;
            else if (led == 2) max_current = this.led2_max_current;
            else if (led == 3) max_current = this.led3_max_current;
            else throw new Exception("UnExpect LED:" + led);
            return max_current;
        }
    }
}
