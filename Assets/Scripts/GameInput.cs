using System;
using UnityEngine;

public class GameInput : MonoBehaviour {


    public static GameInput Instance { get; private set; }


    public event EventHandler OnShoot;


    private PlayerControls playerControls;


    private void Awake() {
        Instance = this;

        playerControls = new PlayerControls();
        playerControls.Player.Enable();

        playerControls.Player.Shoot.performed += Shoot_performed;
    }

    private void OnDisable() {
        playerControls.Player.Disable();
    }

    private void Shoot_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnShoot?.Invoke(this, EventArgs.Empty);
    }


    public float GetMoveInput() {
        float moveInput = playerControls.Player.Move.ReadValue<float>();

        return moveInput;
    }

}
