using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] Waypoints;
    public float Delay = 0.5f;
    static private float _dealy = 0.5f;
    static float DelayTimer = 0;
    public int MaxBalls = 30;
    private int SpawnedCount = 0;
   
    void Start(){
        _dealy = Delay;
        DelayTimer = 0;
    }

    void Update(){
        DelayTimer += Time.deltaTime;
        if (DelayTimer >= Delay && SpawnedCount < MaxBalls) {
            DelayTimer = 0;
            Spawn();
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

    public void SpawnFromController(Sprite sprite) {
        GameObject b = Instantiate(Controller.BallPrefab, new Vector3(transform.position.x, transform.position.y, -1), Quaternion.identity);
        b.SendMessage("SetWaypoints", Waypoints);

        b.SendMessage("SetSprite", sprite);

        Controller.BallsList.Add(b);
        b.SendMessage("SetHidden", true);
        b.SendMessage("SetBallInList", Controller.BallsList.tail);
    }

    static public void Wait() {
        DelayTimer = -.5f * _dealy;
    }
}
