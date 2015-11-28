using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace CommonTool
{
    /// <summary>
    /// 通用帮助类
    /// </summary>
    public class CommonHelper
    {
        /// <summary>
        /// 生产随机验证码  111111-999999之间
        /// </summary>
        /// <returns></returns>
        public static string GetNextCheckCode()
        {
            System.Random ran = new Random();
            return ran.Next(111111, 999999).ToString();
        }

        private const string MobileMatchStr = @"^(?<国家代码>(\+86)|(\(\+86\)))?(?<手机号>((13[0-9]{1})|(15[0-9]{1})|(18[0,5-9]{1}))+\d{8})$";
        private const string TelMatchStr = @"^(?<国家代码>(\+86)|(\(\+86\)))?\D?(?<电话号码>(?<三位区号>((010|021|022|023|024|025|026|027|028|029|852)|(\(010\)|\(021\)|\(022\)|\(023\)|\(024\)|\(025\)|\(026\)|\(027\)|\(028\)|\(029\)|\(852\)))\D?\d{8}|(?<四位区号>(0[3-9][1-9]{2})|(\(0[3-9][1-9]{2}\)))\D?\d{7,8}))(?<分机号>\D?\d{1,4})?$";

        private static readonly Regex MobileMatchRegex = new Regex(MobileMatchStr, RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
        private static readonly Regex TelMatchRegex = new Regex(TelMatchStr, RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

        public static bool IsMobile(string text)
        {
            return MobileMatchRegex.IsMatch(text);
        }

        public static bool IsTel(string text)
        {
            return TelMatchRegex.IsMatch(text);
        }
    }
}
