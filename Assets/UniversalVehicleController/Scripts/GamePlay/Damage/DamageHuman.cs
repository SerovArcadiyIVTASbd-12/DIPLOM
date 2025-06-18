using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using TMPro;


namespace PG
{
    public class DamageHuman : MonoBehaviour
    {
        public static DatabaseHandler dbHandler;


        public static int numberSolution = 0;
        public static List<List<float>> damageData = new List<List<float>>();
        public static Vector3 localPointForList;
        public static float humanWeight = 70.0f;
        public static float localx;
        public static float localy;
        public static float localz;
        public static int lastSpeed;
        public static int realSpeed;
        public static int realTest;
        public static int templastSpeed;
        public static int temprealSpeed;
        static float impactTime = 0.02f;

        //Список сторон удара, чтобы не было перегрузки
        public static List<string> damageSideList = new List<string>
        {
            "Удар слева спереди",
            "Удар справа спереди",
            "Удар середина спереди",
            "Удар слева сзади",
            "Удар справа сзади",
            "Удар середина сзади",
            "Удар сбоку слева спереди",
            "Удар сбоку слева сзади",
            "Удар сбоку справа спереди",
            "Удар сбоку справа сзади"
        };


        public static bool activateShemHuman = true;
        private static bool readyPanel = false;


        public static void activateBoolStatusShemHuman()
        {
            activateShemHuman = true;
            Debug.Log("Значение булевого значения " + activateShemHuman);
        }
        public static void impactDetection(int maxIndex)
        {
            
            if (damageData[0][2] < -0.5f && damageData[0][4] > 0.5)
            {
                numberSolution = 1;
            }
            else if (damageData[0][2] > 0.5f && damageData[0][4] > 0.5)
            {
                numberSolution = 2;
            }
            else if (damageData[0][2] > -0.5f && damageData[0][2] < 0.5f && damageData[0][4] > 0.5)
            {
                numberSolution = 3;
            }
            else if (damageData[0][2] < -0.5f && damageData[0][4] < -1)
            {
                numberSolution = 4;
            }
            else if (damageData[0][2] > 0.5f && damageData[0][4] < -1)
            {
                numberSolution = 5;
            }
            else if (damageData[0][2] > -0.5f && damageData[0][2] < 0.5f && damageData[0][4] < -1)
            {
                numberSolution = 6;
            }
            else if (damageData[0][2] <= -0.9f && damageData[0][4] > 0 && damageData[0][4] < 1.5f)
            {
                numberSolution = 7;
            }
            else if (damageData[0][2] <= -0.9f && damageData[0][4] < 0 && damageData[0][4] < -1.5f)
            {
                numberSolution = 8;
            }
            else if (damageData[0][2] >= 0.9f && damageData[0][2] > 0 && damageData[0][4] < 1.5f)
            {
                numberSolution = 9;
            }
            else if (damageData[0][2] >= 0.9f && damageData[0][4] < 0 && damageData[0][4] < -1.5f)
            {
                numberSolution = 10;
            }

            

        }
        public static void getNowSpeed()
        {
            CarStateUI carUI = FindObjectOfType<CarStateUI>(); // Найти объект с этим скриптом
            int speed = carUI.getSpeed();
            realSpeed = speed;
            carUI.startingTimerCrash();
        }
        
        public static int getSizeDamageList()
        {
            return damageData.Count;
            
        }

        public static void offText()
        {
            GameObject canvas = GameObject.Find("messageForDamage");
            if (canvas != null)
            {
                Transform textTransform = canvas.transform.Find("messageText");
                if (textTransform != null)
                {
                    textTransform.gameObject.SetActive(false); // Включаем текстовый объект
                }
            }
        }

