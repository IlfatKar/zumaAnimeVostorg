using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject BallPrefab;
    public Transform[] Waypoints;
    public float Delay = 0.5f;
    float DelayTimer = 0;
    private List<GameObject> Balls = new List<GameObject>();
    public Sprite[] Sprites;

    void Start(){}


    void Update(){
        DelayTimer += Time.deltaTime;
        if (DelayTimer >= Delay) {
            DelayTimer -= Delay;
            Spawn();
        }
    }
    void Spawn() {
        GameObject b = Instantiate(BallPrefab, new Vector3(transform.position.x, transform.position.y, -1), 
            Quaternion.identity);
        b.SendMessage("SetWaypoints", Waypoints);
        Sprite sp = Sprites[Random.Range(0, Sprites.Length - 1)];
        while (Balls.Count >= 3 && sp.name == Balls[Balls.Count - 1].GetComponentInChildren<SpriteRenderer>().sprite.name && 
            sp.name == Balls[Balls.Count - 2].GetComponentInChildren<SpriteRenderer>().sprite.name) {
            sp = Sprites[Random.Range(0, Sprites.Length - 1)];
        }
        b.SendMessage("SetSprite", sp);
        Balls.Add(b);
    }
    void FixedUpdate() {
    }
}
