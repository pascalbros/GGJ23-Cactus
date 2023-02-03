using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Finish : MonoBehaviour
{
    public CinemachineVirtualCamera camera;
    public Transform newTarget;
    public Transform confetti;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name != "Cactus") { return; }
        camera.m_Follow = newTarget;
        confetti.gameObject.SetActive(true);
    }
}
