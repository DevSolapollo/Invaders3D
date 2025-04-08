using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{


    [SerializeField] private GameObject[] aliens;
    [SerializeField] private int columns = 10;
    [SerializeField] private float spacingX = 1.5f;     
    [SerializeField] private float spacingY = 1.5f;
    [SerializeField] private Vector3 startPosition = new Vector3(-7f, 5f, 0f);


    private void Start() {
        SpawnInvaders();
    }

    private void SpawnInvaders() {
        for (int row = 0; row < aliens.Length; row++) {
            for (int col = 0; col < columns; col++) {
                Vector3 spawnPos = startPosition + new Vector3(col * spacingX, -row * spacingY, 0f);
                Instantiate(aliens[row], spawnPos, Quaternion.identity, transform);
            }
        }
    }
}
