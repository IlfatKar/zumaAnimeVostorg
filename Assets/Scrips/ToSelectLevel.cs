using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ToSelectLevel : MonoBehaviour
{
     void OnMouseDown() {        
        SceneManager.LoadScene( "LevelSecect");
                GetComponent<AudioSource>().Play();   
 
     }
}
