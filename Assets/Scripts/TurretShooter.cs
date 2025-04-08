using System;
using UnityEngine;

public class TurretShooter : MonoBehaviour
{


    [SerializeField] private Transform rocketPosition;
    [SerializeField] private GameObject rocketPrefab;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float rocketSpeed;


    private void Start() {
        gameInput.OnShoot += GameInput_OnShoot;
    }

    private void GameInput_OnShoot(object sender, EventArgs e) {
        GameObject rocket = Instantiate(rocketPrefab, rocketPosition.position, Quaternion.identity);

        Rigidbody rb = rocket.GetComponent<Rigidbody>();
        if (rb != null)
            rb.linearVelocity = Vector3.up * rocketSpeed;
    }

    private void Update() {
        
    }

}
