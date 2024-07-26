using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FortuneWheel : MonoBehaviour
{
    [SerializeField] private Image wheel;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float rotationTimeMaxSpeed;
    [SerializeField] private float accelerationTime;
    [SerializeField] private int numberOfSpins;
    [SerializeField] private bool randomOptionForSpin;
    [SerializeField] private List<Prize> prizes;

    private bool isSpin = false;
    private float slowdownTime;
    private float randomAngle = 0f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Spin();
        }

        /*if (Input.GetKeyDown(KeyCode.X))
        {
            for (int i = 0; i < 10000; i++)
            {
                int index = getRandomPrize();
                Debug.Log("WIN : " + prizes[index].name + " | Index = " + index);
            }
        }*/
    }

    IEnumerator SpinWheel() 
    {
        if (randomOptionForSpin)
        {
            setRandomOptions();
        }
        setWin();
        isSpin = true;
        float elapsedTime = 0f;
        float rotSpeed;
        while (elapsedTime < accelerationTime)
        {
            rotSpeed = Mathf.Lerp(0, rotationSpeed, elapsedTime / accelerationTime);
            wheel.transform.rotation *= Quaternion.Euler(0, 0, rotSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;
        while (elapsedTime < rotationTimeMaxSpeed)
        {
            wheel.transform.rotation *= Quaternion.Euler(0, 0, rotationSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        float distance = (numberOfSpins * 360f) + randomAngle - wheel.transform.rotation.eulerAngles.z;
        slowdownTime = (2 * distance) / rotationSpeed;

        elapsedTime = 0f;
        while (elapsedTime < slowdownTime)
        {
            rotSpeed = Mathf.Lerp(rotationSpeed, 0, elapsedTime / slowdownTime);
            wheel.transform.rotation *= Quaternion.Euler(0, 0, rotSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        isSpin = false;
    }

    private void setWin() 
    {
        int randomSector = getRandomPrize();
        float maxAngle = 360f / prizes.Count * (randomSector + 1);
        float minAngle = 360f / prizes.Count * randomSector;
        randomAngle = Random.Range(minAngle + 8, maxAngle - 8);
    }

    private int getRandomPrize()
    {
        int randomSector = Random.Range(0, prizes.Count);
        if (prizes[randomSector].wight <= Random.Range(0f, 1f))
        {
            return getRandomPrize();
        }
        Debug.Log("WIN:" + randomSector);
        return randomSector;
    }

    private void setRandomOptions()
    {
        rotationSpeed = Random.Range(250,1000);
        rotationTimeMaxSpeed = Random.Range(0, 1.2f);
        accelerationTime = Random.Range(0, 4f);
        numberOfSpins = Random.Range(2, 6);
    }
    
    public void Spin()
    {
        if (!isSpin)
        {
            StartCoroutine(SpinWheel());
        }
    }
}
