using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerryDllFramework
{
    internal class Currency
    {
        /// <summary>
        /// 获取FW
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal string GetFW(string value)
        {
            var valuearr = value.Split(' ');
            var message = "V";
            foreach (var item in valuearr)
            {
                message += Convert.ToInt32(item, 16).ToString() + ".";
            }
            return message.Substring(0, message.Length - 1);
        }
    }
}
