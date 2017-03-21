# Spectator Server (`:13337`)

The spectator server operates on the port `:13337` by default on the hosting server. 

Communications are transmitted as follows...

---

Server: Writes Room.num + @ + Room.members[0] + @ + Room.members[_n_]...

Client: Reads and splits string on "@" setting Room.num to str[0] and populating _n_ Actors in Room.members to reflect server screen


_Note_: There is presently (`9b36d9fb`) no syntax to distinguish between sprites that should be printed, this is a planned feature.

