using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public bool TriggerActive { get; private set; }

    private void Start()
    {
        TriggerActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            TriggerActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            TriggerActive = false;
        }
    }
}