using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshCar : MonoBehaviour
{

    // Update is called once per frame
    void Awake()
    {
        StartCoroutine(MoveTo());
    }

    private IEnumerator MoveTo()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            GetComponent<NavMeshAgent>().destination = GameManager.Instance.player.position;
        }
    }
}
