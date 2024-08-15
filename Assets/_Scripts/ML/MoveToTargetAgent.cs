using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine.PlayerLoop;
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

    public Rigidbody rb;

    private void Start()
    {
        envHelper = GetComponentInParent<EnvHelper>();
        carController = GetComponent<PrometeoCopController>();
        rb = GetComponent<Rigidbody>();
        lastPosition = transform.position;
    }
    
    public override void OnEpisodeBegin()
    { 
        rb.velocity = Vector3.zero;
        
        int spawnA = Random.Range(0, envHelper.copSpawnPos.Length);
        
        transform.position = envHelper.copSpawnPos[spawnA].position;
        transform.rotation = Quaternion.Euler(0f,Random.Range(0f, 360f), 0f);
        
        target.transform.position = envHelper.playerSpawnPos[Random.Range(0, envHelper.playerSpawnPos.Length)].position;
    }
    
    public override void OnActionReceived(ActionBuffers actions)
    {
        forward = actions.ContinuousActions[0];
        backward = actions.ContinuousActions[1];
        right = actions.ContinuousActions[2];
        left = actions.ContinuousActions[3];
        float none = actions.ContinuousActions[4];

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
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;

        continuousActions[0] = Input.GetKey(KeyCode.W) ? 1f : -1f;
        continuousActions[1] = Input.GetKey(KeyCode.S) ? 1f : -1f;
        continuousActions[2] = Input.GetKey(KeyCode.D) ? 1f : -1f;
        continuousActions[3] = Input.GetKey(KeyCode.A) ? 1f : -1f;
    }
    
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation((Vector3)transform.localPosition);
        sensor.AddObservation((Quaternion)(transform.localRotation));
        sensor.AddObservation((Vector3)target.localPosition);
        sensor.AddObservation((Vector3)rb.velocity);
        
        GetObservationRay(sensor, transform.forward);
        GetObservationRay(sensor, -transform.forward);
        GetObservationRay(sensor, transform.right);
        GetObservationRay(sensor, -transform.right);
        
        GetObservationRay(sensor, transform.right + transform.forward);
        GetObservationRay(sensor, transform.right - transform.forward);
        GetObservationRay(sensor, transform.right + transform.forward);
        GetObservationRay(sensor, transform.right - transform.forward);
    }

    private void GetObservationRay(VectorSensor sensor, Vector3 rayDirection)
    {
        Ray forwardRay = new Ray(transform.position + transform.up, rayDirection * 50f);
        RaycastHit hit;
        
        if (Physics.Raycast(forwardRay, out hit, 100f, LayerMask.GetMask("Default")))
        {
            if (hit.transform.CompareTag("Wall"))
            {
                sensor.AddObservation((Vector3)hit.transform.localPosition);
                sensor.AddObservation((float)Vector3.Distance(hit.point, transform.position));
            }
            if (hit.transform.CompareTag("Player"))
            {
                sensor.AddObservation((Vector3)hit.transform.localPosition);
                sensor.AddObservation((float)Vector3.Distance(hit.point, transform.position));
            }
        }
    }
    
    private void FixedUpdate()
    {
        // lastPosDistance = Vector3.Distance(lastPosition, target.position);
        // currentPosDistance = Vector3.Distance(transform.position, target.position);
        //
        lastPosition = transform.position;
        
        AddReward(-0.005f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerDamagable"))
        {
            print("TARGET COMPLETE: +1");
            MLStats.Instance.Wins++;
            AddReward(1f);
            envHelper.groundMat.color = Color.green;
            EndEpisode();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Cop"))
        {
            print("WALL || COP COLLISION: -0.5f");
            envHelper.groundMat.color = Color.red;

            MLStats.Instance.Loses++;
            AddReward(-0.5f);
            EndEpisode();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position + transform.up, transform.forward * 25f);
        Gizmos.DrawRay(transform.position + transform.up, -transform.forward * 25f);
        Gizmos.DrawRay(transform.position + transform.up, transform.right * 25f);
        Gizmos.DrawRay(transform.position + transform.up, -transform.right * 25f);
        
        Gizmos.DrawRay(transform.position + transform.up, ((transform.forward + transform.right) * 25f));
        Gizmos.DrawRay(transform.position + transform.up, ((transform.forward - transform.right) * 25f));
        Gizmos.DrawRay(transform.position + transform.up, ((-transform.forward + transform.right) * 25f));
        Gizmos.DrawRay(transform.position + transform.up, ((-transform.forward - transform.right) * 25f));
        
    }
}
