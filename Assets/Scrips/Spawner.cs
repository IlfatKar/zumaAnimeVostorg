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
    public int MaxBalls = 30;
    private int SpawnedCount = 0;
    void Start(){
        Delay = _delay;
        FirstDelay = Delay;
    }

    void Update(){
        DelayTimer += Time.deltaTime;
        if (DelayTimer >= Delay && SpawnedCount < MaxBalls) {
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
        b.SendMessage("SetWaypoints", Waypoints);
        Sprite sp = Controller.Sprites[Random.Range(0, Controller.Sprites.Length )];

         while (Controller.BallsList.count >= 2 && sp.name == Controller.BallsList.tail.Data.GetComponentInChildren<SpriteRenderer>().sprite.name && 
            sp.name == Controller.BallsList.tail.Previous.Data.GetComponentInChildren<SpriteRenderer>().sprite.name) {
            sp = Controller.Sprites[Random.Range(0, Controller.Sprites.Length )];
        }

        b.SendMessage("SetSprite", sp);
        Controller.BallsList.Add(b);
        b.SendMessage("SetBallInList", Controller.BallsList.tail);
        SpawnedCount++;
    }

    public void SpawnFromController(Sprite sprite, Vector3 pos, int WaypointIdx) {
        GameObject b = Instantiate(Controller.BallPrefab, pos, Quaternion.identity);
        b.SendMessage("SetWaypoints", Waypoints);
        b.SendMessage("SetWaypointIdx", WaypointIdx);

        b.SendMessage("SetSprite", sprite);

        Controller.BallsList.Add(b);
        b.SendMessage("SetBallInList", Controller.BallsList.tail);
    }

    static public void Wait() {
        Delay = FirstDelay * 2;
        isDelayX = true;
    }
}
