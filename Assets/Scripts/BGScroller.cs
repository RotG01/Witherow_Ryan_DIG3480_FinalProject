using UnityEngine;
using System.Collections;

public class BGScroller : MonoBehaviour
{

    public float scrollSpeed;
    public float tileSizeZ;

    private Vector3 startPosition;
    private GameController gameController;

    void Start()
    {
        gameController = GetComponent<GameController>();

        startPosition = transform.position;

    }

    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
        transform.position = startPosition + Vector3.forward * newPosition;
    }

    /*void Speedup()
    {
        if (gameController.score > 0)
        {
            if(gameController.score % 5 == 0)
            {
                scrollSpeed = scrollSpeed - 2f;
            }
        }
    }*/
}