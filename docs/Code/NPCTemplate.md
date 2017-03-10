# public class PC : Actor
<!--'></!-->

Class coresponding to player characters.

## Fields

| Type | Name | Description |
|---|---|---|
| Texture2D | PlayerTexture | Player's visual Sprite |
| Vector2 | Postions | (x,y) position relative to upper left corner |
| bool  | Active  | current state |
| uint | HP | current HitPoints |
| uint | HPMax | maximum HitPoints |
| uint | EP | current EnergyPoints |
| uint | EPMax | maximum EnergyPoints |
| uint | Debt | current Debt|
| uint | DebtTotal | total Debt|
| string | Name | |
| uint[] | Stats | array of stats for each subject|

## Constructors

| Modifier | Constructor | Description |
|---|---|---|
|public|PC()| default constructor with base values|
 
## Methods

| Type | Method | Description |
|---|---|---|
|public int |Width| width of the texture|
|public int |Height| height of the texture|
|public void| Initialize(Texture2D texture, Vector2 postion)| initializes a player in a given position|

