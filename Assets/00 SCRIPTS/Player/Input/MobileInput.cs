using UnityEngine;

public class MobileInput : MonoBehaviour, IPlayerInput
{
    public Vector2 Direction {  get; private set; }
    public float DirectionX => Direction.x;
    public float DirectionY => Direction.y;
    public bool IsBoostingButtonDown { get; private set; }


    void Update()
    {
        //Thực hiện cách Input của Mobile
    }
}
