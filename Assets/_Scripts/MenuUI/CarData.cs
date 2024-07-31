using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Car", menuName = "Car")]

public class CarData : ScriptableObject
{
    [Header("Data")] 
    public string carName;
    public GameObject carPrefab;

    public string carDescription;
    public string rarity;
    
    public int maxSpeed;
    public int maxReverseSpeed;
    public int accelerationMultiplier;
    public int maxSteeringAngle;
    public int steeringSpeed;
    public int brakeForce;
    public int decelerationMultiplier;
    public int handbrakeDriftMultiplier;
    public Vector3 bodyMassCenter;
    
    /*[Header("Car Structure / Body")] 
    public GameObject body;

    [Header("Car Structure / Wheels")] 
    public GameObject wheel;
    public Vector3 FL_Wheel;
    public Vector3 FR_Wheel;
    public Vector3 RL_Wheel;
    public Vector3 RR_Wheel;
    */
    
}
