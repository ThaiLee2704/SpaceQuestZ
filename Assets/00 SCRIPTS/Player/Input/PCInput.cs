using UnityEngine;

public class PCInput : MonoBehaviour, IPlayerInput
{
    public Vector2 Direction 
    { 
        get
        {
            float x = Input.GetAxisRaw("Horizontal"); 
            float y = Input.GetAxisRaw("Vertical");
            return new Vector2(x, y).normalized;
        }
    }
    public float DirectionX => Direction.x;
    public float DirectionY => Direction.y;
    public bool IsBoostBtnDown 
    { 
        get
        {
            return Input.GetKey(KeyCode.Space) || Input.GetButton("Fire2");
        }
    }
    public bool IsPausePressed 
    { 
        get
        {
            return Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape);
        }
    }
}
