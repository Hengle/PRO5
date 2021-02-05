using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Runtime.InteropServices;

[RequireComponent(typeof(FMODUnity.StudioEventEmitter))]

public class Game : MonoBehaviour
{

}


public class SpectrumManager : MonoBehaviour
{

    public static SpectrumManager _instance;
    public float _smoothBuffer;

    public FMODUnity.StudioEventEmitter emitter;
    public FMOD.Studio.EventInstance musicInstance;


    //---SPECTRUM ANALYZER---
    FMOD.DSP fft;
    FMOD.ChannelGroup channelGroup;

    //Werden benutzt um zu überprüfen ob Audio schon vorhanden ist
    bool isPlaying = false;
    bool ready = false;

    //Lenght of Sample Array
    const int WindowSize = 512;

    //Creating Analyzer for Environment/Game

    //variables for the 8band audio spectrum
    private float[] _freqBand8 = new float[8];

    private float[] _bandBuffer8 = new float[8];

    private float[] _bufferDecrease = new float[8];
    public float[] _freqBandHighest8 = new float[8];

    [HideInInspector]
    public static float[] _audioBand8, _audioBandBuffer8;

    [HideInInspector]
    public static float[] _audioBand32, _audioBandBuffer32;

    [HideInInspector]
    public static float _amplitude, _amplitudeBuffer;
    private float _amplitudeHighest;
    public float _audioProfile;


    public enum _channel { Stereo, Left, Right };
    public _channel channel = new _channel();

    //GETTER

    //returned das fqBand mit Bufferfunkiont -> smoothed die werte
    //die id gibt dabei an welches Band man will
    // 0 - 7, je höher die Nummer desto höhere Frequenzen beherbergt das Band
    //will man denn Bass so nimmt man die 0, die Höhen bei 6 - 7
    public float getFqBandBuffer8(int id)
    {
        return _audioBandBuffer8[id];
    }

    public float getFqBand8(int id)
    {
        return _audioBand8[id];
    }

    public float getAmplitudeBuffer()
    {
        return _amplitudeBuffer;
    }

    public FMOD.Studio.EventInstance getMusicInstance()
    {
        return musicInstance;
    }
    private void OnEnable()
    {
        GlobalEventSystem.instance.onLoadFinish += StartLoad;
    }
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    void StartLoad()
    {
        Debug.Log(this.GetType());
        _audioBand8 = new float[8];
        _audioBandBuffer8 = new float[8];

        AudioProfile(8, _audioProfile);

        //fetch the musicEvent from the EventEmitter
        // musicInstance = emitter.EventInstance;

        //set up fft dsp
        FMODUnity.RuntimeManager.CoreSystem.createDSPByType(FMOD.DSP_TYPE.FFT, out fft);
        fft.setParameterInt((int)FMOD.DSP_FFT.WINDOWTYPE, (int)FMOD.DSP_FFT_WINDOW.BLACKMAN);
        fft.setParameterInt((int)FMOD.DSP_FFT.WINDOWSIZE, WindowSize * 2);

        //assign the dsp to a channel
        musicInstance.getChannelGroup(out channelGroup);
    }


    public Game CreateAwesomeTopDownActionPuzzleGameWithCyberSoundtrack(String AIandSystems, String MusicObstacles, String PowerUp, String EnvironmentArt, String CharacterArt, String CrazySound)
    {
        return null;
    }








