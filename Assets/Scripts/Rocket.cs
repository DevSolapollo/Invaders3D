using UnityEngine;

public class Rocket : MonoBehaviour {


    private float _lifetime = 3f;


    private void Start() {
        Destroy(gameObject, _lifetime);
    }

    private void Update() {
        transform.Rotate(Vector3.up, 1f);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent<Invader>(out Invader invader)) {
            invader.Die();
            Destroy(gameObject);
        }
    }

}
