using System.Collections;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pipe : MonoBehaviour
{
    public Transform connection;
    private float vertical;
    public Vector3 enterDirection = Vector3.down;
    public Vector3 exitDirection = Vector3.zero;
    private bool isPipe;
    [SerializeField] private float MaxScale;
    [SerializeField] private Vector3 NoMoreScale;


    private void Update()
    {
        if(isPipe)
        {
            PlayerMovement2D.PlayerMovement2Dinstance.isOnPipe = true;
        }
        else
        {
            PlayerMovement2D.PlayerMovement2Dinstance.isOnPipe = false;
        }

    }
    public void DownPlatform(InputAction.CallbackContext context)
    {
        vertical = context.ReadValue<Vector2>().y;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (connection != null && other.CompareTag("Player"))
        {
            if (vertical < 0)
            { 
                StartCoroutine(Enter(other.transform));
                isPipe = true;
            }
        }
    }

    private IEnumerator Enter(Transform player)
    {
        player.GetComponent<PlayerMovement2D>().enabled = false;

        Vector3 enteredPosition = transform.position + enterDirection;

        Vector3 enteredScale = Vector3.one * 0.5f;
        if(player.transform.localScale.x <= MaxScale && player.transform.localScale.y <= MaxScale)
        {
            enteredScale = NoMoreScale;
        }


        yield return Move(player, enteredPosition, enteredScale);
        yield return new WaitForSeconds(1f);

        if (exitDirection != Vector3.zero)
        {
            player.position = connection.position - exitDirection;
            yield return Move(player, connection.position + exitDirection, Vector3.one);
        }
        else
        {
            player.position = connection.position;
            player.localScale = Vector3.one;
        }
        player.GetComponent<PlayerMovement2D>().enabled = true;
    }

    private IEnumerator Move(Transform player, Vector3 endPosition, Vector3 endScale)
    {
        float elapsed = 0f;
        float duration = 1f;

        Vector3 startPosition = player.position;
        Vector3 startScale = player.localScale;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            player.position = Vector3.Lerp(startPosition, endPosition, t);
            player.localScale = Vector3.Lerp(startScale, endScale, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        player.position = endPosition;
        player.localScale = endScale;
        PlayerMovement2D.PlayerMovement2Dinstance.facingRight = true;

        isPipe = false;
    }

}
