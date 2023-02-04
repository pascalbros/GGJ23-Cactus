using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject[] menuButtons;

    void Start() {
        StartCoroutine(EnableButtons(1));
    }

    void Update() {
        
    }
    
    IEnumerator EnableButtons(int secs) {
        yield return new WaitForSeconds(secs);
        foreach (var button in menuButtons) {
            button.SetActive(true);
        }
    }
}
