using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpinWheel : MonoBehaviour
{
    public FortuneWheel fortuneWheel;
    public Button spinButton;
    public Text timerText;

    private DateTime nextSpinTime;
    public bool canSpin;

    public int hoursToNextSpin = 3;

    private void Start()
    {
        // Инициализируем состояние кнопки и таймера
        nextSpinTime = PlayerPrefs.HasKey("NextSpinTime") 
            ? DateTime.Parse(PlayerPrefs.GetString("NextSpinTime")) 
            : DateTime.MinValue;
        canSpin = DateTime.Now >= nextSpinTime;

        // Настраиваем начальное состояние кнопки
        spinButton.interactable = canSpin;
        StartCoroutine(UpdateTimer());
    }

    public void Spin()
    {
        if (!canSpin) return;

        fortuneWheel.Spin();

        // Обновляем время следующего возможного нажатия
        nextSpinTime = DateTime.Now.AddHours(hoursToNextSpin);
        PlayerPrefs.SetString("NextSpinTime", nextSpinTime.ToString());
        PlayerPrefs.Save();

        canSpin = false;
        spinButton.interactable = false;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (!canSpin)
        {
            TimeSpan remainingTime = nextSpinTime - DateTime.Now;
            if (remainingTime <= TimeSpan.Zero)
            {
                canSpin = true;
                spinButton.interactable = true;
                timerText.text = "You can spin now!";
            }
            else
            {
                timerText.text = string.Format("Next spin in: {0:D2}:{1:D2}:{2:D2}", 
                    remainingTime.Hours, remainingTime.Minutes, remainingTime.Seconds);
            }
            yield return new WaitForSeconds(1);
        }
    }
}