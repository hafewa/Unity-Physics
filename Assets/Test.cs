using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Enemy m_Enemy;
    public Slider HealthSlider; 
    public void Start()
    {
        m_Enemy.Health = 100;
        HealthSlider.maxValue = m_Enemy.Health;
        HealthSlider.value = m_Enemy.Health;
    }

    public void TestFunc()
    {
        m_Enemy.Health -= 5;
        Debug.Log("Attacked" + m_Enemy.name + " " + m_Enemy.Health);
        HealthSlider.value = m_Enemy.Health;
    }
}