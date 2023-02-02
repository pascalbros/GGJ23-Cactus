using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public Rigidbody2D boneRigidbody;
    public HingeJoint2D boneHinge;
    public TipController tip;
    public int playerIndex = 0;
    public float maxForce = 0.17f;

    private Vector2 currentForce = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate() {
        if (tip != null && !tip.canAttach) {
            boneRigidbody.AddForce(currentForce);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var axis = InputSingleton.current.dPads[playerIndex];
        if (axis.x != 0 || axis.y != 0) {
            tip.canAttach = false;
            var force = maxForce;
            var freeRoots = Forces.current.FreeRoots();
            force /= freeRoots == 0 ? 1 : freeRoots;
            if (freeRoots == Forces.current.players) {
                force = maxForce / (freeRoots * 2);
            }
            currentForce = new Vector2(axis.x * force, axis.y * force);
        } else {
            boneRigidbody.bodyType = RigidbodyType2D.Dynamic;
            tip.canAttach = true;
        }
    }
}
