using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Ball b = collision.gameObject.GetComponent<Ball>();

        if (b.Point == 0) // ถ้าเป็นลูกขาว
        {
            b.transform.position = GameManager.instance.cueBallStartPosition;
            Rigidbody rb = b.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            GameManager.instance.StopBall(); // ให้หยุดลูกบอลอัตโนมัติ
        }
        else
        {
            GameManager.instance.PlayerScore += b.Point;
            GameManager.instance.UpdateScoreText();
            Destroy(b.gameObject);
        }


    }
}
