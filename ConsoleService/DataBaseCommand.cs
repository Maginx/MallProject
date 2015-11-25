using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MallHost
{
    internal sealed class DataBaseCommand : ICommand
    {
        public void ExcuteCommand()
        {
            ConsoleHelper.PrintText("输入数据库连接字符串：\r\n");
            string temp = string.Format("Data Source={0};Initial Catalog=endoscopeARM;Persist Security Info=True;User ID=sa;Password=s@123456", Console.ReadLine());

            if (ReadXML.WriteXMLService("SQL", temp))
            {
                ConsoleHelper.PrintText("修改成功，需要重启系统");
            }
        }
    }
}
