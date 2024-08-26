using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BoxManager : MonoBehaviour
{
    [Header("Data")]
    public int boxCost;
    public Animator anim;

    [Header("UI")]
    public Button openButton;

    public GameObject[] gifts;


    private void Start()
    {
        openButton.onClick.AddListener(() =>
        {
            openButton.interactable = false;
            anim.SetTrigger("Open");
        });
    }

    public void OnReceiveReward()
    {
        Debug.Log("Attempting to give reward from box!");
        anim.SetTrigger("Default");
        openButton.interactable = true;

        Instantiate(gifts[Random.Range(0, gifts.Length)], transform.parent);
        
        DestroyItself();
    }

    public void DestroyItself()
    {
        Destroy(this.gameObject);
    }
}