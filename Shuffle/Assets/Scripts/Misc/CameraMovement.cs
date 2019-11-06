using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField]
    [Range(0, 1)]
    private float _followSpeed = 0.1f;

    private Vector3 _offset;

    private void Start()
    {
        _offset = transform.position - _target.position;
    }

    private void Update()
    {
        if (Mathf.Abs(transform.position.x - _target.position.x) <= 0.1f &&
            Mathf.Abs(transform.position.y - _target.position.y) <= 0.1f &&
            Mathf.Abs(transform.position.z - _target.position.z) <= 0.1f)
            transform.position = _target.position + _offset;
        else
            transform.position = Vector3.Lerp(transform.position, _target.position + _offset, _followSpeed);
    }
}
