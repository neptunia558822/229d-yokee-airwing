using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Ball b = collision.gameObject.GetComponent<Ball>();

        if (b != null)
        {
            if (b.Point == 0) // ถ้าเป็นลูกขาว ให้รีเซ็ตตำแหน่ง
            {
                b.transform.position = GameManager.instance.cueBallStartPosition;
                Rigidbody rb = b.GetComponent<Rigidbody>();
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                GameManager.instance.StopBall();
            }
            else
            {
                // ✅ ตรวจสอบว่าลูกที่ยิงเข้าหลุมถูกต้องหรือไม่
                if (b.BallColor == GameManager.correctOrder[GameManager.currentBallIndex])
                {
                    GameManager.instance.PlayerScore += b.Point;
                    GameManager.instance.UpdateScoreText();
                    GameManager.currentBallIndex++; // ไปยังลูกถัดไป
                    Destroy(b.gameObject); // ✅ ทำลายลูกบอลเมื่อยิงถูกลำดับ
                }
                else
                {
                    if (GameManager.currentBallIndex > 0) // ✅ ต้องยิงอย่างน้อย 1 ลูกก่อนถึงจะรีเซ็ต
                    {
                        GameManager.instance.ResetGame();
                    }
                }
            }
        }
    }
}
