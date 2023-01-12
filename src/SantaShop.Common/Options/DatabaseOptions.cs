using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace SantaShop.Common.Options;

public class DatabaseOptions
{
    /// <summary>
    ///     Mssql Server address
    /// </summary>
    public string Mssql_Server { get; set; } = "localhost"; 
    
    /// <summary>
    ///     Mssql Server port
    /// </summary>
    public string Mssql_Port { get; set; } = "1433"; 
    
    /// <summary>
    ///     Mssql database
    /// </summary>
    public string Mssql_Database { get; set; } = "GiftShop"; 
    
    /// <summary>
    ///     Mssql User
    /// </summary>
    public string Mssql_User { get; set; } = "sa"; 
    
    /// <summary>
    ///     Mssql password
    /// </summary>
    public string Mssql_Password { get; set; } = "MyStrong!Password";


    public string BuildConnectionString()
    {
        var builder = new SqlConnectionStringBuilder();
        builder["Server"] = string.IsNullOrEmpty(Mssql_Port)? Mssql_Server:$"{Mssql_Server},{Mssql_Port}";
        builder["Database"] = Mssql_Database;
        builder.Encrypt = false;
        builder.UserID = Mssql_User;
        builder.Password = Mssql_Password;

        return builder.ConnectionString;

    }

}