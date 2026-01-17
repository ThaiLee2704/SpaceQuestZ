using UnityEngine;

namespace ObserverTest
{
    public class EnemyManager : MonoBehaviour, ISubscriber
    {
        private void Start()
        {
            GameManager.Instance.OBS.AddListener(this);
        }

        public void OnNotify()
        {
            Debug.Log("Enemy do something");
        }
    }

}
