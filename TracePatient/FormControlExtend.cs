﻿using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TracePatient
{
    /// <summary>
    /// ListBox control extend 
    /// </summary>
    public static class FormControlExtend
    {
        /// <summary>
        /// A grammer candy to add new item in ListBox 
        /// </summary>
        /// <param name="listBox">ListBox instance</param>
        /// <param name="message">item content</param>
        public static void AppendItem(this KryptonListBox listBox, string message)
        {
            var dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss - ");
            listBox.Items.Add(String.Concat(dateTime, message));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="comboBox"></param>
        /// <param name="itemList"></param>
        public static void BindToComboBox<T>(this KryptonComboBox comboBox, IEnumerable<T> itemList)
        {
            foreach (T obj in itemList)
            {
                comboBox.Items.Add(obj);
            }
        }
    }
}
