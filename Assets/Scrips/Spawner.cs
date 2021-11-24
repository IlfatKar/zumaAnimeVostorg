using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject BallPrefab;
    public Transform[] Waypoints;
    static public float Delay = 0.5f;
    public float _delay = 0.5f;
    float DelayTimer = 0;
    static private float FirstDelay = 0.5f;
    private List<GameObject> Balls = new List<GameObject>();
    public Sprite[] Sprites;
    [HideInInspector]
    public static GameObject BALLPREF; 
    static private bool isDelayX = false; 
    void Start(){
        BALLPREF = BallPrefab;
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
        GameObject b = Instantiate(BallPrefab, new Vector3(transform.position.x, transform.position.y, -1), 
            Quaternion.identity);
        if (Balls.Count >= 1) {
            Balls[Balls.Count - 1].SendMessage("SetNext", b);
        }
        b.SendMessage("SetWaypoints", Waypoints);
        Sprite sp = Sprites[Random.Range(0, Sprites.Length - 1)];
        while (Balls.Count >= 3 && sp.name == Balls[Balls.Count - 1].GetComponentInChildren<SpriteRenderer>().sprite.name && 
            sp.name == Balls[Balls.Count - 2].GetComponentInChildren<SpriteRenderer>().sprite.name) {
            sp = Sprites[Random.Range(0, Sprites.Length - 1)];
        }
        b.SendMessage("SetSprite", sp);
        Balls.Add(b);
    }
    
    static public void Wait() {
        Delay = FirstDelay * 2;
        isDelayX = true;
    }
}
