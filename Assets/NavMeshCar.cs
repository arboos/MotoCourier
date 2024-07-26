using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshCar : MonoBehaviour
{
    [SerializeField] private Transform playerT;

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(MoveTo());
    }

    private IEnumerator MoveTo()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            GetComponent<NavMeshAgent>().destination = playerT.position;
        }
    }
}
