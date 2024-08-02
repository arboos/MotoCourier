using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrometeoTouchInput : MonoBehaviour
{

    public bool changeScaleOnPressed = false;
    public bool buttonPressed;
    RectTransform rectTransform;
    Vector3 initialScale;
    float scaleDownMultiplier = 0.85f;

    void Start(){
      rectTransform = GetComponent<RectTransform>();
      initialScale = rectTransform.localScale;
    }

    public void ButtonDown(){
      print("Button down");
      this.buttonPressed = true;
      if(changeScaleOnPressed){
        rectTransform.localScale = initialScale * scaleDownMultiplier;
      }
    }

    public void ButtonUp(){
      print("Button up");
      this.buttonPressed = false;
      if(changeScaleOnPressed){
        rectTransform.localScale = initialScale;
      }
    }

}
