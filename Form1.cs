using Midi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogiMidi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public InputDevice inDevice;

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (Midi.InputDevice input in Midi.InputDevice.InstalledDevices)
                comboBox1.Items.Add(input.Name);
        }

        private void button2_Click(object sender, EventArgs e) // Button TestSDK
        {
            
        }

        private void button1_Click(object sender, EventArgs e) // Button StartListening
        {
            inDevice = InputDevice.InstalledDevices[comboBox1.SelectedIndex];

            if (!inDevice.IsOpen)
            {
                

                inDevice.Open();
                inDevice.StartReceiving(null);
                inDevice.NoteOn += NoteOn;
                inDevice.NoteOff += NoteOff;
            }
            else
            {
                inDevice.Close();

                inDevice.Open();
                inDevice.StartReceiving(null);
                inDevice.NoteOn += NoteOn;
                inDevice.NoteOff += NoteOff;
            }
        }

        public void NoteOn(NoteOnMessage msg)
        {
            if(msg.Velocity < 128)
            {
                Gmidi.palette_led(Convert.ToInt32(msg.Pitch), msg.Velocity);
                pictureBox4.BackColor = Color.FromArgb(163, 133, 0);
            }
            else
            {
                Gmidi.palette_led(Convert.ToInt32(msg.Pitch), 0);
                pictureBox4.BackColor = Color.FromArgb(63, 33, 0);
            }
        }

        public void NoteOff(NoteOffMessage msg)
        {
            Gmidi.palette_led(Convert.ToInt32(msg.Pitch), 0);
            pictureBox4.BackColor = Color.FromArgb(63, 33, 0);
        }

        
    }
}
