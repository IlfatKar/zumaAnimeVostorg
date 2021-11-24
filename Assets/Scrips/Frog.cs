using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    public Sprite[] Sprites;
    private GameObject Curr;
    public GameObject Projectile;
    private float deltaX = 1f;
    private float deltaY = .25f;

    void Start() {
        Sprite sprite = Sprites[Random.Range(0, Sprites.Length - 1)];
        Curr = Instantiate(Projectile, new Vector3(transform.position.x + deltaX, transform.position.y + deltaY, 1), transform.rotation);
        Curr.SendMessage("SetSprite", sprite);
    }

    void Update(){
        OnClick();   
    }

    void OnClick() {
        var MousePosition = Input.mousePosition;
        MousePosition = Camera.main.ScreenToWorldPoint(MousePosition);
        float Angle = Vector2.Angle(Vector2.right, MousePosition - transform.position);
        if (Input.GetMouseButtonDown(0)){
            Angle = transform.position.y < MousePosition.y ? Angle : -Angle;
            Curr.SendMessage("Push", Angle);
            Curr = Instantiate(Projectile, new Vector3(transform.position.x + deltaX, transform.position.y + deltaY, 1), transform.rotation);
            Sprite sprite = Sprites[Random.Range(0, Sprites.Length - 1)];
            Curr.SendMessage("SetSprite", sprite);
        }

    }
}
