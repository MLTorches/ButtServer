
# Butt Server ![Smiling Peach](https://camo.githubusercontent.com/82d932c73232f2fa5afaad48b74c5c16d659ba1a138b56e6965777356370c025/68747470733a2f2f6d6c746f72636865732e6769746875622e696f2f4261736963427574744d616e616765722f7265736f75726365732f66617669636f6e32342e706e67)

Lightweight server for controlling sex toys via plain websockets.

The purpose of this project is to remove the client's dependency upon certain framework-dependent libraries (*ahem* Json.NET *ahem*) that can cause issues if you just reference BasicButtManager (or Buttplug itself for that matter) directly from within a Unity game built on an older .NET framework.

And of course there's also the issue of integrating Buttplug with games and applications written in other languages/frameworks entirely independent of C#/.NET or one of the supported ports...

With this middleman application, it doesn't matter what language/framework you are using and from where, ***as long as you are capable of sending plain strings to a local port, you can control all your sex toys***. C#, C, C++, Java, Python, Rust, JS, etc. whatever just send a message to localhost:42069.

That's it, no other dependencies required.

![Sample Server](https://github.com/MLTorches/ButtServer/blob/master/resources/server.PNG?raw=true)

## Usage

To set all your machines to half intensity:

1. Launch [Intiface :copyright: Central](https://intiface.com/central/) and connect your toys.
2. Launch [ButtServer](https://github.com/MLTorches/ButtServer/releases/latest).
3. Somewhere in your code...

```
IPHostEntry ipHostInfo = Dns.GetHostEntryAsync("localhost").Result;
IPAddress ipAddress = ipHostInfo.AddressList[0];
IPEndPoint ipEndPt = new IPEndPoint(ipAddress, 42069);

Socket socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
socket.Connect(ipEndPt);
Send("Set 0.5");
Send("Disconnect");
```

## Butt Client
If you are thinking, "wow much code" - you would be correct!

Instead of writing all of this yourself (and more), you should probably use the accompanying development package ButtClient instead, available on NuGet.

The code above would then simplify to:
```
ButtClient client = new ButtClient("BuzzRhythm");
client.Set(0.5f);
client.Disconnect();
```

Then you can focus on your game logic instead of these lower level interfaces.<br/>
For more information, check out the GitHub page for ButtClient!

## Download
Click [here](https://github.com/MLTorches/ButtServer/releases/latest) to download the application!

## Credits
Buttplug: [qdot@github.com](https://github.com/qdot) | Favicon: [freepik@flaticon.com](https://www.flaticon.com/authors/frdmn)

## License

This project is BSD 3-Clause licensed.

```text
Copyright (c) 2023, MLTorches
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this
  list of conditions and the following disclaimer.

* Redistributions in binary form must reproduce the above copyright notice,
  this list of conditions and the following disclaimer in the documentation
  and/or other materials provided with the distribution.

* Neither the name of the copyright holder nor the names of its
  contributors may be used to endorse or promote products derived from
  this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
```