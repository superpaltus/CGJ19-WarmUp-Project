using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashMeterCanvas : MonoBehaviour
{

    public float fade = 0;
    public bool doFade = false;

    void Update()
    {
        if (Player.instance.timeUntilDash > 0)
        {
            doFade = false;
            GetComponent<CanvasGroup>().alpha = 1;
            fade = 1;
            transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = 1 - Player.instance.timeUntilDash;
            if(transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount > 0.97)
            {
                transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = 1;
            }
        } else
        {
            doFade = true;
        }

        if(doFade == true && fade > 0)
        {
            GetComponent<CanvasGroup>().alpha = fade;
            fade -= Time.deltaTime * 2;
        } else if(doFade == true && fade < 0)
        {
            fade = 0;
        }

    }
}
