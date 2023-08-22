using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ButtServer
{
    /// <summary>
    /// Lightweight server providing the features of BasicButtManager via plain websockets.<br/><br/>
    /// </summary>
    /// <remarks>No 3rd-party framework dependency by design for high stability.</remarks>
    public class ButtServer
    {
        private static readonly BasicButtManager.BasicButtManager manager = new BasicButtManager.BasicButtManager("ButtServer");

        /// <summary>
        /// Server loop. Automatically started as the executable's entry point.<br/><br/>
        /// 
        /// Listen for all incoming messages on local port 42069, parse them, and
        /// pass them to Intiface Central via a local instance of BasicButtManager.
        /// 
        /// </summary>
        static void Main()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntryAsync("localhost").Result;
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 42069);

            Console.Title = "MLTorches's Butt Server";
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetWindowSize(80, 20);
            Console.SetBufferSize(80, 20);

            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("                               MLT's Butt Server                               ");
            Console.WriteLine("                                    v.1.0.0                                    ");
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine($"Server is active. Listening to requests on {ipEndPoint} . . .");
            Console.WriteLine("Press any key to stop.\n");

            try
            {
                Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                Socket handler = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                listener.Bind(ipEndPoint);
                listener.Listen(16);

                _= listener.AcceptAsync(handler).ContinueWith(_ => {
                    while (true)
                    {
                        byte[] buffer = new byte[1024];
                        int count = handler.Receive(buffer);
                        String payload = Encoding.ASCII.GetString(buffer, 0, count);
                        Process(payload);
                    }
                });

                Console.ReadKey(true);
                Console.WriteLine();
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch (Exception){}
        }

        /// <summary>
        /// Process the payload from ButtClient and call the appropriate BasicButtManager interface.
        /// </summary>
        /// <param name="payload">The raw payload received from the client.</param>
        /// <remarks>Sample valid messages (excluding quotations): <br/><br/>
        /// "ClientName FadeIn" <br/>
        /// "ClientName Set 0.5" <br/>
        /// "ClientName Control 0.5 0.5 false"<br/><br/>
        /// 
        /// Developers should consider using the pre-developed wrapper ButtClient package to communicate with this server,<br/>
        /// as the messages have already been formatted to meet the specification there.
        /// </remarks>
        private static void Process(String payload)
        {
            try
            {
                if (payload.Length == 0) return;

                String[] tokens = payload.Split(' ');
                String client = tokens[0];
                String timestamp = DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss.fff");

                Console.Write($"[{timestamp}] <{client}>: {payload.Substring(tokens[0].Length + 1)}");

                switch (tokens[1])
                {
                    case "Set":
                        manager.Set(float.Parse(tokens[2]));
                        break;

                    case "Pulse":
                        manager.Pulse(float.Parse(tokens[2]));
                        break;

                    case "Press":
                        manager.Press(float.Parse(tokens[2]));
                        break;

                    case "Hold":
                        manager.Press(float.Parse(tokens[2]));
                        break;

                    case "Fade":
                        manager.Fade(float.Parse(tokens[2]), float.Parse(tokens[3]));
                        break;

                    case "Control":
                        manager.Control(float.Parse(tokens[2]), float.Parse(tokens[3]), bool.Parse(tokens[4]));
                        break;

                    case "FadeIn":
                        manager.FadeIn();
                        break;

                    case "FadeOut":
                        manager.FadeOut();
                        break;

                    case "Stop":
                        manager.Stop();
                        break;

                    case "Exit":
                        manager.Exit();
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex}\n\n[INTERNAL ERROR PROCESSING COMMAND \"{payload}\"] Please submit a GitHub issue, thank you.");
            }
        }
    }
}
