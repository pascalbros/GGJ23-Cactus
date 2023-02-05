using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
public class Finish : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachine;
    public Transform newTarget;
    public Transform confetti;
    public GameObject fadeOut;
    bool startTimer = false;
    float timer = 6;

    private void Update() {
        if (!startTimer) { return; }
        timer -= Time.deltaTime;
        if (timer < 0) {
            var fade = Instantiate(fadeOut);
            fade.GetComponent<Fade>().nextScene = "Credits";
            startTimer = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name != "Cactus") { return; }
        cinemachine.m_Follow = newTarget;
        confetti.gameObject.SetActive(true);
        startTimer = true;
    }
}
