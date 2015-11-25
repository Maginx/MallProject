﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;

namespace WindowsFormsApplication9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Socket sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private void user_Click(object sender, EventArgs e)
        {
            //Send("EEEE01FF00027075750000000000EDED");
            Send("EEEE01FF00000000010000000000EDED");
        }
        private void Send(string msg)
        {
            byte[] bs = new byte[16];
            string tem = "";
            for (int i = 0; i < 16; i++)
            {
                tem = msg.Substring(i * 2, 2);
                bs[i] = byte.Parse(tem, System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            if (!sendSocket.Connected)
                sendSocket.Connect("localhost", 10000);
            sendSocket.Send(bs);
        }

        #region 灌流器
        private void firstWash_Click(object sender, EventArgs e)
        {
            Send("EEEE010100000000000000000000EDED");
        }

        private void firstWash2_Click(object sender, EventArgs e)
        {
            Send("EEEE010200000000000000000000EDED");//01 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Send("EEEE020200000000000000000000EDED");
        }

        private void enzymeWash_Click(object sender, EventArgs e)
        {
            Send("EEEE020100000000000000000000EDED");
        }

        private void cleanOut_Click(object sender, EventArgs e)
        {
            Send("EEEE030100000000000000000000EDED");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Send("EEEE030200000000000000000000EDED");
        }

        private void dipin_Click(object sender, EventArgs e)
        {
            Send("EEEE040100000000000000000000EDED");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Send("EEEE040200000000000000000000EDED");
        }

        private void lastWash_Click(object sender, EventArgs e)
        {
            Send("EEEE050100000000000000000000EDED");
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Send("EEEE050200000000000000000000EDED");
        }
        #endregion

        #region  内镜打卡

        private void endoscope_Click(object sender, EventArgs e)
        {
            Send("EEEE01FF11111111110000000000EDED");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Send("EEEE02FF11111111110000000000EDED");//02
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Send("EEEE03FF11111111110000000000EDED");//03
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Send("EEEE04FF11111111110000000000EDED");//04
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Send("EEEE05FF11111111110000000000EDED");//05 
        }

        #endregion

        #region 内镜打卡1
        private void button10_Click(object sender, EventArgs e)
        {
            Send("EEEE01FF00027075740000000000EDED");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Send("EEEE02FF00027075740000000000EDED");

        }

        private void button12_Click(object sender, EventArgs e)
        {
            Send("EEEE03FF00027075740000000000EDED");

        }

        private void button13_Click(object sender, EventArgs e)
        {
            Send("EEEE04FF00027075740000000000EDED");

        }

        private void button14_Click(object sender, EventArgs e)
        {
            Send("EEEE05FF00027075740000000000EDED");
        }
        #endregion
        #region 二次
        private void button17_Click(object sender, EventArgs e)
        {
            Send("EEEE20FF00027075730000000000EDED");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Send("EEEE20FF00027075740000000000EDED");
        }
        private void button16_Click(object sender, EventArgs e)
        {
            Send("EEEE20FF00027075740000000000EDED");
        }
        private void button18_Click(object sender, EventArgs e)
        {
            Send("EEEE20FF00027075730000000000EDED");
        }

        #endregion

        private void button19_Click(object sender, EventArgs e)
        {
            Send("EEEE200100000000000000000000EDED");
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Send("EEEE200200000000000000000000EDED");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Send("EEEE200100000000000000000000EDED");
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Send("EEEE200200000000000000000000EDED");
        }

        private void button23_Click(object sender, EventArgs e)
        {
            Send("EEEE20FF00000000010000000000EDED");
        }

        private void button32_Click(object sender, EventArgs e)
        {
            //EEEE01FF00000000010000000000EDED
            Send("EEEE06FF00000000010000000000EDED");
        }

        private void button31_Click(object sender, EventArgs e)
        {
            Send("EEEE06FF00027075740000000000EDED");
        }

        private void button29_Click(object sender, EventArgs e)
        {
            Send("EEEE06FF11111111110000000000EDED");
        }

        private void button30_Click(object sender, EventArgs e)
        {
            Send("EEEE07FF00027075740000000000EDED");
        }

        private void button24_Click(object sender, EventArgs e)
        {
            Send("EEEE07FF11111111110000000000EDED");
        }

        private void button28_Click(object sender, EventArgs e)
        {
            Send("EEEE060100000000000000000000EDED");
        }

        private void button25_Click(object sender, EventArgs e)
        {
            Send("EEEE060200000000000000000000EDED");
        }

        private void button27_Click(object sender, EventArgs e)
        {
            Send("EEEE070100000000000000000000EDED");
        }

        private void button26_Click(object sender, EventArgs e)
        {
            Send("EEEE070200000000000000000000EDED");
        }

        private void button33_Click(object sender, EventArgs e)
        {
            byte[] bs = new byte[16];
            string tem = "";
            for (int i = 0; i < 5; i++)
            {
                tem = "1111111111".Substring(i * 2, 2);
                bs[i] = byte.Parse(tem, System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            if (!sendSocket.Connected)
                sendSocket.Connect("localhost", 10000);
            sendSocket.Send(bs);
        }

        private void button34_Click(object sender, EventArgs e)
        {
            byte[] bs = new byte[16];
            string tem = "";
            for (int i = 0; i < 5; i++)
            {
                tem = "0002707574".Substring(i * 2, 2);
                bs[i] = byte.Parse(tem, System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            if (!sendSocket.Connected)
                sendSocket.Connect("127.0.0.1", 10000);
            sendSocket.Send(bs);
        }

        private void button35_Click(object sender, EventArgs e)
        {
            Send("EEEE02FF00027075750000000000EDED");
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button35_Click_1(object sender, EventArgs e)
        {
            Send("EEEE20FF11111111110000000000EDED");
        }

        private void button36_Click(object sender, EventArgs e)
        {
            Send("EEEE200100000000000000000000EDED");
        }

        private void button37_Click(object sender, EventArgs e)
        {
            Send("EEEE200200000000000000000000EDED");
        }

        private void button38_Click(object sender, EventArgs e)
        {
            Send("EEEE20FF11111111110000000000EDED");
        }

        private void button41_Click(object sender, EventArgs e)
        {
            Send("EEEE20FF00000000010000000000EDED");
        }

        private void button39_Click(object sender, EventArgs e)
        {
            Send("EEEE200100000000000000000000EDED");
        }

        private void button40_Click(object sender, EventArgs e)
        {
            Send("EEEE200200000000000000000000EDED");
        }



    }
}
