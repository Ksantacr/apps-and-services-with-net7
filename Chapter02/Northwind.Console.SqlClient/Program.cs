using Dapper;
using Microsoft.Data.SqlClient; // SqlConnection and so on
using System.Data; // CommandType
SqlConnectionStringBuilder builder = new();
builder.InitialCatalog = "Northwind";
builder.MultipleActiveResultSets = true;
builder.Encrypt = true;
builder.TrustServerCertificate = true;
builder.ConnectTimeout = 10;
WriteLine("Connect to:");
WriteLine(" 1 - SQL Server on local machine");
WriteLine(" 2 - Azure SQL Database");
WriteLine(" 3 – Azure SQL Edge");
WriteLine();
Write("Press a key: ");
ConsoleKey key = ReadKey().Key;
WriteLine(); WriteLine();
if (key is ConsoleKey.D1 or ConsoleKey.NumPad1)
{
    builder.DataSource = "(localdb)\\MSSQLLocalDB"; // Local SQL Server
    // @".\net7book"; // Local SQL Server with an instance name
}
else if (key is ConsoleKey.D2 or ConsoleKey.NumPad2)
{
    builder.DataSource = // Azure SQL Database
    "tcp:apps-services-net7.database.windows.net,1433";
}
else if (key is ConsoleKey.D3 or ConsoleKey.NumPad3)
{
    builder.DataSource = "tcp:127.0.0.1,1433"; // Azure SQL Edge
}
else
{
    WriteLine("No data source selected.");
    return;
}

WriteLine("Authenticate using:");
WriteLine(" 1 – Windows Integrated Security");
WriteLine(" 2 – SQL Login, for example, sa");
WriteLine();
Write("Press a key: ");
key = ReadKey().Key;
WriteLine(); WriteLine();

if (key is ConsoleKey.D1 or ConsoleKey.NumPad1)
{
    builder.IntegratedSecurity = true;
}
else if (key is ConsoleKey.D2 or ConsoleKey.NumPad2)
{
    builder.UserID = "sa"; // Azure SQL Edge
                           // "markjprice"; // change to your username
    Write("Enter your SQL Server password: ");
    string? password = ReadLine();
    if (string.IsNullOrWhiteSpace(password))
    {
        WriteLine("Password cannot be empty or null.");
        return;
    }
    builder.Password = password;
    builder.PersistSecurityInfo = false;
}
else
{
    WriteLine("No authentication selected.");
    return;
}

SqlConnection connection = new(builder.ConnectionString);
WriteLine(connection.ConnectionString);
WriteLine();
connection.StateChange += Connection_StateChange;
connection.InfoMessage += Connection_InfoMessage;

try
{
    WriteLine("Opening connection. Please wait up to {0} seconds...",
    builder.ConnectTimeout);
    WriteLine();
    await connection.OpenAsync();
    WriteLine($"SQL Server version: {connection.ServerVersion}");
    connection.StatisticsEnabled = true;
}
catch (SqlException ex)
{
    WriteLine($"SQL exception: {ex.Message}");
    return;
}

Write("Enter a unit price: ");
string? priceText = ReadLine();
if (!decimal.TryParse(priceText, out decimal price))
{
    WriteLine("You must enter a valid unit price.");
    return;
}

SqlCommand cmd = connection.CreateCommand();

WriteLine("Execute command using:");
WriteLine(" 1 - Text");
WriteLine(" 2 - Stored Procedure");
WriteLine();
Write("Press a key: ");
key = ReadKey().Key;
WriteLine(); WriteLine();
SqlParameter p1, p2 = new(), p3 = new();
if (key is ConsoleKey.D1 or ConsoleKey.NumPad1)
{

    cmd.CommandType = CommandType.Text;
    cmd.CommandText = "SELECT ProductId, ProductName, UnitPrice FROM Products"
        + " WHERE UnitPrice > @price";
    cmd.Parameters.AddWithValue("price", price);
}
else if (key is ConsoleKey.D2 or ConsoleKey.NumPad2)
{
    cmd.CommandType = CommandType.StoredProcedure;
    cmd.CommandText = "GetExpensiveProducts";
    p1 = new()
    {
        ParameterName = "price",
        SqlDbType = SqlDbType.Money,
        SqlValue = price
    };
    p2 = new()
    {
        Direction = ParameterDirection.Output,
        ParameterName = "count",
        SqlDbType = SqlDbType.Int
    };
    p3 = new()
    {
        Direction = ParameterDirection.ReturnValue,
        ParameterName = "rv",
        SqlDbType = SqlDbType.Int
    };
    cmd.Parameters.Add(p1);
    cmd.Parameters.Add(p2);
    cmd.Parameters.Add(p3);
}
SqlDataReader r = await cmd.ExecuteReaderAsync();

