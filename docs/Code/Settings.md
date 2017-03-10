# public class Settings
<!--'></!-->

Class manages the settings in regard to if it is a client and the resolution.

## Fields

| Type | Name | Description |
|---|---|---|
| boolean | isClient | if it is a client |
| uint | res_x | holds the resolution in the horizontal direction |
| uint | res_y | holds the resolution in the vertical direction |

## Constructor

| Modifier | Constructor |Description |
|---|---|
| public | Settings() | default constructor that reads the file |

## Methods

| Type | Method | Decription |
|---|---|
| bool | getIsClient() | returns if it this is a client |
| uint | getResX() | returns the horizontal resolution |
| uint | getResY() | returns the vertical resolution |
| void | setResX(uint res) | sets the horizontal resolution |
| void | setResY(uint res) | sets the veritcal resolution |

