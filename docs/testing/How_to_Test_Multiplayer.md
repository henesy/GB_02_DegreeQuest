# How to Test Multiplayer

* Within Visual Studio: Build -> Build Solution
* Upon completion of build, `cd` to the root project directory (The one with the `.sln` file)
* Copy the `DegreeQuest` folder tree to an arbitrary external directory (I use a `tmp` folder in my home folder)
* Set the proper role for the instance within `config.txt` (at least one instance must be `server=true` and `client=false` as of `9d95e689` and run first as a result)
* `cd` within the `tmp` instance to `DegreeQuest/bin/DesktopGL/x86/Debug`
* Run `DegreeQuest.exe`

The client should connect automatically and defaults to `127.0.0.1` on ports `:13337` and `:13338` for the Spectator and POST servers, respectively (as of `9d95e689` there is no way to change this default).

The server automatically serves both aforementioned ports by default and should be allowed through the system's firewall, if any, as a result.

As of `9d95e689` the client and server will both crash if the other disconnects whatsoever.

