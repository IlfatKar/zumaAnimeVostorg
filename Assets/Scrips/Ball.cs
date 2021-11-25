using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Transform[] Waypoints;
    public float MoveSpeed = 2f;
    [HideInInspector]
    public int WaypointIdx = 0;
    [HideInInspector]
    public GameObject NextBall = null;
    private bool isStop = false;

    void Update(){
        if (!isStop) {
            Move();
        }
    }

    void Move() {
        transform.position = Vector2.MoveTowards(transform.position,
            Waypoints[WaypointIdx].transform.position, MoveSpeed * Time.deltaTime);
        if(Vector2.Distance(transform.position, Waypoints[WaypointIdx].transform.position) <= .5f) {
            //Rotate();
            WaypointIdx++;
        }
        if (WaypointIdx == Waypoints.Length) {
            WaypointIdx = 0;
            // GAME OVER
        }
    }

    void SetWaypoints(Transform[] points) {
        Waypoints = points;
    }

    void SetSprite(Sprite sp) {
         GetComponentInChildren<SpriteRenderer>().sprite = sp;
    }

    void SetWaypointIdx(int Idx) {
        WaypointIdx = Idx;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (isStop && collision.gameObject.tag != "Line") {
            isStop = false;
        }
    }

    void SetNext(GameObject Next) {
        NextBall = Next;
    }

    void GoBack() {
        isStop = true;
    }

}
