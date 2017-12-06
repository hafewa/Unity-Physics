using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HookesLaw
{
    public class DriverUIBehaviour : MonoBehaviour
    {
        public Toggle GravityToggle, WindToggle, TopLeftToggle, TopRightToggle, BotLeftToggle, BotRightToggle;

        public SpringDriver Driver;

        // Use this for initialization
        void Start()
        {
            Driver = FindObjectOfType<SpringDriver>();
            Driver.LockBotLeft = true;
            Driver.LockTopLeft = true;
            Driver.ApplyWind = true;
            Driver.GravityAll = true;
            GravityToggle.isOn = Driver.GravityAll;
            WindToggle.isOn = Driver.ApplyWind;
            TopLeftToggle.isOn = Driver.LockTopLeft;
            TopRightToggle.isOn = Driver.LockTopRight;
            BotLeftToggle.isOn = Driver.LockBotLeft;
            BotRightToggle.isOn = Driver.LockBotRight;
        }

        // Update is called once per frame
        void Update()
        {
            Driver.GravityAll = GravityToggle.isOn;
            Driver.ApplyWind = WindToggle.isOn;
            Driver.LockTopLeft = TopLeftToggle.isOn;
            Driver.LockTopRight = TopRightToggle.isOn;
            Driver.LockBotLeft = BotLeftToggle.isOn;
            Driver.LockBotRight = BotRightToggle.isOn;
        }
    }
}