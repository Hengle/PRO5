using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerManager : MonoBehaviour
{
    public enum MusicPart { Start, Mid, Break, ArenaFight, Ending };
    public MusicPart musicPart;


    private bool _kickLock = false;
    private bool _snareLock = false;
    private bool _hiHatLock = false;

    private bool _kickSkillLock = false;
    private bool m_snareSkillLock = false;
    private bool _hiHatSkillLock = false;

    private void OnEnable()
    {
        // SceneManager.sceneLoaded += Init;
    }

    public FMODUnity.StudioEventEmitter _emitter;
    FMOD.Studio.EventInstance _musicInstance;


    // Start is called before the first frame update
    void Start()
    {
        //_emitter = GetComponent<FMODUnity.StudioEventEmitter>();
        _musicInstance = _emitter.EventInstance;

    }

    public void Init()
    {
        //MyEventSystem.instance.ActivateSkill += deactivateListener;
       // MyEventSystem.instance.DeactivateSkill += activateListener;
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void parseMarkerString(String marker)
    {    
        if (marker[0] != '&')
        {
            for (int i = 0; i < marker.Length; i++)
            {
                switch (marker[i])
                {
                    case 'S':
                        if (MyEventSystem.instance == null)
                        {

                            
                        }
                        else
                        {
                            float active;
                            _musicInstance.getParameterByName("SnareLayer", out active);
                            if (!_snareLock && active == 1) 
                            {
                                Debug.Log("Snare");
                                MyEventSystem.instance.OnSnare();
                            }
                            lockInstrument("lockSnare");

                        }
                        break;

                    case 'K':
                        if (MyEventSystem.instance == null)
                        {
                            
                        }
                        else
                        {
                            float active;
                            _musicInstance.getParameterByName("KickLayer", out active);
                            if (!_kickLock && active == 1)
                            {
                                Debug.Log("Kick");
                                MyEventSystem.instance.OnKick();
                            }
                            lockInstrument("lockKick");
                        }
                        break;
                    case 'H':

                        if (MyEventSystem.instance == null)
                        {

                        }
                        else
                        {
                            float active;
                            _musicInstance.getParameterByName("HiHatLayer", out active);
                            if (!_hiHatLock && active == 1)
                            {
                                Debug.Log("HiHat");
                                MyEventSystem.instance.OnHighHat();
                            }
                            lockInstrument("lockHighHat");
                        }
                        break;
                    case 'R':
                        _emitter.SetParameter("nextPart", 0);
                        break;
                    case 'X':
                        break;

                }
            }
        }
    }

    /*
    public void deactivateListener(Skills skill)
    {
        if (skill.name == "LowPass")
        {
            m_highHatSkillLock = true;
        }
        if (skill.name == "HighPass")
        {
            m_kickSkillLock = true;
        }
    }
   


    public void activateListener(Skills skill)
    {
        if (skill.name == "LowPass")
        {
            m_highHatSkillLock = false;
            Debug.Log(m_highHatSkillLock);
        }
        if (skill.name == "HighPass")
        {
            m_kickSkillLock = false;
        }
    }
     */
    public void lockInstrument(String lockName)
    {
        StartCoroutine(lockName);
    }

    IEnumerator lockSnare()
    {

        _snareLock = true;
        yield return new WaitForSeconds(.1f);
        _snareLock = false;
    }

    IEnumerator lockKick()
    {

        _kickLock = true;
        yield return new WaitForSeconds(.1f);
        _kickLock = false;
    }

    IEnumerator lockHighHat()
    {

        _hiHatLock = true;
        yield return new WaitForSeconds(.1f);
        _hiHatLock = false;
    }

    void debugJumpToMusicPart()
    {
        switch (musicPart)
        {
            case MusicPart.Start:
                _emitter.SetParameter("PlayFrom", 0);
                break;
            case MusicPart.Mid:
                _emitter.SetParameter("PlayFrom", 1);
                break;
            case MusicPart.Break:
                _emitter.SetParameter("PlayFrom", 2);
                break;
            case MusicPart.ArenaFight:
                _emitter.SetParameter("PlayFrom", 3);
                break;
            case MusicPart.Ending:
                _emitter.SetParameter("PlayFrom", 4);
                break;
        }
    }

}
