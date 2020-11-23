using UnityEngine;

public interface IDamageObstacle
{
    void PullTrigger(Collider c, float dmg);
    float _dmgOnEnter { get; set; }
    float _dmgOnStay { get; set; }
}