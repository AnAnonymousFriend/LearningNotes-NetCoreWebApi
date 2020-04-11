using System.IO;

namespace API.Core.Repository.sugar
{
    public class BaseDBConfig
    {
        public static string ConnectionString = File.ReadAllText(@"D:\my-file\dbCountPsw2.txt").Trim();

        //正常格式是

        //public static string ConnectionString = ""; 


    }
}
