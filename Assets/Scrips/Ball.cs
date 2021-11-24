using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
  //  public List<Color> Colors;
    // Start is called before the first frame update
    static public Transform[] Waypoints;
    public float MoveSpeed = 2f;
    private int WaypointIdx = 0;
    void Start(){
    }

    // Update is called once per frame
    void Update(){
       Move();
    }

    void SetSprite(Sprite sp) {
         GetComponentInChildren<SpriteRenderer>().sprite = sp;
    }


    void Move() {
         transform.position = Vector2.MoveTowards(transform.position,
            Waypoints[WaypointIdx].transform.position, MoveSpeed * Time.deltaTime);
        if(transform.position == Waypoints[WaypointIdx].transform.position) {
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
}
