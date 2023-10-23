using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 일자패턴테스트 : MonoBehaviour
{
    float radius = 10f;
    [SerializeField] GameObject testBulletPrf;
    [SerializeField] Player player;
    [SerializeField] List<GameObject> bullets = new();

    private Vector2 RandomPoint()
    {
        float randAngle = Random.Range(0f, 2f * Mathf.PI);

        float x = radius * Mathf.Cos(randAngle);
        float y = radius * Mathf.Sin(randAngle);

        return new Vector2(x, y);
    }

    private void OnEnable()
    {
        StartCoroutine(LinearPatternTest());
    }

    IEnumerator LinearPatternTest()
    {
        float duration = 0.4f;
        float time;
        Vector2 firstPlayerPos = player.transform.position;
        for (time = 0; time < 1; time += Time.fixedDeltaTime / duration)
        {
            Vector2 randPos = RandomPoint();
            GameObject obj = Instantiate(testBulletPrf, randPos + firstPlayerPos, Quaternion.identity);
            bullets.Add(obj);

            yield return null;
        }

        foreach (GameObject obj in bullets)
        {
            obj.GetComponent<Component>().GetComponent<GameObject>();
            Debug.Log(obj);
        }
    }
}
