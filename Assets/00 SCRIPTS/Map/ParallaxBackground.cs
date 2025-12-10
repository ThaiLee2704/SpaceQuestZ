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

    void Start()
    {
        
    }

    void Update()
    {
        var playerBoostSpeed = GameManager.Instant.Player.BoostSpeed;

        float moveX = moveSpeed * playerBoostSpeed * Time.deltaTime;
        transform.position += new Vector3(moveX, 0);
        if (Mathf.Abs(transform.position.x) - imageWidthInGame > 0)
            transform.position = new Vector3(0f, transform.position.y);
    }
}
