using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public enum ControlMode { Keyboard = 1, Touch = 2 }

namespace UnityStandardAssets.Vehicles.Car
{

public class InputSystem : MonoBehaviour
{
    public ControlMode control;
    CarController Car;

    public float  Accel;
    public float Steer;
    public float Brake;

    public GameObject UI;

    public void AccelInput(float input) {
       Accel = input;
    }

    public void SteerInput(float input) {
       Steer = input;
    }

    public void BrakeInput(float input) {
       Brake = input;
    }

    // Start is called before the first frame update
    void Start()
    {
        Car = GetComponent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (control == ControlMode.Keyboard) {
            Accel = Input.GetAxis("Vertical");
            Steer = Input.GetAxis("Horizontal");
            Brake = Input.GetAxis("Jump");
            UI.SetActive(false);
        } else
        {
            UI.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Car.Move(Steer, Accel, Accel, Brake);
    }
}

}
