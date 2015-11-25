using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MallHost
{
    internal sealed class EnterProxyCommand:ICommand
    {
        public void ExcuteCommand()
        {
            ConsoleHelper.PrintText("输入新的服务器地址：\r\n");

            string tempStr = Console.ReadLine();

            if (ReadXML.WriteXMLService("mallProxy", tempStr))
            {
                ConsoleHelper.PrintText("修改成功，需要重启系统");
            }

        }
    }
}
