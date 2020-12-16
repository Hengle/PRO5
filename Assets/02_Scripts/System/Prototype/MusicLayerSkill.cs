using UnityEngine;

[System.Serializable] 
public class MusicLayerSkill
{
    public bool skill;
    public string skillName;
    public int uses;

    [SerializeField]
    public MusicLayerSkill(bool skill, string skillName, int uses)
    {
        this.skill = skill;
        this.skillName = skillName;
        this.uses = uses;
    }
}