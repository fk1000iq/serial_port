using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Management;    //需要再引用中添加
using Microsoft.Win32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Threading;

namespace serial_port
{
    public partial class Form1 : Form
    {

        private ManagementEventWatcher arrivalWatcher;
        private ManagementEventWatcher removalWatcher;

        public uint sum_val = 0;


        public Form1()
        {
            InitializeComponent();
            SetupUsbEventWatchers();//热插拔动作设定
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateSerialPortList();//上电初始化

			//combobox 下拉显示宽度，下拉类型设置
            comboBoxPorts.DropDownWidth = 200;
			comboBoxPorts.DropDownStyle = ComboBoxStyle.DropDownList;//这样不能修改项
		}


        

        private void DeviceArrival(object sender, EventArrivedEventArgs e)
        {
			DebounceUpdateSerialPortList();
		}

        private void DeviceRemoval(object sender, EventArrivedEventArgs e)
        {
			DebounceUpdateSerialPortList();
		}

		private System.Threading.Timer debounceTimer;
		private void DebounceUpdateSerialPortList()
		{
			// 如果定时器已经存在，则取消它
			if (debounceTimer != null)
			{
				debounceTimer.Change(Timeout.Infinite, Timeout.Infinite);
				debounceTimer.Dispose();
			}

			// 设置一个新的定时器，在500毫秒后执行更新操作
			debounceTimer = new System.Threading.Timer((state) =>
			{
				UpdateSerialPortList();
			}, null, 500, Timeout.Infinite);
		}


		//public string[] portDevName = null;

		/*
            usb xxx xxx (COMx) ---> COMx #usb xxx xxx
        */
		private string ConvertStringOrder(string input)
		{
			// 找到左括号的位置
			int startIndex = input.IndexOf('(');
			// 找到右括号的位置
			int endIndex = input.IndexOf(')');

			// 提取括号内的内容（COM端口号）
			string comPort = input.Substring(startIndex + 1, endIndex - startIndex - 1);
			// 提取括号外的内容
			string otherPart = input.Substring(0, startIndex) + input.Substring(endIndex + 1);

			// 重新组合字符串
			string result = $"{comPort} #{otherPart}";
			return result;
		}
        
        // 修改字符串组顺序
		private string[] changeStringOrder(string[] strsIn)
		{
            string[] strsOut;
			List<string> stringList = new List<string>();

			foreach (string str in strsIn)
			{
				string str_new = ConvertStringOrder(str);
				stringList.Add(str_new);
			}
			strsOut = stringList.ToArray();

            return strsOut;
		}
		/*
            1.需要添加名称空间
            2.需要在“引用”中添加 System.Management
         */
		private string[] getPortDevName()
		{
            string[] portDevName;
			using (ManagementObjectSearcher searcher = new ManagementObjectSearcher
				("select * from Win32_PnPEntity where Name like '%(COM%'"))
			{
				List<string> stringList = new List<string>();

				var hardInfos = searcher.Get();
				foreach (var hardInfo in hardInfos)
				{
					if (hardInfo.Properties["Name"].Value != null)
					{
						string devName = hardInfo.Properties["Name"].Value.ToString(); //deviceName like--- "USB-Enhanced-SERIAL CH343 (COM8)"
						Console.WriteLine(devName);
						stringList.Add(devName);
					}
				}
				portDevName = stringList.ToArray();
			}

			// portDevName as "USB-Enhanced-SERIAL CH343 (COM8)" --->portDevName_new as "COM8 #USB-Enhanced-SERIAL CH343" 
			string[] portDevName_new = changeStringOrder(portDevName);
  
			return portDevName_new;
		}


		private void UpdateSerialPortList()
        {
            if (comboBoxPorts.InvokeRequired)
            {
                comboBoxPorts.Invoke(new MethodInvoker(UpdateSerialPortList));
            }
            else
            {
                comboBoxPorts.Items.Clear();//清除旧的下拉列表
                string[] ports = getPortDevName();//获取当前可用串口信息
				comboBoxPorts.Items.AddRange(ports);//更新下拉列表

                if (comboBoxPorts.Items.Count > 0)//若存在至少一个串口
                {
                    comboBoxPorts.SelectedIndex = 0;    //choose the first one
					comboBoxPorts.SelectionStart = 0;   //让光标回到最左侧，若是DropdownList，则不显示光标，显示内容从最左侧开始
				}
                else {//无串口则清除之前选中的串口的文本信息（注意清除列表时，不会清除Text, 需要自己额外清除）
					comboBoxPorts.Text = string.Empty;  // clear Text
				}
            }
        }


        private void SetupUsbEventWatchers()
        {
            WqlEventQuery arrivalQuery = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 2");
            arrivalWatcher = new ManagementEventWatcher(arrivalQuery);
            arrivalWatcher.EventArrived += new EventArrivedEventHandler(DeviceArrival);
            arrivalWatcher.Start();

            WqlEventQuery removalQuery = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 3");
            removalWatcher = new ManagementEventWatcher(removalQuery);
            removalWatcher.EventArrived += new EventArrivedEventHandler(DeviceRemoval);
            removalWatcher.Start();
        }




    }
}
