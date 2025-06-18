using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using static DatabaseHandler;
using PG;
using System.IO;
using System.Text;
using System.Globalization;
using System;

public class NameListRenderer : MonoBehaviour
{
    [Header("UI References")]
    public Transform contentParent;            // Контейнер: ScrollView > Viewport > Content
    public GameObject nameItemPrefab;          // Префаб: TextMeshPro (одна строка)
    public GameObject panel;

    private List<GameObject> spawnedItems = new List<GameObject>();
    private bool ascending = true;


    private string currentSortFieldName = "HitIdText";
    private List<string> allowedSortFields = new List<string>
    {
        "HitIdText",
        "HitSideText",
        "HitForceText",
        "CarSpeedText",
        "headKGText",
        "neckKGText",
        "chestKGText",
        "brushLKGText",
        "brushRKGText",
        "kneeLKGText",
        "kneeRKGText",
        "loinKGText",
        "pelvisKGText",
        "shoulderLKGText",
        "shoulderRKGText",
        "elbowLKGText",
        "elbowRKGText",
        "hipLKGText",
        "hipRKGText",
        "shinLKGText",
        "shinRKGText"
    };
    /// <summary>
    /// Отрисовать список имён в ScrollView
    /// </summary>
    //public void RenderNameList(List<string> nameList)
    //{
    //    ClearList();
    //    panel.SetActive(true);
    //    foreach (var name in nameList)
    //    {
    //        GameObject newItem = Instantiate(nameItemPrefab, contentParent);
    //        newItem.GetComponent<TMP_Text>().text = name;
    //        spawnedItems.Add(newItem);
    //    }
    //}

    public void SortBy(string fieldName, List<FullInformationKGDamage> damageData)
    {
        if (!allowedSortFields.Contains(fieldName))
        {
            Debug.LogWarning($"Field '{fieldName}' не разрешен для сортировки.");
            return;
        }

        if (fieldName == currentSortFieldName)
            ascending = !ascending; // меняем порядок сортировки при повторном клике
        else
            ascending = true; // сортируем по возрастанию для нового поля

        currentSortFieldName = fieldName;

        var propName = fieldName.Replace("Text", ""); // убираем "Text", чтобы получить имя свойства модели

        var prop = typeof(FullInformationKGDamage).GetProperty(propName);

        if (prop != null)
        {
            if (ascending)
                damageData = damageData.OrderBy(x => prop.GetValue(x)).ToList();
            else
                damageData = damageData.OrderByDescending(x => prop.GetValue(x)).ToList();
        }

        RenderNameList(damageData);
    }

    public void RenderNameList(List<FullInformationKGDamage> damageList)
    {
        //ClearList();
        //panel.SetActive(true);

        //foreach (var item in damageList)
        //{
        //    GameObject newItem = Instantiate(nameItemPrefab, contentParent);


        //    // Поиск компонентов в prefab
        //    TMP_Text hitSideText = newItem.transform.Find("HitSideText").GetComponent<TMP_Text>();
        //    TMP_Text hitForceText = newItem.transform.Find("HitForceText").GetComponent<TMP_Text>();

        //    // Заполнение данных
        //    hitSideText.text = item.HitSide;
        //    hitForceText.text = $"{item.HitForce} кг";

        //    spawnedItems.Add(newItem);
        //}



        ClearList();
        panel.SetActive(true);

        foreach (var item in damageList)
        {
            GameObject newItem = Instantiate(nameItemPrefab, contentParent);
            newItem.transform.Find("HitIdText").GetComponent<TMP_Text>().text = item.HitId.ToString();
            newItem.transform.Find("HitSideText").GetComponent<TMP_Text>().text = item.HitSide;
            newItem.transform.Find("HitForceText").GetComponent<TMP_Text>().text = item.HitForce.ToString("F2");
            newItem.transform.Find("CarSpeedText").GetComponent<TMP_Text>().text = item.CarSpeed.ToString("F2");
            newItem.transform.Find("headKGText").GetComponent<TMP_Text>().text = item.headKG.ToString("F2");
            newItem.transform.Find("neckKGText").GetComponent<TMP_Text>().text = item.neckKG.ToString("F2");
            newItem.transform.Find("chestKGText").GetComponent<TMP_Text>().text = item.chestKG.ToString("F2");
            newItem.transform.Find("brushLKGText").GetComponent<TMP_Text>().text = item.brushLKG.ToString("F2");
            newItem.transform.Find("brushRKGText").GetComponent<TMP_Text>().text = item.brushRKG.ToString("F2");
            newItem.transform.Find("kneeLKGText").GetComponent<TMP_Text>().text = item.kneeLKG.ToString("F2");
            newItem.transform.Find("kneeRKGText").GetComponent<TMP_Text>().text = item.kneeRKG.ToString("F2");
            newItem.transform.Find("loinKGText").GetComponent<TMP_Text>().text = item.loinKG.ToString("F2");
            newItem.transform.Find("pelvisKGText").GetComponent<TMP_Text>().text = item.pelvisKG.ToString("F2");
            newItem.transform.Find("shoulderLKGText").GetComponent<TMP_Text>().text = item.shoulderLKG.ToString("F2");
            newItem.transform.Find("shoulderRKGText").GetComponent<TMP_Text>().text = item.shoulderRKG.ToString("F2");
            newItem.transform.Find("elbowLKGText").GetComponent<TMP_Text>().text = item.elbowLKG.ToString("F2");
            newItem.transform.Find("elbowRKGText").GetComponent<TMP_Text>().text = item.elbowRKG.ToString("F2");
            newItem.transform.Find("hipLKGText").GetComponent<TMP_Text>().text = item.hipLKG.ToString("F2");
            newItem.transform.Find("hipRKGText").GetComponent<TMP_Text>().text = item.hipRKG.ToString("F2");
            newItem.transform.Find("shinLKGText").GetComponent<TMP_Text>().text = item.shinLKG.ToString("F2");
            newItem.transform.Find("shinRKGText").GetComponent<TMP_Text>().text = item.shinRKG.ToString("F2");
            Debug.Log("Вывод чего-то " + item.HitId.ToString());
            spawnedItems.Add(newItem);
        }
        //string filePath = "DamageData.csv";
        //ExportToCSV(damageList, filePath);

    }


