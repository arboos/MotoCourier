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
    public TextMeshProUGUI costText;
    public Button openButton;

    private void Start()
    {
        costText.text = boxCost.ToString();
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
    }

    public void DestroyItself()
    {
        Destroy(this.gameObject);
    }
}