using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private float _speed = 5f;

    private Vector2 _moveVector2;

    public bool FaceRight { get; private set; }

    private const string Horizontal = "Horizontal";

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        FaceRight = true;
    }

    private void FixedUpdate()
    {
        Move();
        Reflect();
    }

    private void Move()
    {
        _moveVector2.x = Input.GetAxis(Horizontal);
        _rigidbody2D.velocity = new Vector2(_moveVector2.x * _speed, _rigidbody2D.velocity.y);

        _animator.SetFloat(PlayerAnimatorController.Params.ParamMoveX, Mathf.Abs(_moveVector2.x));
    }
    private void Reflect()
    {
        if ((_moveVector2.x > 0 && FaceRight == false) || (_moveVector2.x < 0 && FaceRight == true))
        {
            _spriteRenderer.flipX = FaceRight;
            FaceRight = !FaceRight;
        }
    }
}