using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private Camera cam;

    public float boundXLeft;
    public float boundXRight;
    public int distanceBetweenLilypadsY = 4;

    private List<GameObject> lilypadsOnScreen;
    private List<GameObject> lilypadsToRemove;
    public int nextLilypadSpawnPosition;

    public GameObject[] lilypadPrefabs;

    public GameObject backgroundPrefab;
    public GameObject frog;

    private GameObject firstBackground;
    private GameObject secondBackground;
    private GameObject thirdBackground;

    public int[] spawnRates = { 100, 0, 0, 0, 0 };
    public int[] percentModifiers = { 0, 4000, 6000, 8000, 10000 };
    public ScoreManager scoreManager;

    void Start()
    {
        cam = Camera.main;

        boundXLeft = -5.5f;
        boundXRight = 5.5f;
        firstBackground = Instantiate(backgroundPrefab, new Vector3(0, 6, 0), Quaternion.identity);
        secondBackground = Instantiate(backgroundPrefab, new Vector3(0, 6 + backgroundPrefab.GetComponent<SpriteRenderer>().bounds.size.y, 0), Quaternion.identity);
        thirdBackground = Instantiate(backgroundPrefab, new Vector3(0, secondBackground.transform.position.y +
                                            secondBackground.GetComponent<SpriteRenderer>().bounds.size.y, 0), Quaternion.identity);

        lilypadsOnScreen = new List<GameObject>();
        lilypadsToRemove = new List<GameObject>();

        Vector3 position = new Vector3(Random.Range(boundXLeft, boundXRight), 7, 0);
        lilypadsOnScreen.Add(Instantiate(GetRandomLilypad(), position, Quaternion.identity));

        position = new Vector3(Random.Range(boundXLeft, boundXRight), 11, 0);
        lilypadsOnScreen.Add(Instantiate(GetRandomLilypad(), position, Quaternion.identity));

        position = new Vector3(Random.Range(boundXLeft, boundXRight), 15, 0);
        lilypadsOnScreen.Add(Instantiate(GetRandomLilypad(), position, Quaternion.identity));

        position = new Vector3(Random.Range(boundXLeft, boundXRight), 19, 0);
        lilypadsOnScreen.Add(Instantiate(GetRandomLilypad(), position, Quaternion.identity));

        position = new Vector3(Random.Range(boundXLeft, boundXRight), 23, 0);
        lilypadsOnScreen.Add(Instantiate(GetRandomLilypad(), position, Quaternion.identity));

        nextLilypadSpawnPosition = 27;
    }

    void Update()
    {
        if (frog.transform.position.y + 5 > thirdBackground.transform.position.y)
        {
            Destroy(firstBackground);
            firstBackground = secondBackground;
            secondBackground = thirdBackground;
            thirdBackground = Instantiate(backgroundPrefab, new Vector3(0, thirdBackground.transform.position.y +
                                            thirdBackground.GetComponent<SpriteRenderer>().bounds.size.y, 0), Quaternion.identity);
        }

        if (scoreManager.IsScoreModified())
        {
            scoreManager.UpdatedToLastCheck();
            CalculateNewSpawnRates(scoreManager.GetScore());
        }

        CheckAndRemoveLilypadsOffScreen();
        CheckAndSpawnLilypadsIfNeeded();
    }

    private void CheckAndRemoveLilypadsOffScreen()
    {
        foreach (GameObject lilypad in lilypadsOnScreen)
        {
            if (lilypad.transform.position.y + 1 < cam.ScreenToWorldPoint(Vector3.zero).y)
            {
                lilypadsToRemove.Add(lilypad);
            }
        }

        foreach (GameObject lilypad in lilypadsToRemove)
        {
            if (lilypad.transform.position.y + 1 < cam.ScreenToWorldPoint(Vector3.zero).y)
            {
                lilypadsOnScreen.Remove(lilypad);
                Destroy(lilypad);
            }
        }

        lilypadsToRemove.Clear();
    }

    private void CheckAndSpawnLilypadsIfNeeded()
    {
        if (lilypadsOnScreen.Count < 5)
        {
            Vector3 position = new Vector3(Random.Range(boundXLeft, boundXRight), nextLilypadSpawnPosition, 0);
            lilypadsOnScreen.Add(Instantiate(GetRandomLilypad(), position, Quaternion.identity));
            nextLilypadSpawnPosition += distanceBetweenLilypadsY;
        }
    }

    private void CalculateNewSpawnRates(int score)
    {
        int accumulatedPercent = 0;
        int value = (int)(((double)score / percentModifiers[spawnRates.Length - 1]) * 100);
        spawnRates[spawnRates.Length - 1] = value;
        accumulatedPercent = value;

        for (int i = spawnRates.Length - 2; i > 0; i--)
        {
            if ((double)score >= percentModifiers[i])
            {
                spawnRates[i] = 100 - accumulatedPercent;
                accumulatedPercent = 100;
            }
            else
            {
                value = (int)((((double)score / percentModifiers[i]) * ((double)(100 - accumulatedPercent) / 100)) * 100);
                accumulatedPercent += value;
                spawnRates[i] = value;
            }
        }
        spawnRates[0] = 100 - accumulatedPercent;
    }

    private GameObject GetRandomLilypad()
    {
        int random = Random.Range(1, 101);
        int index = 0;
        int accumulatedPercent = 0;
        bool indexFound = false;

        while (!indexFound && index < spawnRates.Length)
        {
            if (spawnRates[index] + accumulatedPercent >= random)
                indexFound = true;
            accumulatedPercent += spawnRates[index];
            index++;
        }
        index--;
        return lilypadPrefabs[index];
    }
}
