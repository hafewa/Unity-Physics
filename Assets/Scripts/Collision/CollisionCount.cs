using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Collision
{
    public class CollisionCount : MonoBehaviour
    {
        public SortAndSweep Sorter;

        public void UpdateText()
        {
            GetComponent<Text>().text = "Colliding Count: " + Sorter.CurrentPairs.Count;
        }
    }
}