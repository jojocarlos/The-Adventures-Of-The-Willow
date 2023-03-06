using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformSpiderBoss : MonoBehaviour
{
    public float platformDisappearTime = 1.0f;
    public float platformReappearTime = 2.0f;
    public GameObject[] platforms;

    public void FallNow()
    {
        StartCoroutine(DisappearPlatforms());
    }

    IEnumerator DisappearPlatforms()
    {
        int platformCount = Random.Range(1, 3);
        List<int> platformIndexes = new List<int>();
        while (platformIndexes.Count < platformCount)
        {
            int platformIndex = Random.Range(0, platforms.Length);
            if (!platformIndexes.Contains(platformIndex))
            {
                platformIndexes.Add(platformIndex);
            }
        }

        foreach (int platformIndex in platformIndexes)
        {
            GameObject platform = platforms[platformIndex];
            platform.SetActive(false);
            yield return new WaitForSeconds(platformDisappearTime);
            platform.SetActive(true);
            yield return new WaitForSeconds(platformReappearTime);
        }
    }
}
