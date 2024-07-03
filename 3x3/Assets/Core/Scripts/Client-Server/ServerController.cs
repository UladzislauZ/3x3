using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class ServerController : IServerController
{
    private readonly ZoneModel _zoneModel;

    private TcpListener _server = null;
    private TcpClient _client = null;
    private NetworkStream _stream = null;
    private Thread _thread;

    public ServerController(ZoneModel zoneModel)
    {
        _zoneModel = zoneModel;
    }

    public void InitServer()
    {
        _thread = new Thread(new ThreadStart(SetupServer));
        _thread.Start();
    }
    public void CloseServer()
    {
        _stream.Close();
        _client.Close();
        _server.Stop();
        _thread.Abort();
    }

    public void SendMessageToClient(string message)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(message);
        _stream.Write(buffer, 0, buffer.Length);
    }

    private void SetupServer()
    {
        try
        {
            IPAddress localAddr = IPAddress.Parse("172.33.133.57");
            _server = new TcpListener(localAddr, 1997);
            _server.Start();

            byte[] buffer = new byte[1024];

            while (true)
            {
                int i;
                _client = _server.AcceptTcpClient();
                _stream = _client.GetStream();
                while ((i = _stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    string data = Encoding.UTF8.GetString(buffer, 0, i);
                    var dataModel = Serializer.DataModelFromJson(data);
                    RegisterClientMessage(dataModel);
                    var zoneThirdCubes = _zoneModel.zoneThirdCubes.Select(x => x.transform.position).ToList();
                    SendMessageToClient(Serializer.DataModelToJson(new DataModel(_zoneModel.zoneFirst, zoneThirdCubes)));
                }

                _client.Close();
            }
        }
        catch (SocketException e)
        {
            Debug.Log("SocketException: " + e);
        }
        finally
        {
            _server.Stop();
        }
    }

    private void RegisterClientMessage(DataModel dataModel)
    {
        //todo set client data in PlayerController
    }
}
