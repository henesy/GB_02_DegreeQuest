# public class DBReader

Class of static meathods that comunitcates with mySQL database

## Fields

| Modifier | Type | Name | Description |
| --- | --- | --- | --- |
| static readonly | String | myConnectionString | contains the String of the mySQL server name, id, password, and database that conn is connected to|
| static readonly | MySQLconnection | conn | the representation of the connection to the mySQL database

## Methods

| Return type | Name | Description |
| --- | --- | --- |
| void | queryAll(String tableName) | query the entire table by name and print it to console |
| void | deleteRecordUsingKey(String tableName, String columnName, String key) | takes in the name of a table, the name of column that contains the unique key, and a unique key and deletes that record |
| void | insertRecord(string tableName, String[] columnNames, String[] values) | takes in the table name and the name of the columns that the value go into and the values and creates a new record in the table |
|void | updateRow(String tableName, String[] columnNames, String[] values, String key) | Finds a record by an unique key and updates the values in that record |
| String | selectQuery(String tableName, String[] columnNames, String columnConditionID, String condition) | query the table by the selected column value and returns the string representation of those records | 
