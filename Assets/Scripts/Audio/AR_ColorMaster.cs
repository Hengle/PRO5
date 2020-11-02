using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AR_ColorMaster : MonoBehaviour
{

    protected static Color magenta = new Color(0.2660f, 0, 0.8f, 1);


    protected static Color m_blueChannelColor = Color.cyan;
    protected static Color m_redChannelColor = Color.red;
    protected static Color m_bothChannelColor = magenta;

    protected static Color m_blueChannelActiveColor = m_blueChannelColor;
    protected static Color m_redChannelActiveColor = m_redChannelColor;
    protected static Color m_bothChannelActiveColor = m_bothChannelColor;

    protected static Color m_blueChannelErrorColor = Color.red;
    protected static Color m_redChannelErrorColor = Color.cyan;
    protected static Color m_bothChannelErrorColor = Color.blue;



    public static bool colorErrorActive = false;
    public static bool colorErrorEnd = false;


    private void OnEnable()
    {
        // SceneManager.sceneLoaded += addActionToEvent;
        // SceneManager.sceneLoaded += activateComponent;
    }

    // void Init(Scene scene, LoadSceneMode mode)
    // {
    //     MyEventSystem.instance.ActivateSkill += activateColorError;
    //     MyEventSystem.instance.DeactivateSkill += deactivateColorError;
    // }

    // Update is called once per frame
    void Update()
    {
        if (colorErrorActive)
        {
            Debug.Log("CollorError in Master");
            m_blueChannelActiveColor = Color.Lerp(m_blueChannelColor, m_blueChannelErrorColor, Mathf.PingPong(Time.time, 1));
            m_redChannelActiveColor = Color.Lerp(m_redChannelColor, m_redChannelErrorColor, Mathf.PingPong(Time.time, 1));
            m_bothChannelActiveColor = Color.Lerp(m_bothChannelColor, m_bothChannelErrorColor, Mathf.PingPong(Time.time, 1));
            colorErrorEnd = true;
        }


        if (!colorErrorActive && colorErrorEnd)
        {
            Color help1 = m_blueChannelActiveColor;
            Color help2 = m_redChannelActiveColor;
            Color help3 = m_bothChannelActiveColor;
            m_blueChannelActiveColor = Color.Lerp(help1, m_blueChannelColor, 1);
            m_redChannelActiveColor = Color.Lerp(help2, m_redChannelColor, 1);
            m_bothChannelActiveColor = Color.Lerp(help3, m_bothChannelColor, 1);
        }
    }







}
