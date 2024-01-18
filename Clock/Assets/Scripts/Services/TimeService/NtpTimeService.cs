using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace MSuhinin.Clock
{
    public class NtpTimeService: IWorldTimeService
    {
        private DateTime _currentDateTime = DateTime.Now;
        public void Initialize(string ntpServer)
        {
            GetCurrentDateTimeFromNTP(ntpServer);
        }
        
        public DateTime UpdateTimeFromWeb()
        {
            //here we don't need to get the datetime from the server again
            // just add elapsed time since the game start to _currentDateTime

            return _currentDateTime.AddSeconds(Time.realtimeSinceStartup);
        }
        
        
        private  DateTime GetCurrentDateTimeFromNTP(string ntpServer)
        {
  

            //Размер данных, получаемых от ntp сервера
            var ntpData = new byte[48];

            //выставляем Leap Indicator, Version Number and Mode values
            ntpData[0] = 0x1B; //LI = 0 (без предупреждений), VN = 3 (только IPv4), Mode = 3 (Клиент)

            //получаем адрес от днс сервера
            var addresses = Dns.GetHostEntry(ntpServer).AddressList;

            //Выставляем порт 123 для NTP 
            var ipEndPoint = new IPEndPoint(addresses[0], 123);


            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                //Открываем подключение    
                socket.Connect(ipEndPoint);

                socket.ReceiveTimeout = 3000;
                //отсылваем запрос
                socket.Send(ntpData);

                //Получаем ответ
                socket.Receive(ntpData);
                socket.Close();
            }

            //Смещение для перехода в отсек с временем
            const byte serverReplyTime = 40;

            //получаем первую часть запроса и переводим биты в числа
            ulong intPart = BitConverter.ToUInt32(ntpData, serverReplyTime);

            //Получаем следующую часть и переводим биты в числа
            ulong fractPart = BitConverter.ToUInt32(ntpData, serverReplyTime + 4);

            //меняем порядок байтов big-endian в little-endian
            intPart = SwapEndianness(intPart);
            fractPart = SwapEndianness(fractPart);

            //получаем кол-во миллисекунд, прошедшее с 1900 года
            var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);

            //переводим время в **UTC**
            _currentDateTime =
                (new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds((long)milliseconds);


            return _currentDateTime.ToLocalTime();
        }

        static uint SwapEndianness(ulong x)
        {
            return (uint)(((x & 0x000000ff) << 24) +
                          ((x & 0x0000ff00) << 8) + ((x & 0x00ff0000) >> 8) +
                          ((x & 0xff000000) >> 24));
        }

      
    }
}