WriteLine("----------------------------------------------------------");
WriteLine("| {0,5} | {1,-35} | {2,8} |", "Id", "Name", "Price");
WriteLine("----------------------------------------------------------");

while (await r.ReadAsync())
{
    WriteLine("| {0,5} | {1,-35} | {2,8:C} |",
    r.GetInt32("ProductId"),
    r.GetString("ProductName"),
    r.GetDecimal("UnitPrice"));
}

WriteLine("----------------------------------------------------------");

WriteLine("Using Dapper");

IEnumerable<Supplier> suppliers = connection.Query<Supplier>(
 sql: "SELECT * FROM Suppliers WHERE Country=@Country",
 param: new { Country = "Germany" });
foreach (Supplier supplier in suppliers)
{
    WriteLine("{0}: {1}, {2}, {3}",
        supplier.SupplierId, supplier.CompanyName,
        supplier.City, supplier.Country);
}

await r.CloseAsync();
WriteLine($"Output count: {p2.Value}");
WriteLine($"Return value: {p3.Value}");
await connection.CloseAsync();

/*
 1. Which NuGet package should you reference in a .NET project to get the best performance 
when working with data in SQL Server?
Microsoft.Data.SqlClient (ADO.NET)

2. What is the safest way to define a database connection string for SQL Server?
Out of source code. You should not hardcode security information in the connection string

3. What must T-SQL parameters and variables be prefixed with?
with at (@)
DECLARE @Number Int;
SET @Number = 3;

4. What must you do before reading an output parameter of a command executed using 
ExecuteReader?*
Create SqlCommand and add the SQL
Initialize the property

5. What can the dotnet-ef tool be used for?
To scaffold our db structure using Code first or Db first.

NET has a command-line tool named dotnet. It can be extended with capabilities useful for working 
with EF Core. It can perform design-time tasks like creating and applying migrations from an older 
model to a newer model and generating code for a model from an existing database.

6. What type would you use for the property that represents a table, for example, the Products
property of a data context?
DbSet

7. What type would you use for the property that represents a one-to-many relationship, for 
example, the Products property of a Category entity?
ICollection

8. What is the EF Core convention for primary keys?
Use the Id/ID keyword or the Prefix of the class + Id
Also we can use [Key]

The primary key is assumed to be a property that is named Id or ID, or when the entity model 
class is named Product, then the property can be named ProductId or ProductID. If this property is of an integer type or the Guid type, then it is also assumed to be an IDENTITY column 
(a column type that automatically assigns a value when inserting).

9. Why might you choose the Fluent API in preference to annotation attributes?
Is better to populate data

Another benefit of the Fluent API is to provide initial data to populate a database. EF Core automatically 
works out what insert, update, or delete operations must be executed.

10. Why might you implement the IMaterializationInterceptor interface in an entity type?
EF Core 7 adds an IMaterializationInterceptor interface that allows interception before and after 
an entity is created, and when properties are initialized. This is useful for calculated values. 

 */

/*
 Exercise 2.2 – Practice benchmarking ADO.NET against EF 
Core
In the Chapter02 solution/workspace, create a console app named Ch02Ex02_ADONETvsEFCore that 
uses Benchmark.NET to compare retrieving all the products from the Northwind database using 
ADO.NET (SqlClient) and using EF Core.
 */

/*
 Exercise 2.3 – Explore topics
Use the links on the following page to learn more details about the topics covered in this chapter:
https://github.com/markjprice/apps-services-net7/blob/main/book-links.md#chapter-2---managing-relational-data-using-sql-server
 */