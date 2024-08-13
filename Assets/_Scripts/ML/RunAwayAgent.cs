using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Random = UnityEngine.Random;

public class RunAwayAgent : Agent
{
    [SerializeField] private Transform target_a;
    [SerializeField] private Transform target_b;
    [SerializeField] private Transform target_c;
    
    public float ta_lastPosDistance;
    public float tb_lastPosDistance;
    public float tc_lastPosDistance;
    
    public float ta_currentPosDistance;
    public float tb_currentPosDistance;
    public float tc_currentPosDistance;
    
    private Vector3 lastPosition;

    private EnvHelper envHelper;

    private void Start()
    {
        envHelper = GetComponentInParent<EnvHelper>();
    }

    public override void OnEpisodeBegin()
    {
        transform.position = envHelper.playerSpawnPos[Random.Range(0, envHelper.playerSpawnPos.Length)].position;
    }
    
    public override void OnActionReceived(ActionBuffers actions)
    {
        float forward = actions.ContinuousActions[0];
        float backward = actions.ContinuousActions[1];
    } 

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation((Vector3)transform.position);
        sensor.AddObservation((Vector3)target_a.position);
        sensor.AddObservation((Vector3)target_b.position);
        sensor.AddObservation((Vector3)target_c.position);
    }
    
    private void FixedUpdate()
    {
        ta_lastPosDistance = Vector3.Distance(lastPosition, target_a.position);
        tb_lastPosDistance = Vector3.Distance(lastPosition, target_b.position);
        tc_lastPosDistance = Vector3.Distance(lastPosition, target_c.position);
        
        ta_currentPosDistance = Vector3.Distance(transform.localPosition, target_a.position);
        tb_currentPosDistance = Vector3.Distance(transform.localPosition, target_b.position);
        tc_currentPosDistance = Vector3.Distance(transform.localPosition, target_c.position);
        
        if (ta_currentPosDistance > ta_lastPosDistance)
        {
            AddReward((ta_currentPosDistance - ta_lastPosDistance)/3f);
        }
        
        if (tb_currentPosDistance > tb_lastPosDistance)
        {
            AddReward((tb_currentPosDistance - tb_lastPosDistance)/3f);
        }
        
        if (tc_currentPosDistance > tc_lastPosDistance)
        {
            AddReward((tc_currentPosDistance - tc_lastPosDistance)/3f);
        }

        lastPosition = transform.localPosition;
    }
}
