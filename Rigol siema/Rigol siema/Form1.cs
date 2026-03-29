// Kamil Tchórzewski 188768, Jerzy Grzonkowski 188622, Marcel Sokołowski 188983

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using System.Data;
using System.Media;
using System.IO.Ports;
using System.Threading;
using System.IO;
using System.Windows.Forms.DataVisualization;

namespace Rigol_siema
{
    public partial class Form1 : Form
    {
        public SerialPort serialPort = new SerialPort();
        public enum functions {DC_V, AC_V, DC_I, AC_I, C, R}
        public string currentFunction;
        public bool portOpened = false;
        public bool continuousMeasument = false;
        public System.Windows.Forms.Timer continuousTimer;
        List<char> tempChars = new List<char>();
        List<string> resultsToFiles = new List<string>();
        public double timer_buf = 0;
        public List<double> timer = new List<double>();
        double currentValueDouble = 500;
        public bool saving_flag = false;

        public Form1()
        {
            InitializeComponent();
            serialPort = DeviceConnection();
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
            comboBox1.SelectedItem = "0.5s";
        }

        public SerialPort DeviceConnection()
        {
            SerialPort serialPort = new SerialPort("COM13", 9600, Parity.None, 8, StopBits.One );
            serialPort.Open();
            portOpened = true;
            return serialPort;
        }

        public void DeviceClose(SerialPort serialPort)
        {
            portOpened = false;
            serialPort.Close();
        }
        public void Measure()
        {
                switch (currentFunction)
                {
                    case "DC_V":
                        serialPort.WriteLine(":MEASure:VOLTage:DC?");
                        break;
                    case "AC_V":
                        serialPort.WriteLine(":MEASure:VOLTage:AC?");
                        break;
                    case "AC_I":
                        serialPort.WriteLine(":MEASure:CURRent:AC?");
                        break;
                    case "DC_I":
                        serialPort.WriteLine(":MEASure:CURRent:DC?");
                        break;
                    case "C":
                        serialPort.WriteLine(":MEASure:CAPacitance?");
                        break;
                    case "R":
                        serialPort.WriteLine(":MEASure:RESistance?");
                        break;
                }
                Thread.Sleep(300);
                string response = serialPort.ReadLine();
                bool NoError = true;
                tempChars = response.ToList();
                response = String.Empty;
                for (int i = 0; i < tempChars.Count; i++)
                {
                    if (tempChars[i] == 'E' || tempChars[i] == 'R')
                    {
                        NoError = false;
                    }
                    if (i == 0)
                    {
                        tempChars[i] = ' ';
                    }
                    response += tempChars[i];
                }
                
                if (NoError)
                {
                    DateTime actualdate = DateTime.Now;
                    ResultLabel.Text = response + " " + currentFunction;
                    resultsToFiles.Add(response);
                    listBox1.Items.Add(response);
                    if(saving_flag) saving();
                    chart1.Series[0].Points.AddXY(timer.Last(),response);
                }
                else
                {
                     ResultLabel.Text = "Rigol to chińskie urządzenie pozbawione wad.";
                }
        }
        public void OnProcessExit(object sender, EventArgs e)
        {
            DeviceClose(serialPort);
        }

        private void InitializeTimer()
        {
            string currentValue = comboBox1.SelectedItem.ToString();
            currentValue = currentValue.Replace('s', ' ');
            currentValue.Trim();
            // currentValue.TrimEnd('s');
            currentValueDouble = Convert.ToDouble(currentValue);
            continuousTimer = new System.Windows.Forms.Timer();
            // continuousTimer.Interval = 500;
            continuousTimer.Interval = Convert.ToInt32(currentValueDouble * 1000);
            continuousTimer.Tick += new EventHandler(Timer_Tick);
            continuousTimer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            string currentValue = comboBox1.SelectedItem.ToString();
            currentValue = currentValue.Replace('s', ' ');
            currentValue.Trim();
            double currentValueDouble = Convert.ToDouble(currentValue);
            timer_buf += currentValueDouble;
            timer.Add(timer_buf);
            Measure();
        }

        private void AC_I_CheckedChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            resultsToFiles.Clear();
            chart1.Series[0].Points.Clear();
            Continuous_measuring.Enabled = true;
            currentFunction = functions.AC_I.ToString();
            serialPort.WriteLine(":FUNCtion:CURRent:AC");
            Thread.Sleep(300);
            Measure();
        }

        private void DC_V_CheckedChanged_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            resultsToFiles.Clear();
            chart1.Series[0].Points.Clear();
            Continuous_measuring.Enabled = true;
            currentFunction = functions.DC_V.ToString();
            serialPort.WriteLine(":FUNCtion:VOLTage:DC");
            Thread.Sleep(300);
            Measure();
        }

        private void AC_V_CheckedChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            resultsToFiles.Clear();
            chart1.Series[0].Points.Clear();
            Continuous_measuring.Enabled = true;
            currentFunction = functions.AC_V.ToString();
            serialPort.WriteLine(":FUNCtion:VOLTage:AC");
            Thread.Sleep(300);
            Measure();
        }

        private void DC_I_CheckedChanged_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            resultsToFiles.Clear();
            chart1.Series[0].Points.Clear();
            Continuous_measuring.Enabled = true;
            currentFunction = functions.DC_I.ToString();
            serialPort.WriteLine(":FUNCtion:CURRent:DC");
            Thread.Sleep(300);
            Measure();
        }

        private void Resistance_CheckedChanged_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            resultsToFiles.Clear();
            chart1.Series[0].Points.Clear();
            Continuous_measuring.Enabled = true;
            currentFunction = functions.R.ToString();
            serialPort.WriteLine(":FUNCtion:RESistance");
            Thread.Sleep(300);
            Measure();
        }

        private void Capacitance_CheckedChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            resultsToFiles.Clear();
            chart1.Series[0].Points.Clear();
            Continuous_measuring.Enabled = true;
            currentFunction = functions.C.ToString();
            serialPort.WriteLine(":FUNCtion:CAPacitance");
            Thread.Sleep(300);
            Measure();
        }

        private void Continuous_measuring_CheckedChanged_1(object sender, EventArgs e)
        {
            continuousMeasument = true;
            if (continuousMeasument) InitializeTimer();      
        }

        private void Continuous_measuring_Unchecked(object sender, EventArgs e)
        {
            
        }

        private void saveButton_Click_1(object sender, EventArgs e)
        {
            saving_flag = true;
        }
        public void saving()
        {
            DateTime actualdate = DateTime.Now;
            string actualdatestring = actualdate.ToString("d");
            Random rand = new Random();
            int random = rand.Next(1, 599999);
            string path = "C:/rigolprogramik/" + textBox1.Text + ".txt";
            //File.WriteAllLines(path, resultsToFiles);
            using (StreamWriter writer = new StreamWriter(path, append: true))
            {
                writer.WriteLine(resultsToFiles.Last());
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void end_save_button_Click(object sender, EventArgs e)
        {
            saving_flag = false;
        }

      
    }
}
