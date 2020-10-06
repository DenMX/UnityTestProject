using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject Enemy;
    public GameObject Asteroid;
    GameObject Founded; //Переменная для найденных врагов
    public GameObject Player;

    public AudioClip MenuAudio;
    public AudioClip GameAudio;
    private AudioSource audioSource;

    public GameObject inGame; //Интерфейс во время игры
    public GameObject startGame;//Интерфейс при старте игры
    public GameObject endGame;//Интерфейс при поражении
    public GameObject PauseCan;//Интерфейс при паузе

    //Получаем тектовые поля для выведения счета
    private static Text score1;
    private static Text score2;
    public GameObject textScore1;
    public GameObject textScore2;

    private bool isPlaying;

    private static int _score;
    public static int Score //прибавляем количество полученных очков
    {
        get
        {
            return _score;
        }
        set 
        {
            _score += value;
            score1.text = _score.ToString();
        }
    }
    

    void Start()
    {
        Time.timeScale = 0;
        score1 = textScore1.GetComponent<Text>();
        score2 = textScore2.GetComponent<Text>();
        audioSource= transform.GetComponent<AudioSource>();
        audioSource.Play();
    }

    
    void Update()
    {
        Founded = GameObject.FindGameObjectWithTag("Enemy");//Поиск врагов и их спавн при отсутствии
        if(Founded == null && isPlaying == true)
        {
            var ships = 1;
            var asteroids = 1;
            
            if(Score < 10) //Имитация увеличения сложности при наборе N-очков.
            {
                ships = 1;
                asteroids = 1;
            }
            else if(Score > 10 && Score < 20) 
            {
                ships = 2;
                asteroids = 2;
            }
            else if (Score > 20 && Score < 50)
            {
                ships = 3;
                asteroids = 3;
            }
            else if(Score > 50 && Score < 100)
            {
                ships = 4;
            }
            else if (Score > 100)
            {
                ships = 5;
                asteroids = 5;
            }
            for (int i = 0; i < ships; i++)
            {
                RandomPosition(Enemy);
            }

            for (int i = 0; i < asteroids; i++)
            {
                RandomPosition(Asteroid);
            }
            Founded = null;
        }

        var player = GameObject.FindGameObjectWithTag("Player"); //Ищем объект с тэгом плеер и мониторим для завершения игры при уничтожении игрока
        if(player == null)
        {
            EndGame();
        }
                
    }

    private void ResetScore()
    {
        _score = 0;
    }

    void RandomPosition(GameObject obj)
    {
        int rand = Random.Range(1, 4);
        Vector3 trans;
        switch (rand)
        {
            case 1:
                trans = new Vector3(Screen.width- Random.Range(Screen.width + 10, Screen.width + 2), Screen.height + Random.Range(-Screen.height, 10));
                Instantiate(obj, Camera.main.ScreenToWorldPoint(trans), obj.transform.rotation);
                break;
            case 2:
                trans = new Vector3(Screen.width + Random.Range(-Screen.width, 10), Screen.height + Random.Range(2, 10));
                Instantiate(obj, Camera.main.ScreenToWorldPoint(trans), obj.transform.rotation);
                break;
            case 3:
                trans = new Vector3(Screen.width + Random.Range(2, 10), Screen.height - Random.Range(Screen.height + 10, 10));
                Instantiate(obj, Camera.main.ScreenToWorldPoint(trans), obj.transform.rotation);
                break;
            case 4:
                trans = new Vector3(Screen.width - Random.Range(Screen.width, 10), Screen.height - Random.Range(Screen.height + 10, Screen.height +2));
                Instantiate(obj, Camera.main.ScreenToWorldPoint(trans), obj.transform.rotation);
                break;
            
        }
    }

    public void StartGame()
    {
        startGame.SetActive(false);
        inGame.SetActive(true);
        Time.timeScale = 1;
        isPlaying = true;
        
        audioSource.Stop();
        audioSource.clip = GameAudio;
        audioSource.Play();
    }

    public void Pause()
    {
        Time.timeScale = 0;
        PauseCan.SetActive(true);
        isPlaying = false;
    }

    public void Resume()
    {
        PauseCan.SetActive(false);
        Time.timeScale = 1;
        isPlaying = true;
    }

    public void Restart()
    {
        ResetScore();
        SceneManager.LoadScene(0);
    }

    public void EndGame()
    {
        Time.timeScale = 0;
        isPlaying = false;
        inGame.SetActive(false);
        endGame.SetActive(true);
        score2.text = Score.ToString();
        audioSource.Stop();
        audioSource.clip = MenuAudio;
        audioSource.Play();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
