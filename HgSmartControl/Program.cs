﻿/*
    This file is part of HomeGenie Project source code.

    HomeGenie is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    HomeGenie is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with HomeGenie.  If not, see <http://www.gnu.org/licenses/>.  
*/

/*
 *     Author: Generoso Martello <gene@homegenie.it>
 *     Project Homepage: http://homegenie.it
 */

using HomeGenie.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HgSmartControl
{
    static class Program
    {
        private static string hgServerAddress = "127.0.0.1";
        private static string hgServerUser = "admin";
        private static string hgServerPassword = "";
        private static ControlApi homegenie = new ControlApi();
        private static SmartControl smartcontrol;

        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            homegenie.SetServer(hgServerAddress, hgServerUser, hgServerPassword);

            smartcontrol = new SmartControl();
            Application.Run(smartcontrol);
        }

        public static ControlApi HomeGenie
        {
            get { return homegenie; }
        }

        public static SmartControl SmartControl
        {
            get { return smartcontrol; }
        }

    }
}
