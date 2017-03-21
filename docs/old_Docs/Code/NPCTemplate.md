# public class NPCTemplate
<!--'></!-->

A template for an NPC character. Allows generated NPCs with slight variance.

## Fields

| Type | Name | Description |
|---|---|---|
| public static readonly String | NPC_FILE | pathname for the txt file for Templates |
| public static String[] | columns | names of the item columns in the database|
|public String| name |  |
| public Dice | HP | dice to generate HP |
| public Dice | EP | dice to generate EP |
| public BitVector32 | subjects| possible subjects for this NPC|
| Texture2D | texture | maximum EnergyPoints |
| uint | Debt | current Debt|

## Constructors

| Modifier | Constructor | Description |
|---|---|---|
|public|NPCTemplate(String name, Dice HP, Dice EP,BitVector32 subjects)| manual constructor|
|public NPCTemplate(String name, Dice HP, Dice EP, int subjects)|manual with subjects taken in as int|

## Methods

| Type | Method | Description |
|---|---|---|
|public NPC |create()| creates a new NPC off this template|
|public static void| update()|updates the database from the txt file|

