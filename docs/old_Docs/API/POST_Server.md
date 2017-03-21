# POST Server (`:13338`)

The POST server operates on the port `:13338` on the host server.

Things of note: 

* cc: Client character, exists on the server-side
* pc: Client character, exists on the client-side

Communications are performed in the following order...

---

Client: Writes "OPEN " + pc.Name

Server: Reads from client and creates/initialises cc

Server: Writes cc.Position

Client: Reads cc.Position and sets the position for pc, respectively


> Server could, but does not, respond past this point, could replace Spectator server in theory


_Main loop begins ;; Preamble ends_

Client writes: 

* If the player inputs a directional key/command: "MOVE " + N & E & S & W
* _Other commands to be added_

Server reads: 

_Decides if communication contains the following and then performs an action..._


* "MOVE": Adjusts the cc any combination of North, East, South, or West as per the concatenated directions provided by the client
* _Other commands to be added_


