using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
#if NETFX_CORE
using System.Threading.Tasks;
using Windows.Networking.Sockets;
using Windows.Networking;
using Windows.Storage.Streams;
using Windows.Web.Http;
using Windows.Web.Http.Headers;
using Windows.Web.Http.Filters;
using System.IO;
using Windows.Security.Credentials;
using System.Runtime.InteropServices.WindowsRuntime;
#else

using System.Net.Sockets;
using System.Text;
#endif




public class TCPServer : MonoBehaviour
{

#if NETFX_CORE
	static string PortNumber = "1337";
	Windows.Networking.HostName HostAddress;
#endif

	public String message;
	
	// Use this for initialization
	void Start()
	{
		string message = "Hello :)";

#if NETFX_CORE
		HostAddress = new Windows.Networking.HostName("192.168.43.41");
		SendMessage(message);
	#endif

	}

#if NETFX_CORE
	public async void SendMessage(string message)
	{
		Windows.Networking.Sockets.StreamSocket streamSocket = null;
		streamSocket = new Windows.Networking.Sockets.StreamSocket();
		try
		{
			Debug.Log("Trying to connect");
			await streamSocket.ConnectAsync(HostAddress, "1337");
			Debug.Log("Connected!");
			Stream streamOut = streamSocket.OutputStream.AsStreamForWrite();
			StreamWriter writer = new StreamWriter(streamOut);
			await writer.WriteLineAsync(message);
			Debug.Log("The following message was sent: " + message);
			await writer.FlushAsync();

			//creat input stream and read it into a string (for responses from RPI3)
			/*Stream streamIn = streamSocket.InputStream.AsStreamForRead();
			StreamReader reader = new StreamReader(streamIn);
			string response = await reader.ReadLineAsync();*/

		}
		catch (Exception e)
		{
			//catch an error messages and display
			Debug.Log(e.Message);
		}
		finally
		{
			//end by disposing the socket and clearing the resources
			streamSocket.Dispose();
		}

	}
#endif
}