        public static void finalCapture()
        {

            GameObject canvas = GameObject.Find("messageForDamage");
            if (canvas != null)
            {
                Transform textTransform = canvas.transform.Find("messageText");
                if (textTransform != null)
                {
                    textTransform.gameObject.SetActive(true); // Включаем текстовый объект
                }
            }

            Debug.Log("После секунды вызов");

            float maxG = float.MinValue;
            int maxIndex = -1;


            Debug.Log("Вывод количесва элементов" + damageData.Count);

            for (int i = 0; i < damageData.Count; i++)
            {
                if (damageData[i][0] > maxG)
                {
                    maxG = damageData[i][0];
                    maxIndex = i;
                }

            }
            
            List<float> singleImpact = calkForce();

            Debug.Log("Первый элеменет " + damageData[0][1] + "Последний элемент " + damageData[damageData.Count - 1][1]);

            Debug.Log("Скорость до удара " + singleImpact[2] + "       Скорость при ударе " + singleImpact[1] + "\nПерегрузка в G " + singleImpact[0] + "     Эквивалент в кг на человека " + singleImpact[3] + "кг");
            impactDetection(maxIndex);
            Debug.Log("Координаты повреждения x" + damageData[0][2] + " z " + damageData[0][4]);


            readyPanel = true;
            SkeletDamage.caseSolution(humanWeight, numberSolution, singleImpact[0], singleImpact[3], singleImpact[2], singleImpact[1]);

            dbHandler = FindObjectOfType<DatabaseHandler>();

            float speedCar;
            if (singleImpact[2] - singleImpact[1] < 0)
            {
                speedCar = singleImpact[2];
            }
            else
            {
                speedCar = singleImpact[2] - singleImpact[1];
            }

            dbHandler.InitDatabase(damageSideList[numberSolution - 1], singleImpact[0], speedCar, SkeletDamage.getDamageLevelList(), SkeletDamage.getdamagePartBodyKG());
            
            damageData.Clear();
        }

        public static void deleteAllData()
        {
            dbHandler = FindObjectOfType<DatabaseHandler>();
            dbHandler.ClearDatabase();
        }
        public static void activateMessagePanel()
        {
            if (!readyPanel) return;

            

            GameObject canvas = GameObject.Find("messageForDamage");  // Найди объект Canvas
            Canvas canvasComponent = canvas.GetComponent<Canvas>();
            if (canvasComponent != null)
            {
                canvasComponent.sortingOrder = 0;
            }
            else
            {
                Debug.LogWarning("Компонент Canvas не найден у объекта messageForDamage");
            }
            GameObject canvasHuman = GameObject.Find("uiMessage");
            Canvas canvasComponentHuman = canvasHuman.GetComponent<Canvas>();
            if (canvasComponentHuman != null)
            {
                canvasComponentHuman.sortingOrder = 1;
            }
            else
            {
                Debug.LogWarning("Компонент CanvasHuman не найден у объекта messageForDamage");
            }
            if (canvas != null)
            {
                Transform panelTransform = canvas.transform.Find("panelMessageDamage");  // Найди панель по имени
                if (panelTransform != null)
                {
                    TMP_Text headText = panelTransform.Find("damageInfo")?.GetComponent<TMP_Text>();
                    if (headText != null)
                    {
                        offText();
                        panelTransform.gameObject.SetActive(true);  // Включаем панель
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        


                        if (activateShemHuman)
                        {
                            SkeletDamage.algoritmMoveParts();
                            activateShemHuman = false;
                        }
                    }
                    else
                    {
                        Debug.Log("Данные пустые");
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

        private static List<float> calkForce()
        {
            float lastSpeedMS = damageData[0][1] / 3.6f;
            float realSpeedMS = damageData[damageData.Count - 1][1] / 3.6f;

            // Ускорение (м/с²)
            float acceleration = (lastSpeedMS - realSpeedMS) / impactTime;

            // Перегрузка в G
            float gForce = acceleration / 9.81f;

            float forceInKg = gForce * humanWeight;
            List<float> singleImpact = new List<float>
            {
                Mathf.Abs(gForce),
                damageData[damageData.Count - 1][1],
                damageData[0][1],
                forceInKg
            };
            return singleImpact;
        }
        public static void transformToG()
        {
            // Перевод скорости из км/ч в м/с
            float lastSpeedMS = lastSpeed / 3.6f;
            float realSpeedMS = realSpeed / 3.6f;

            // Ускорение (м/с²)
            float acceleration = (lastSpeedMS - realSpeedMS) / impactTime;

            // Перегрузка в G
            float gForce = acceleration / 9.81f;

            float mass = 70f; // масса в килограммах
            float forceInKg = gForce * mass;


            
            localx = localPointForList.x;
            localy = localPointForList.y;
            localz = localPointForList.z;

            List<float> singleImpact = new List<float>
            {
                Mathf.Abs(gForce),
                realSpeed,
                localx,
                localy,
                localz
            };

            damageData.Add(singleImpact);

        }
        public static void setCoordinate(Vector3 localImpactPoint)
        {
            localPointForList = localImpactPoint;
            getNowSpeed();
            transformToG();

        }
        public static void  setValue(int realTemp)
        {
            lastSpeed = realTest;

            realTest = realTemp;

        }

    }

}
