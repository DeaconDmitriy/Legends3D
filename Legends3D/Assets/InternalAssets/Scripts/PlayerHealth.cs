using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 100;
    [SerializeField] private float timeBetweenHits = 1f;
    [SerializeField] private Collider[] weapons;

    private int _currentHealth;
    private int _currentMaxHealth;
    private float lastHitTime = 0;
    private Animator animator;
    public AudioSource _HP;
    public AudioSource Music;
    public AudioSource MusicGameOver;
    public AudioSource prokachka;
    public Slider sliderXP;
    public GameObject GameOverPanel;

    public static bool isAlive = true;

    public int CurrentHealth
    {
        get { return _currentHealth; }
        set
        {
            if (value < 0)
                _currentHealth = 0;
            else
                _currentHealth = value;
        }
    }

    public void EnableWapons()
    {
        foreach (Collider weapon in weapons)
            weapon.enabled = true;
    }
    public void DisableWapons()
    {
        foreach (Collider weapon in weapons)
            weapon.enabled = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("EnemyWeapon") && isAlive && Time.time - lastHitTime > timeBetweenHits)
        {
            TakeDamage(5);
        }
    }

    public void TakeDamage(int damage)

    {
        lastHitTime = Time.time;
        _currentHealth -= damage;
        Debug.Log("Current Health: " + _currentHealth);
        if (_currentHealth > 0)
        {
            animator.SetTrigger("HitBack");
            _HP.Play();
        }
        else
        {
            animator.SetTrigger("Death");
            isAlive = false;
            Music.Stop();
            GameOverPanel.SetActive(true);
            MusicGameOver.Play();
            Time.timeScale = 0;
        }
    }

    public void NewLevel()
    {
        _currentHealth = startingHealth;
        prokachka.Play();
        sliderXP.value += 25;
    }
    void Awake()
    {
        animator = GetComponent<Animator>();
        _currentHealth = startingHealth;
        _currentMaxHealth = startingHealth;
        isAlive = true;
        DisableWapons();
    }

    public float GetHealthRatio()
    {
        return (float)_currentHealth / (float)_currentMaxHealth;
    }

    void Update()
    {
        
    }
}
