using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour {
    private GameObject Curr;
    public GameObject Projectile;
    float Delay = 0.3f;
    float DelayTimer = 0;
    void Start() {
        Sprite sprite = Controller.Sprites[Random.Range(0, Controller.Sprites.Length)];
        Curr = Instantiate(Projectile, new Vector3(transform.position.x, transform.position.y, -1), transform.rotation);
        Curr.GetComponent<Rigidbody2D>().simulated = false;
        Curr.SendMessage("SetSprite", sprite);
    }

    void Update() {
        OnClick();
        Rotate();
    }

    void Rotate() {
        var mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        var angle = Vector2.Angle(Vector2.right, mousePosition - transform.position);
        transform.eulerAngles = new Vector3(0f, 0f, transform.position.y < mousePosition.y ? angle : -angle);
        Curr.transform.eulerAngles = new Vector3(0f, 0f, transform.position.y < mousePosition.y ? angle : -angle);
    }

    float GetAngleToMouse() {
        var MousePosition = Input.mousePosition;
        MousePosition = Camera.main.ScreenToWorldPoint(MousePosition);
        float Angle = Vector2.Angle(Vector2.right, MousePosition - transform.position);
        Angle = transform.position.y < MousePosition.y ? Angle : -Angle;

        return Angle;
    }

    void OnClick() {
        DelayTimer += Time.deltaTime;
        if (DelayTimer >= Delay) {
            if (Input.GetMouseButtonDown(0)){
                GetComponent<AudioSource>().Play();
                float Angle = GetAngleToMouse();

                Curr.GetComponent<Rigidbody2D>().simulated = true;
                Curr.SendMessage("Push", Angle);
                Sprite sprite = Controller.Sprites[Random.Range(0, Controller.Sprites.Length)];
                Curr = Instantiate(Projectile, new Vector3(transform.position.x, transform.position.y, -1), transform.rotation);
                Curr.GetComponent<Rigidbody2D>().simulated = false;
                Curr.SendMessage("SetSprite", sprite);
                DelayTimer = 0;
            }
        }
    }
}
