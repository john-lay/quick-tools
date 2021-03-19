using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public AnimationCurve ac;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(ac.length);
        Debug.Log(ac[0].value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
