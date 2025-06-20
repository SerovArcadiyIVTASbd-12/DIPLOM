﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using System.Linq;
using System.ComponentModel.Design;

namespace PG
{
    public class CarStateUI :InitializePlayer
    {
#pragma warning disable 0649


        public float timerDuration = 1f;
        public int sizeList = 0;
        public bool protectedWright = true;
        public float timer;
        public int count = 0;
        public int lastValue = 0;
        public int realValue;




        public static CarStateUI speedCar { get; private set; }

        [Header("Main info settings")]
        [SerializeField] float UpdateSpeedTime;
        [SerializeField] TextMeshProUGUI CurrentSpeedText;
        [SerializeField] TextMeshProUGUI CurrentGearText;
        [SerializeField] TextMeshProUGUI MeasurementUnits;
        [SerializeField] Image GearCircleImage;
        [SerializeField] Image ArrowImage;
        [SerializeField] Transform ArrowRotateTransform;
        [SerializeField] float ZeroRpmArrowAngle;
        [SerializeField] float MaxRpmArrowAnle;

        [SerializeField] Image FillTachometrImage;
        [SerializeField] float ZeroRpmFill;
        [SerializeField] float MaxRpmFill;

        [SerializeField] Color LowRpmColor;
        [SerializeField] Color MediumRpmColor;
        [SerializeField] Color HighRpmColor;
        [SerializeField] float ChangeColorSpeed = 10f;

        [Header("Turbo")]
        [SerializeField] GameObject TurboStateGO;
        [SerializeField] Transform TurboArrowTransform;
        [SerializeField] float ZeroTurboAngle = 0;
        [SerializeField] float MaxTurboAngle = -90;

        [Header("Boost")]
        [SerializeField] GameObject BoostStateGO;
        [SerializeField] Image BoostFillImage;
        [SerializeField] float ZeroBoostFillAmount = 0;
        [SerializeField] float MaxBoostFillAmount = 1;

        [Header("DashboardIcons")]
        [SerializeField] Image ABSIcon;
        [SerializeField] Image TCSIcon;
        [SerializeField] Image HandbrakeIcon;

        [SerializeField] Image WheelImageRef;
        [SerializeField] Image EngineImage;

        [SerializeField] Color FullHPPartColor;
        [SerializeField] Color HightHPPartColor;
        [SerializeField] Color LowHPPartColor;
        [SerializeField] Color DeadPartColor;
        [SerializeField] float MaxLocalDistanceBetweenWheels = 20;     //For images per pixel.

        [Header("SplitScreen settings")]
        [SerializeField] Vector2 AnchorMinP1 = new Vector2 (1, 0.5f);
        [SerializeField] Vector2 AnchorMaxP1 = new Vector2 (1, 0.5f);
        [SerializeField] Vector2 AnchorPosP1 = new Vector2 (-20, 20);
        [SerializeField] Vector2 AnchorMinP2 = new Vector2 (1, 0);
        [SerializeField] Vector2 AnchorMaxP2 = new Vector2 (1, 0);
        [SerializeField] Vector2 AnchorPosP2 = new Vector2 (-20, 20);
        [SerializeField] float SplitScreenLocalScaleMultiplier = 0.7f;

#pragma warning restore 0649

        int CurrentGear;
        float UpdateSpeedTimer;
        float RPMPercent;
        RpmState CurrentRpmState;
        Coroutine SetColorCoroutine;
        Image[] WheelImages;
        Dictionary<DamageableObject, System.Action<float>> ActionsDict = new Dictionary<DamageableObject, System.Action<float>>();

        const string NeutralGear = "N";
        const string RearGear = "R";

        private void Awake ()
        {
            MeasurementUnits.text = B.GameSettings.EnumMeasurementSystem == MeasurementSystem.KM ? "kmh" : "mph";
        }


        


