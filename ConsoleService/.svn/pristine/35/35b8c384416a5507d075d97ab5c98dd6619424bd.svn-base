using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MallHost
{
    internal sealed class EnterMetaData : ICommand
    {
        public void ExcuteCommand()
        {
            ConsoleHelper.PrintText("输入后台数据服务地址：\r\n");

            string tempStr = Console.ReadLine();

            if (tempStr.Length > 0)
            {
                if (ReadXML.WriteXMLService("mallPManager", tempStr))
                {
                    ConsoleHelper.PrintText("修改成功，需要重启系统");
                }
            }
            else
            {
                ConsoleHelper.PrintText("输入包含非法字符", ConsoleColor.Red);
            }
        }
    }
}
