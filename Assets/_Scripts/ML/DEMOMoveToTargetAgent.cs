using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class DEMOMoveToTargetAgent : Agent
{
    [SerializeField] private Transform target;

    private PrometeoCopController carController;

    public float forward;
    public float backward;
    public float right;
    public float left;

    public Rigidbody2D rb;

    private void Start()
    {
        carController = GetComponent<PrometeoCopController>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    public override void OnEpisodeBegin()
    { 
        rb.velocity = Vector2.zero;
        
        transform.localPosition = new Vector3(-3f, Random.Range(-3f, 3f), 0f);

        target.transform.localPosition = new Vector3(3f, Random.Range(-3f, 3f), 0f);
    }
    
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1];

        rb.velocity = new Vector2(moveX, moveY) * 200 * Time.deltaTime;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;

        continuousActions[0] = Input.GetAxis("Horizontal");
        continuousActions[1] = Input.GetAxis("Vertical");
    }
    
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation((Vector3)transform.localPosition);
        sensor.AddObservation((Vector3)target.localPosition);
    }
    
    private void FixedUpdate()
    {
        AddReward(-0.005f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            print("Wall collision: -1f");
            AddReward(-1f);
            EndEpisode();
        }
        
        if (other.CompareTag("Player"))
        {
            print("Player collision: +2f");
            AddReward(2f);
            EndEpisode();
        }
    }
    
}
