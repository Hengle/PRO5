using System.Collections;
using System.Collections.Generic;
using Pada1.BBCore;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Layer
{
    SnareLayer,
}
public class MusicLayerController : MonoBehaviour
{
    public FMODUnity.StudioEventEmitter _musicEvent;

    [HideInInspector] public bool _snareActive;
    [HideInInspector] public bool _hiHatActive;
    [HideInInspector] public bool _leadBassActive;
    [HideInInspector] public bool _atmoActive;

    public SkillController _skillController;

    
    [SerializeField]private List<MusicLayerSkill> activeSkill => new List<MusicLayerSkill>();
    /*public  MusicLayerSkill snare => new MusicLayerSkill(false, "SnareLayer", 1);
    public  MusicLayerSkill hiHat => new MusicLayerSkill(false, "HiHatLayer", 1);
    public  MusicLayerSkill leadBass => new MusicLayerSkill(false, "LeadBassLayer", 1);
    public  MusicLayerSkill atmo => new MusicLayerSkill(false, "AtmoLayer", 1);
    private bool foo;
    public int skillAmount = 3;*/

    // Start is called before the first frame update
    void Start()
    {
        _snareActive = false;
        _hiHatActive = false;
        _leadBassActive = false;
        _atmoActive = false;
    }

    /*void skillListAdder(MusicLayerSkill layerSkill)
    {
        activeSkill.Add(layerSkill);
        if (activeSkill.Count > skillAmount)
        {
            skillListThrowOut();
        }
    }

    void skillListThrowOut()
    {
        MusicLayerSkill layerSkill = activeSkill[0];
        activeSkill.RemoveAt(0);
        _musicEvent.SetParameter(layerSkill.skillName, 0);
        layerSkill.skill = false;
        Debug.Log(layerSkill.skill);
    }
    
    public void LayerSkill(MusicLayerSkill layerSkill)
    {
        if (!layerSkill.skill && _skillController.currentCharges > 0)
        {
            _musicEvent.SetParameter(layerSkill.skillName, 1);
            layerSkill.skill = true;
            skillListAdder(layerSkill);
            _skillController.chargeUse(layerSkill.uses);
            Debug.Log(layerSkill.skill);
        }
    }*/

    public void LayerSkill(ref bool skill, string skillName, int uses)
    {
        if (!skill && _skillController.currentCharges > 0)
        {
            _musicEvent.SetParameter(skillName, 1);
            skill = true;
            //skillListAdder(skill);
            _skillController.chargeUse(uses);
            Debug.Log(skill);
            
        }
        else
        {
            _musicEvent.SetParameter(skillName, 0);
            skill = false;
            Debug.Log(skill);
        }
    }
    
   
    
    


    //normal
    /*public void SnareLayer()
    {
        if (!_snareActive && _skillController.currentCharges > 0)
        {
            _musicEvent.SetParameter("SnareLayer", 1);
            _snareActive = true;
            _skillController.chargeUse(1);
        }
        else
        {
            _musicEvent.SetParameter("SnareLayer", 0);
            _snareActive = false;
        }
    }

    public void HiHatLayer()
    {
        if (!_hiHatActive && _skillController.currentCharges > 0)
        {
            _musicEvent.SetParameter("HiHatLayer", 1);
            _hiHatActive = true;
            _skillController.chargeUse(1);
        }
        else
        {
            _musicEvent.SetParameter("HiHatLayer", 0);
            _hiHatActive = false;
        }
    }

    public void LeadBassLayer()
    {
        if (!_leadBassActive && _skillController.currentCharges > 0)
        {
            _musicEvent.SetParameter("LeadBassLayer", 1);
            _leadBassActive = true;
            _skillController.chargeUse(1);
        }
        else
        {
            _musicEvent.SetParameter("LeadBassLayer", 0);
            _leadBassActive = false;
        }
    }

    public void AtmoLayer()
    {
        if (!_atmoActive && _skillController.currentCharges > 0)
        {
            _musicEvent.SetParameter("AtmoLayer", 1);
            _atmoActive = true;
            _skillController.chargeUse(1);
        }
        else
        {
            _musicEvent.SetParameter("AtmoLayer", 0);
            _atmoActive = false;
        }
    }*/
}