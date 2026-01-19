using Unity.VisualScripting;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    float imageWidthInGame;


    private void Awake()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        imageWidthInGame = sprite.texture.width / sprite.pixelsPerUnit;
    }

    void Update()
    {
        float playerBoostMultiplier = GameManager.Instant.Player.BoostSpeed;

        float moveX = moveSpeed * playerBoostMultiplier * Time.deltaTime;
        transform.position += new Vector3(moveX, 0);
        if (Mathf.Abs(transform.position.x) - imageWidthInGame > 0)
            transform.position = new Vector3(0f, transform.position.y);
    }
}
