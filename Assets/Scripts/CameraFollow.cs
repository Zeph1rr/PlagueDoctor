using System;
using UnityEngine;

public class CameraFollow: MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _moveSpeed;

    private void Awake()
    {
        transform.position = new Vector3()
        {
            x = _playerTransform.position.x,
            y = 0,
            z = _playerTransform.position.z - 10,
        };
    }

    private void Update()
    {
        if (!_playerTransform) return;
        Vector3 target = new Vector3()
        {
            x = _playerTransform.position.x,
            y = 0,
            z = _playerTransform.position.z - 10,
        };

        Vector3 position = Vector3.Lerp(transform.position, target, _moveSpeed * Time.deltaTime);

        transform.position = position;

    }
}
