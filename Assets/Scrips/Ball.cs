using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Transform[] Waypoints;
    public float MoveSpeed = 2f;
    public float MoveSpeedMult = 1f;
    [HideInInspector]
    public int WaypointIdx = 0;
    [HideInInspector]
    public bool isStop = false;
    [HideInInspector]
    public DoublyNode<GameObject> BallInList = null;
    public float MaxDistance = 1.3f;
    private bool isHidden = false;
    void Update(){
        if (!isStop) {
            Move();
             if (!(Controller.DistanceToNext(BallInList) < MaxDistance)) {
                isStop = true;
            }
        } else {
            if (Controller.DistanceToNext(BallInList) < MaxDistance && Controller.IsNextsMove(BallInList)) {
                isStop = false;
            }
        }
        if (isHidden) {
            if (Controller.DistanceToPrev(BallInList) < MaxDistance) {
                SetHidden(false);
            }
        }
    }

    private void Start(){
        StartCoroutine(YieldOneSecond());
    }

    IEnumerator YieldOneSecond(){
        while (Application.isPlaying && BallInList.Next == null)
        {
            yield return new WaitForSecondsRealtime(.5f);
        }
    }

    void Move() {
        transform.position = Vector2.MoveTowards(transform.position,
            Waypoints[WaypointIdx].transform.position, MoveSpeed * MoveSpeedMult * Time.deltaTime);
        if(Vector2.Distance(transform.position, Waypoints[WaypointIdx].transform.position) <= 1f) {
            WaypointIdx++;
        }
        if (WaypointIdx == Waypoints.Length) {
            WaypointIdx = 0;
            Controller.GameOver();
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

    void SetHidden(bool b) {
        GetComponentInChildren<SpriteRenderer>().color = b ? new Color(255,255,255,0) : new Color(255,255,255,1);
        if (b) {
            isHidden = true;
            MoveSpeedMult = 10f * (Controller.BallsList.tail.Previous.Data.GetComponent<Ball>().WaypointIdx - WaypointIdx);
        } else {
            isHidden = false;
            MoveSpeedMult = 1f;
        }
    }
}
