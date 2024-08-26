using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo Instance { get; private set; }

    public PlayerCopTrigger copTrigger;
    public int currentHealth = 3;

    public float speed;
    
    private Vector3 lastPosition;

    public float AliveTime;

    public SaveSphere saveSphere;

    public bool immunity;

    public float immunityTime;
    
    public Rigidbody rb;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        speed = Mathf.Lerp(speed, (transform.position - lastPosition).magnitude, 0.7f /*adjust this number in order to make interpolation quicker or slower*/);
        lastPosition = transform.position;
    }

    private void Update()
    {
        immunityTime -= Time.deltaTime;
        if (immunityTime <= 0) immunity = false;
        AliveTime += Time.deltaTime;
        CheckMedals();
    }

    private void CheckMedals()
    {
        if (AliveTime >= 60 && !GameResultInfo.Instance.MedalsGotStr.Contains("Survivor_1") && !GameResultInfo.Instance.MedalsGotStr.Contains("Survivor_2") 
            && !GameResultInfo.Instance.MedalsGotStr.Contains("Survivor_3"))
        {
            UIManager.Instance.AddMedal(UIManager.Instance.medals[0], 10, 25);
            GameResultInfo.Instance.MedalsGotStr.Add("Survivor_1");
        }
        else if (AliveTime >= 120 && !GameResultInfo.Instance.MedalsGotStr.Contains("Survivor_2") 
            && !GameResultInfo.Instance.MedalsGotStr.Contains("Survivor_3"))
        {
            UIManager.Instance.AddMedal(UIManager.Instance.medals[1], 25, 50);
            GameResultInfo.Instance.MedalsGotStr.Add("Survivor_2");
        }
        else if (AliveTime >= 180 && !GameResultInfo.Instance.MedalsGotStr.Contains("Survivor_3"))
        {
            UIManager.Instance.AddMedal(UIManager.Instance.medals[2], 50, 100);
            GameResultInfo.Instance.MedalsGotStr.Add("Survivor_3");
        }
    }

    public void DealDamage(int damage)
    {
        if(immunity) return;
        print("Damage: " + damage);
        currentHealth -= damage;
        immunity = true;
        immunityTime = 3f;
        saveSphere.Play();
        if (currentHealth <= 0)
        {
            Death();
        }

        UIManager.Instance.healthText.text = currentHealth.ToString();
        GameManager.OnDealDamage?.Invoke();
        // switch (currentHealth)
        // {
        //     case 3:
        //         UIManager.Instance.healthImage.color = Color.green;
        //         break;
        //     
        //     case 2:
        //         UIManager.Instance.healthImage.color = Color.yellow;
        //         break;
        //     
        //     case 1:
        //         UIManager.Instance.healthImage.color = Color.red;
        //         break;
        // }
    }
    
    public void TakeHealth(int count)
    {
        print("health+= : " + count);
        currentHealth += count;

        UIManager.Instance.healthText.text = currentHealth.ToString();
        GameManager.OnTakeHealth?.Invoke();
    }

    public void Death()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        UIManager.Instance.deathScreen.SetActive(true);
        UIManager.Instance.wastedCounter.gameObject.SetActive(false);
        GetComponent<PrometeoCarController>().enabled = false;
    }
    
    public void Respawn()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<PrometeoCarController>().enabled = true;
        
        copTrigger.wasted = false;
        UIManager.Instance.wastedCounter.gameObject.SetActive(false);
        copTrigger.timeToWaste = 4f;
        copTrigger.RemoveMissing();
        
        TakeHealth(1);

        StartCoroutine(DestroyCops());
        
        UIManager.Instance.deathScreen.SetActive(false);
    }

    public IEnumerator DestroyCops()
    {
        yield return new WaitForSeconds(0.5f);
        
        foreach (var cop in CopSpawner.Instance.copsSpawned)
        {
            cop.GetComponent<NavMeshCar>().DestroyCar();
        }
    }
}