    public void ExportToCSV(List<FullInformationKGDamage> damageDataList, string filePath = "DamageExport.csv")
    {
        var csv = new StringBuilder();

        // Заголовки столбцов с разделителем ;
        //csv.AppendLine("id удара;сторона удара;сила;скорость;headKGText;neckKGText;chestKGText;brushLKGText;brushRKGText;kneeLKGText;kneeRKGText;loinKGText;pelvisKGText;shoulderLKGText;shoulderRKGText;elbowLKGText;elbowRKGText;hipLKGText;hipRKGText;shinLKGText;shinRKGText");
        csv.AppendLine("id удара;сторона удара;сила;скорость;голова;шея;грудь;кистьЛ;кистьП;коленоЛ;коленоП;поясница;таз;плечоЛ;плечоП;локотьЛ;локотьП;бедроЛ;бедроП;голеньЛ;голеньП");

        // Форматируем каждую строку
        foreach (var item in damageDataList)
        {
            // Если некоторые поля - строки с возможными спецсимволами - экранируйте их в кавычки
            string line = string.Join(";",
                item.HitId.ToString(),
                EscapeCsvField(item.HitSide),
                item.HitForce.ToString("F2", new CultureInfo("ru-RU")),
                item.CarSpeed.ToString("F2", new CultureInfo("ru-RU")),
                item.headKG.ToString("F2", new CultureInfo("ru-RU")),
                item.neckKG.ToString("F2", new CultureInfo("ru-RU")),
                item.chestKG.ToString("F2", new CultureInfo("ru-RU")),
                item.brushLKG.ToString("F2", new CultureInfo("ru-RU")),
                item.brushRKG.ToString("F2", new CultureInfo("ru-RU")),
                item.kneeLKG.ToString("F2", new CultureInfo("ru-RU")),
                item.kneeRKG.ToString("F2", new CultureInfo("ru-RU")),
                item.loinKG.ToString("F2", new CultureInfo("ru-RU")),
                item.pelvisKG.ToString("F2", new CultureInfo("ru-RU")),
                item.shoulderLKG.ToString("F2", new CultureInfo("ru-RU")),
                item.shoulderRKG.ToString("F2", new CultureInfo("ru-RU")),
                item.elbowLKG.ToString("F2", new CultureInfo("ru-RU")),
                item.elbowRKG.ToString("F2", new CultureInfo("ru-RU")),
                item.hipLKG.ToString("F2", new CultureInfo("ru-RU")),
                item.hipRKG.ToString("F2", new CultureInfo("ru-RU")),
                item.shinLKG.ToString("F2", new CultureInfo("ru-RU")),
                item.shinRKG.ToString("F2", new CultureInfo("ru-RU"))
            );

            csv.AppendLine(line);
        }




        try
        {
            var encoding = new UTF8Encoding(true);
            File.WriteAllText(filePath, csv.ToString(), encoding);
            SkeletDamage.SetDebugText($"CSV файл успешно сохранён: {filePath}");
        }
        catch (Exception ex)
        {
            SkeletDamage.SetDebugText($"Ошибка при сохранении CSV:\n{ex.Message}\nПуть: {filePath}");
        }
    }

    // Метод для экранирования полей CSV (если есть ; или кавычки)
    private string EscapeCsvField(string field)
    {
        if (string.IsNullOrEmpty(field))
            return "";

        if (field.Contains(";") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r"))
        {
            // Заменяем " на ""
            field = field.Replace("\"", "\"\"");
            // Оборачиваем в двойные кавычки
            return $"\"{field}\"";
        }

        return field;
    }




    public void turnOffPanel()
    {
        panel.SetActive(false);
    }
    /// <summary>
    /// Очистить старые элементы
    /// </summary>
    private void ClearList()
    {
        foreach (var item in spawnedItems)
        {
            Destroy(item);
        }
        spawnedItems.Clear();
    }
}