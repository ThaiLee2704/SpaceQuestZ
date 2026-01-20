using UnityEngine;

public interface IPlayerInput
{
    Vector2 Direction { get; }
    float DirectionX { get; }
    float DirectionY { get; }
    bool IsBoostBtnDown { get; }
}

//Trong thực tế chúng ta có rất nhiều kiểu Input như: PCInput, MobileInput, BotInput,...
//Trong PlayerController sẽ nhận dữ liệu Input đầu vào, nhưng không thể để mặc định
//một kiểu Input được. Vậy nên chúng ta có Interface IPlayerInput nhằm mục đích cho
//các kiểu Input khác nhau implement Interface đó, sau đó PlayerController chỉ cần
//tham chiếu đến các thuộc tính và phương thức có trong Interface để lấy dữ liệu là 
//được chứ không cần biết nguồn gốc dữ liệu đó được Input vào bằng cách nào.

//=> Vì tất cả đều cung cấp Output giống nhau (Direction, DirectionX, DirectionY trong
//trương hợp này), nhưng cách tạo ra Input lại khác nhau (PC, Mobile, Bot,...)
//Interface giúp PlayerController không quan tâm đến Input đến từ đâu mà vẫn dùng Output luôn

//=> Interface = PlayerController chỉ cần biết cần gì, không cần biết ai cung cấp

//public class PlayerInputBase 
//{
//    protected virtual Vector2 Direction { get; }
//    protected virtual float DirectionX { get; }
//    protected virtual float DirectionY { get; }

//    protected virtual bool IsBoosting { get; }
//}