using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CivilianCar : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 currentPoint;

    public void Initialize()
    {
        agent = GetComponent<NavMeshAgent>();
        SetPoint(CivilianCarManager.Instance.GetRandomPoint());
    }

    private void SetPoint(Vector3 point)
    {
        agent.SetDestination(point);
        currentPoint = point;
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, currentPoint) <= 0.5f)
        {
            SetPoint(CivilianCarManager.Instance.GetRandomPoint(currentPoint));
        }
    }
    
}