        private void Update ()
        {

            if (!IsInitialized)
            {
                return;
            }

            UpdateSpeed ();

            if (Car)
            {
                UpdateGear ();
                UpdateTachometr ();
                UpdateColors ();
                UpdateTurbo ();
                UpdateBoost ();
                UpdateDashboard ();
            }
            timer += Time.deltaTime; // Увеличиваем таймер каждую секунду

            if (timer >= 0.1f) // Если прошла 1 секунда
            {
                count++;
                if (count % 2 != 0)
                {
                    lastValue = realValue;
                    if (count > 100)
                    {
                        count = 0;
                    }
                    
                }
                else
                {
                    realValue = Vehicle.SpeedInHour.ToInt();
                }

                PG.DamageHuman.setValue(realValue);

                    // Выводим текст, который меняется каждую секунду
                    //Debug.Log("Скорость " + Vehicle.SpeedInHour.ToInt().ToString());
                    //Debug.Log("Вывод таймер " + timer);
                timer = 0f; // Сбрасываем таймер
            }
        }

        public void startingTimerCrash()
        {
            int number = DamageHuman.getSizeDamageList();
            // Запускаем корутину
            if (protectedWright)
            {
                if ((sizeList != number) || (sizeList == 0))
                {
                    StartCoroutine(CallFunctionAfterDelay());
                    protectedWright = false;
                }
                else
                {
                    sizeList = 0;
                    protectedWright = true;
                    Debug.Log("Все значения получены! " + number);
                    DamageHuman.finalCapture();

                }


                sizeList = number;
                
            }

        }


        private IEnumerator CallFunctionAfterDelay()
        {
            float timer = 0f;

            while (timer < timerDuration)
            {
                timer += Time.deltaTime;
                yield return null; // ждём следующий кадр
            }

            protectedWright = true;
            Debug.Log("Продление времени!");
            startingTimerCrash();
        }
        //IEnumerator CallFunctionAfterDelay()
        //{
        //    yield return new WaitForSeconds(1f); // ждём 1 секунду
        //    protectedWright = true;
        //    Debug.Log("Продление времени!");
        //    startingTimerCrash();
        //    //PG.DamageHuman.finalCapture();
        //}







        public int getSpeed()
        {
            return Vehicle.SpeedInHour.ToInt();
        }

        private void OnDestroy ()
        {
            foreach (var kv in ActionsDict)
            {
                kv.Key.OnChangeHealthAction -= kv.Value;
            }
        }

