using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement _player;

    [SerializeField]
    private float _smoothingMovements = 3.5f;

    [SerializeField]
    private Vector2 _offset = new Vector2(_defaultOffsetX, _defaultOffsetY);

    private const float _defaultOffsetX = 2.5f;
    private const float _defaultOffsetY = 3f;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (_player.FaceRight == true)
        {
            ChangePosition(TargetPosition.right);
        }
        else
        {
            ChangePosition(TargetPosition.left);
        }
    }

    private void ChangePosition(TargetPosition direction)
    {
        float x = 0f;

        if (direction == TargetPosition.right)
        {
            x = _player.transform.position.x + _offset.x;
        }
        else if (direction == TargetPosition.left)
        {
            x = _player.transform.position.x - _offset.x;
        }

        transform.position = Vector3.Lerp(transform.position,
            new Vector3(x, _player.transform.position.y + _offset.y, transform.position.z),
            _smoothingMovements * Time.fixedDeltaTime);
    }

    private enum TargetPosition
    {
        left,
        right
    }
}