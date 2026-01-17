using UnityEngine;

namespace ObserverTest
{
    public class ABCManager : MonoBehaviour, ISubscriber
    {
        private void Start()
        {
            GameManager.Instance.OBS.AddListener(this);
        }

        public void OnNotify()
        {
            Debug.Log("ABC do something");
        }
    }
}
