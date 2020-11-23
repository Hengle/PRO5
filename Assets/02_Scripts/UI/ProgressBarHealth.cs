using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarHealth : ProgessBar
{
    public PlayerStatistics player;

    public FloatVariable currentPlayerHealth;

    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerStatistics>();
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }


    public override void GetCurrentFill()
    {
        maximum = (player.GetStatValue(StatName.MaxHealth));
        current = currentPlayerHealth.Value;
        float fillAmount = (float) current / (float) maximum;
        slider.value = fillAmount;
    }
}