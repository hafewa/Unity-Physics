using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryBehaviour : MonoBehaviour {
    public FloatVariable boundaryDistance;
	// Update is called once per frame
	void Update ()
    {
        var left = Vector3.left * boundaryDistance.Value;
        var top = Vector3.up * boundaryDistance.Value;
        var bottom = Vector3.down * boundaryDistance.Value;
        var right = Vector3.right * boundaryDistance.Value;
        Debug.DrawLine(Vector3.zero, bottom);
        Debug.DrawLine(Vector3.zero, top);
        Debug.DrawLine(Vector3.zero, left);
        Debug.DrawLine(Vector3.zero, right);


    }
}
