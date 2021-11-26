using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void OnMouseDown() {
        GetComponent<AudioSource>().Play(); 
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
}