        public override bool Initialize (VehicleController vehicle)
        {
            base.Initialize (vehicle);
            TurboStateGO.SetActive (Car != null && Car.Engine.EnableTurbo);
            BoostStateGO.SetActive (Car != null && Car.Engine.EnableBoost);

            System.Action<float> action;
            float maxDistance = 0;
            WheelImages = new Image[Vehicle.Wheels.Length];
            WheelImageRef.SetActive (true);

            for (int i = 0; i < Vehicle.Wheels.Length; i++)
            {
                var wheel = Vehicle.Wheels[i];
                var wheelImage = Instantiate (WheelImageRef, WheelImageRef.transform.parent);
                wheelImage.name = string.Format ("{0}_{1}", WheelImageRef.name, i);
                WheelImages[i] = wheelImage;
                action = (float value) => OnChangeHealthPart (wheel, wheelImage);
                action.SafeInvoke (0);
                Vehicle.Wheels[i].OnChangeHealthAction += action;
                ActionsDict.Add (wheel, action);

                if (Vehicle.Wheels[i].LocalPositionOnAwake.magnitude > maxDistance)
                {
                    maxDistance = Vehicle.Wheels[i].LocalPositionOnAwake.magnitude;
                }
            }

            WheelImageRef.SetActive (false);

            for (int i = 0; i < Vehicle.Wheels.Length; i++)
            {
                Vector2 localPos = new Vector2(Vehicle.Wheels[i].LocalPositionOnAwake.x, Vehicle.Wheels[i].LocalPositionOnAwake.z) *  (MaxLocalDistanceBetweenWheels / maxDistance);
                WheelImages[i].rectTransform.localPosition = localPos;
            }

            if (Car && Car.EngineDamageableObject)
            {
                action = (float value) => OnChangeHealthPart (Car.EngineDamageableObject, EngineImage);
                action.SafeInvoke (0);
                Car.EngineDamageableObject.OnChangeHealthAction += action;
                ActionsDict.Add (Car.EngineDamageableObject, action);
            }
            else
            {
                EngineImage.SetActive (false);
            }

            if (GameController.SplitScreen)
            {
                var rectTR = GetComponent<RectTransform>();
                if (GameController.PlayerCar1 == Car)
                {
                    rectTR.anchorMin = AnchorMinP1;
                    rectTR.anchorMax = AnchorMaxP1;
                    rectTR.anchoredPosition = AnchorPosP1;
                }
                else
                {
                    rectTR.anchorMin = AnchorMinP2;
                    rectTR.anchorMax = AnchorMaxP2;
                    rectTR.anchoredPosition = AnchorPosP2;
                }

                transform.localScale = Vector3.one * SplitScreenLocalScaleMultiplier;
                
            }

            Color color;

            if (HandbrakeIcon)
            {
                color = HandbrakeIcon.color;
                color.a = 0;
                HandbrakeIcon.color = color;
            }

            if (ABSIcon)
            {
                color = ABSIcon.color;
                color.a = 0;
                ABSIcon.color = color;
            }

            if (TCSIcon)
            {
                color = TCSIcon.color;
                color.a = 0;
                TCSIcon.color = color;
            }

            return IsInitialized;
        }

        public override void Uninitialize ()
        {
            foreach (var kv in ActionsDict)
            {
                kv.Key.OnChangeHealthAction -= kv.Value;
            }

            foreach (var wheelImage in WheelImages)
            {
                if (wheelImage)
                {
                    Destroy (wheelImage.gameObject);
                }
            }

            WheelImages = new Image [0];

            ActionsDict.Clear ();

            base.Uninitialize ();
        }

        /// <summary>
        /// Update speed text, with a small interval between updates (To prevent flickering).
        /// </summary>
        void UpdateSpeed ()
        {
            if (UpdateSpeedTimer <= 0)
            {
                CurrentSpeedText.text = Vehicle.SpeedInHour.ToInt ().ToString ();
                UpdateSpeedTimer = UpdateSpeedTime;
            }
            else
            {
                UpdateSpeedTimer -= Time.deltaTime;
            }
            //Debug.Log("Вывод скорости " + Vehicle.SpeedInHour.ToInt().ToString());
        }

        void UpdateGear ()
        {
            var currentGear = Car.InChangeGear? 0: Car.CurrentGear;

            if (CurrentGear != currentGear)
            {
                CurrentGear = currentGear;
                if (CurrentGear < 0)
                {
                    CurrentGearText.text = RearGear;
                }
                else if (CurrentGear == 0)
                {
                    CurrentGearText.text = NeutralGear;
                }
                else
                {
                    CurrentGearText.text = CurrentGear.ToString();
                }
            }
        }

        /// <summary>
        /// Update tachometer arrow position.
        /// </summary>
        void UpdateTachometr ()
        {
            RPMPercent = Mathf.Lerp (RPMPercent, Car.EngineRPM / Car.Engine.MaxRPM, Time.deltaTime * 15);

            var arrowAngle = Mathf.Lerp(ZeroRpmArrowAngle, MaxRpmArrowAnle, RPMPercent);
            ArrowRotateTransform.localRotation = Quaternion.AngleAxis (arrowAngle, Vector3.forward);

            var fill = Mathf.Lerp(ZeroRpmFill, MaxRpmFill, RPMPercent);
            FillTachometrImage.fillAmount = fill;
        }

