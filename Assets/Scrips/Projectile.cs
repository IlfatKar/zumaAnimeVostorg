using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private bool isMove = false;
    public Vector3 direction = Vector3.right;
    public float shotSpeed = 4f;
    void Start(){}    

    void Update(){
        if (isMove) {
            float angle = transform.eulerAngles.z * Mathf.Deg2Rad;
            float sin = Mathf.Sin(angle);
            float cos = Mathf.Cos(angle);
 
            Vector3 forward = new Vector3(
                direction.x * cos - direction.y * sin,
                direction.x * sin + direction.y * cos,
                -1);
 
            transform.position += forward * shotSpeed * Time.deltaTime;
        }  
    }

    void SetSprite(Sprite sp) {
        GetComponentInChildren<SpriteRenderer>().sprite = sp;
    }   

    void Push(float Angle) {
        transform.rotation = Quaternion.Euler(new Vector3(0,0, Angle));
        isMove = true;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Ball"){
            GameObject b = Instantiate(Spawner.BALLPREF, new Vector3(transform.position.x, transform.position.y, -1), 
            Quaternion.identity);
            Sprite sp = GetComponentInChildren<SpriteRenderer>().sprite;
            b.SendMessage("SetSprite", sp);
            int idx = collision.gameObject.GetComponent<Ball>().WaypointIdx;
            b.SendMessage("SetWaypointIdx", idx);
            Destroy(gameObject);
        }
    }
}