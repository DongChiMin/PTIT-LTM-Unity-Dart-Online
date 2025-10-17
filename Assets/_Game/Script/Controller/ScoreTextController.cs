
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTextController : Singleton<ScoreTextController>
{
    [Header("Score text")]
    [SerializeField] ScoreText scoreTextPrefab;

    public void TrigerText(Vector3 pos, string content)
    {
        Debug.Log("ĐÂY LÀ VỊ TRÍ" + pos);
        ScoreText scoreText = Instantiate(scoreTextPrefab, pos, Quaternion.identity);
        scoreText.textMesh.text = content;
        StartCoroutine(DestroyDelay(scoreText.gameObject, 1f));
    }

    IEnumerator DestroyDelay(GameObject gameObject, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
