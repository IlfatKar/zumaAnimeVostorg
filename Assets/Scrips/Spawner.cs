using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] Waypoints;
    static public float Delay = 0.5f;
    public float _delay = 0.5f;
    float DelayTimer = 0;
    static private float FirstDelay = 0.5f;
    static private bool isDelayX = false; 
    void Start(){
        Delay = _delay;
        FirstDelay = Delay;
    }

    void Update(){
        DelayTimer += Time.deltaTime;
        if (DelayTimer >= Delay) {
            DelayTimer -= Delay;
            Spawn();
        }
        if(isDelayX) {
            Delay = Delay / 2;
            isDelayX = false;
        }
    }
    void Spawn() {
        GameObject b = Instantiate(Controller.BallPrefab, new Vector3(transform.position.x, transform.position.y, -1), 
            Quaternion.identity);
        if (Controller.BallsList.count >= 1) {
            Controller.BallsList.tail.Data.SendMessage("SetNext", b);
        }
        b.SendMessage("SetWaypoints", Waypoints);
        Sprite sp = Controller.Sprites[Random.Range(0, Controller.Sprites.Length )];

         while (Controller.BallsList.count >= 2 && sp.name == Controller.BallsList.tail.Data.GetComponentInChildren<SpriteRenderer>().sprite.name && 
            sp.name == Controller.BallsList.tail.Previous.Data.GetComponentInChildren<SpriteRenderer>().sprite.name) {
            sp = Controller.Sprites[Random.Range(0, Controller.Sprites.Length )];
        }

        b.SendMessage("SetSprite", sp);
        Controller.BallsList.Add(b);
    }

    static public void Wait() {
        Delay = FirstDelay * 2;
        isDelayX = true;
    }
}
