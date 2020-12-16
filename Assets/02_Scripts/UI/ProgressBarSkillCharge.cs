using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarSkillCharge : ProgessBar
{
    public Slider slider;
    public SkillController controller;
    public Text text;

    void Update()
    {
        GetCurrentFill();
    }

    public override void GetCurrentFill()
    {
        maximum = controller.maxChargeValue;
        current = controller.currentChargeValue;
        float fillAmount = (float) current / (float) maximum;
        slider.value = fillAmount;
        text.text = controller.currentCharges.ToString();
    }
    
}
