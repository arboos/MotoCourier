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

    public int MoneyToAdd;
    public int ExpToAdd;
    public int BP_ExpToAdd;

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

    public void DestroyCar()
    {
        GameResultInfo.Instance.Exp += ExpToAdd;
        GameResultInfo.Instance.BattlePass_Exp += BP_ExpToAdd;
        GameResultInfo.Instance.Money += MoneyToAdd;
        UIManager.OnAddCoins();
        PlayerInfo.Instance.rb.velocity = new Vector3(PlayerInfo.Instance.rb.velocity.x * 0.75f,
            PlayerInfo.Instance.rb.velocity.y * 0.75f, PlayerInfo.Instance.rb.velocity.z * 0.75f);
        Destroy(gameObject);
    }
}
