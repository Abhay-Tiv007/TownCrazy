using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCarAtLevelStart : MonoBehaviour
{
    private CarController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindWithTag("Player").GetComponent<CarController>();
    }

    public void ActivateCar()
    {
        controller.ActivateCar();
    }
}
