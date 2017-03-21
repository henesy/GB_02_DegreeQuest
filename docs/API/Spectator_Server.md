# Spectator Server (`:13337`)

The spectator server operates on the port `:13337` by default on the hosting server. 

Communications are transmitted as follows...

---

Server: Writes Room.num + @ + Room.members[0] + @ + Room.members[_n_]...

Client: Reads and splits string on "@" and then further upon "#".

"@" split yields an index of Sprites to render with Room.num set to the length of the index-1. 

"#" sub-split yields two fields for a given Sprite with sub[0] being Position and sub[1] being Texture (as per Actor).


