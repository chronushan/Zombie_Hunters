using UnityEngine;
using Random = UnityEngine.Random;

public class RePosition : MonoBehaviour
{
    private Collider2D collider;
    
    private Vector3 playerPosition;
    private Vector3 mapPosition;
    
    private float diffX;
    private float diffY;
    
    private Vector3 playerDirection;
    private float dirX;
    private float dirY;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Area"))
            return;

        playerPosition = GameManager.I.playerHandler.transform.position;
        mapPosition = transform.position;

        diffX = Mathf.Abs(playerPosition.x - mapPosition.x);
        diffY = Mathf.Abs(playerPosition.y - mapPosition.y);
        
        playerDirection = GameManager.I.playerHandler.GetInput();
        dirX = playerDirection.x < 0 ? -1 : 1;
        dirY = playerDirection.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 80f);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 80f);
                }
                break;
            case "Enemy":
                if (collider.enabled)
                {
                    transform.Translate(playerDirection * 80 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0));
                }
                break;
        }

    }
}
