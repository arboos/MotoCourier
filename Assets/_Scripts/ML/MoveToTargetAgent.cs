using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Random = UnityEngine.Random;

public class MoveToTargetAgent : Agent
{
    [SerializeField] private Transform target;

    public float lastPosDistance;
    public float currentPosDistance;
    private Vector3 lastPosition;
    
    private EnvHelper envHelper;

    private PrometeoCopController carController;

    public float forward;
    public float backward;
    public float right;
    public float left;

    private void Start()
    {
        envHelper = GetComponentInParent<EnvHelper>();
        carController = GetComponent<PrometeoCopController>();
        lastPosition = transform.position;
    }
    
    public override void OnEpisodeBegin()
    { 
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        
        int spawnA = Random.Range(0, envHelper.copSpawnPos.Length);
        
        transform.position = envHelper.copSpawnPos[spawnA].position;
        
        target.transform.position = envHelper.playerSpawnPos[Random.Range(0, envHelper.playerSpawnPos.Length)].position;
    }
    
    public override void OnActionReceived(ActionBuffers actions)
    {
        print("OnActionRecieved();");
        forward = actions.ContinuousActions[0];
        backward = actions.ContinuousActions[1];
        right = actions.ContinuousActions[2];
        left = actions.ContinuousActions[3];

        if (forward > 0f)
        {
            carController.Forward();
        }
        if(backward > 0f) carController.Backward();
        if(right > 0f) carController.TurnRight();
        if(left > 0f) carController.TurnLeft();
        
        if((!(backward > 0f) && !(forward > 0f))){
            carController.ThrottleOff();
        }
        if((!(backward > 0f) && !(forward > 0f)) && !carController.deceleratingCar){
            carController.InvokeRepeating("DecelerateCar", 0f, 0.1f);
            carController.deceleratingCar = true;
        }
        if(!(left > 0f) && !(right > 0f) && carController.steeringAxis != 0f){
            carController.ResetSteeringAngle();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        print("Heuristic");
        
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;

        continuousActions[0] = Input.GetKey(KeyCode.W) ? 1f : -1f;
        continuousActions[1] = Input.GetKey(KeyCode.S) ? 1f : -1f;
        continuousActions[2] = Input.GetKey(KeyCode.D) ? 1f : -1f;
        continuousActions[3] = Input.GetKey(KeyCode.A) ? 1f : -1f;
    }
    
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation((Vector3)envHelper.cops[0].position);
        sensor.AddObservation((Vector3)envHelper.cops[1].position);
        sensor.AddObservation((Vector3)envHelper.cops[2].position);
        sensor.AddObservation((Vector3)target.position);
    }

    private void FixedUpdate()
    {
        lastPosDistance = Vector3.Distance(lastPosition, target.position);
        currentPosDistance = Vector3.Distance(transform.position, target.position);
        
        AddReward(lastPosDistance - currentPosDistance);

        lastPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerDamagable"))
        {
            print("TARGET COMPLETE: +500f");
            AddReward(500f);
            EndEpisode();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Cop"))
        {
            print("WALL || COP COLLISION: -10f");
            AddReward(-10f);
            EndEpisode();
        }
    }
}
