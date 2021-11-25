using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ToMenu : MonoBehaviour
{
    private float Delay = 8.2f;
    float DelayTimer = 0;
    void Update()
    {
        DelayTimer += Time.deltaTime;
        if (DelayTimer >= Delay) {
            DelayTimer -= Delay;
            SceneManager.LoadScene("Menu");
        }
    }
}
