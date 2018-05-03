using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 7;
    public event System.Action OnPlayerDeath;

    float screenHalfWidthInWorldUnits;

    // Use this for initialization
    void Start()
    {
        //Screen.SetResolution(600,1000,false);
        float halfPlayerWidth = transform.localScale.x / 2f;
        screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize + halfPlayerWidth;
    }

    // Update is called once per frame
    void Update()
    {

        float inputX = Input.GetAxisRaw("Horizontal");
        float velocity = inputX * speed;
        transform.Translate(Vector2.right * velocity * Time.deltaTime);


        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            Debug.Log("mouse down:" + Input.mousePosition);

            transform.Translate(Vector2.right * velocity * Time.deltaTime);

        }

        if (transform.position.x < -screenHalfWidthInWorldUnits)
        {
            transform.position = new Vector2(screenHalfWidthInWorldUnits, transform.position.y);
        }

        if (transform.position.x > screenHalfWidthInWorldUnits)
        {
            transform.position = new Vector2(-screenHalfWidthInWorldUnits, transform.position.y);
        }

        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
    }
    void OnTriggerEnter2D(Collider2D triggerCollider)
    {
        if (triggerCollider.tag == "Falling Block")
        {
            //FindObjectOfType<GameOver>().OnGameOver();
            if (OnPlayerDeath != null)
            {
                OnPlayerDeath();
            }
            Destroy(gameObject);
        }
    }
}
