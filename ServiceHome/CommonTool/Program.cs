using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;
namespace CommonTool
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Console.WriteLine(SMSHelper.SendRegisterCheckCode("18612112092", "23123"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.Read();
        }
    }
}
