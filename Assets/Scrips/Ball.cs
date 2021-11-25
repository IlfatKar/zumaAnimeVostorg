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
    public bool isStop = false;
    [HideInInspector]
    public DoublyNode<GameObject> BallInList = null;
    public float MaxDistance = 1.3f;

    void Update(){
        if (!isStop) {
            Move();
        } else {
            if (Controller.DistanceToNext(BallInList) < MaxDistance && Controller.IsNextsMove(BallInList)) {
                isStop = false;
            }
        }  
    }

    void Move() {
        transform.position = Vector2.MoveTowards(transform.position,
            Waypoints[WaypointIdx].transform.position, MoveSpeed * Time.deltaTime);
        if(Vector2.Distance(transform.position, Waypoints[WaypointIdx].transform.position) <= .5f) {
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

    void SetBallInList(DoublyNode<GameObject> ball) {
        BallInList = ball;
    }

    void GoBack() {
        isStop = true;
    }

}
