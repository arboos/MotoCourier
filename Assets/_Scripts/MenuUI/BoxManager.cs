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
        // тут просто потом добавить логику выдачи машины или чего либо там
        DestroyItself();
    }

    public void DestroyItself()
    {
        Destroy(this.gameObject);
    }
}