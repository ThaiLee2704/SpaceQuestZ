using UnityEngine;

public class PCInput : MonoBehaviour, IPlayerInput
{
    public Vector2 Direction { get; private set; }
    public float DirectionX => Direction.x;
    public float DirectionY => Direction.y;
    public bool IsBoostingButtonDown { get; private set; }


    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Direction = new Vector2(x, y).normalized;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire2"))
            IsBoostingButtonDown = true;
        else if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Fire2"))
            IsBoostingButtonDown = false;

    }
}
