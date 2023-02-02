using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.InputSystem;

public class InputManager: MonoBehaviour {
    public int playerIndex;

    void Start() {
        if (InputSingleton.current == null) {
            InputSingleton.current = new InputSingleton();
        }
    }

    public void OnMove(CallbackContext context) {
        var type = context.control.device.path;
        if (!type.StartsWith("/Keyboard")) {
            var hashCode = context.control.device.GetHashCode();
            var player = InputSingleton.current.GetPlayerIndexFromController(hashCode);
            if (player < 0) {
                var freePlayer = InputSingleton.current.GetFirstFreePlayer();
                if (freePlayer >= 0) {
                    InputSingleton.current.controllers[freePlayer] = hashCode;
                    player = freePlayer;
                } else {
                    player = 0;
                }
            }
            UpdateMovement(context.ReadValue<Vector2>(), player);
        } else {
            UpdateMovement(context.ReadValue<Vector2>(), playerIndex);
        }
    }

    void UpdateMovement(Vector2 value, int player) {
        InputSingleton.current.dPads[player] = value;
    }
}
