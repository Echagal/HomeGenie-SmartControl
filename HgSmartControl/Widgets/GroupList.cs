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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using HomeGenie.Client.Data;

namespace HgSmartControl.Widgets
{
    public partial class GroupList : UserControl
    {
        public event EventHandler<int> GroupSelected;


        public GroupList()
        {
            InitializeComponent();
        }

        public void SetGroups(List<Group> groups)
        {
            foreach(Group g in groups)
            {
                this.listBoxGroups.Items.Add(g.Name);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (GroupSelected != null) GroupSelected(this, listBoxGroups.SelectedIndex);
        }
    }
}
