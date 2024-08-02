using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCopTrigger : MonoBehaviour
{
    public List<GameObject> copsInside;
    public Rigidbody playerRB;

    public bool wasted = false;

    public float timeToWaste;
    
    private void Start()
    {
        copsInside = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cop"))
        {
            copsInside.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cop"))
        {
            copsInside.Remove(other.gameObject);
        }
    }

    public void RemoveMissing()
    {
        StartCoroutine(IRemoveMissing());
    }

    public IEnumerator IRemoveMissing()
    {
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < copsInside.Count; i++)
        {
            if (copsInside[i] == null)
            {
                copsInside.Remove(copsInside[i]);
            }
        }
    }
    
    private void FixedUpdate()
    {
        if (copsInside.Count >= 1 && wasted == false && playerRB.velocity.x <= 2f && playerRB.velocity.z <= 2f)
        {
            wasted = true;
            timeToWaste = 3f;
        }
        else if (wasted)
        {
            if (copsInside.Count == 0)
            {
                wasted = false;
                UIManager.Instance.wastedCounter.gameObject.SetActive(false);
                timeToWaste = 3f;
            }
            else
            {
                timeToWaste -= Time.deltaTime;
                UIManager.Instance.wastedCounter.gameObject.SetActive(true);
                UIManager.Instance.wastedCounter.text = (((int)timeToWaste) + 1).ToString();
                if (timeToWaste <= 0)
                {
                    SceneManager.LoadScene(0);
                }
            }
            
        }
    }

    public IEnumerator WastedTimer()
    {
        UIManager.Instance.wastedCounter.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(1f);
        UIManager.Instance.wastedCounter.text = "2";
        yield return new WaitForSeconds(1f);
        UIManager.Instance.wastedCounter.text = "1";
        yield return new WaitForSeconds(1f);

        
    }
}