using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraChanger : MonoBehaviour
{
    public CinemachineVirtualCamera camera;
    public bool dampingEnabled = false;
    public Vector2 damping;
    private Vector2 dampingInitialValue;

    public bool screenPositionEnabled = false;
    public Vector2 screenPosition;
    private Vector2 screenPositionInitialValue;

    public bool ortoSizeEnabled = false;
    public float ortoSize;
    private float ortoSizeInitialValue;

    void Start() {
        var component = camera.GetCinemachineComponent<CinemachineFramingTransposer>();
        dampingInitialValue = new Vector2(component.m_XDamping, component.m_YDamping);
        screenPositionInitialValue = new Vector2(component.m_ScreenX, component.m_ScreenY);
        ortoSizeInitialValue = camera.m_Lens.OrthographicSize;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name != "Cactus") { return; }
        iTween.ValueTo(gameObject, iTween.Hash("from", 0f, "to", 1f, "time", 2, "onupdatetarget", gameObject, "onupdate", "OnTriggerAnimation"));
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.name != "Cactus") { return; }
        iTween.ValueTo(gameObject, iTween.Hash("from", 1f, "to", 0f, "time", 2, "onupdatetarget", gameObject, "onupdate", "OnTriggerAnimation"));
    }

    void OnTriggerAnimation(float value) {
        var transposer = camera.GetCinemachineComponent<CinemachineFramingTransposer>();

        if (dampingEnabled) {
            transposer.m_XDamping = Mathf.Lerp(dampingInitialValue.x, damping.x, value);
            transposer.m_YDamping = Mathf.Lerp(dampingInitialValue.y, damping.y, value);
        }
        if (screenPositionEnabled) {
            transposer.m_ScreenX = Mathf.Lerp(screenPositionInitialValue.x, screenPosition.x, value);
            transposer.m_ScreenY = Mathf.Lerp(screenPositionInitialValue.y, screenPosition.y, value);
        }

        if (ortoSizeEnabled) {
            camera.m_Lens.OrthographicSize = Mathf.Lerp(ortoSizeInitialValue, ortoSize, value);
        }
    }
}
