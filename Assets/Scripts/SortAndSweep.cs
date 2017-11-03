using System.Collections.Generic;
using UnityEngine;

public class SortAndSweep : MonoBehaviour
{
    public List<CheckCollision.ReportedPair> Collision;

    private GameObject _xCollision, _yCollision;

    // Use this for initialization
    public void Start()
    {
        Collision = new List<CheckCollision.ReportedPair>();
        _xCollision = GameObject.FindGameObjectWithTag("XCol");
        _yCollision = GameObject.FindGameObjectWithTag("YCol");
    }

    // Update is called once per frame
    public void Update()
    {
        Collision.Clear();

        var xColList = _xCollision.GetComponent<CheckCollision>().PairsList;
        var yColList = _yCollision.GetComponent<CheckCollision>().PairsList;

        foreach (var xcols in xColList)
            if (yColList.Contains(xcols) || yColList.Contains(new CheckCollision.ReportedPair { Object1 = xcols.Object2, Object2 = xcols.Object1 }))
                Collision.Add(xcols);
    }
}