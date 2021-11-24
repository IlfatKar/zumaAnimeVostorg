using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Click!");
            //что-то умное чтобы стреляло            
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(ImpSpeed,0));
        }
    }
}
