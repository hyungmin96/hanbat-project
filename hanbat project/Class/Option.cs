using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using hanbat_project;

namespace WindowsFormsApp2.Class
{

    public class DelayOp
    {

        public static readonly Random getrandom = new Random();

        public static int GetRandomNumber(int min, int max)
        {

            lock (getrandom) // synchronize
            {
                return getrandom.Next(min, max);
            }
        }

        public static DateTime Delay(int MS)
        {

            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents();
                ThisMoment = DateTime.Now;
            }

            return DateTime.Now;
        }

    }

}
