using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;


namespace UdpListener
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private const int listenPort = 8800;

        

        private void btnConnect_Click(object sender, EventArgs e)
        {
            UdpClient listener = new UdpClient(listenPort);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);

            try
            {
               // txtmsg.AppendText("Waiting for broadcast\n\n");
                byte[] bytes = listener.Receive(ref groupEP);

               // txtmsg.AppendText($"Received broadcast from {groupEP} :");
                txtmsg.AppendText($" {Encoding.ASCII.GetString(bytes, 0, bytes.Length)}");

            }
            catch (SocketException)
            {
                return;
            }
            finally
            {
                listener.Close();
            }

        }


        UdpClient Client = new UdpClient(8080);
        private void btnsend_Click(object sender, EventArgs e)
        {
            

            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPAddress broadcast = IPAddress.Parse("192.168.1.101"); //slave adress (Uygulamada Sol yukarda yazan yani telefonun ip si)

            byte[] sendbuf = Encoding.ASCII.GetBytes(txtsnd.Text);

            IPEndPoint ep = new IPEndPoint(broadcast, 8080); //slave adress, slave port(uygulama ayarlar sekmesindeki local port)

            s.SendTo(sendbuf, ep);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }


       

       
    }

