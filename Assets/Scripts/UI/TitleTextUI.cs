using UnityEngine;

public class TitleTextUI : MonoBehaviour
{


    [SerializeField] private float _amplitude = 0.1f;
    [SerializeField] private float _frequency = 1f;


    private Vector3 _startPos;


    private void Start() {
        _startPos = transform.localPosition;
    }

    private void Update() {
        float offsetY = Mathf.Sin(Time.time * _frequency) * _amplitude;
        transform.localPosition = _startPos + new Vector3(0f, offsetY, 0f);
    }

}