        /// <summary>
        /// Coloring tachometer arrow and ring around gear in RPM-dependent colors.
        /// </summary>
        void UpdateColors ()
        {
            RpmState newState =
                Car.EngineRPM > Car.Engine.CutOffRPM - 300?
                    RpmState.High:
                    Car.EngineRPM > Car.Engine.CutOffRPM - 1500?
                        RpmState.Medium:
                    RpmState.Low;

            if (newState != CurrentRpmState)
            {
                CurrentRpmState = newState;

                Color targetColor;
                switch (CurrentRpmState)
                {
                    case RpmState.High:
                    targetColor = HighRpmColor;
                    break;
                    case RpmState.Medium:
                    targetColor = MediumRpmColor;
                    break;
                    default:
                    targetColor = LowRpmColor;
                    break;
                }

                if (SetColorCoroutine != null)
                {
                    StopCoroutine (SetColorCoroutine);
                }

                SetColorCoroutine = StartCoroutine (DoSetColor (targetColor));
            }
        }

        void UpdateTurbo ()
        {
            if (!Car.Engine.EnableTurbo)
            {
                return;
            }

            var angle = Mathf.Lerp(ZeroTurboAngle, MaxTurboAngle, Car.CurrentTurbo);
            TurboArrowTransform.localRotation = Quaternion.AngleAxis (angle, Vector3.forward);
        }

        void UpdateBoost ()
        {
            if (!Car.Engine.EnableBoost)
            {
                return;
            }

            var fill = Car.BoostAmount / Car.Engine.BoostAmount;
            BoostFillImage.fillAmount = Mathf.Lerp (ZeroBoostFillAmount, MaxBoostFillAmount, fill);
        }

        void UpdateDashboard ()
        {
            Color color;
            float offSpeed = 10 * Time.deltaTime;
            if (HandbrakeIcon)
            {
                color = HandbrakeIcon.color;
                if (Car.InHandBrake)
                {
                    color.a = 1;
                }
                else
                {
                    color.a = Mathf.MoveTowards (color.a, 0, offSpeed);
                }
                HandbrakeIcon.color = color;
            }

            if (ABSIcon)
            {
                color = ABSIcon.color;
                if (Car.ABSIsActive)
                {
                    color.a = 1;
                }
                else
                {
                    color.a = Mathf.MoveTowards (color.a, 0, offSpeed);
                }
                ABSIcon.color = color;
            }

            if (TCSIcon)
            {
                color = TCSIcon.color;
                if (Car.Steer.TCS > 0 && Car.TCSMultiplayer < 1f)
                {
                    color.a = 1;
                }
                else
                {
                    color.a = Mathf.MoveTowards (color.a, 0, offSpeed);
                }
                TCSIcon.color = color;
            }
        }

        /// <summary>
        /// Smooth color change.
        /// </summary>
        IEnumerator DoSetColor (Color targetColor)
        {
            float t = 0;
            Color startColor = CurrentGearText.color;
            Color currentColor;
            while (t < 1)
            {
                t += Time.deltaTime * ChangeColorSpeed;
                currentColor = Color.Lerp (startColor, targetColor, t);
                CurrentGearText.color = currentColor;
                GearCircleImage.color = currentColor;
                ArrowImage.color = currentColor;

                yield return null;
            }

            CurrentGearText.color = targetColor;
            GearCircleImage.color = targetColor;
            ArrowImage.color = targetColor;

            SetColorCoroutine = null;
        }

        void OnChangeHealthPart (DamageableObject part, Image image)
        {
            if (part.HealthPercent >= 1)
            {
                image.color = FullHPPartColor;
            }
            else if (part.HealthPercent <= 0)
            {
                image.color = DeadPartColor;
            }
            else
            {
                image.color = Color.Lerp(LowHPPartColor, HightHPPartColor, part.HealthPercent);
            }
        }

        enum RpmState
        {
            Low,
            Medium,
            High
        }
    }
}
