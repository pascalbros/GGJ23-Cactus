using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSingleton
{
    public static InputSingleton current;
    public Vector2[] dPads = new Vector2[2];
    public long[] controllers = new long[2];

    public int GetPlayerIndexFromController(long controller) {
        for (var i = 0; i < controllers.Length; i++) {
            if (controllers[i] == controller) {
                return i;
            }
        }
        return -1;
    }

    public int GetFirstFreePlayer() {
        for (var i = 0; i < controllers.Length; i++) {
            if (controllers[i] == 0) {
                return i;
            }
        }
        return -1;
    }
}
