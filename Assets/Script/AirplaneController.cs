using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AirplaneController : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 100f;
    public int scorePerCollectible = 10;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector3 movement = transform.forward * speed * moveVertical * Time.deltaTime;
        rb.MovePosition(rb.position + movement);

        Quaternion turnRotation = Quaternion.Euler(0f, moveHorizontal * rotationSpeed * Time.deltaTime, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            ScoreManager.Instance.AddScore(scorePerCollectible);
            Destroy(other.gameObject);
        }
    }
}
