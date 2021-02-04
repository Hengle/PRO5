using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LVLEndDoor : MonoBehaviour
{

   public bool openDoor = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (openDoor)
        {
            activateMechanism();
            openDoor = false;
        }
    }

    public void activateMechanism()
    {
        transform.GetChild(0).DOScaleY(0, 2f);
    }
}
