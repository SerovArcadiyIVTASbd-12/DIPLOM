using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using static DatabaseHandler;

public class DamageTableRenderer : MonoBehaviour
{
    public GameObject rowPrefab;
    public Transform content;
    private List<GameObject> spawnedRows = new List<GameObject>();

    private List<FullInformationKGDamage> damageData = new();

    private string currentSortField = "HitId";
    private bool ascending = true;

    public void SetData(List<FullInformationKGDamage> data)
    {
        damageData = data;
        RenderTable(damageData);
    }

    public void SortBy(string fieldName)
    {
        ascending = fieldName == currentSortField ? !ascending : true;
        currentSortField = fieldName;

        var prop = typeof(FullInformationKGDamage).GetProperty(fieldName);
        if (prop != null)
        {
            if (ascending)
                damageData = damageData.OrderBy(x => prop.GetValue(x)).ToList();
            else
                damageData = damageData.OrderByDescending(x => prop.GetValue(x)).ToList();
        }

        RenderTable(damageData);
    }

    private void RenderTable(List<FullInformationKGDamage> list)
    {
        foreach (var row in spawnedRows)
            Destroy(row);
        spawnedRows.Clear();

        foreach (var item in list)
        {
            GameObject row = Instantiate(rowPrefab, content);
            var texts = row.GetComponentsInChildren<TMP_Text>();

            texts[0].text = item.HitId.ToString();
            texts[1].text = item.HitSide;
            texts[2].text = item.HitForce.ToString("F2");
            texts[3].text = item.CarSpeed.ToString("F2");
            texts[4].text = item.headKG.ToString("F2");
            texts[5].text = item.neckKG.ToString("F2");
            texts[6].text = item.chestKG.ToString("F2");
            texts[7].text = item.brushLKG.ToString("F2");
            texts[8].text = item.brushRKG.ToString("F2");
            texts[9].text = item.kneeLKG.ToString("F2");
            texts[10].text = item.kneeRKG.ToString("F2");
            texts[11].text = item.loinKG.ToString("F2");
            texts[12].text = item.pelvisKG.ToString("F2");
            texts[13].text = item.shoulderLKG.ToString("F2");
            texts[14].text = item.shoulderRKG.ToString("F2");
            texts[15].text = item.elbowLKG.ToString("F2");
            texts[16].text = item.elbowRKG.ToString("F2");
            texts[17].text = item.hipLKG.ToString("F2");
            texts[18].text = item.hipRKG.ToString("F2");
            texts[19].text = item.shinLKG.ToString("F2");
            texts[20].text = item.shinRKG.ToString("F2");

            spawnedRows.Add(row);
        }
    }
}