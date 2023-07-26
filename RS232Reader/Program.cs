using System;
using System.ComponentModel.DataAnnotations;
using System.IO.Ports;

class Program
{
    static SerialPort serialPort;

    static void Main(string[] args)
    {
        //Console.WriteLine("Press any key to send data.");
        //Console.ReadKey();

        //// 傳送資料
        //string dataToSend = "Hello, World!";
        //SendData(dataToSend);

        //Console.WriteLine("Data sent. Press any key to exit.");
        //Console.ReadKey();

        //// 關閉串口
        //serialPort.Close();

        Console.WriteLine("Enter the COM port name:");
        string comPort = Console.ReadLine();

        Console.WriteLine("Enter the baud rate:");
        int baudRate = int.Parse(Console.ReadLine());

        serialPort = new SerialPort(comPort, baudRate);
        serialPort.DataReceived += SerialPortDataReceived;

        // 開啟串口
        serialPort.Open();

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();

        serialPort.Close();
    }

    static void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        SerialPort serialPort = (SerialPort)sender;
        int bufferSize = serialPort.BytesToRead;
        byte[] buffer = new byte[bufferSize];

        // 读取字节数据到缓冲区
        serialPort.Read(buffer, 0, bufferSize);

        // 处理接收到的字节数据
        ProcessReceivedBytes(buffer);
        Thread.Sleep(100);
        SendData(buffer);
    }

    static void ProcessReceivedBytes(byte[] buffer)
    {
        // 在这里对接收到的字节数据进行处理
        Console.WriteLine("Received data:");
        foreach (byte dataByte in buffer)
        {
            Console.Write(dataByte.ToString("X2") + " "); // 打印每个字节的十六进制表示形式            
        }
        Console.WriteLine();
    }

    static void SendData(byte[] data)
    {
        // 傳送資料到串口
        serialPort.Write(data, 0, data.Length);

        // 在這裡添加相應的處理邏輯，以應對傳送後的操作
        Console.WriteLine("Sent: " );
        Console.Write(BitConverter.ToString(data));
        Console.WriteLine();
    }
}