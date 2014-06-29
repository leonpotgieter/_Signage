﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace DisplayClient
{
    class TaskbarHelper
    {
        [DllImport("user32.dll")]
        private static extern int FindWindow(string className, string windowText);
        [DllImport("user32.dll")]
        private static extern int ShowWindow(int hwnd, int command);
   
        private const int SW_HIDE = 0;
        private const int SW_SHOW = 1;

        private int _taskbarHandle;

        public TaskbarHelper()
        {
            _taskbarHandle = FindWindow("Shell_TrayWnd","");
        }
   
        public void Show()
        {
            ShowWindow(_taskbarHandle, SW_SHOW);
        }
   
        public void Hide()
        {
            ShowWindow(_taskbarHandle, SW_HIDE);
        }
        }
}
