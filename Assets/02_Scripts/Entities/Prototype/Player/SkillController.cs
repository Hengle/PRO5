using System;
using UnityEngine;

public enum ChargeTypes
{
    button,
    noMove,
    none
}

public class SkillController : MonoBehaviour
{
    public int maxCharges = 3;
    public int currentCharges = 0;

    public float maxChargeValue = 100;
    public float currentChargeValue;

    private float chargeTime;

    //time it takes to recharge in seconds!
    public float rechargeTime = 5;
    private float timeStop;


    private bool chargeButtonPressed;

    public PlayerStateMachine player;
    public ChargeTypes chargeTypes;

    private void Start()
    {
        timeReset();
    }

    private void Update()
    {
        /*which kind of charge type
         *button only charges on Q pressed
         * nomove when player stands still
         * none charges always
         */
        switch (chargeTypes)
        {
            case ChargeTypes.button:
                if (chargeButtonPressed)
                {
                    isItTimeToCharge();
                }
                else
                {
                    timeStop += Time.deltaTime;
                }

                break;
            case ChargeTypes.noMove:
                if (player.currentMoveDirection == Vector3.zero)
                {
                    isItTimeToCharge();
                }
                else
                {
                    timeStop += Time.deltaTime;
                }

                break;
            case ChargeTypes.none:
                isItTimeToCharge();
                break;
        }
    }

    //if it should charge
    private void isItTimeToCharge()
    {
        if (currentCharges < maxCharges)
        {
            chargeCooldown();
        }
    }

    //charging the bar
    private void chargeCooldown()
    {
        float timeSinceDashEnded = Time.time - (chargeTime + timeStop);

        float perc = timeSinceDashEnded / rechargeTime;

        currentChargeValue = Mathf.Lerp(0, maxChargeValue, perc);

        if (currentChargeValue >= maxChargeValue)
        {
            currentCharges++;
            timeReset();
        }
    }

    //when button is pressed
    public void chargeIsPressed(bool pressed)
    {
        chargeButtonPressed = pressed;
    }

    //decrease currentcharge by int i, and if it wasn't charging it start now again
    public void chargeUse(int i)
    {
        if (currentCharges >= maxCharges)
        {
            timeReset();
        }

        currentCharges -= i;
    }

    //reset time for charging
    public void timeReset()
    {
        chargeTime = Time.time;
        timeStop = 0;
    }
}