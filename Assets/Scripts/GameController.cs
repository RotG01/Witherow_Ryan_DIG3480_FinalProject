using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public GameObject Mothership;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public BGScroller scroller;
    public BGScroller Starspeed;

    public Transform MothershipSpawn;

    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioClip musicClipThree;
    public AudioClip musicClipFour;
    public AudioClip musicClipFive;
    public AudioSource musicSource;

    public Text ScoreText;
    public Text restartText;
    public Text gameOverText;
    public Text winText;
    public Text loseText;
    public Text BossText;


    public int score;
    public int bossLives;
    private int count;
    private bool restart;
    private bool gameOver;
    private bool Bossopening;
    private bool Bosstheme;

    private MusicManager warningSound;
    private MusicManager bossMusicIntro;
    private MusicManager bossMusicLoop;
    private MusicManager loseMusic;
    private MusicManager winMusic;
    private int mothershipcount;
    private Mothership mothership;

    void Start()
    {
        mothership = GetComponent<Mothership>();

        warningSound = FindObjectOfType<MusicManager>();
        bossMusicIntro = FindObjectOfType<MusicManager>();
        bossMusicLoop = FindObjectOfType<MusicManager>();
        loseMusic = FindObjectOfType<MusicManager>();
        winMusic = FindObjectOfType<MusicManager>();

        score = 0;
        bossLives = 0;
        count = 0;
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        winText.text = "";
        loseText.text = "";
        BossText.text = "";
        UpdateScore();
        SetBossText();
        StartCoroutine(SpawnWaves());
    }

    private void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SceneManager.LoadScene("Final Project");
            }
        }
        if (Input.GetKey("escape"))
            Application.Quit();
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            if (score < 100)
            {
                for (int i = 0; i < hazardCount; i++)
                {
                    GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                    Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(hazard, spawnPosition, spawnRotation);
                    yield return new WaitForSeconds(spawnWait);
                }
                yield return new WaitForSeconds(waveWait);
            }
            if (score >= 100)
            {
                scroller.scrollSpeed = -40.25f;
                Starspeed.scrollSpeed = -40.25f;

                if (count == 0 && loseText.text != "You lose!" && winText.text != "You win!")
                {
                    warningSound.ChangeBM(musicClipOne);
                    musicSource.loop = false;
                    yield return new WaitForSeconds(musicClipOne.length);
                    count += 1;
                }
                if (mothershipcount != 1)
                {
                    Instantiate(Mothership, MothershipSpawn.position, MothershipSpawn.rotation);                  

                    mothershipcount = mothershipcount + 1;
                }
                if (count == 1 && loseText.text != "You lose!" && winText.text != "You win!")
                {
                    bossMusicIntro.ChangeBM(musicClipTwo);
                    musicSource.loop = false;
                    yield return new WaitForSeconds(musicClipTwo.length);
                    count += 1;
                }
                if (musicClipThree != null && count == 2 && loseText.text != "You lose!" && winText.text != "You win!")
                {
                    bossMusicLoop.ChangeBM(musicClipThree);
                    musicSource.loop = true;
                    yield return new WaitForSeconds(musicClipThree.length);
                }
            }

            if (gameOver)
            {
                //restartText.text = "Press 'Q' to Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    public void BossHealth(int bosslives)
    {
        bossLives = bosslives;
        SetBossText();
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;

        if (score >= 200 && loseText.text != "You lose!")
        {
            winText.text = "You win!";
            gameOverText.text = "Game Created By Ryan Witherow";

            if (musicClipFive != null)
            {
                winMusic.ChangeBM(musicClipFive);
            }

            gameOver = true;
            restart = true;

            restartText.text = "Press 'Q' to Restart";
        }
    }
    void SetBossText()
    {
        BossText.text = "Boss Health: " + bossLives;
    }

    public void GameOver()
    {
        if (score < 200 && winText.text != "You win!")
        {
            loseText.text = "You lose!";
            gameOverText.text = "Game Created By Ryan Witherow";
            if (musicClipFour != null)
            {
                winMusic.ChangeBM(musicClipFour);
            }
            gameOver = true;
            restart = true;

            restartText.text = "Press 'Q' to Restart";
        }
    }
}