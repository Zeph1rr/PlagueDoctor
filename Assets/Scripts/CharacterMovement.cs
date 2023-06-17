using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class CharacterMovement : MonoBehaviour, IMovable
{
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _jumpForce = 5.0f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundCheckRadius;

    private bool _isGrounded;
    private Rigidbody2D _rigidbody;

    public void Move(Vector3 direction)
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction * _speed, Time.deltaTime);
        transform.localScale = direction.x switch
        {
            > 0 => new Vector2(1, 1),
            < 0 => new Vector2(-1, 1),
            _ => transform.localScale
        };
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
        Debug.Log(_isGrounded);
    }
}
