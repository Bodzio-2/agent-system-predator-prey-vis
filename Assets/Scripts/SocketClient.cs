using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;
using System.Threading.Tasks;

public class SocketClient
{
    const string IP_ADDRESS = "127.0.0.1";
    const int PORT = 65432;

    Socket sender;
    IPAddress iPAddress;
    IPEndPoint remoteEP;

    public bool alive = false;

    public void StartClient()
    {
        try
        {
            iPAddress = IPAddress.Parse(IP_ADDRESS);
            remoteEP = new IPEndPoint(iPAddress, PORT);

            sender = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                sender.Connect(remoteEP);

                Debug.Log("Socket Connected to " + sender.RemoteEndPoint.ToString());
                alive = true;

                //byte[] msg = Encoding.ASCII.GetBytes("Test <EOF>");

                //int bytesToSend = sender.Send(msg);

                //int bytesRec = sender.Receive(bytes);

                //Debug.Log("Got response: " + Encoding.ASCII.GetString(bytes, 0, bytesRec));

                //sender.Shutdown(SocketShutdown.Both);
                //sender.Close();
            }catch(Exception e)
            {
                Debug.LogWarning("Exception!: " + e.ToString());
            }
        }catch(Exception e)
        {
            Debug.LogWarning("Exception!: " + e.ToString());
        }
    }
    
    public async Task<string> ReceiveDataAsync()
    {
        try
        {
            byte[] bytes = new byte[65536];

            Debug.Log("Begin receive!");

            //Task<int> response = sender.ReceiveAsync(bytes, SocketFlags.None);

            int bytesRec = await sender.ReceiveAsync(bytes, SocketFlags.None);

            //await response;
            Debug.Log("End receive!");
            if(bytesRec > 0)
                sender.Send(Encoding.ASCII.GetBytes("Got async response!"));
            return Encoding.ASCII.GetString(bytes, 0, bytesRec);
        }catch(Exception e)
        {
            Debug.Log("Bad stuff happens lol: " + e);
            return "";
        }
    }

    public string ReceiveData()
    {
        try
        {
            byte[] bytes = new byte[65536];
            int bytesRec = sender.Receive(bytes);

            if(bytesRec > 0)
                sender.Send(Encoding.ASCII.GetBytes("Got Response!"));

            return Encoding.ASCII.GetString(bytes, 0, bytesRec);
        }catch(Exception e)
        {
            Debug.Log("Bad stuff happens lol: " + e);
            return "";
        }
    } 

    public void ShutdownConnection()
    {
        sender.Shutdown(SocketShutdown.Both);
        sender.Close();
        alive = false;
    }

}
