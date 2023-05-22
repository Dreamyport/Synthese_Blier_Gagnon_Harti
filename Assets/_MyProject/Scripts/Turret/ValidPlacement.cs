using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidPlacement : MonoBehaviour
{
    [SerializeField] private GameObject[] _placements;

    public void Activation(bool activer)
    {
        foreach (GameObject placement in _placements)
        {
            placement.SetActive(activer);
        }
    }
}
