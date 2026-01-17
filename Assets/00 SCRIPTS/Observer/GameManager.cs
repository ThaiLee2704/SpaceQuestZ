using Unity.VisualScripting;
using UnityEngine;

namespace ObserverTest
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        public static GameManager Instance => instance;

        Observer obs = new Observer();
        public Observer OBS => obs;

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                obs.Notify();
            }
        }
    }

}