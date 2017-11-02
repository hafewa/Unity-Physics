﻿using System;
using System.Collections.Generic;
using System.Linq;
using Donray;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    public enum AXIS
    {
        X,
        Y
    }

    public AXIS AxisCheck;
    public List<AABB> AxisList;
    public List<ReportedPair> PairsList;

    public void Start()
    {
        AxisList = new List<AABB>();
        PairsList = new List<ReportedPair>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        PairsList.Clear();
        //Fill a list with all objects in the world
        AxisList = FindObjectsOfType<AABB>().ToList();

        foreach (var colliderB in AxisList)
            colliderB.UpdateAabb(colliderB.Width, colliderB.Height);

        //Sort the list
        if (AxisCheck == AXIS.X)
            AxisList.Sort((ob1, ob2) => ob1.Min.x.CompareTo(ob2.Min.x));
        else
            AxisList.Sort((ob1, ob2) => ob1.Min.y.CompareTo(ob2.Min.y));
        //Create new active list and add the first item from AxisList
        var activeList = new List<AABB> {AxisList[0]};
        for (var i = 1; i < AxisList.Count; i++)
        {
            var tempList = new List<AABB>();

            if (activeList.Count == 0)
                activeList.Add(AxisList[i - 1]);

            tempList.AddRange(activeList);

            foreach (var boxColliderD in activeList)
            {
                var check = AxisCheck == AXIS.X
                    ? AxisList[i].Min.x > boxColliderD.Max.x
                    : AxisList[i].Min.y > boxColliderD.Max.y;

                if (check)
                {
                    tempList.Remove(boxColliderD);
                }
                else
                {
                    //Add newItem to ActiveList
                    tempList.Add(AxisList[i]);
                    var pair = new ReportedPair
                    {
                        Object1 = boxColliderD,
                        Object2 = AxisList[i]
                    };
                    //Add Pair to pairsList
                    PairsList.Add(pair);
                }
            }
            activeList.Clear();
            activeList.AddRange(tempList);
        }
        if (PairsList.Count == 0)
            return;
        Debug.Log(PairsList.Count);
    }

    [Serializable]
    public struct ReportedPair
    {
        public AABB Object1;
        public AABB Object2;
    }
}
//var axis = new List<BoxColliderD>();
//axis.Sort((a, b) => a.collider.Min.y.CompareTo(b.collider.Min.y));
//foreach (var p in pairsList)
//{
//    if (axis.Contains(p.Object1))
//        axis.Add(p.Object1);
//    if (axis.Contains(p.Object2))
//        axis.Add(p.Object2);
//}

//activeList = new List<BoxColliderD> {AxisList[0]};
//for (var i = 1; i < axis.Count; i++)
//{
//    var tempList = new List<BoxColliderD>();

//    tempList.AddRange(activeList);
//    foreach (var boxColliderD in activeList)
//        //if newItem.Left > currentItem.right ->Remove currentItem
//        if (axis[i].collider.Min.y > boxColliderD.collider.Max.y)
//            tempList.Remove(boxColliderD);
//        else
//        {
//            //Add newItem to ActiveList
//            tempList.Add(axis[i]);
//            var pair = new ReportedPair
//            {
//                Object1 = boxColliderD,
//                Object2 = axis[i]
//            };
//            //Add Pair to pairsList
//            pairsList.Add(pair);
//        }
//    activeList.Clear();
//    activeList.AddRange(tempList);
//}