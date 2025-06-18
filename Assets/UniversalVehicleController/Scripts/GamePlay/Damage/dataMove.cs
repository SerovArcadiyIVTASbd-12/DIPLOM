using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static DatabaseHandler;
using SQLite4Unity3d;
using System.Linq;

public class dataMove : MonoBehaviour
{
    static string GetDatabasePath()
    {
        string dbName = "MyDatabase.db";
#if UNITY_EDITOR
        return Path.Combine(Directory.GetParent(Application.dataPath).FullName, dbName);
#else
        string root = Directory.GetParent(Application.dataPath).FullName;
        string dataFolder = Path.Combine(root, "Data");
        if (!Directory.Exists(dataFolder))
            Directory.CreateDirectory(dataFolder);
        return Path.Combine(dataFolder, dbName);
#endif
    }

    public List<FullInformationKGDamage> GetAllFullInformationKGDamage()
    {
        string dbPath = GetDatabasePath();
        using (var connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create))
        {
            connection.CreateTable<FullInformationKGDamage>(); // На всякий случай
            return connection.Table<FullInformationKGDamage>().ToList();
        }
    }
}