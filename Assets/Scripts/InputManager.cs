using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.InputSystem;

public class InputManager: MonoBehaviour {
    public static int currentPlayers;
    public int playerIndex;

    void Start() {
        if (InputSingleton.current == null) {
            InputSingleton.current = new InputSingleton();
            currentPlayers = 1;
        } else {
            currentPlayers += 1;
            playerIndex = currentPlayers - 1;
        }
    }

    public void OnMove(CallbackContext context) {
        var type = context.control.device.path;
        if (!type.StartsWith("/Keyboard")) {
            UpdateMovement(context.ReadValue<Vector2>(), playerIndex);
        } else {
            UpdateMovement(context.ReadValue<Vector2>(), playerIndex);
        }
    }

    void UpdateMovement(Vector2 value, int player) {
        InputSingleton.current.dPads[player] = value;
    }

    public void OnPlayerJoined() {
        Debug.Log("Connected");
        GetComponent<PlayerInput>().enabled = true;
    }
}
