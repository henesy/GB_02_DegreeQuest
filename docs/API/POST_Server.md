# POST Server (`:13338`)

The POST server operates on the port `:13338` on the host server.

Things of note: 

* cc: Client character, exists on the server-side
* pc: Client character, exists on the client-side

All that presently occurs (`161f06eb`) is that the client serializes the PC to a binary stream that is then written and read from the server.

After being read, the player character streamed to the server can be summarily processed server-side.

For the processing there exists a PC string element "LastAction" which can be used for the aforementioned processing to pass client-side action requests or information to the server, this may be expanded in the future.

Although presently (`161f06eb`) disabled, a system in which then serializes a modified PC back to the client for counter-processing.

