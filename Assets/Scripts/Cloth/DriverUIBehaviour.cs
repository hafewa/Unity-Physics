using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HookesLaw
{
    public class DriverUIBehaviour : MonoBehaviour
    {
        public Toggle GravityToggle, WindToggle, TopLeftToggle, TopRightToggle, BotLeftToggle, BotRightToggle;
        public Slider WindSlider, ConstantSlider, DamperSlider, ClothSizeSlider;

        public SpringDriver Driver;

        // Use this for initialization
        void Start()
        {
            Driver = FindObjectOfType<SpringDriver>();
            Driver.LockBotLeft = true;
            Driver.LockTopLeft = true;
            Driver.ApplyWind = true;
            Driver.GravityAll = true;

            #region Toggles
            GravityToggle.isOn = Driver.GravityAll;
            WindToggle.isOn = Driver.ApplyWind;
            TopLeftToggle.isOn = Driver.LockTopLeft;
            TopRightToggle.isOn = Driver.LockTopRight;
            BotLeftToggle.isOn = Driver.LockBotLeft;
            BotRightToggle.isOn = Driver.LockBotRight;
            #endregion

            WindSlider.value = 5;
            ConstantSlider.value = 10;
            DamperSlider.value = .5f;
            ClothSizeSlider.value = Driver.Size;
            Time.timeScale = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = Time.timeScale == 0 ? 1 : 0;
            }

            #region Toggles
            Driver.GravityAll = GravityToggle.isOn;
            Driver.ApplyWind = WindToggle.isOn;
            Driver.LockTopLeft = TopLeftToggle.isOn;
            Driver.LockTopRight = TopRightToggle.isOn;
            Driver.LockBotLeft = BotLeftToggle.isOn;
            Driver.LockBotRight = BotRightToggle.isOn;
            #endregion

            Driver.Size = (int)ClothSizeSlider.value;

            Driver.Wind.z = WindSlider.value;
            Driver.Wind.x = WindSlider.value;
            Driver.ks = ConstantSlider.value;
            Driver.kd = DamperSlider.value;
        }
    }
}