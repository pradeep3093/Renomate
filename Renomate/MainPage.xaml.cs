using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Modbus.Device;
using System.IO.Ports;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Renomate
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        SerialPort _port;
        ModbusSerialMaster _master;

        const int SLAVE_ADDRESS = 184;
        const int COIL_ADDRESS = 32;
        /**
         * Holding register address for set temperatuer, set1, set2, set3
         */
        //184
        //187
        //190
        // divide by 10 is celcious with fraction
        // ex 188 / 10 = 18.8 degree

        /**
         * Input register values for reading current temperature value from censor
         * 
         * Address: 6
         * divide by 10 is celcious with fraction
         * ex 188 / 10 = 18.8 degree
         * 
         */

        public MainPage()
        {
            this.InitializeComponent();
            string portName = Environment.OSVersion.Platform == PlatformID.Win32NT ? "COM1" : "/dev/serial0";
            _port = new SerialPort(portName, 115200);
            _port.ReadTimeout = 100;
            _port.WriteTimeout = 100;
            _port.Open();

            // Initialize Modbus master
            _master = ModbusSerialMaster.CreateRtu(_port);

            // Read the current state of the output
            ReadState();
        }

        private void ReadState()
        {
            // Read the current state of the output
            var state = _master.ReadHoldingRegisters(SLAVE_ADDRESS, COIL_ADDRESS, 1);

            // Update the UI
            if (state[0])
            {
                StateLabel.Text = "On";
            }
            else
            {
                StateLabel.Text = "Off";
            }
        }

        private void Hamburger_Click(object sender, RoutedEventArgs e)
        {
            view1.IsPaneOpen = !view1.IsPaneOpen;
        }

        private void IconsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (shared.IsSelected)
            {
                RightsideBlock.Text = "shared";
            }
            else if (Important.IsSelected)
            {
                RightsideBlock.Text = "Important";
            }
            else if (Details.IsSelected)
            {
                RightsideBlock.Text = "Details";
            }
        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            if (toggleSwitch != null)
            {
                if (toggleSwitch.IsOn == true)
                {
                   // progress1.IsActive = true;
                   // progress1.Visibility = Visibility.Visible;
                }
                else
                {
                  //  progress1.IsActive = false;
                  //  progress1.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Slider slider = sender as Slider;
            if (slider != null)
            {
                // slider.Value;
            }
        }

    }
}
