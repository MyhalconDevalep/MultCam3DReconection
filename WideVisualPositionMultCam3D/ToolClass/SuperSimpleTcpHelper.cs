using SuperSimpleTcp;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WideVisualPositionMultCam3D.ToolClass
{
    public class SuperSimpleTcpHelper
    {
        public SimpleTcpServer _server;
        public bool _startFlag { get; set; } = false;
        public string CurrenIPPort { get; set; } = "";

        public Action<int, string> ActionPrintConnectionLog { get; set; }
        private CancellationTokenSource Cts1;
        private CancellationToken token;

        public Action<byte[]> ActionReceivedMsg { get; set; }
        public int[] msg { get; set; }

        public SuperSimpleTcpHelper(string Ip, int Port, bool HeartEnable)
        {
            msg = new int[4];
            _server = new SimpleTcpServer(Ip, Port);

            _server.Events.ClientConnected += (s, e) =>
            {
                _startFlag = true;
                CurrenIPPort = e.IpPort;
                if (HeartEnable)
                {
                    Cts1 = new CancellationTokenSource();
                    token = Cts1.Token;
                    Task.Factory.StartNew(() => { StartHeartBeat(CurrenIPPort, token); }, token);
                }
                Task.Run(() => ActionPrintConnectionLog?.Invoke(Port, "已连接"));

            };
            _server.Events.ClientDisconnected += (s, e) =>
            {
                _startFlag = false;
                if (HeartEnable)
                {
                    Cts1?.Cancel();
                }

                Task.Run(() => ActionPrintConnectionLog?.Invoke(Port, "已断开"));
            };
            _server.Events.DataReceived += (s, e) =>
            {

                //msg[0]=BitConverter.ToInt32(e.Data.Array,0);
                //msg[1] = BitConverter.ToInt32(e.Data.Array, 4);
                //msg[2] = BitConverter.ToInt32(e.Data.Array, 8);
                //msg[3] = BitConverter.ToInt32(e.Data.Array, 12);
                //byte[] way = new byte[4];
                // int[] msg= ConvertBytesToIntArray(e.Data.Array);

                // string msg = Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count);


                ActionReceivedMsg?.Invoke(e.Data.Array);

            };
            try
            {
                _server.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Port + ":" + ex.Message);
            }

        }



        public void Send(string msg)
        {
            if (_startFlag)
                _server.Send(CurrenIPPort, msg);
        }
        public void Send(ushort msg)
        {
            if (_startFlag)
            {
                _server.Send(CurrenIPPort, UshortToByte(msg));
            }

        }
        public void Send(ushort msg1, ushort msg2)
        {
            if (_startFlag)
            {
                _server.Send(CurrenIPPort, UshortToByte(msg1, msg2));
            }
        }

        public void Send(ushort msg1, ushort msg2, ushort msg3, ushort msg4)
        {
            if (_startFlag)
            {
                _server.Send(CurrenIPPort, UshortToByte(msg1, msg2, msg3, msg4));
            }
        }

        public void Send(bool msg1, bool msg2, bool msg3, bool msg4)
        {
            if (_startFlag)
            {
                byte[] bytes = new byte[4];
                SetBit(bytes, 0, msg1);
                SetBit(bytes, 1, msg2);
                SetBit(bytes, 2, msg3);
                SetBit(bytes, 3, msg4);
                if (_startFlag)
                {
                    _server.Send(CurrenIPPort, bytes);
                }
            }

        }
        public readonly object _lock = new object();
        public void Send(int msg1, int msg2, int msg3, int msg4)
        {
            byte[] sendData = new byte[1];
            sendData[0] = ConvertIntsToByte(msg1, msg2, msg3, msg4);
            if (_startFlag)
            {
                try
                {
                    lock (_lock)
                    {
                        _server?.Send(CurrenIPPort, sendData);
                    }
                }
                catch (Exception)
                {

                }

            }
        }


        public void Send(byte bytess)
        {
            byte[] bytes = new byte[1];
            bytes[0] = bytess;

            if (_startFlag)
            {
                _server.Send(CurrenIPPort, bytes);
            }
        }

        /// <summary>
        /// 设置或清除字节数组中的特定位
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="bitIndex">位索引（从0开始）</param>
        /// <param name="value">布尔值（true或false）</param>
        private void SetBit(byte[] bytes, int bitIndex, bool value)
        {
            if (bitIndex < 0 || bitIndex >= 8 * bytes.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(bitIndex), "Bit index is out of range.");
            }

            int byteIndex = bitIndex / 8;
            int bitPosition = bitIndex % 8;

            if (value)
            {
                // 设置位
                bytes[byteIndex] |= (byte)(1 << (7 - bitPosition));
            }
            else
            {
                // 清除位
                bytes[byteIndex] &= (byte)~(1 << (7 - bitPosition));
            }
        }

        private async void StartHeartBeat(string prot, CancellationToken tokenSource)
        {
            int counter = 0;
            byte[] bytesheart = new byte[1];
            while (!tokenSource.IsCancellationRequested)
            {
                try
                {
                    tokenSource.ThrowIfCancellationRequested();
                    byte[] heartbeat = (counter % 2 == 0) ? UshortToByte(0) : UshortToByte(1); // 0和1交替
                   // bytesheart[0] = (counter % 2 == 0) ? (byte)0 : (byte)1; // 0和1交替
                    _server.Send(prot, heartbeat);
                    counter++;
                    await Task.Delay(500, tokenSource);
                }
                catch (Exception)
                {
                    break;

                }

            }

        }


        /// <summary>
        /// 将4个int值（每个值为0或1）转换为一个byte
        /// </summary>
        /// <param name="value1">对应第7位</param>
        /// <param name="value2">对应第6位</param>
        /// <param name="value3">对应第5位</param>
        /// <param name="value4">对应第4位</param>
        /// <returns>转换后的byte值</returns>
        private byte ConvertIntsToByte(int value1, int value2, int value3, int value4)
        {
            // 确保输入的值是0或1
            if (value1 != 0 && value1 != 1) throw new ArgumentException("value1 must be 0 or 1");
            if (value2 != 0 && value2 != 1) throw new ArgumentException("value2 must be 0 or 1");
            if (value3 != 0 && value3 != 1) throw new ArgumentException("value3 must be 0 or 1");
            if (value4 != 0 && value4 != 1) throw new ArgumentException("value4 must be 0 or 1");

            // 使用位操作将4个int值组合成一个byte
            byte result = (byte)(
                (value1 << 0) |  // 设置第7位
                (value2 << 1) |  // 设置第6位
                (value3 << 2) |  // 设置第5位
                (value4 << 3) |   // 设置第4位
                (0 << 4) |  // 
                (0 << 5) |  // 
                (0 << 6) |  // 
                (0 << 7)    // 
            );

            return result;
        }


        //byte[] data = UshortToByte(1);
        private byte[] UshortToByte(ushort number1)
        {

            byte[] data = new byte[2];
            data[0] = (byte)(number1 >> 8); // 高位字节
            data[1] = (byte)number1;        // 低位字节
            return data;
        }

        private byte[] UshortToByte(ushort number1, ushort number2)
        {
            ushort[] sxs = new ushort[2] { number1, number2 };
            byte[] data = new byte[4];

            data[0] = (byte)(sxs[0] >> 8); // 高位字节
            data[1] = (byte)sxs[0];        // 低位字节
            data[2] = (byte)(sxs[1] >> 8);
            data[3] = (byte)sxs[1];
            return data;
        }


        private byte[] UshortToByte(ushort number1, ushort number2, ushort number3, ushort number4)
        {

            ushort[] sxs = new ushort[4] { number1, number2, number3, number4 };
            byte[] data = new byte[8];

            data[0] = (byte)(sxs[0] >> 8); // 高位字节
            data[1] = (byte)sxs[0];        // 低位字节
            data[2] = (byte)(sxs[1] >> 8);
            data[3] = (byte)sxs[1];
            data[4] = (byte)(sxs[2] >> 8); // 高位字节
            data[5] = (byte)sxs[2];        // 低位字节
            data[6] = (byte)(sxs[3] >> 8);
            data[7] = (byte)sxs[3];
            return data;
        }
    }
}
