using UnityEngine;

public class Invader : MonoBehaviour
{


    public void Die() {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Turret>() != null) {
            GameEvents.TriggerGameOver();
        }
    }

}
