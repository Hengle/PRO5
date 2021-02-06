using System.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


//Diese Klasse ist zuständig um aus einem FMOD Event die Marker und BPM zu extrahieren.
//Die Marker werden von dieser Klasse aus an den MarkerManger weitergegeben der diese dann auswertet bzw zu einem Event zuordnet.



[RequireComponent(typeof(FMODUnity.StudioEventEmitter))]
public class FmodTimeline : MonoBehaviour
{
    //BEATSYSTEM
    [StructLayout(LayoutKind.Sequential)]
    class TimelineInfo
    {
        public int currentMusicBeat = 0;
        public FMOD.StringWrapper lastMarker = new FMOD.StringWrapper();
    }

    //Um das Event des Emitters zu holen und zuzuweisen
    public FMODUnity.StudioEventEmitter _emitter;
    FMOD.Studio.EventInstance _musicInstance;

    public static FmodTimeline _instance;
    public MarkerManager _markerManager;
    public int _beat;
    public string _marker;
    public bool _fullBar;
    public int _barCounter;



    bool isLoaded;
    TimelineInfo _timelineInfo;
    GCHandle _timelineHandle;

    FMOD.Studio.EVENT_CALLBACK _beatCallback;
    private void OnEnable()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        GlobalEventSystem.instance.onInit += StartLoad;
    }

    private void OnDisable()
    {
        isLoaded = false;
        GlobalEventSystem.instance.onInit -= StartLoad;
    }

    // Start is called before the first frame update
    void StartLoad()
    {
        if (!isLoaded)
        {
            isLoaded = true;
            _emitter.Play();
            _musicInstance = _emitter.EventInstance;
            AssignBeatEvent(_musicInstance);
            _markerManager = GetComponent<MarkerManager>();
            _markerManager._emitter = _emitter;
            _markerManager._musicInstance = _musicInstance;

            GetComponent<MusicLayerController>()._musicEvent = _emitter;
            SpectrumManager._instance.musicInstance = _musicInstance;
        }
    }

    //---BEATDETECTION---
    void AssignBeatEvent(FMOD.Studio.EventInstance instance)
    {
        _timelineInfo = new TimelineInfo();
        _timelineHandle = GCHandle.Alloc(_timelineInfo, GCHandleType.Pinned);
        _beatCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallback);
        instance.setUserData(GCHandle.ToIntPtr(_timelineHandle));
        instance.setCallback(_beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
    }

    void StopAndClear(FMOD.Studio.EventInstance instance)
    {
        instance.setUserData(IntPtr.Zero);
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        instance.release();
        _timelineHandle.Free();
    }

    [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
    FMOD.RESULT BeatEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, FMOD.Studio.EventInstance instance, IntPtr parameterPtr)
    {
        IntPtr timelineInfoPtr;
        FMOD.RESULT result = instance.getUserData(out timelineInfoPtr);
        if (result != FMOD.RESULT.OK)
        {
            Debug.LogError("Timeline Callback error: " + result);
        }
        else if (timelineInfoPtr != IntPtr.Zero)
        {
            GCHandle timelineHandle = GCHandle.FromIntPtr(timelineInfoPtr);
            TimelineInfo timelineInfo = (TimelineInfo)timelineHandle.Target;

            switch (type)
            {
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));
                        timelineInfo.currentMusicBeat = parameter.beat;

                        _beat = timelineInfo.currentMusicBeat;
                        _barCounter = parameter.bar - 1;
                        _fullBar = false;
                        //Debug.Log("BEAT: " + beat);
                        if (_beat == 4)
                        {
                            _fullBar = true;
                        }
                    }
                    break;
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
                        timelineInfo.lastMarker = parameter.name;
                        _marker = timelineInfo.lastMarker;
                        _markerManager.parseMarkerString(_marker);
                    }
                    break;
            }
        }
        return FMOD.RESULT.OK;
    }



}
