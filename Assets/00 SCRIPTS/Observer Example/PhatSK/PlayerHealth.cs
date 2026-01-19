using Unity.VisualScripting;
using UnityEngine;

namespace ObserverTest
{
    //BẢN KHÔNG TRUYỀN DATA

    //public class PlayerHealth : MonoBehaviour
    //{
    //    [SerializeField] private float currentHealth = 5;
    //    private float maxHealth = 10;

    //    private void Update()
    //    {
    //        if (Input.GetKeyDown(KeyCode.Space))
    //            TakeDamage();
    //    }
    //    private void TakeDamage()
    //    {
    //        Observer.Notify("takeDamage");
    //    }
    //}

    //BẢN TRUYỀN DATA
    
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float currentHealth = 5;
        private float maxHealth = 10;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                TakeDamage();
        }
        private void TakeDamage()
        {
            Observer.Notify("takeDamage", currentHealth, maxHealth);
        }
    }

}