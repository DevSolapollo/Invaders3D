using UnityEngine;

public class ThreeDUI : MonoBehaviour
{


    void Update() {
        transform.Rotate(0f, 50f * Time.deltaTime, 0f);
    }

}
