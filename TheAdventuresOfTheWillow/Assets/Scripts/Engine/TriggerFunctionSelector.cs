using UnityEngine;
using UnityEngine.Events;

public class TriggerFunctionSelector : MonoBehaviour
{
    public UnityEvent triggerFunction;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            triggerFunction.Invoke();
        }
    }
}
