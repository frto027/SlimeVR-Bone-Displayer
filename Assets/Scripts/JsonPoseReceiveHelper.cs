
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonPoseReceiveHelper
{
    private static System.Threading.Thread jsonReveiveThread;
    private static string jsonData;

    public static void ThreadStart(object thread)
    {
        System.Net.Sockets.Socket socket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, System.Net.Sockets.ProtocolType.Udp);
        socket.Bind(new System.Net.IPEndPoint(System.Net.IPAddress.Loopback, 6788));
        byte[] buffer = new byte[1024 * 1024 * 10];
        while (jsonReveiveThread == thread)
        {
            System.Net.EndPoint remoteEP = new System.Net.IPEndPoint(System.Net.IPAddress.Any,0);
            int count = socket.ReceiveFrom(buffer, ref remoteEP);
            jsonData = System.Text.Encoding.UTF8.GetString(buffer, 0, count);
            System.Threading.Thread.MemoryBarrier();
        }
        socket.Close();
    }

    public static string GetLatestPackage()
    {
        if (jsonReveiveThread == null)
        {
            jsonReveiveThread = new System.Threading.Thread(ThreadStart);
            jsonReveiveThread.Start(jsonReveiveThread);
        }

        return jsonData;
    }
}
