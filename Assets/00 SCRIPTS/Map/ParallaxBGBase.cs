using UnityEngine;

public abstract class ParallaxBGBase : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    protected float imageWidthInGame;

    protected void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        imageWidthInGame = sprite.texture.width / sprite.pixelsPerUnit;
    }

    protected abstract void Update();
}
