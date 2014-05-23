﻿using HgSmartControl.Client;
using HgSmartControl.Client.Data;
using HgSmartControl.Widgets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HgSmartControl
{

    public partial class SmartControl : Form
    {
        private string hgServerAddress = "127.0.0.1";
        private string hgServerUser = "admin";
        private string hgServerPassword = "";

        private Timer widgetCycle = new Timer();
        private UserControl currentWidget = null;

        private Weather weatherWidget = new Weather();
        private List<GroupView> groupWidgets = new List<GroupView>();

        private int currentGroup = 0;

        public SmartControl()
        {
            InitializeComponent();

            widgetCycle.Interval = 5000;
            widgetCycle.Tick += widgetCycle_Tick;
            widgetCycle.Enabled = true;
            widgetCycle.Start();

            currentWidget = weatherWidget;
            currentWidget.Dock = DockStyle.Fill;
            this.Controls.Add(currentWidget);

            ControlApi hg = new ControlApi();
            hg.SetServer(hgServerAddress, hgServerUser, hgServerPassword);
            hg.LoadDataCompleted = () =>
            {

                foreach (Group g in hg.Groups)
                {
                    GroupView widget = new GroupView();
                    widget.ItemClicked = (sender, tile) =>
                    {
                        ShowModuleScreen(tile.Module);
                    };
                    widget.SetGroup(g);
                    widget.GroupsButtonClicked += (sender, args) =>
                    {
                        this.Controls.Add(groupList1);
                        this.Controls.SetChildIndex(groupList1, 0);
                        groupList1.Invalidate();
                    };
                    groupWidgets.Add(widget);
                }

                groupList1.SetGroups(hg.Groups);

            };
            hg.Connect();
        }

        void ShowModuleScreen(Module m)
        {
            widgetCycle.Stop();
            this.Controls.Remove(currentWidget);
            Dimmer dimmerWidget = new Dimmer();
            dimmerWidget.CloseButtonClicked += (sender, args) =>
            {
                this.Controls.Remove(dimmerWidget);
                this.Controls.Add(currentWidget);
                widgetCycle.Start();
            };
            dimmerWidget.Module = m;
            this.Controls.Add(dimmerWidget);
        }

        void widgetCycle_Tick(object sender, EventArgs e)
        {
            return;

            if (currentWidget.Equals(weatherWidget))
            {
                if (groupWidgets.Count > 0)
                {
                    this.Controls.Remove(currentWidget);
                    currentWidget = groupWidgets[currentGroup];
                    currentGroup++;
                    if (currentGroup >= groupWidgets.Count) currentGroup = 0;
                }
            }
            else
            {
                this.Controls.Remove(currentWidget);
                currentWidget = weatherWidget;
            }
            currentWidget.Dock = DockStyle.Fill;
            this.Controls.Add(currentWidget);
        }

        private void groupList1_GroupSelected(object sender, int e)
        {
            this.Controls.Remove(groupList1);
            if (currentWidget != null)
            {
                this.Controls.Remove(currentWidget);
            }
            currentWidget = groupWidgets[e];
            currentWidget.Dock = DockStyle.Fill;
            this.Controls.Add(currentWidget);
        }
    }
}