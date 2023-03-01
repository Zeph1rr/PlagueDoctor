using UnityEngine;


public class ParallaxBackground: MonoBehaviour
{
    [SerializeField] private float _parallaxFactor = 0.5f;
    [SerializeField] private Transform _followTarget;

    private Vector3 _lastFollowTargetPosition;
    private Vector3 _deltaFollowTargetPosition;

    private void Start()
    {
        _lastFollowTargetPosition = _followTarget.position;
    }

    private void LateUpdate()
    {
        _deltaFollowTargetPosition = _followTarget.position - _lastFollowTargetPosition;

        transform.position += _deltaFollowTargetPosition * _parallaxFactor;;

        _lastFollowTargetPosition = _followTarget.position;
    }
}
