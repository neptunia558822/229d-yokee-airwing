using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Ball b = collision.gameObject.GetComponent<Ball>();

        if (b.Point == 0) // ������١���
        {
            b.transform.position = GameManager.instance.cueBallStartPosition;
            Rigidbody rb = b.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            GameManager.instance.StopBall(); // �����ش�١����ѵ��ѵ�
        }
        else
        {
            GameManager.instance.PlayerScore += b.Point;
            GameManager.instance.UpdateScoreText();
            Destroy(b.gameObject);
        }


    }
}
