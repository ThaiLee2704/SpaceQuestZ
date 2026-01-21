using UnityEngine;

[RequireComponent(typeof(IPlayerInput))]

public class GameManager : Singleton<GameManager>
{
    public float worldSpeed;

    [SerializeField] private PlayerController player;
    public PlayerController Player => player;

    private IPlayerInput input;

    // Vì GameManager kế thừa Singleton mà trong Singleton có Awake() rồi nên muốn dùng
    // Awake trong GameManager thì ta phải kế thừa và viết lại dựa trên base Singleton
    // Nếu không muốn dùng Awake thì có thể đặt trong Start()
    // Nếu gọi thẳng trực tiếp luôn trong Awake() có thể dẫn đến lỗi NullEx vì
    // input trong Awake được tạo nhưng Instant trong Singleton chưa được tạo dẫn đến
    // không thể tham chiếu Player qua GameManager.Instant => các class gọi đến Player
    // đều bị NullEx

    //Cách dùng Awake() (Sửa cả bên Singleton để cho kế thừa)
    //protected override void Awake()
    //{
    //    base.Awake();
    //    input = GetComponent<IPlayerInput>();
    //}

    //Cách dùng Start()
    private void Start()
    {
        input = GetComponent<IPlayerInput>();
    }

    private void Update()
    {
        if (input.IsPausePressed)
            UIManager.Instant.PausePanel();
    }

}
