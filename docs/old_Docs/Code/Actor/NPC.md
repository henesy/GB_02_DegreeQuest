# public class NPC : Actor
<!--'></!-->

Any game character that is not controlled by a player.

## Fields

| Type | Name | Description |
|---|---|---|
| public Texture2D | Texture | visual texture |
| vector2 | Postion | (x,y) position relative to upper left corner |
| uint | HP | current HitPoints |
| uint | HPMax | maximum HitPoints |
| uint | EP | current EnergyPoints |
| uint | EPMax | maximum EnergyPoints |
| int| subject| |
| string | Name | |

## Constructors

| Modifier | Constructor | Description |
|---|---|---|
|public|NPC()| default constructor with base values|
|public| NPC(NPCTemplate temp)| constructor based on a template| 

## Methods

