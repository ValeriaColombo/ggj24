using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    [SerializeField] private GameObject[] stars;

    public void UpdateValue(int value)
    {
        stars[0].SetActive(true);
        stars[1].SetActive(value > 1);
        stars[2].SetActive(value > 2);
        stars[3].SetActive(value > 3);
        stars[4].SetActive(value > 4);
    }
}
