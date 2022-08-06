using UnityEngine;
using UnityEngine.Events;

public class AudioTrigger : MonoBehaviour
{
    public static event UnityAction<bool> Triggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            Triggered?.Invoke(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            Triggered?.Invoke(false);
        }
    }
}