using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider xpSlider;
    [SerializeField] private PlayerHealth playerHealth;

    void Update()
    {
        healthSlider.value = playerHealth.GetHealthRatio();
    }

    public void Start()
    {
        Time.timeScale = 0;
    }

    public void StartButton()
    {
        Time.timeScale = 1;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
