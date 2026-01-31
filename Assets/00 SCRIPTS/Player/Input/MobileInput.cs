using UnityEngine;

public class MobileInput : MonoBehaviour, IPlayerInput
{
    //Thực hiện cách Input của Mobile
    public Vector2 Direction {  get; private set; }
    public float DirectionX => Direction.x;
    public float DirectionY => Direction.y;
    public bool IsBoostBtnDown { get; private set; }
    public bool IsPausePressed { get; private set; }
    public bool IsFirePressed {  get; private set; }

}
