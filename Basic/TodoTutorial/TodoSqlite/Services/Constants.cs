using SQLite;

namespace TodoSqlite.Services
{
    public class Constants
    {
        public const string DatabaseFileName = "TodoList.db3";

        public const SQLiteOpenFlags Flags =
            SQLiteOpenFlags.ReadWrite |     // 읽기/쓰기모드로 DB오픈
            SQLiteOpenFlags.Create |        // DB가 없으면 생성할 것
            SQLiteOpenFlags.SharedCache;    // 멀티스레드 DB 접근 가능

        public static string DatabasePath
        {
            get
            {
                var basePath = 
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFileName);
            }
        }

    }
}
