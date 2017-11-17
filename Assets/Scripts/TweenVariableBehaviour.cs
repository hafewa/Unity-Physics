using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TweenVariableBehaviour : MonoBehaviour {

    public FloatVariable fVar;
    public float val;
    public AnimationCurve tween;
    public float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;
        val = tween.Evaluate(timer);
        fVar.Value = tween.Evaluate(timer) * 10;

    }

    
}
