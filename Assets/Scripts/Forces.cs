using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forces : MonoBehaviour
{
    public static Forces current;

    public int players = 2;
    public bool[] freeRoots;
    // Start is called before the first frame update

    private void Awake() {
        current = this;
    }

    public void SetPlayers(int players) {
        this.players = players;
        freeRoots = new bool[players];
    }

    public void SetIsRootFree(bool value, int player) {
        freeRoots[player - 1] = value;
    }

    public int FreeRoots() {
        var value = 0;
        foreach (bool v in freeRoots) {
            value += v ? 0 : 1;
        }
        return value;
    }
}
