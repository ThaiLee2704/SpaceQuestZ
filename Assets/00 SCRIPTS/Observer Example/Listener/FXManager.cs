using UnityEngine;

namespace ObserverTest
{
    //BẢN KHÔNG TRUYỀN DATA

    //public class FXManager : MonoBehaviour
    //{
    //    private void Start()
    //    {
    //        Observer.AddListener(CONSTANT.INVOKE_ACTION_TAKE_DAMAGE, OnTakeDamageFX);
    //    }

    //    private void OnDestroy()
    //    {
    //        Observer.RemoveListener(CONSTANT.INVOKE_ACTION_TAKE_DAMAGE, OnTakeDamageFX);
    //    }

    //    void OnTakeDamageFX()
    //    {
    //        Debug.Log("Phat FX bi danh");
    //    }
    //}

    //BẢN TRUYỀN DATA
    
    public class FXManager : MonoBehaviour
    {
        private void Start()
        {
            Observer.AddListener(CONSTANT.INVOKE_ACTION_TAKE_DAMAGE, OnTakeDamageFX);
        }

        private void OnDestroy()
        {
            Observer.RemoveListener(CONSTANT.INVOKE_ACTION_TAKE_DAMAGE, OnTakeDamageFX);
        }

        void OnTakeDamageFX(object[] datas)
        {
            Debug.Log("Phat FX bi danh");
        }
    }
}
