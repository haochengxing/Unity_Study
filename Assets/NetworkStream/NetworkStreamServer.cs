using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class NetworkStreamServer : MonoBehaviour
{
    private static TcpListener tcpLister;

    private static TcpClient tcpClient = null;

    private static Thread receiveThread;
    private static Thread sendThread;
    private static Thread acceptThread;

    private static NetworkStream ns;
    private static BinaryReader br;
    private static BinaryWriter bw;

    private static byte[] buf = new byte[1024*100];


    private static byte[] send_buf;

    private static long time = 0;

    // Start is called before the first frame update
    void Start()
    {
        send_buf = File.ReadAllBytes(Application.dataPath+ "/test.txt");

        Debug.Log(send_buf.Length);

        tcpLister = new TcpListener(IPAddress.Parse("127.0.0.1"), 9999);
        tcpLister.Start();
        acceptThread = new Thread(AcceptThread);
        acceptThread.Start();
    }

    private static void AcceptThread()
    {
        tcpClient = null;

        while (true)
        {
            AcceptAClient();
            Thread.Sleep(100);
        }
    }


    private static void AcceptAClient()
    {
        if (tcpClient != null) return;

        try
        {
            if (tcpClient == null)
            {
                tcpClient = tcpLister.AcceptTcpClient();
            }
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log(e);
            Close();
            return;
        }

        tcpClient.ReceiveTimeout = 1000000;
        ns = tcpClient.GetStream();
        br = new BinaryReader(ns);
        bw = new BinaryWriter(ns);
        ns.ReadTimeout = 600000;

        // 启动一个线程来接受请求
        receiveThread = new Thread(DoReceiveMessage);
        receiveThread.Start();

        // 启动一个线程来发送请求
        sendThread = new Thread(DoSendMessage);
        sendThread.Start();
    }


    private static void DoReceiveMessage()
    {

        //sign为true 循环接受数据
        while (true)
        {
            try
            {
                if (tcpClient == null)
                {
                    Close();
                    return;
                }

                if (ns.CanRead && ns.DataAvailable)
                {
                    try
                    {
                        int length = br.Read(buf,0,buf.Length);

                        Debug.Log(length);
                       
                    }
#pragma warning disable 0168
                    catch (EndOfStreamException ex)
                    {
                        UnityEngine.Debug.Log(ex);
                        Close();
                        return;
                    }
#pragma warning restore 0168
                }

            }
#pragma warning disable 0168
            catch (ThreadAbortException e)
            {
                UnityEngine.Debug.Log(e);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log(e);
                Close();
            }
#pragma warning restore 0168
            Thread.Sleep(10);
        }
    }

    private static void DoSendMessage()
    {
        while (true)
        {
            try
            {
                if (time % 10000 == 0)
                {
                    if (ns.CanWrite)
                    {
                        bw.Write(send_buf, 0, send_buf.Length);
                    }
                }
                
            }
#pragma warning disable 0168
            catch (ThreadAbortException e) {
                UnityEngine.Debug.Log(e);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log(e);
                Close();
            }
#pragma warning restore 0168
            Thread.Sleep(10);
        }
    }

    private static void Close()
    {
        tcpClient = null;
        if (receiveThread != null)
        {
            try
            {
                receiveThread.Abort();
            }
            catch { }
            receiveThread = null;
        }
        if (sendThread != null)
        {
            try
            {
                sendThread.Abort();
            }
            catch { }
            sendThread = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        time ++;
        if (time>=long.MaxValue)
        {
            time = 0;
        }
    }

    private void OnDestroy()
    {
        Close();
    }
}
