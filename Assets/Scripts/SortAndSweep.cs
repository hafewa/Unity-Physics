using System.Collections.Generic;
using UnityEngine;

public class SortAndSweep : MonoBehaviour
{
    public List<CheckCollision.ReportedPair> CurrentPairs;
    private List<CheckCollision.ReportedPair> _previousPairs;
    public GameEvent OnCollisionEnter;
    private GameObject _xCollision, _yCollision;

    // Use this for initialization
    public void Start()
    {
        CurrentPairs = new List<CheckCollision.ReportedPair>();
        _previousPairs = new List<CheckCollision.ReportedPair>();
        _xCollision = GameObject.FindGameObjectWithTag("XCol");
        _yCollision = GameObject.FindGameObjectWithTag("YCol");
    }

    // Update is called once per frame
    public void Update()
    {
        CurrentPairs.Clear();
        var xColList = _xCollision.GetComponent<CheckCollision>().PairsList;
        var yColList = _yCollision.GetComponent<CheckCollision>().PairsList;

        foreach (var xcols in xColList)
            if (yColList.Contains(xcols) ||
                yColList.Contains(new CheckCollision.ReportedPair {Object1 = xcols.Object2, Object2 = xcols.Object1}))
            {
                CurrentPairs.Add(xcols);
                OnCollisionEnter.Raise();
            }
        foreach (var p in _previousPairs)
        {
            if(!CurrentPairs.Contains(p))
                OnCollisionEnter.Raise();
        }
        _previousPairs.Clear();
        _previousPairs.AddRange(CurrentPairs);
    }
}