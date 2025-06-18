using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class panelActions : MonoBehaviour
{

    private static List<string> sortFields = new List<string>
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


    public static void enterBelt()
    {
        SkeletDamage.enterSavtyVariants(0);
    }
    public static void enterPillow()
    {
        SkeletDamage.enterSavtyVariants(1);
    }
    public static void callBodyPartOutput(int number)
    {
        //Debug.Log(number);
        SkeletDamage.callTextOutputFH(number);
    }
    public static void callViewBD()
    {
        SkeletDamage.viewListDamage();
        SkeletDamage.returnPanelButton();
        SkeletDamage.SetDebugText("");

    }

    public void callOffPanel()
    {
        SkeletDamage.offPannelNameListRender();
        SkeletDamage.algoritmMoveParts();

    }

    public static void sortList(int number)
    {
        SkeletDamage.callSortList(sortFields[number]);
    }

    public static void loadCSV()
    {
        SkeletDamage.loadToCSV(); 
    }

}
