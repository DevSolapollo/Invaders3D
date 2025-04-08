using UnityEngine;

public class Rocket : MonoBehaviour {


    private float lifetime = 3f;


    private void Start() {
        Destroy(gameObject, lifetime);
    }

    private void Update() {
        transform.Rotate(Vector3.up, 1f);
    }

}
