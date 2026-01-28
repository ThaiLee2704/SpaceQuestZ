using UnityEngine;
using UnityEngine.UI;

public class ParallaxBGMainMenu : ParallaxBGBase
{
    protected override void Update()
    {
        float moveX = moveSpeed * Time.deltaTime;
        transform.position += new Vector3(moveX, 0);
        if (Mathf.Abs(transform.position.x) - imageWidthInGame > 0)
            transform.position = new Vector3(0f, transform.position.y);
    }
}
