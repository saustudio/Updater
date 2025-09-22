using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using MessageEx;

namespace Updater
{
    public class ClientData
    {
        private readonly Socket ClientSocket = new Socket
            (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private const int BUFFER_SIZE = 2048;

        private string ip;
        private int port;

        private static Random random = new Random();

        public static string Sha1Hash(string input)
        {
            byte[] hash;
            using (var sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider())
                hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sb = new StringBuilder();
            foreach (byte b in hash) sb.AppendFormat("{0:x2}", b);
            return sb.ToString();
        }

       


        //[Obfuscation(Feature = "virtualization", Exclude = false)]
        private byte[] XorEncode(byte[] buf)
        {
            //byte[] _out = new byte[buf.Length + 2];
            //_out[0] = (byte)random.Next(1, 225);
            //_out[1] = (byte)random.Next(4, 100);
            //for (int i = 0; i < buf.Length; i++)
            //{
            //    _out[i + 2] = (byte)(_out[i + 2] ^ _out[1]);
            //    _out[i + 2] = (byte)(_out[i + 2] ^ 24);
            //    _out[i + 2] = (byte)(buf[i] ^ _out[0]);
            //}
            //_out[0] = (byte)(_out[0] ^ _out[1]);
            //return _out;
            throw new NotImplementedException();
        }


        //[Obfuscation(Feature = "virtualization", Exclude = false)]
        private byte[] XorDecode(byte[] buf)
        {
            //byte[] _out = new byte[buf.Length - 2];
            //int a = 0;
            //for (int i = 2; i < buf.Length; i++)
            //{
            //    _out[a] = (byte)(_out[a] ^ buf[1]);
            //    _out[a] = (byte)(_out[a] ^ 24);
            //    _out[a] = (byte)(buf[i] ^ (byte)(buf[0] ^ buf[1]));
            //    a++;
            //}
            //return _out;
            throw new NotImplementedException();
        }

        public ClientData(string ipp, int portt)
        {
            this.ip = ipp;
            this.port = portt;
        }

        public bool ConnectToServer()
        {

            try
            {
                if (!ClientSocket.Connected)
                    ClientSocket.Connect(IPAddress.Parse(this.ip), this.port);
            }
            catch (SocketException ex)
            {
                MessageBoxEx.ShowError(ex.Message, "Error", 10000);
                return false;
            }

            return true;
        }

        [Obfuscation(Feature = "virtualization", Exclude = false)]
        public bool SendString(string str)
        {
            try
            {
                if (ClientSocket.Connected)
                {
                    byte[] buffer = XorEncode(Encoding.UTF8.GetBytes(str));

                    ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);

                    return true;
                }
            }
            catch(SocketException ex) { MessageBoxEx.ShowError(ex.Message, "Error", 10000); }
            return false;
        }

        [Obfuscation(Feature = "virtualization", Exclude = false)]
        public string ReceiveResponse()
        {
            try
            {
                if (ClientSocket.Connected)
                {
                    var buffer = new byte[BUFFER_SIZE];
                    int received = ClientSocket.Receive(buffer, SocketFlags.None);
                    if(received > 0)
                    {
                        var data = new byte[received];
                        Array.Copy(buffer, data, received);

                        return Encoding.UTF8.GetString(XorDecode(data));
                    }
                }
            }
            catch (SocketException ex) { MessageBoxEx.ShowError(ex.Message, "Error", 10000); }
            return "";
        }


        public void Disconnect()
        {
            if(ClientSocket.Connected)
            {
                ClientSocket.Shutdown(SocketShutdown.Both);
                ClientSocket.Close();
            }
        }

    }
}
