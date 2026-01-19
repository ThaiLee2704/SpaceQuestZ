using UnityEngine;

namespace ObserverTest
{
    // BẢN KHÔNG TRUYỀN DATA

    //public class UIManager : MonoBehaviour
    //{
    //    private void Start()
    //    {
    //        Observer.AddListener(CONSTANT.INVOKE_ACTION_TAKE_DAMAGE, OnUpdateHealthSlider);
    //    }

    //    private void OnDestroy()
    //    {
    //        Observer.RemoveListener(CONSTANT.INVOKE_ACTION_TAKE_DAMAGE, OnUpdateHealthSlider);
    //    }

    //    void OnUpdateHealthSlider()
    //    {
    //        Debug.Log($"Thay doi UI thanh mau");
    //    }
    //}

    //BẢN TRUYỀN DATA
    
    public class UIManager : MonoBehaviour
    {
        private void Start()
        {
            Observer.AddListener(CONSTANT.INVOKE_ACTION_TAKE_DAMAGE, OnUpdateHealthSlider);
        }

        private void OnDestroy()
        {
            Observer.RemoveListener(CONSTANT.INVOKE_ACTION_TAKE_DAMAGE, OnUpdateHealthSlider);
        }

        void OnUpdateHealthSlider(object[] datas)
        {
            Debug.Log($"Thay doi UI thanh mau: {datas[0]} / {datas[1]}");
        }
    }
}