    void Update()
    {
        //BPM Calculation Methods

        if (!ready)
        {
            musicInstance.getChannelGroup(out channelGroup);
            channelGroup.isPlaying(out isPlaying);
        }


        if (isPlaying && !ready)
        {
            channelGroup.addDSP(FMOD.CHANNELCONTROL_DSP_INDEX.HEAD, fft);
            ready = true;
        }

        if (ready)
        {

            //Creating the spectrum with 512 samples
            IntPtr unmanagedData;
            uint length;
            fft.getParameterData((int)FMOD.DSP_FFT.SPECTRUMDATA, out unmanagedData, out length);
            FMOD.DSP_PARAMETER_FFT fftData = (FMOD.DSP_PARAMETER_FFT)Marshal.PtrToStructure(unmanagedData, typeof(FMOD.DSP_PARAMETER_FFT));
            var spectrum = fftData.spectrum;


            //Checking if the channels already loaded
            if (fftData.numchannels == 0)
            {
                //Debug.Log("keine FFT Channels vorhanden");
            }
            //Checking if the channels already loaded
            if (fftData.numchannels > 0)
            {
                //--- Frequency Bands 8
                //cant call the methdo because i cant convert the var to an actual array
                int count = 0;

                for (int i = 0; i < 8; i++)
                {
                    float average = 0;
                    int sampleCount = (int)Mathf.Pow(2, i) * 2;

                    if (i == 7)
                    {
                        sampleCount += 2;
                    }

                    for (int j = 0; j < sampleCount; j++)
                    {
                        if (channel == _channel.Stereo)
                        {
                            average += (spectrum[0][count] + spectrum[1][count]) * (count + 1);
                        }
                        if (channel == _channel.Left)
                        {
                            average += (spectrum[0][count]) * (count + 1);
                        }
                        if (channel == _channel.Right)
                        {
                            average += (spectrum[1][count]) * (count + 1);
                        }
                        count++;
                    }
                    average /= count;
                    _freqBand8[i] = average * 10;
                }
                BandBuffer8();
                CreateAudioBands8();
                GetAmplitude(8);
            }

        }

    }


    //normalisiert die 8 Frequenzbänder
    //damit erhalten wir Werte zwischen 0 und 1 für jedes Frequenzband
    void CreateAudioBands8()
    {
        for (int i = 0; i < 8; i++)
        {

            if (_freqBand8[i] > _freqBandHighest8[i])
            {
                _freqBandHighest8[i] = _freqBand8[i];
            }
            _audioBand8[i] = (_freqBand8[i] / _freqBandHighest8[i]);
            _audioBandBuffer8[i] = (_bandBuffer8[i] / _freqBandHighest8[i]);
        }
    }

    //smootht die Werte damit diese nicht so zappeln


    //damit bekommen wir die Lautstärke


    //kann für jeden Track angepasst werden
    //damit die Werte auch gleich am Anfang smooth sind, ohne dem AudioProfile brauchen diese ein bisschen bis sie sich einpendeln
    void AudioProfile(int bandAmount, float audioProfile)
    {
        for (int i = 0; i < bandAmount; i++)
        {
            _freqBandHighest8[i] = audioProfile;
        }
    }

    public void changeAudioProfile(float[] a)
    {
        for (int i = 0; i < 8; i++)
        {
            _freqBandHighest8[i] *= a[i];
        }
    }



    void BandBuffer8()
    {

        for (int g = 0; g < 8; ++g)
        {
            if (_freqBand8[g] > _bandBuffer8[g])
            {
                _bandBuffer8[g] = _freqBand8[g];
                _bufferDecrease[g] = _smoothBuffer;
            }

            if (_freqBand8[g] < _bandBuffer8[g])
            {
                _bufferDecrease[g] = (_bandBuffer8[g] - _freqBand8[g]) / 8;
                _bandBuffer8[g] -= _bufferDecrease[g];
            }
        }
    }




    /*

    public Game createPRO5()
    {
        Game waveform = CreateAwesomeTopDownActionPuzzleGameWithCyberSoundtrack();
        return waveform;
    }
    */


    void GetAmplitude(int bandAmount)
    {
        float _currentAmplitude = 0;
        float _CurrentAmplitudeBuffer = 0;
        for (int i = 0; i < bandAmount; i++)
        {
            _currentAmplitude += _audioBand8[i];
            _CurrentAmplitudeBuffer += _audioBandBuffer8[i];
        }
        if (_currentAmplitude > _amplitudeHighest)
        {
            _amplitudeHighest = _currentAmplitude;
        }
        _amplitude = _currentAmplitude / _amplitudeHighest;
        _amplitudeBuffer = _CurrentAmplitudeBuffer / _amplitudeHighest;
    }




}












