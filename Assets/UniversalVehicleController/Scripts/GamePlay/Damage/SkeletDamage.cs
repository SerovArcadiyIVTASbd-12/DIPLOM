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
    //����� ������ ������, ���, �����, ����, ����
    public static List<List<string>> messageDamage = new List<List<string>>
    {
        new List<string> { "����� ����������", "����������, ��������������, �������", 
            "����������, ����������� ������ ��������", "������ ���������� �����, ���������� ������", "���������� ���������� ��� ����� ����" },
        new List<string> { "���������� (�����)", "���������� � ������ ������", 
            "������ ������, ��������, �������� ��������", "������ ������, �������� ��������������", "���������� ���������� ��� ����� ����" },
        new List<string> { "������ ����� ������� ������, ������", "�������� �����, �������� ������������, ���������� ������",
            "������� � ���� ����, ������ ����", "�������� ����, ����������� ����������� �������", "���������� ���������� ��� ����� ����" },
        new List<string> { "�����, ��������, ����� ����������", "��������� �����, �������� ������ ������", 
            "������ ������� � �����, ��������� ��������", "������ �������, ���������� ����", "���������� ���������� ��� ����� ����" },
        new List<string> { "�����, ��������, ����� ����������", "��������� �����, �������� ������ ������",
            "������ ������� � �����, ��������� ��������", "������ �������, ���������� ����", "���������� ���������� ��� ����� ����" },
        new List<string> { "���� ������, ��������� ��������", "�������� ��������� ������� �������������� ��� ������� ������, ����������� �������", 
            "������� � ������������ ��� � ������� ����� ��������������/��������� �����",
            "������ ������� ������������, ������������ �������, ����� �����/������, ���������������� �������� ������", "���������� ���������� ��� ����� ����" },
        new List<string> { "���� ������, ��������� ��������", "�������� ��������� ������� �������������� ��� ������� ������, ����������� �������",
            "������� � ������������ ��� � ������� ����� ��������������/��������� �����",
            "������ ������� ������������, ������������ �������, ����� �����/������, ���������������� �������� ������", "���������� ���������� ��� ����� ����" },
        new List<string> { "����� ����������� ���������� �������", "������� ����������� ���������� �������", "��������� ������������", 
            "������� �����������, ���������� ����������� ����������� ������� ����� ������", "���������� ���������� ��� ����� ����" },
        new List<string> { "����� ����������� ���������� �������", "������� ����������� ���������� �������", "���������� ������������", 
            "������� �����������, ���������� ����������� ����������� ������� ����� ������", "���������� ���������� ��� ����� ����" },
        new List<string> { "����� �����", "��������� �����, �������� ������ ������", "������ ������� � �����, ��������� �������", 
            "������ �������, ���������� ������", "���������� ���������� ��� ����� ����" },
        new List<string> { "����� �����", "��������� �����, �������� ������ ������", "������ ������� � �����, ��������� �������",
            "������ �������, ���������� ������", "���������� ���������� ��� ����� ����" },
        new List<string> { "����� �����", "��������� �����, �������� ������ ������", "������ ������� � �����, ��������� �������",
            "������ �������, ���������� ������", "���������� ���������� ��� ����� ����" },
        new List<string> { "����� �����", "��������� �����, �������� ������ ������", "������ ������� � �����, ��������� �������",
            "������ �������, ���������� ������", "���������� ���������� ��� ����� ����" },
        new List<string> { "����� �����", "��������� �����, �������� ������ ������", "������ ������� � �����, ��������� �������",
            "������ �������, ���������� ������", "���������� ���������� ��� ����� ����" },
        new List<string> { "����� �����", "��������� �����, �������� ������ ������", "������ ������� � �����, ��������� �������",
            "������ �������, ���������� ������", "���������� ���������� ��� ����� ����" },
        new List<string> { "����� �����", "��������� �����, �������� ������ ������", "������ ������� � �����, ��������� �������",
            "������ �������, ���������� ������", "���������� ���������� ��� ����� ����" },
        new List<string> { "����� �����", "��������� �����, �������� ������ ������", "������ ������� � �����, ��������� �������",
            "������ �������, ���������� ������", "���������� ���������� ��� ����� ����" },
    };
    //��������� � ������ � �������� �������
    public static List<List<string>> listPanelName = new List<List<string>>
    {
        new List<string> { "������ ", "������ ����������� ������ - ", "����������� - " },
        new List<string> { "��� ", "������ ����������� ��� -  ", "����������� - " },
        new List<string> { "������� ������ ", "������ ����������� ������� ������ - ", "����������� - " },
        new List<string> { "����� ����� ", "������ ����������� ����� ����� - ", "����������� - " },
        new List<string> { "������ ����� ", "������ ����������� ������ ����� - ", "����������� - " },
        new List<string> { "����� ������ ", "������ ����������� ������ ������ ", "����������� - " },
        new List<string> { "������ ������ ", "������ ����������� ������� ������ ", "����������� - " },
        new List<string> { "��������", "������ ����������� �������� - ", "����������� - " },
        new List<string> { "���", "������ ����������� ���� - ", "����������� - "},
        new List<string> { "����� �����", "������ ����������� ������ ����� - ", "����������� - " },
        new List<string> { "������ �����", "������ ����������� ������� ����� - ", "����������� - " },
        new List<string> { "����� ������", "������ ����������� ������ ����� - ", "����������� - " },
        new List<string> { "������ ������", "������ ����������� ������� ����� - ", "����������� - " },
        new List<string> { "����� �����", "������ ����������� ������ ����� - ", "����������� - " },
        new List<string> { "������ �����", "������ ����������� ������� ����� - ", "����������� - " },
        new List<string> { "����� ������", "������ ����������� ����� ������ - ", "����������� - " },
        new List<string> { "������ ������", "������ ����������� ������ ������ - ", "����������� - " }
    };

    public static List<int> subsequenceEditHumanLimbs = new List<int> { 9, 11, 3, 13, 5, 15, 16, 6, 14, 4, 12, 10 };
    public static List<int> subsequenceEditHumanTorso = new List<int> { 0, 1, 2, 7, 8 };
    //���������� ����� �������������� ������ ��������
    public static List<Vector2> referenceValueHuman = Enumerable.Repeat(Vector2.zero, 17).ToList();

    //������ � �������
    //public static List<string> accesColorPanel = new List<string> { "head", "neck", "chest", "brushL", "brushR", "kneeL", "kneeR" };
    public static List<string> accesColorPanel = new List<string> { "headButton", "neckButton", "chestButton", "brushLButton", "brushRButton", "kneeLButton", "kneeRButton", "loinButton", "pelvisButton", "shoulderLButton", "shoulderRButton", "elbowLButton", "elbowRButton", "hipLButton", "hipRButton", "shinLButton",  "shinRButton" };
    public static List<string> savetyIcons = new List<string> {"beltSavety", "pillowSavety" };

    //������������ ������ �������, ������ ��� ������ ���������� � �����������
    public static List<string> listPanelStatsHuman = new List<string> { "damageInfo", "statsDamage", "damageOutput" };

    //������� ����������� �� ����� ���� �� 0 �� 4. 3 - 2 - 1 - 0 ��� �����������, � 4 ��� ����������� ���
    public static List<int> levelDamagePartBody = Enumerable.Repeat(0, 17).ToList();

    //����������� �������� � ��, �� � ������ ��� ��
    public static List<float> damagePartBodyKG = Enumerable.Repeat(0f, 17).ToList();

    //����� ���������
    static List<Color> damageColors = new List<Color>
        {
            new Color(0f, 0.68f, 0f),      //��� ����������� (Ǹ����)
            new Color(1f, 0.55f, 0f),  // ˸���� ����������� (�����-������)
            new Color(0.8f, 0.3f, 0f),    // ����� ������ (���������)
            new Color(1f, 0f, 0f),   // ������ (�����-���������)
            new Color(0.5f, 0f, 0f)   // ����� ������ (�������)
        };

    //                    ���   ���   ���   ��   ��    ��   ��
    public static List<List<float>> matrixMultiplyBelt = new List<List<float>>
    {
        new List<float> { 1f, 1f, 0.6f, 1f, 0.9f, 1f, 0.9f, 0.6f, 0.6f, 0.6f, 0.6f, 1f, 1f, 0.6f, 0.6f, 1f, 1f },       //���� ����� �������
        new List<float> { 1f, 1f, 0.6f, 0.9f, 1f, 0.9f, 1f, 0.6f, 0.6f, 0.6f, 0.6f, 1f, 1f, 0.6f, 0.6f, 1f, 1f },       //���� ������ �������
        new List<float> { 1f, 1f, 0.6f, 1f, 1f, 1f, 1f, 0.6f, 0.6f, 0.6f, 0.6f, 1f, 1f, 0.6f, 0.6f, 1f, 1f },           //���� �������� �������
        new List<float> { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f },             //���� ����� �����
        new List<float> { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f },             //���� ������ �����
        new List<float> { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f },             //���� �������� �����
        new List<float> { 1f, 1f, 0.8f, 1f, 0.7f, 1f, 0.8f, 0.7f, 0.8f, 0.8f, 0.8f, 1f, 0.7f, 1f, 0.7f, 1f, 0.7f },       //���� ����� ����� �������
        new List<float> { 1f, 1f, 0.9f, 0.8f, 0.9f, 0.8f, 1f, 0.8f, 0.9f, 0.9f, 0.9f, 0.7f, 1f, 0.7f, 1f, 0.7f, 1f },     //���� ����� ����� �����
        new List<float> { 1f, 1f, 0.9f, 0.6f, 0.9f, 0.8f, 1f, 0.8f, 0.9f, 0.9f, 0.9f, 0.7f, 1f, 0.7f, 1f, 0.7f, 1f },     //���� ������ ����� �������
        new List<float> { 1f, 1f, 0.8f, 1f, 0.7f, 1f, 0.8f, 0.7f, 0.8f, 0.8f, 0.8f, 1f, 0.7f, 1f, 0.7f, 1f, 0.7f }        //���� ������ ����� �����
    };


    public static List<List<float>> matrixMultiplyPillow = new List<List<float>>
    {
        new List<float> { 0.5f, 0.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 0.5f, 0.5f, 1f, 1f, 1f, 1f, 1f, 1f },         //���� ����� �������
        new List<float> { 0.5f, 0.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 0.5f, 0.5f, 1f, 1f, 1f, 1f, 1f, 1f },         //���� ������ �������
        new List<float> { 0.5f, 0.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 0.5f, 0.5f, 1f, 1f, 1f, 1f, 1f, 1f },         //���� �������� �������
        new List<float> { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f },         //���� ����� �����
        new List<float> { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f },         //���� ������ �����
        new List<float> { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f },         //���� �������� �����
        new List<float> { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f },             //���� ����� ����� �������
        new List<float> { 0.7f, 0.7f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f },         //���� ����� ����� �����
        new List<float> { 0.7f, 0.7f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f },         //���� ������ ����� �������
        new List<float> { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }              //���� ������ ����� �����
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



    //��������� ������, ���, ������� ����� � ��� �����
    public static float headStability = 1;
    public static float neckStability = 1;
    public static float chestStability = 1;
    public static float handStabilityL = 1;
    public static float handStabilityR = 1;
    public static float legStabilityL = 1;
    public static float legStabilityR = 1;


    public static List<float> halfHuman = new List<float> { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };


    //��������� ����
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
    //���
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


    //������������ - �������� ������� ������������ � ����� ������������
    //������� ������, ����� �������
    public static List<bool> variantsSavty = new List<bool> { false, false};


    public static int locationDamage;

    //���� ����� ����� ����
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
        nameListRender.RenderNameList(damageList); // ������� ������ ��������, � �� �����
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
        // � ��������� ��������� � ����� ����� � ��������
        folderPath = Path.Combine(Directory.GetParent(Application.dataPath).FullName, "Exports");
#else
    // � ����� (���������) ��������� � �� �� ����� Data, ��� � ����
    string root = Directory.GetParent(Application.dataPath).FullName;
    folderPath = Path.Combine(root, "Data");

#endif

        // ��������, ��� ����� ����������
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        string filePath = Path.Combine(folderPath, "DamageData.csv");
        SetDebugText(filePath);

        nameListRender.ExportToCSV(damageList, filePath);

        
    }




    public static void SetDebugText(string newText)
    {
        // ���� ������ �� ����� (���� �������!)
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
                Debug.LogWarning("��������� Text �� ������ �� ������� textDebugTest");
            }
        }
        else
        {
            Debug.LogWarning("������ textDebugTest �� ������ � �����");
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
        GameObject canvas = GameObject.Find("canvasSavetyCar");  // ����� ������ Canvas
        if (canvas != null)
        {
            Transform iconsTransform = canvas.transform.Find("savetyIcons");  // ����� ������ �� �����

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
            Debug.LogError("�� ������� ����� ������ Canvas");
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
        GameObject canvas = GameObject.Find("uiMessage");  // ����� ������ Canvas

        if (canvas != null)
        {
            Transform panelTransform = canvas.transform.Find(accesColorPanel[currentValue]);  // ����� ������ �� �����

            Button panelButton = panelTransform.GetComponent<Button>();
            if (panelButton != null)
            {

                // ������ �������
                RectTransform rectTransform = panelTransform.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    referenceValueHuman[currentValue] = rectTransform.anchoredPosition;
                    rectTransform.anchoredPosition = new Vector2(posX, posY); // ������ ����� �������
                }
            }
            

        }
        else
        {
            Debug.Log("������ �� ������");
        }
    }

    public static void returnPanelButton()
    {

        GameObject canvas = GameObject.Find("uiMessage");  // ����� ������ Canvas

        if (canvas != null)
        {
            for (int i = 0; i < accesColorPanel.Count; i++)
            {
                Transform panelTransform = canvas.transform.Find(accesColorPanel[i]);  // ����� ������ �� �����

                Button panelButton = panelTransform.GetComponent<Button>();
                if (panelButton != null)
                {

                    // ������ �������
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



        GameObject canvas = GameObject.Find("canvasSavetyCar");  // ����� ������ Canvas
        if (canvas != null)
        {
            Transform panelTransform = canvas.transform.Find("savetyPanel");  // ����� ������ �� �����
            if (panelTransform != null)
            {
                panelTransform.gameObject.SetActive(true);  // �������� ������
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                
            }
            else
            {
                Debug.LogError("�� ������� ����� ������ � Canvas");
            }
        }
        else
        {
            Debug.LogError("�� ������� ����� ������ Canvas");
        }

    }

    public static void activateMessagePanel(int numberPartBody, int numberColor)
    {
        

        GameObject canvas = GameObject.Find("uiMessage");  // ����� ������ Canvas
        if (canvas != null)
        {
            
            Transform panelTransform = canvas.transform.Find(accesColorPanel[numberPartBody]);  // ����� ������ �� �����
            
            Button panelButton = panelTransform.GetComponent<Button>();
            if (panelButton != null)
            {

                

                Image buttonImage = panelButton.GetComponent<Image>();
                if (buttonImage != null)
                {
                    buttonImage.color = damageColors[numberColor];  // ������ ���� ���� ������
                }
            }
            else
            {
                Debug.LogError("�� ������� ����� ������ � Canvas");
            }
        }
        else
        {
            Debug.LogError("�� ������� ����� ������ Canvas");
        }

    }


    private static void callingMainFunction(int number, float gForce)
    {

        //���������� ��������� �������� �� ��������� ������� ������������, �� ��������� ����� ������������
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
                Debug.Log("���� ����� �������");
                informationDamage += "���� ����� �������       ";

                callingMainFunction(0, gForce);
                


                break;
            case 2:
                Debug.Log("���� ������ �������");
                informationDamage += "���� ������ �������      ";

                callingMainFunction(1, gForce);
                
                break;
            case 3:
                Debug.Log("���� �������� �������");
                informationDamage += "���� �������� �������    ";
                                      
                callingMainFunction(2, gForce);
                
                break;
            case 4:
                Debug.Log("���� ����� �����");
                informationDamage += "���� ����� �����         ";
                                     
                callingMainFunction(3, gForce);
                
                break;
            case 5:
                Debug.Log("���� ������ �����");
                informationDamage += "���� ������ �����        ";
                                      
                callingMainFunction(4, gForce);
                
                break;
            case 6:
                Debug.Log("���� �������� �����");
                informationDamage += "���� �������� �����      ";
                                      
                callingMainFunction(5, gForce);
                
                break;
            case 7:
                Debug.Log("���� ����� ����� �������");
                informationDamage += "���� ����� ����� ������� ";
                                      
                callingMainFunction(6, gForce);
                
                break;
            case 8:
                Debug.Log("���� ����� ����� �����");
                informationDamage += "���� ����� ����� �����   ";
                                      
                callingMainFunction(7, gForce);
                
                break;
            case 9:
                Debug.Log("���� ������ ����� �������");
                informationDamage += "���� ����� ������ �������";
                callingMainFunction(8, gForce);
                
                break;
            default:
                Debug.Log("���� ������ ����� �����");
                informationDamage += "���� ����� ������ �����  ";
                callingMainFunction(9, gForce);
               
                break;
        }
        //textOutput("damageCharacteristics", "���������� �������� - ", allDamageParts);
        informationDamage += ". ���������� � G ����� " + gForce;
        informationDamage += ". ���������� �� �� ���� � �� ����� " + gForce * humanWeight + ".";
        informationDamage += "\n�������� �� ����� " + speedTo + "��/�. �������� �� ����� ����� " + speedAt + "��/�.";
        textOutput("fullInformationDamage", "�������� ", informationDamage);
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

        Debug.Log("��������� " + multiplyDamage + " ���������� �� ��� ����� ���� " + weightHuman[number]);

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
        Debug.Log("������� ������������");
        
 
    }

}
