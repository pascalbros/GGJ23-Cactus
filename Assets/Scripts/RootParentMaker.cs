using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootParentMaker : MonoBehaviour
{
    public int numberOfPlayers = 2;
    public int numberOfPieces = 13;
    public Vector2 hingeOffsetStart;
    public Vector2 piecesOffset;
    public Material lineMaterial;
    public float maxForce = 0.11f;
    public bool mirrorRootPosition = false;
    public bool canChangeForce = true;

    void Start() {
        Forces.current.SetPlayers(numberOfPlayers);
        var xOffset = Mathf.Abs((hingeOffsetStart.x * 2) / (numberOfPlayers - 1));
        for (int i = 0; i < numberOfPlayers; i++) {
            CreateRootParentTransform(xOffset * i, i + 1);
        }
    }

    void CreateRootParentTransform(float xOffset, int player) {
        var rootParent = new GameObject();
        rootParent.transform.parent = transform;
        rootParent.transform.localPosition = new Vector3(hingeOffsetStart.x + xOffset, hingeOffsetStart.y, 0.05f);
        rootParent.name = "Root"+player;
        var input = rootParent.AddComponent<InputController>();
        input.playerIndex = player - 1;
        input.canChangeForce = canChangeForce;
        var rootMaker = rootParent.AddComponent<RootMaker>();
        rootMaker.maxForce = maxForce;
        rootMaker.playerNumber = player;
        rootMaker.numberOfPieces = numberOfPieces;
        rootMaker.lineMaterial = lineMaterial;
        rootMaker.piecesOffset = piecesOffset;
        if (mirrorRootPosition) {
            if (player > numberOfPlayers / 2) {
                rootMaker.piecesOffset *= Vector2.left;
            }
        }
    }


}
