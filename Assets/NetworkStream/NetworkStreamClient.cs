using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class NetworkStreamClient : MonoBehaviour
{

    private static TcpClient m_client = null;
    private static Thread m_sendThread;
    private static Thread m_receiveThread;

    private static NetworkStream ns;
    private static BinaryWriter bw;
    private static BinaryReader br;

    private static byte[] buf = new byte[1024 * 100];

    // Start is called before the first frame update
    void Start()
    {
        m_client = new TcpClient();

        m_client.NoDelay = true;

        m_client.Connect("127.0.0.1", 9999);

        m_client.Client.SendTimeout = 30000;

        ns = m_client.GetStream();
        bw = new BinaryWriter(ns);
        br = new BinaryReader(ns);

        m_sendThread = new Thread(new ThreadStart(DoSendMessage));
        m_sendThread.Start();
        m_receiveThread = new Thread(new ThreadStart(DoRecieveMessage));
        m_receiveThread.Priority = System.Threading.ThreadPriority.Lowest;
        m_receiveThread.Start();
    }

    private static void DoSendMessage()
    {
        while (true)
        {
            try
            {
                if (m_sendThread == null)
                {
                    return;
                }
                bw.Write(0);
                Thread.Sleep(10);
            }
#pragma warning disable 0168
            catch (ThreadAbortException e) { }
            catch (Exception e)
            {
                UnityEngine.Debug.Log(e);
                Close();
            }
#pragma warning restore 0168
        }

    }

    private static void DoRecieveMessage()
    {
        while (true)
        {
            try
            {
                if (m_receiveThread == null)
                {
                    return;
                }
                if (ns.CanRead && ns.DataAvailable)
                {
                    int length = br.Read(buf, 0, buf.Length);

                    Debug.Log(length);

                }
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log(e);
            }
            Thread.Sleep(10);
        }
    }

    private static void Close()
    {
        try
        {
            if (m_client != null)
            {
                if (m_client.Connected)
                {
                    m_client.Close();
                }
                m_client = null;
            }
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log(e);
        }
        finally
        {
        }

        if (m_receiveThread != null)
        {
            var tmp = m_receiveThread;
            m_receiveThread = null;
            tmp.Abort();
        }

        if (m_sendThread != null)
        {
            var tmp = m_sendThread;
            m_sendThread = null;
            tmp.Abort();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        Close();
    }
}
