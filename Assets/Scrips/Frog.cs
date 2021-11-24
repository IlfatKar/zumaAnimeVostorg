using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    public Sprite[] Sprites;
    private GameObject Curr;
    public GameObject Projectile;

    void Start()
    {
        Sprite sprite = Sprites[Random.Range(0, Sprites.Length - 1)];
        Curr = Instantiate(Projectile, new Vector3(transform.position.x, transform.position.y, -1), transform.rotation);
        Curr.SendMessage("SetSprite", sprite);
    }

    void Update()
    {
        var MousePosition = Input.mousePosition;
        MousePosition = Camera.main.ScreenToWorldPoint(MousePosition);
        float Angle = Vector2.Angle(Vector2.right, MousePosition - transform.position);
        
        if (Input.GetMouseButtonDown(0))
        {
            Angle = transform.position.y < MousePosition.y ? Angle : -Angle;
            Curr.SendMessage("Push", Angle);
            Curr = Instantiate(Projectile, new Vector3(transform.position.x, transform.position.y, -1), transform.rotation);
            Sprite sprite = Sprites[Random.Range(0, Sprites.Length - 1)];
            Curr.SendMessage("SetSprite", sprite);
        }
    }
}
