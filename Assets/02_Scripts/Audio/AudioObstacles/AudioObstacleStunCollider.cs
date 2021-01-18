using UnityEngine;


public class AudioObstacleStunCollider : MonoBehaviour
{
    //handles if collider is active or not
    bool _disabled = false;

    float _dmgOnEnter = 3;
    float _dmgOnStay = 1;

    public void Start()
    {
        //This solution with the bools is a bit dumb, needs rework i would say
    }

    //when triggered, the method of the parent PullTrigger gets called that handels the damage
    void OnTriggerEnter(Collider c)
    {
        Debug.Log("enemy stunned4");
        gameObject.GetComponentInParent<IDamageObstacle>().PullTrigger(c, _dmgOnEnter);
    }

    /*private void OnTriggerStay(Collider c)
    {
        gameObject.GetComponentInParent<IDamageObstacle>().PullTrigger(c, _dmgOnStay);
    }*/

    public void DisableSelf()
    {
        GetComponent<BoxCollider>().enabled = false;
        _disabled = true;
    }

    public void EnableSelf()
    {
        GetComponent<BoxCollider>().enabled = true;
        _disabled = false;
    }
    public int duration = 500;
    


   
    
}