using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static int score;
    public GameObject zombiePrefab;
    public GameObject zombieHelmetPrefab;
    public GameObject zombieTeleportPrefab;

    public static Scene currentScene;

    void Awake()
    {
        score = 0;
        ZombieScript.isShuttingDown = false;
    }

    void Start()
    {
        StartCoroutine(ZombieGenerate());
        StartCoroutine(ZombieHelmetGenerate());
        StartCoroutine(ZombieTeleportGenerate());
    

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        currentScene = SceneManager.GetActiveScene();

        if (currentScene.buildIndex == 1)
        {
            if (PlayerController.playerHealth <= 0)
            {
                ZombieScript.isShuttingDown = true;
                StopAllCoroutines();
                SceneManager.LoadScene(2);
            }
        }
        
    }

    IEnumerator ZombieGenerate()
    {
        while (score >= 0 && score < 30)
        {
            RandomLocationMethod(zombiePrefab);
            yield return new WaitForSeconds(3.5f);
        }
        while (score >= 30 && score < 100)
        {
            RandomLocationMethod(zombiePrefab);
            yield return new WaitForSeconds(3f);
        }
        while (score >= 100 && score < 200)
        {
            RandomLocationMethod(zombiePrefab);
            yield return new WaitForSeconds(2.5f);
        }
        while (score >= 200 && score < 300)
        {
            RandomLocationMethod(zombiePrefab);
            yield return new WaitForSeconds(2.25f);
        }
        while (score >= 300)
        {
            RandomLocationMethod(zombiePrefab);
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator ZombieHelmetGenerate()
    {
        while (score >= 0 && score < 30)
        {
            yield return new WaitForSeconds(2f);
        }
        while (score >= 30 && score < 100)
        {
            RandomLocationMethod(zombieHelmetPrefab);
            yield return new WaitForSeconds(6f);
        }
        while (score >= 100 && score < 200)
        {
            RandomLocationMethod(zombieHelmetPrefab);
            yield return new WaitForSeconds(4f);
        }
        while (score >= 200 && score < 300)
        {
            RandomLocationMethod(zombieHelmetPrefab);
            yield return new WaitForSeconds(3f);
        }
        while (score >= 300)
        {
            RandomLocationMethod(zombieHelmetPrefab);
            yield return new WaitForSeconds(2.5f);
        }
    }

    IEnumerator ZombieTeleportGenerate()
    {
        while (score >= 0 && score < 70)
        {
            yield return new WaitForSeconds(2f);
        }
        while (score >= 70 && score < 150)
        {
            RandomLocationMethod(zombieTeleportPrefab);
            yield return new WaitForSeconds(10f);
        }
        while (score >= 150 && score < 250)
        {
            RandomLocationMethod(zombieTeleportPrefab);
            yield return new WaitForSeconds(7f);
        }
        while (score >= 250 && score < 400)
        {
            RandomLocationMethod(zombieTeleportPrefab);
            yield return new WaitForSeconds(5f);
        }
        while (score >= 400)
        {
            RandomLocationMethod(zombieTeleportPrefab);
            yield return new WaitForSeconds(4f);
        }

    }

    void RandomLocationMethod(GameObject prefabType)
    {
            int locationChoice = Random.Range(0, 4);
            //killing fields
            if (locationChoice == 0)
            {
                Instantiate(prefabType, new Vector3(Random.Range(-17, 17), 2, Random.Range(-17, 17)), Quaternion.identity);
            }
            //tunnel
            else if (locationChoice == 1)
            {
                int tunnelChoice = Random.Range(0, 7);
                if (tunnelChoice == 0)
                {
                    Instantiate(prefabType, new Vector3(Random.Range(-2, 2), 2, Random.Range(18, 37)), Quaternion.identity);
                }
                else if (tunnelChoice == 1)
                {
                    Instantiate(prefabType, new Vector3(Random.Range(8, 17), 2, Random.Range(43, 47)), Quaternion.identity);
                }
                else if (tunnelChoice == 2)
                {
                    Instantiate(prefabType, new Vector3(Random.Range(28, 32), 2, Random.Range(-19.5f, 29.5f)), Quaternion.identity);
                }
                else if (tunnelChoice == 3)
                {
                    Instantiate(prefabType, new Vector3(Random.Range(18, 37), 2, Random.Range(-2, 2)), Quaternion.identity);
                }
                else if (tunnelChoice == 4)
                {
                    Instantiate(prefabType, new Vector3(Random.Range(-2, 2), 2, Random.Range(-32, -18)), Quaternion.identity);
                }
                else if (tunnelChoice == 5)
                {
                    Instantiate(prefabType, new Vector3(Random.Range(2.5f, 17), 2, Random.Range(-32, -28)), Quaternion.identity);
                }
                else if (tunnelChoice == 6)
                {
                    Instantiate(prefabType, new Vector3(Random.Range(47, 52), 2, Random.Range(-34.5f, -10.5f)), Quaternion.identity);
                }
            }
            //small rooms
            else if (locationChoice == 2)
            {
                int quadChoice = Random.Range(0, 3);
                if (quadChoice == 0)
                {
                    Instantiate(prefabType, new Vector3(Random.Range(-7f, 7f), 2, Random.Range(36.5f, 52f)), Quaternion.identity);
                }
                else if (quadChoice == 1)
                {
                    Instantiate(prefabType, new Vector3(Random.Range(18f, 32), 2, Random.Range(30.5f, 59.5f)), Quaternion.identity);
                }
                else if (quadChoice == 2)
                {
                    Instantiate(prefabType, new Vector3(Random.Range(38f, 52f), 2, Random.Range(-9.5f, 9.5f)), Quaternion.identity);
                }

            }
            //cave room
            if (locationChoice == 3)
            {
                Instantiate(prefabType, new Vector3(Random.Range(18, 42), 2, Random.Range(-44.5f, -19.5f)), Quaternion.identity);
            }
        }

    IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(2);
    }


}
