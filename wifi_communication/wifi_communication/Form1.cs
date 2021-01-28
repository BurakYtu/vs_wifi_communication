using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace wifi_communication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            UdpClient Client = new UdpClient(8800); //this device Port number (uygulama ayarlar sekmesinde remoto port yerine bunu yazılacak)
                                                                            //(uygulamada ayarlar sekmesindeki remote addr yerine, pc de cmd ye girip ipconfig yazılmalı ve çıkan ipv4 adresi girilmeli)
            InitializeComponent();
        }

        private void send_btn_Click(object sender, EventArgs e)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPAddress broadcast = IPAddress.Parse("192.168.1.45"); //slave adress (Uygulamada Sol yukarda yazan yani telefonun ip si)

            byte[] sendbuf = Encoding.ASCII.GetBytes(richTextBox1.Text);

            IPEndPoint ep = new IPEndPoint(broadcast, 8080); //slave adress, slave port(uygulama ayarlar sekmesindeki local port)

            s.SendTo(sendbuf, ep);
        }

        private void read_btn_Click(object sender, EventArgs e)
        {
            UdpClient recieveClient = new UdpClient(8800);

            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 8080);
            try
            {
                // Blocks until a message returns on this socket from a remote host.
                Byte[] receiveBytes = recieveClient.Receive(ref RemoteIpEndPoint);

                string Data = Encoding.ASCII.GetString(receiveBytes);

                richTextBox1.Text = Data.ToString();
            }
            catch (Exception)
            {
                Close();
            }
        }
    }
}
