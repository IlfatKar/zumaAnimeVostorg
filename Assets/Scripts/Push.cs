using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    public float ImpSpeed=5;
    public GameObject BallPrefab;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Click!");
            //что-то умное чтобы стреляло            
            //Instantiate(BallPrefab, new Vector3(transform.position.x, transform.position.y, -1), Quaternion.identity);
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1) * ImpSpeed, ForceMode2D.Impulse);
        }
    }
}
