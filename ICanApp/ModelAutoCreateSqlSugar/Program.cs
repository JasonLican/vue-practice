using SqlSugar;
using System;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var db = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString= "Server=localhost;Database=ICanDb;Trusted_Connection=True;",
                DbType=DbType.SqlServer,
                IsAutoCloseConnection=true,
                InitKeyType=InitKeyType.Attribute,
            });
            db.DbFirst.CreateClassFile("modules");
        }
    }
}