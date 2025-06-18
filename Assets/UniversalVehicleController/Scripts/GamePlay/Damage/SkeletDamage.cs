using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class SkeletDamage : MonoBehaviour
{

    public static NameListRenderer nameListRender;
    public static dataMove datamove;
     

    public static List<List<int>> criticalDamage = new List<List<int>>
    {
        new List<int> { 50, 150, 250, 300},
        new List<int> { 40, 100, 150, 230},
        new List<int> { 100, 200, 350, 500},
        new List<int> { 100, 200, 300, 400},
        new List<int> { 100, 200, 300, 400},
        new List<int> { 150, 300, 450, 600},
        new List<int> { 150, 300, 450, 600},
        new List<int> { 80, 180, 280, 400},
        new List<int> { 120, 250, 400, 550},
        new List<int> { 100, 200, 350, 500},
        new List<int> { 100, 200, 350, 500},
        new List<int> { 100, 200, 350, 500},
        new List<int> { 100, 200, 350, 500},
        new List<int> { 200, 350, 500, 650},
        new List<int> { 200, 350, 500, 650},
        new List<int> { 100, 200, 300, 400},
        new List<int> { 100, 200, 300, 400}
    };
    //Текст травмы Голова, шея, грудь, рука, нога
    public static List<List<string>> messageDamage = new List<List<string>>
    {
        new List<string> { "лёгкое сотрясение", "сотрясение, головокружение, тошнота", 
            "сотрясение, вероятность потери сознания", "тяжёлое сотрясение мозга, внутренние травмы", "перегрузка минимальна для части тела" },
        new List<string> { "растяжение (лёгкое)", "растяжение и мелкие травмы", 
            "тяжёлые травмы, надлолмы, возможны переломы", "тяжёлые травмы, переломы гарантированны", "перегрузка минимальна для части тела" },
        new List<string> { "мягкие ушибы грудной клетки, синяки", "глубокие ушибы, возможны микротрещены, растяжение хрящей",
            "трещены в паре рёбер, резкая боль", "переломы рёбер, повреждение дыхательной системы", "перегрузка минимальна для части тела" },
        new List<string> { "ушибы, гематомы, лёгкое растяжение", "серьёзные ушибы, возможен надрыв связок", 
            "мелкие трещины в кости, частичные переломы", "полный перелом, деформация руки", "перегрузка минимальна для части тела" },
        new List<string> { "ушибы, гематомы, лёгкое растяжение", "серьёзные ушибы, возможен надрыв связок",
            "мелкие трещины в кости, частичные переломы", "полный перелом, деформация руки", "перегрузка минимальна для части тела" },
        new List<string> { "ушиб колена, небольшая отёчность", "возможны частичные разрывы крестообразных или боковых связок, повреждение мениска", 
            "трещины в надколеннике или в верхней части большеберцовой/бедренной кости",
            "полный перелом надколенника, раздробление сустава, вывих бедра/голени, сопровождающееся разрывом связок", "перегрузка минимальна для части тела" },
        new List<string> { "ушиб колена, небольшая отёчность", "возможны частичные разрывы крестообразных или боковых связок, повреждение мениска",
            "трещины в надколеннике или в верхней части большеберцовой/бедренной кости",
            "полный перелом надколенника, раздробление сустава, вывих бедра/голени, сопровождающееся разрывом связок", "перегрузка минимальна для части тела" },
        new List<string> { "лёгкое повреждение внутренних органов", "сильное повреждение внутренних органов", "внутренне кровотечение", 
            "сильные повреждения, внутреннее кровотчение большинства органов этого отдела", "перегрузка минимальна для части тела" },
        new List<string> { "лёгкое повреждение внутренних органов", "сильное повреждение внутренних органов", "внутреннее кровотечение", 
            "сильные повреждения, внутреннее кровотчение большинства органов этого отдела", "перегрузка минимальна для части тела" },
        new List<string> { "лёгкие ушибы", "серьёзные ушибы, возможен надрыв связок", "мелкие трещены в кости, частичный перелом", 
            "полный перелом, деформация отдела", "перегрузка минимальна для части тела" },
        new List<string> { "лёгкие ушибы", "серьёзные ушибы, возможен надрыв связок", "мелкие трещены в кости, частичный перелом",
            "полный перелом, деформация отдела", "перегрузка минимальна для части тела" },
        new List<string> { "лёгкие ушибы", "серьёзные ушибы, возможен надрыв связок", "мелкие трещены в кости, частичный перелом",
            "полный перелом, деформация отдела", "перегрузка минимальна для части тела" },
        new List<string> { "лёгкие ушибы", "серьёзные ушибы, возможен надрыв связок", "мелкие трещены в кости, частичный перелом",
            "полный перелом, деформация отдела", "перегрузка минимальна для части тела" },
        new List<string> { "лёгкие ушибы", "серьёзные ушибы, возможен надрыв связок", "мелкие трещены в кости, частичный перелом",
            "полный перелом, деформация отдела", "перегрузка минимальна для части тела" },
        new List<string> { "лёгкие ушибы", "серьёзные ушибы, возможен надрыв связок", "мелкие трещены в кости, частичный перелом",
            "полный перелом, деформация отдела", "перегрузка минимальна для части тела" },
        new List<string> { "лёгкие ушибы", "серьёзные ушибы, возможен надрыв связок", "мелкие трещены в кости, частичный перелом",
            "полный перелом, деформация отдела", "перегрузка минимальна для части тела" },
        new List<string> { "лёгкие ушибы", "серьёзные ушибы, возможен надрыв связок", "мелкие трещены в кости, частичный перелом",
            "полный перелом, деформация отдела", "перегрузка минимальна для части тела" },
    };
    //Сообщение о доступ к тектовым панелям
    public static List<List<string>> listPanelName = new List<List<string>>
    {
        new List<string> { "Голова ", "статус повреждения головы - ", "повреждения - " },
        new List<string> { "Шея ", "статус повреждения шеи -  ", "повреждения - " },
        new List<string> { "Грудная клетка ", "статус повреждения грудной клетки - ", "повреждения - " },
        new List<string> { "Левая кисть ", "статус повреждения левой кисти - ", "повреждения - " },
        new List<string> { "Правая кисть ", "статус повреждения правой кисти - ", "повреждения - " },
        new List<string> { "Левое колено ", "статус повреждения левого колена ", "повреждения - " },
        new List<string> { "Правое колено ", "статус повреждения правого колена ", "повреждения - " },
        new List<string> { "поясница", "статус повреждения поясницы - ", "повреждения - " },
        new List<string> { "таз", "статус повреждения таза - ", "повреждения - "},
        new List<string> { "левое плечо", "статус повреждения левого плеча - ", "повреждения - " },
        new List<string> { "правое плечо", "статус повреждения правого плеча - ", "повреждения - " },
        new List<string> { "левый локоть", "статус повреждения левого локтя - ", "повреждения - " },
        new List<string> { "правый локоть", "статус повреждения правого локтя - ", "повреждения - " },
        new List<string> { "левое бедро", "статус повреждения левого бедра - ", "повреждения - " },
        new List<string> { "правое бедро", "статус повреждения правого бедра - ", "повреждения - " },
        new List<string> { "левая голень", "статус повреждения левой голени - ", "повреждения - " },
        new List<string> { "правая голень", "статус повреждения правой голени - ", "повреждения - " }
    };

    public static List<int> subsequenceEditHumanLimbs = new List<int> { 9, 11, 3, 13, 5, 15, 16, 6, 14, 4, 12, 10 };
    public static List<int> subsequenceEditHumanTorso = new List<int> { 0, 1, 2, 7, 8 };
    //Координаты точки восстановления кнопок человека
    public static List<Vector2> referenceValueHuman = Enumerable.Repeat(Vector2.zero, 17).ToList();

    //Доступ к панелям
    //public static List<string> accesColorPanel = new List<string> { "head", "neck", "chest", "brushL", "brushR", "kneeL", "kneeR" };
    public static List<string> accesColorPanel = new List<string> { "headButton", "neckButton", "chestButton", "brushLButton", "brushRButton", "kneeLButton", "kneeRButton", "loinButton", "pelvisButton", "shoulderLButton", "shoulderRButton", "elbowLButton", "elbowRButton", "hipLButton", "hipRButton", "shinLButton",  "shinRButton" };
    public static List<string> savetyIcons = new List<string> {"beltSavety", "pillowSavety" };

    //Наименование списка панелей, нужных для вывода информации о повреждении
    public static List<string> listPanelStatsHuman = new List<string> { "damageInfo", "statsDamage", "damageOutput" };

    //Уровень повреждения на части тела от 0 до 4. 3 - 2 - 1 - 0 это повреждения, а 4 это повреждений нет
    public static List<int> levelDamagePartBody = Enumerable.Repeat(0, 17).ToList();

    //Повреждения человека в кг, всё в списке для бд
    public static List<float> damagePartBodyKG = Enumerable.Repeat(0f, 17).ToList();

    //Цвета состояний
    static List<Color> damageColors = new List<Color>
        {
            new Color(0f, 0.68f, 0f),      //Нет повреждений (Зёлынй)
            new Color(1f, 0.55f, 0f),  // Лёгкое повреждение (темно-зелёный)
            new Color(0.8f, 0.3f, 0f),    // Более тяжёлое (оранжевый)
            new Color(1f, 0f, 0f),   // Тяжёлое (темно-оранжевый)
            new Color(0.5f, 0f, 0f)   // Очень тяжёлое (красный)
        };

    //                    Гол   Шея   Тул   РЛ   РП    НЛ   НП
    public static List<List<float>> matrixMultiplyBelt = new List<List<float>>
    {
        new List<float> { 1f, 1f, 0.6f, 1f, 0.9f, 1f, 0.9f, 0.6f, 0.6f, 0.6f, 0.6f, 1f, 1f, 0.6f, 0.6f, 1f, 1f },       //Удар слева спереди
        new List<float> { 1f, 1f, 0.6f, 0.9f, 1f, 0.9f, 1f, 0.6f, 0.6f, 0.6f, 0.6f, 1f, 1f, 0.6f, 0.6f, 1f, 1f },       //Удар справа спереди
        new List<float> { 1f, 1f, 0.6f, 1f, 1f, 1f, 1f, 0.6f, 0.6f, 0.6f, 0.6f, 1f, 1f, 0.6f, 0.6f, 1f, 1f },           //Удар середина спереди
        new List<float> { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f },             //Удар слева сзади
        new List<float> { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f },             //Удар справа сзади
        new List<float> { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f },             //Удар середина сзади
        new List<float> { 1f, 1f, 0.8f, 1f, 0.7f, 1f, 0.8f, 0.7f, 0.8f, 0.8f, 0.8f, 1f, 0.7f, 1f, 0.7f, 1f, 0.7f },       //Удар слева сбоку спереди
        new List<float> { 1f, 1f, 0.9f, 0.8f, 0.9f, 0.8f, 1f, 0.8f, 0.9f, 0.9f, 0.9f, 0.7f, 1f, 0.7f, 1f, 0.7f, 1f },     //Удар слева сбоку сзади
        new List<float> { 1f, 1f, 0.9f, 0.6f, 0.9f, 0.8f, 1f, 0.8f, 0.9f, 0.9f, 0.9f, 0.7f, 1f, 0.7f, 1f, 0.7f, 1f },     //Удар справа сбоку спереди
        new List<float> { 1f, 1f, 0.8f, 1f, 0.7f, 1f, 0.8f, 0.7f, 0.8f, 0.8f, 0.8f, 1f, 0.7f, 1f, 0.7f, 1f, 0.7f }        //Удар справа сбоку сзади
    };


    public static List<List<float>> matrixMultiplyPillow = new List<List<float>>
    {
        new List<float> { 0.5f, 0.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 0.5f, 0.5f, 1f, 1f, 1f, 1f, 1f, 1f },         //Удар слева спереди
        new List<float> { 0.5f, 0.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 0.5f, 0.5f, 1f, 1f, 1f, 1f, 1f, 1f },         //Удар справа спереди
        new List<float> { 0.5f, 0.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 0.5f, 0.5f, 1f, 1f, 1f, 1f, 1f, 1f },         //Удар середина спереди
        new List<float> { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f },         //Удар слева сзади
        new List<float> { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f },         //Удар справа сзади
        new List<float> { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f },         //Удар середина сзади
        new List<float> { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f },             //Удар слева сбоку спереди
        new List<float> { 0.7f, 0.7f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f },         //Удар слева сбоку сзади
        new List<float> { 0.7f, 0.7f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f },         //Удар справа сбоку спереди
        new List<float> { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }              //Удар справа сбоку сзади
    };


    public static List<List<float>> matrixMultiplyHuman = new List<List<float>>
    {
        new List<float> { 1f, 0.6f, 1f, 1f, 1f, 1f, 1f, 0.8f,  0.7f, 1f, 1f, 0.8f, 0.8f, 0.7f, 0.7f, 0.7f, 0.7f },
        new List<float> { 1f, 0.6f, 1f, 1f, 1f, 1f, 1f, 0.8f,  0.7f, 1f, 1f, 0.8f, 0.8f, 0.7f, 0.7f, 0.7f, 0.7f },
        new List<float> { 1f, 0.6f, 1f, 1f, 1f, 1f, 1f, 0.8f,  0.7f, 1f, 1f, 0.8f, 0.8f, 0.7f, 0.7f, 0.7f, 0.7f },
        new List<float> { 0.3f, 0.1f, 0.4f, 0.3f, 0.3f, 0.3f, 0.3f, 0.2f, 0.4f, 0.3f, 0.3f, 0.4f, 0.4f, 0.3f, 0.3f, 0.3f, 0.3f },
        new List<float> { 0.3f, 0.1f, 0.4f, 0.3f, 0.3f, 0.3f, 0.3f, 0.2f, 0.4f, 0.3f, 0.3f, 0.4f, 0.4f, 0.3f, 0.3f, 0.3f, 0.3f },
        new List<float> { 0.3f, 0.1f, 0.4f, 0.3f, 0.3f, 0.3f, 0.3f, 0.2f, 0.4f, 0.3f, 0.3f, 0.4f, 0.4f, 0.3f, 0.3f, 0.3f, 0.3f },
        new List<float> { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 0.8f,  0.7f, 1f, 1f, 0.9f, 0.9f, 0.9f, 0.7f, 1f, 0.7f },
        new List<float> { 1f, 0.7f, 1f, 1f, 1f, 1f, 1f, 0.8f,  0.7f, 1f, 1f, 0.9f, 0.9f, 0.7f, 0.9f, 0.7f, 1f },
        new List<float> { 1f, 0.7f, 1f, 1f, 1f, 1f, 1f, 0.8f,  0.7f, 1f, 1f, 0.9f, 0.9f, 0.7f, 0.9f, 0.7f, 1f },
        new List<float> { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 0.8f,  0.7f, 1f, 1f, 0.9f, 0.9f, 0.9f, 0.7f, 1f, 0.7f },
    };



    //Прочность Головы, шеи, грудной части и так далее
    public static float headStability = 1;
    public static float neckStability = 1;
    public static float chestStability = 1;
    public static float handStabilityL = 1;
    public static float handStabilityR = 1;
    public static float legStabilityL = 1;
    public static float legStabilityR = 1;


    public static List<float> halfHuman = new List<float> { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };


    //Множитель веса
    public static int headMultiplierWeight = 7;
    public static int neckMultiplierWeight = 3;
    public static int chestMultiplierWeight = 15;
    public static int handMultiplierWeightL = 5;
    public static int handMultiplierWeightR = 5;
    public static int legMultiplierWeightL = 16;
    public static int legMultiplierWeightR = 16;
    public static int loinMultiplierWeight = 4;
    public static int pelvisMultiplierWeight = 10;
    public static int shoulderMultiplierWeightL = 2;
    public static int shoulderMultiplierWeightR = 2;
    public static int elbowMultiplierWeightL = 2;
    public static int elbowMultiplierWeightR = 2;
    public static int hipMultiplierWeightL = 9;
    public static int hipMultiplierWeightR = 9;
    public static int shinMultiplierWeightL = 7;
    public static int shinMultiplierWeightR = 7;
    //Вес
    public static float headWeight;
    public static float neckWeight;
    public static float chestWeight;
    public static float handWeightL;
    public static float handWeightR;
    public static float legWeightL;
    public static float legWeightR;
    public static float humanWeight;
    public static float humanWeightOneProcent;


    public static List<float> weightHuman = new List<float> { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };


    //Безопасность - значения подушки безопасности и ремня безопасности
    //Сначала ремень, потом подушка
    public static List<bool> variantsSavty = new List<bool> { false, false};


    public static int locationDamage;

    //Сила удара части тела
    public static float forceAnPart;
    public static float headForce;
    public static float neckForce;
    public static float chestForce;
    public static float handForceL;
    public static float handForceR;
    public static float legForceL;
    public static float legForceR;

    public static string allDamageParts;
    public static string informationDamage;


    public static List<float> getdamagePartBodyKG()
    {
        return damagePartBodyKG;
    }
    public static List<int> getDamageLevelList()
    {
        return levelDamagePartBody;
    }


    public static void viewListDamage()
    {
        datamove = FindObjectOfType<dataMove>();
        var damageList = datamove.GetAllFullInformationKGDamage();

        nameListRender = FindObjectOfType<NameListRenderer>();
        nameListRender.RenderNameList(damageList); // передаём список объектов, а не строк
    }

    public static void callSortList(string fieldName)
    {
        datamove = FindObjectOfType<dataMove>();
        var damageList = datamove.GetAllFullInformationKGDamage();

        nameListRender = FindObjectOfType<NameListRenderer>();
        nameListRender.SortBy(fieldName, damageList);
    }



    public static void loadToCSV()
    {
        datamove = FindObjectOfType<dataMove>();
        var damageList = datamove.GetAllFullInformationKGDamage();

        nameListRender = FindObjectOfType<NameListRenderer>();

        string folderPath;

#if UNITY_EDITOR
        // В редакторе сохраняем в папку рядом с проектом
        folderPath = Path.Combine(Directory.GetParent(Application.dataPath).FullName, "Exports");
#else
    // В билде (экзешнике) сохраняем в ту же папку Data, где и база
    string root = Directory.GetParent(Application.dataPath).FullName;
    folderPath = Path.Combine(root, "Data");

#endif

        // Убедимся, что папка существует
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        string filePath = Path.Combine(folderPath, "DamageData.csv");
        SetDebugText(filePath);

        nameListRender.ExportToCSV(damageList, filePath);

        
    }




    public static void SetDebugText(string newText)
    {
        // Ищем объект по имени (учти регистр!)
        GameObject debugTextGO = GameObject.Find("textDebugTest");
        if (debugTextGO != null)
        {
            TextMeshProUGUI tmpText = debugTextGO.GetComponent<TextMeshProUGUI>();
            if (tmpText != null)
            {
                tmpText.text = newText;
            }
            else
            {
                Debug.LogWarning("Компонент Text не найден на объекте textDebugTest");
            }
        }
        else
        {
            Debug.LogWarning("Объект textDebugTest не найден в сцене");
        }
    }


    public static void offPannelNameListRender()
    {
        nameListRender = FindObjectOfType<NameListRenderer>();
        nameListRender.turnOffPanel();
    }

    public static void enterSavtyVariants(int number)
    {
        variantsSavty[number] = !variantsSavty[number];
        editSavetyPane(number);

    }

    private static void editSavetyPane(int number)
    {
        GameObject canvas = GameObject.Find("canvasSavetyCar");  // Найди объект Canvas
        if (canvas != null)
        {
            Transform iconsTransform = canvas.transform.Find("savetyIcons");  // Найди панель по имени

            Transform imageChild = iconsTransform.Find(savetyIcons[number]);

            if (imageChild != null)
            {
                Image iconImage = imageChild.GetComponent<Image>();
                if (iconImage != null)
                {
                    if (variantsSavty[number])
                    {
                        iconImage.color = damageColors[0];
                    }
                    else
                    {
                        iconImage.color = damageColors[4];
                    }
                    
                }
            }
        }
        else
        {
            Debug.LogError("Не удалось найти объект Canvas");
        }
    }
    public static void algoritmMoveParts()
    {
        float posX = -100;
        float posY = 200;
        float step = 60;
        int count = 0;
        for (int i = 0; i < subsequenceEditHumanLimbs.Count; i++)
        {
            int currentValue = subsequenceEditHumanLimbs[i];
            if (i == subsequenceEditHumanLimbs.Count / 2)
            {
                posX += step;
                count = -1;
            }
            else if (count == 3)
            {
                posX += step;
                posY -= step;
                count = 1;

            }
            else if (count == -3)
            {
                posX += step;
                posY += step;
                count = -1;

            }
            else
            {

                if (count > -1)
                {
                    posY -= step;
                    count++;
                }

                else
                {
                    posY += step;
                    count--;
                }
            }

            activatePanelButton(currentValue, posX, posY);
        }
        posX -= step * 1.5f;
        posY += step * 2;
        for (int i = 0; i < subsequenceEditHumanTorso.Count; i++)
        {
            int currentValue = subsequenceEditHumanTorso[i];
            activatePanelButton(currentValue, posX, posY);
            posY -= step;
        }

    }
    public static void activatePanelButton(int currentValue, float posX, float posY)
    {
        GameObject canvas = GameObject.Find("uiMessage");  // Найди объект Canvas

        if (canvas != null)
        {
            Transform panelTransform = canvas.transform.Find(accesColorPanel[currentValue]);  // Найди панель по имени

            Button panelButton = panelTransform.GetComponent<Button>();
            if (panelButton != null)
            {

                // Меняем позицию
                RectTransform rectTransform = panelTransform.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    referenceValueHuman[currentValue] = rectTransform.anchoredPosition;
                    rectTransform.anchoredPosition = new Vector2(posX, posY); // Пример новой позиции
                }
            }
            

        }
        else
        {
            Debug.Log("Объект не найден");
        }
    }

    public static void returnPanelButton()
    {

        GameObject canvas = GameObject.Find("uiMessage");  // Найди объект Canvas

        if (canvas != null)
        {
            for (int i = 0; i < accesColorPanel.Count; i++)
            {
                Transform panelTransform = canvas.transform.Find(accesColorPanel[i]);  // Найди панель по имени

                Button panelButton = panelTransform.GetComponent<Button>();
                if (panelButton != null)
                {

                    // Меняем позицию
                    RectTransform rectTransform = panelTransform.GetComponent<RectTransform>();
                    if (rectTransform != null)
                    {
                        rectTransform.anchoredPosition = referenceValueHuman[i];
                    }
                }
            }

        }
    }

    public static void activateSavetyPanel()
    {



        GameObject canvas = GameObject.Find("canvasSavetyCar");  // Найди объект Canvas
        if (canvas != null)
        {
            Transform panelTransform = canvas.transform.Find("savetyPanel");  // Найди панель по имени
            if (panelTransform != null)
            {
                panelTransform.gameObject.SetActive(true);  // Включаем панель
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                
            }
            else
            {
                Debug.LogError("Не удалось найти панель в Canvas");
            }
        }
        else
        {
            Debug.LogError("Не удалось найти объект Canvas");
        }

    }

    public static void activateMessagePanel(int numberPartBody, int numberColor)
    {
        

        GameObject canvas = GameObject.Find("uiMessage");  // Найди объект Canvas
        if (canvas != null)
        {
            
            Transform panelTransform = canvas.transform.Find(accesColorPanel[numberPartBody]);  // Найди панель по имени
            
            Button panelButton = panelTransform.GetComponent<Button>();
            if (panelButton != null)
            {

                

                Image buttonImage = panelButton.GetComponent<Image>();
                if (buttonImage != null)
                {
                    buttonImage.color = damageColors[numberColor];  // Меняем цвет фона кнопки
                }
            }
            else
            {
                Debug.LogError("Не удалось найти панель в Canvas");
            }
        }
        else
        {
            Debug.LogError("Не удалось найти объект Canvas");
        }

    }


    private static void callingMainFunction(int number, float gForce)
    {

        //Добавление множителя человека на множитель подушки безопасности, на множитель ремня безопасности
        for (int i = 0; i < halfHuman.Count; i++)
        {
            if ((variantsSavty[0]) && (variantsSavty[1]))
            {
                calcDamageHuman(i, matrixMultiplyHuman[number][i], matrixMultiplyPillow[number][i], matrixMultiplyBelt[number][i], gForce);
            }
            else if ((!variantsSavty[0]) && (!variantsSavty[1]))
            {
                calcDamageHuman(i, matrixMultiplyHuman[number][i], 1f, 1f, gForce);
            }
            else if ((variantsSavty[0]) && (!variantsSavty[1]))
            {
                calcDamageHuman(i, matrixMultiplyHuman[number][i], 1f, matrixMultiplyBelt[number][i], gForce);
            }
            else if ((!variantsSavty[0]) && (variantsSavty[1]))
            {
                calcDamageHuman(i, matrixMultiplyHuman[number][i], matrixMultiplyPillow[number][i], 1f, gForce);
            }

        }
        
    }

    public static void caseSolution(float aWeight, int bLocation, float gForce, float kgForce, float speedTo, float speedAt)
    {
        humanWeight = aWeight;
        humanWeightOneProcent = humanWeight / 100f;
        locationDamage = bLocation;
        headWeight = humanWeightOneProcent * headMultiplierWeight;
        neckWeight = humanWeightOneProcent * neckMultiplierWeight;
        chestWeight = humanWeightOneProcent * chestMultiplierWeight;
        handWeightL = humanWeightOneProcent * handMultiplierWeightL;
        handWeightR = humanWeightOneProcent * handMultiplierWeightR;
        legWeightL = humanWeightOneProcent * legMultiplierWeightL;
        legWeightR = humanWeightOneProcent * legMultiplierWeightR;

        weightHuman[0] = humanWeightOneProcent * headMultiplierWeight;
        weightHuman[1] = humanWeightOneProcent * neckMultiplierWeight;
        weightHuman[2] = humanWeightOneProcent * chestMultiplierWeight;
        weightHuman[3] = humanWeightOneProcent * handMultiplierWeightL;
        weightHuman[4] = humanWeightOneProcent * handMultiplierWeightR;
        weightHuman[5] = humanWeightOneProcent * legMultiplierWeightL;
        weightHuman[6] = humanWeightOneProcent * legMultiplierWeightR;
        weightHuman[7] = humanWeightOneProcent * loinMultiplierWeight;
        weightHuman[8] = humanWeightOneProcent * pelvisMultiplierWeight;
        weightHuman[9] = humanWeightOneProcent * shoulderMultiplierWeightL;
        weightHuman[10] = humanWeightOneProcent * shoulderMultiplierWeightR;
        weightHuman[11] = humanWeightOneProcent * elbowMultiplierWeightL;
        weightHuman[12] = humanWeightOneProcent * elbowMultiplierWeightR;
        weightHuman[13] = humanWeightOneProcent * hipMultiplierWeightL;
        weightHuman[14] = humanWeightOneProcent * hipMultiplierWeightR;
        weightHuman[15] = humanWeightOneProcent * shinMultiplierWeightL;
        weightHuman[16] = humanWeightOneProcent * shinMultiplierWeightR;

        switch (locationDamage)
        {
            case 1:
                Debug.Log("Удар слева спереди");
                informationDamage += "удар слева спереди       ";

                callingMainFunction(0, gForce);
                


                break;
            case 2:
                Debug.Log("Удар справа спереди");
                informationDamage += "удар справа спереди      ";

                callingMainFunction(1, gForce);
                
                break;
            case 3:
                Debug.Log("Удар середина спереди");
                informationDamage += "удар середина спереди    ";
                                      
                callingMainFunction(2, gForce);
                
                break;
            case 4:
                Debug.Log("Удар слева сзади");
                informationDamage += "удар слева сзади         ";
                                     
                callingMainFunction(3, gForce);
                
                break;
            case 5:
                Debug.Log("Удар справа сзади");
                informationDamage += "удар справа сзади        ";
                                      
                callingMainFunction(4, gForce);
                
                break;
            case 6:
                Debug.Log("Удар середина сзади");
                informationDamage += "удар середина сзади      ";
                                      
                callingMainFunction(5, gForce);
                
                break;
            case 7:
                Debug.Log("Удар слева сбоку спереди");
                informationDamage += "удар сбоку слева спереди ";
                                      
                callingMainFunction(6, gForce);
                
                break;
            case 8:
                Debug.Log("Удар слева сбоку сзади");
                informationDamage += "удар сбоку слева сзади   ";
                                      
                callingMainFunction(7, gForce);
                
                break;
            case 9:
                Debug.Log("Удар справа сбоку спереди");
                informationDamage += "удар сбоку справа спереди";
                callingMainFunction(8, gForce);
                
                break;
            default:
                Debug.Log("Удар справа сбоку сзади");
                informationDamage += "удар сбоку справа сзади  ";
                callingMainFunction(9, gForce);
               
                break;
        }
        //textOutput("damageCharacteristics", "Испытанная нагрузка - ", allDamageParts);
        informationDamage += ". Перегрузка в G равна " + gForce;
        informationDamage += ". Перегрузка на всё тело в кг равна " + gForce * humanWeight + ".";
        informationDamage += "\nСкорость до удара " + speedTo + "км/ч. Скорость во время удара " + speedAt + "км/ч.";
        textOutput("fullInformationDamage", "Основной ", informationDamage);
        allDamageParts = "";
        informationDamage = "";
        
    }
    
    public static void callTextOutputFH(int number)
    {

        textOutputForHuman(listPanelStatsHuman[0], listPanelName[number][0]);
        if (levelDamagePartBody[number] == 4)
        {
            textOutput(listPanelStatsHuman[1], listPanelName[number][1], "0");
        }
        else
        {
            textOutput(listPanelStatsHuman[1], listPanelName[number][1], (levelDamagePartBody[number] + 1).ToString());
        }
        textOutput(listPanelStatsHuman[2], listPanelName[number][2], messageDamage[number][levelDamagePartBody[number]]);


        
    }

    public static void textOutput(string textName, string namePart, string outputText)
    {
        GameObject canvas = GameObject.Find("messageForDamage");
        Transform panelTransform = canvas.transform.Find("panelMessageDamage");
        Transform textTransform = panelTransform.Find(textName);
        TMPro.TextMeshProUGUI textComponent = textTransform.GetComponent<TMPro.TextMeshProUGUI>();
        textComponent.text = namePart + outputText;
    }


    public static void textOutputForHuman(string textName, string outputText)
    {
        GameObject canvas = GameObject.Find("messageForDamage");
        Transform panelTransform = canvas.transform.Find("panelMessageDamage");
        Transform textTransform = panelTransform.Find(textName);
        TMPro.TextMeshProUGUI textComponent = textTransform.GetComponent<TMPro.TextMeshProUGUI>();
        textComponent.text = outputText;
    }

    private static void calcDamageHuman(int number, float multiplyDamage,float multiplyPillow, float multiplyBelt,  float gForce)
    {
        int numberColor = 0;
        forceAnPart = gForce * multiplyDamage * multiplyPillow * multiplyBelt * weightHuman[number];
        damagePartBodyKG[number] = forceAnPart;

        Debug.Log("Множитель " + multiplyDamage + " Умноженный на вес части тела " + weightHuman[number]);

        if (forceAnPart >= criticalDamage[number][3])
        {
            halfHuman[number] = halfHuman[number] - 1f;
        }
        else if (forceAnPart >= criticalDamage[number][2])
        {
            halfHuman[number] = halfHuman[number] - 0.85f;
        }
        else if (forceAnPart >= criticalDamage[number][1])
        {
            halfHuman[number] = halfHuman[number] - 0.5f;
        }
        else if (forceAnPart >= criticalDamage[number][0])
        {
            halfHuman[number] = halfHuman[number] - 0.25f;
        }
        if (halfHuman[number] <= 0)
        {
            levelDamagePartBody[number] = 3;
            numberColor = 4;
        }
        else if (halfHuman[number] <= 0.25f)
        {
            levelDamagePartBody[number] = 2;
            numberColor = 3;
        }
        else if (halfHuman[number] <= 0.50f)
        {
            levelDamagePartBody[number] = 1;
            numberColor = 2;
        }
        else if (halfHuman[number] <= 0.85f)
        {
            levelDamagePartBody[number] = 0;
            numberColor = 1;
        }
        else
        {
            levelDamagePartBody[number] = 4;
        }
        activateMessagePanel(number, numberColor);
    }


    public static void recoverHuman()
    {
        for (int i = 0; i < halfHuman.Count; i++)
        {
            halfHuman[i] = 1f;
            activateMessagePanel(i, 0);
        }
        for (int i = 0; i < levelDamagePartBody.Count; i++)
        {
            levelDamagePartBody[i] = 4;
        }
        ;
        Debug.Log("Человек восстановлен");
        
 
    }

}
