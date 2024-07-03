using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class ClientController : IClientController
{
    private readonly ZoneModel _zoneModel;

    public string serverIP = "172.33.133.57";
    public int serverPort = 1997;

    private TcpClient _client;
    private NetworkStream _stream;
    private Thread _clientReceiveThread;

    public ClientController(ZoneModel zoneModel)
    {
        _zoneModel = zoneModel;
    }

    public void StartConnect()
    {
        ConnectToServer();
    }
    public void SendMessageToServer(string message)
    {
        if (_client == null || !_client.Connected)
        {
            Debug.LogError("Client not connected to server.");
            return;
        }

        byte[] data = Encoding.UTF8.GetBytes(message);
        _stream.Write(data, 0, data.Length);
    }

    public void CloseConnect()
    {
        if (_stream != null)
            _stream.Close();
        if (_client != null)
            _client.Close();
        if (_clientReceiveThread != null)
            _clientReceiveThread.Abort();
    }

    private void ConnectToServer()
    {
        try
        {
            _client = new TcpClient(serverIP, serverPort);
            _stream = _client.GetStream();
            _clientReceiveThread = new Thread(new ThreadStart(ListenForData))
            {
                IsBackground = true
            };
            _clientReceiveThread.Start();
        }
        catch (SocketException e)
        {
            Debug.LogError("SocketException: " + e.ToString());
        }
    }

    private void ListenForData()
    {
        try
        {
            byte[] bytes = new byte[1024];
            while (true)
            {
                if (_stream.DataAvailable)
                {
                    int length;
                    while ((length = _stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        var incomingData = new byte[length];
                        Array.Copy(bytes, 0, incomingData, 0, length);
                        string serverMessage = Encoding.UTF8.GetString(incomingData);
                        var model = Serializer.DataModelFromJson(serverMessage);
                        UpdateData(model);
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }

    private void UpdateData(DataModel model)
    {
        _zoneModel.zoneFirst = model.zoneFirst;
        for (int i = 0; i < _zoneModel.zoneThirdCubes.Count; i++)
        {
            _zoneModel.zoneThirdCubes[i].transform.position = model.zoneThirdCubes[i];
        }
    }
}
