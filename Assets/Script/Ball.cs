using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BallColor
{
    White,
    Red,
    Yellow,
    Green,
    Brown,
    Blue,
    Pink,
    Black
}

public class Ball : MonoBehaviour
{
    [SerializeField]
    private int point;
    public int Point {  get { return point; } }

    [SerializeField]
    private BallColor ballColor;
    public BallColor BallColor { get { return ballColor; } }

    [SerializeField]
    private MeshRenderer rd;

    public void SetColorAndPoint(BallColor col)
    {
        ballColor = col; // ✅ บันทึกค่าสีของลูกบอล
        switch (col)
        {
            case BallColor.White:
                point = 0;
                rd.material.color = Color.white;
                break;
            case BallColor.Red:
                point = 1;
                rd.material.color = Color.red;
                break;
            case BallColor.Yellow:
                point = 2;
                rd.material.color = Color.yellow;
                break;
            case BallColor.Green:
                point = 3;
                rd.material.color = Color.green;
                break;
            case BallColor.Brown:
                point = 4;
                rd.material.color = new Color32(255, 123, 0, 255);
                break;
            case BallColor.Blue:
                point = 5;
                rd.material.color = Color.blue;
                break;
            case BallColor.Pink:
                point = 6;
                rd.material.color = new Color32(255, 105, 224, 255);
                break;
            case BallColor.Black:
                point = 7;
                rd.material.color = Color.black;
                break;
        }
    }


    void Awake()
    {
        rd = GetComponent<MeshRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
