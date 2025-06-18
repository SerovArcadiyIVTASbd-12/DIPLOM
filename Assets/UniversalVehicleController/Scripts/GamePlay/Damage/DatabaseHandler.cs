
using UnityEngine;
using System.IO;
using SQLite4Unity3d;
using System.Collections.Generic;

public class DatabaseHandler : MonoBehaviour
{
    [Table("SimplyInformationDamage")]
    public class SimplyInformationDamage
    {
        [PrimaryKey, AutoIncrement]
        public int HitId { get; set; }
        public string HitSide { get; set; }
    }

    [Table("FullInformationLevelDamage")]
    public class FullInformationLevelDamage
    {
        [PrimaryKey, AutoIncrement]
        public int HitId { get; set; }

        public string HitSide { get; set; }
        public float HitForce { get; set; }
        public float CarSpeed { get; set; }

        public int head { get; set; }
        public int neck { get; set; }
        public int chest { get; set; }
        public int brushL { get; set; }
        public int brushR { get; set; }
        public int kneeL { get; set; }
        public int kneeR { get; set; }
        public int loin { get; set; }
        public int pelvis { get; set; }
        public int shoulderL { get; set; }
        public int shoulderR { get; set; }
        public int elbowL { get; set; }
        public int elbowR { get; set; }
        public int hipL { get; set; }
        public int hipR { get; set; }
        public int shinL { get; set; }
        public int shinR { get; set; }
    }

    [Table("FullInformationKGDamage")]
    public class FullInformationKGDamage
    {
        [PrimaryKey, AutoIncrement]
        public int HitId { get; set; }

        public string HitSide { get; set; }
        public float HitForce { get; set; }
        public float CarSpeed { get; set; }

        public float headKG { get; set; }
        public float neckKG { get; set; }
        public float chestKG { get; set; }
        public float brushLKG { get; set; }
        public float brushRKG { get; set; }
        public float kneeLKG { get; set; }
        public float kneeRKG { get; set; }
        public float loinKG { get; set; }
        public float pelvisKG { get; set; }
        public float shoulderLKG { get; set; }
        public float shoulderRKG { get; set; }
        public float elbowLKG { get; set; }
        public float elbowRKG { get; set; }
        public float hipLKG { get; set; }
        public float hipRKG { get; set; }
        public float shinLKG { get; set; }
        public float shinRKG { get; set; }
    }

    private SQLiteConnection _connection;

    public void InitDatabase(string damageSide, float forceDamage, float speed, List<int> levelDamagePartBody, List<float> levelDamagePartBodyKG)
    {
        string dbName = "MyDatabase.db";
        string dbPath;

#if UNITY_EDITOR
        // В редакторе — база будет создаваться рядом с проектом
        dbPath = Path.Combine(Directory.GetParent(Application.dataPath).FullName, dbName);
#else
        // В билде (exe) — база будет создаваться в отдельной папке рядом с exe
        string root = Directory.GetParent(Application.dataPath).FullName;
        string dataFolder = Path.Combine(root, "Data");
        if (!Directory.Exists(dataFolder))
            Directory.CreateDirectory(dataFolder);

        dbPath = Path.Combine(dataFolder, dbName);
#endif

        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

        _connection.CreateTable<SimplyInformationDamage>();
        _connection.CreateTable<FullInformationLevelDamage>();
        _connection.CreateTable<FullInformationKGDamage>();

        _connection.Insert(new SimplyInformationDamage
        {
            HitSide = damageSide
        });

        _connection.Insert(new FullInformationLevelDamage
        {
            HitSide = damageSide,
            HitForce = forceDamage,
            CarSpeed = speed,
            head = levelDamagePartBody[0],
            neck = levelDamagePartBody[1],
            chest = levelDamagePartBody[2],
            brushL = levelDamagePartBody[3],
            brushR = levelDamagePartBody[4],
            kneeL = levelDamagePartBody[5],
            kneeR = levelDamagePartBody[6],
            loin = levelDamagePartBody[7],
            pelvis = levelDamagePartBody[8],
            shoulderL = levelDamagePartBody[9],
            shoulderR = levelDamagePartBody[10],
            elbowL = levelDamagePartBody[11],
            elbowR = levelDamagePartBody[12],
            hipL = levelDamagePartBody[13],
            hipR = levelDamagePartBody[14],
            shinL = levelDamagePartBody[15],
            shinR = levelDamagePartBody[16]
        });

        _connection.Insert(new FullInformationKGDamage
        {
            HitSide = damageSide,
            HitForce = forceDamage,
            CarSpeed = speed,
            headKG = levelDamagePartBodyKG[0],
            neckKG = levelDamagePartBodyKG[1],
            chestKG = levelDamagePartBodyKG[2],
            brushLKG = levelDamagePartBodyKG[3],
            brushRKG = levelDamagePartBodyKG[4],
            kneeLKG = levelDamagePartBodyKG[5],
            kneeRKG = levelDamagePartBodyKG[6],
            loinKG = levelDamagePartBodyKG[7],
            pelvisKG = levelDamagePartBodyKG[8],
            shoulderLKG = levelDamagePartBodyKG[9],
            shoulderRKG = levelDamagePartBodyKG[10],
            elbowLKG = levelDamagePartBodyKG[11],
            elbowRKG = levelDamagePartBodyKG[12],
            hipLKG = levelDamagePartBodyKG[13],
            hipRKG = levelDamagePartBodyKG[14],
            shinLKG = levelDamagePartBodyKG[15],
            shinRKG = levelDamagePartBodyKG[16]
        });

        Debug.Log("База данных инициализирована и заполнена. Ссылка - " + dbPath);
    }

    public void ClearDatabase()
    {
        if (_connection != null)
        {
            _connection.DeleteAll<SimplyInformationDamage>();
            _connection.DeleteAll<FullInformationLevelDamage>();
            _connection.DeleteAll<FullInformationKGDamage>();
            Debug.Log("Все данные из таблиц удалены.");
        }
        else
        {
            Debug.LogWarning("База данных не инициализирована.");
        }
    }
}