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
    private bool _leadBassLock = false;
    private bool _atmoLock = false;



    public FMODUnity.StudioEventEmitter _emitter;
    FMOD.Studio.EventInstance _musicInstance;
    
    private void OnEnable()
    {
        GlobalEventSystem.instance.onLoadFinish += StartLoad;
    }
    // Start is called before the first frame update
    void StartLoad()
    {
        Debug.Log(this.GetType());
        _musicInstance = _emitter.EventInstance;
    }

    void Update()
    {

    }

    //K Kick
    //S Snare
    //H HiHat

    public void parseMarkerString(String marker)
    {    
        if (marker[0] != '&')
        {
            for (int i = 0; i < marker.Length; i++)
            {
                switch (marker[i])
                {
                    case 'S':
                       // Debug.Log("Snare");
                        if (MyEventSystem.instance == null)
                        {
                            Debug.Log("Eventsystem is null");
                            
                        }
                        else
                        {
                            float active;
                            _musicInstance.getParameterByName("SnareLayer", out active);
                            if (!_snareLock && active == 1) 
                            {
                               // Debug.Log("Snare");
                                MyEventSystem.instance.OnSnare();
                            }
                            lockInstrument("lockSnare");
                        }
                        break;

                    case 'K':

                        //Debug.Log("Kick");
                        if (MyEventSystem.instance == null)
                        {
                            
                        }
                        else
                        {
                          
                            float active;
                            _musicInstance.getParameterByName("KickLayer", out active);
                            active = 1;
                            if (!_kickLock && active == 1)
                            {
                              //  Debug.Log("Kick");
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
                                //Debug.Log("HiHat");
                                MyEventSystem.instance.OnHiHat();
                            }
                            lockInstrument("lockHighHat");
                        }
                        break;
                    case 'R':
                        _emitter.SetParameter("nextPart", 0);
                        break;
                    case 'L':
                        if (MyEventSystem.instance == null)
                        {

                        }
                        else
                        {
                            float active;
                            _musicInstance.getParameterByName("LeadBassLayer", out active);
                            if (!_leadBassLock && active == 1)
                            {
                                //Debug.Log("HiHat");
                                MyEventSystem.instance.OnLeadBass();
                            }
                            lockInstrument("lockLeadBass");
                        }
                        break;
                    case 'A':
                        if (MyEventSystem.instance == null)
                        {

                        }
                        else
                        {
                            float active;
                            _musicInstance.getParameterByName("AtmoLayer", out active);
                            if (!_atmoLock && active == 1)
                            {
                                MyEventSystem.instance.OnAtmo();
                            }
                            lockInstrument("lockAtmo");
                        }
                        break;

                }
            }
        }
    }

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

    IEnumerator lockLeadBass()
    {

        _leadBassLock = true;
        yield return new WaitForSeconds(.01f);
        _leadBassLock = false;
    }

    IEnumerator lockAtmo()
    {

        _atmoLock = true;
        yield return new WaitForSeconds(.1f);
        _atmoLock = false;
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
