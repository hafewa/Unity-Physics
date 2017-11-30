using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvoidButton : MonoBehaviour {
    public static bool ToggleAvoidBool;

    public Text AvoidText;
    // Use this for initialization
    void Start () {
        AvoidText.text = "Avoid Object = Off";
        ToggleAvoidBool = false;

    }
    public void ToggleAvoid()
    {
        ToggleAvoidBool = !ToggleAvoidBool;
        AvoidText.text = ToggleAvoidBool == false ? "Avoid Object = Off" : "Avoid Object = On";
    }
}
