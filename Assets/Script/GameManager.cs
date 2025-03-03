using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int playerScore;
    public int PlayerScore { get { return playerScore; } set { playerScore = value; } }

    [SerializeField]
    private GameObject ballPrefab;

    [SerializeField]
    private GameObject[] ballPosition;

    public static GameManager instance;

    [SerializeField]
    private GameObject cueBall;
    [SerializeField]
    private GameObject ballLine;

    [SerializeField]
    private float xInput;

    [SerializeField]
    private GameObject camera;

    [SerializeField]
    private TMP_Text scoreText;

    public Vector3 cueBallStartPosition;

    private bool canShoot = true;

    public static BallColor[] correctOrder = {
    BallColor.Red, BallColor.Yellow, BallColor.Green, BallColor.Brown,
    BallColor.Blue, BallColor.Pink, BallColor.Black
    };

    public static int currentBallIndex = 0;

    private bool isResetting = false; // ✅ ตัวแปรกันรีเซ็ตซ้ำ

    public void ResetGame()
    {
        if (isResetting) return; // ✅ ถ้าเกมกำลังรีเซ็ตอยู่แล้ว ไม่ต้องทำซ้ำ
        isResetting = true;

        playerScore = 0;
        UpdateScoreText();
        currentBallIndex = 0;
        ResetCueBall();

        isResetting = false; // ✅ รีเซ็ตเสร็จแล้ว อนุญาตให้รีเซ็ตใหม่
    }


    public void ResetCueBall()
    {
        cueBall.transform.position = cueBallStartPosition; // ย้ายลูกขาวไปตำแหน่งเริ่มต้น
        cueBall.transform.rotation = Quaternion.identity; // รีเซ็ตการหมุนของลูกขาว
        Rigidbody rb = cueBall.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        CameraBehindCueBall(); // กล้องกลับไปหลังลูกขาว

        camera.transform.eulerAngles = new Vector3(30f, 0f, 0f); // รีเซ็ตมุมกล้อง
        camera.transform.position = cueBall.transform.position + new Vector3(0f, 7f, -10f);

        ballLine.SetActive(true); // แสดงเส้นเล็ง
        canShoot = true; // อนุญาตให้ยิงใหม่
    }

    private void SetBall(BallColor col, int i)
    {
        GameObject obj = Instantiate(ballPrefab,
                                     ballPosition[i].transform.position,
                                     Quaternion.identity);

        Ball b = obj.GetComponent<Ball>();
        b.SetColorAndPoint(col);
    }

    private void RotateBall()
    {
        xInput = Input.GetAxis("Horizontal");
        cueBall.transform.Rotate(new Vector3(0f, xInput, 0f));
    }

    private void ShootBall()
    {
        if (!canShoot) return; // ถ้ายิงไปแล้ว ห้ามยิงซ้ำ

        camera.transform.parent = null;
        Rigidbody rb = cueBall.GetComponent<Rigidbody>();

        float a = 50f;
        float f = rb.mass * a;
        rb.AddRelativeForce(Vector3.forward * f, ForceMode.Impulse);
        ballLine.SetActive(false);

        canShoot = false; // ป้องกันการยิงซ้ำ
    }

    private void CameraBehindCueBall()
    {
        camera.transform.parent = cueBall.transform; // ตั้งให้กล้องตามลูกขาว
        camera.transform.position = cueBall.transform.position + new Vector3(0f, 7f, -10f);
        camera.transform.eulerAngles = new Vector3(30f, 0f, 0f); // ตั้งมุมกล้องให้ถูกต้อง
    }

    public void StopBall()
    {
        Rigidbody rb = cueBall.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        cueBall.transform.eulerAngles = Vector3.zero;

        CameraBehindCueBall();
        camera.transform.eulerAngles = new Vector3(30f, 0f, 0f);
        ballLine.SetActive(true);

        canShoot = true; // อนุญาตให้ยิงได้อีกครั้ง
    }

    public void UpdateScoreText()
    {
        scoreText.text = $"Player Score: {playerScore}";
        if (playerScore >= 15)
        {
            ChangeToScenesWin();
        }
    }

    public void ChangeToScenesWin()
    {
        SceneManager.LoadScene("ScenesWin");
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        cueBallStartPosition = cueBall.transform.position; // บันทึกตำแหน่งเริ่มต้นของลูกขาว
        UpdateScoreText();

        camera = Camera.main.gameObject;
        CameraBehindCueBall();

        SetBall(BallColor.Red, 1);
        SetBall(BallColor.Yellow, 2);
        SetBall(BallColor.Green, 3);
        SetBall(BallColor.Brown, 4);
        SetBall(BallColor.Blue, 5);
        SetBall(BallColor.Pink, 6);
        SetBall(BallColor.Black, 7);

    }

    // Update is called once per frame
    void Update()
    {
        RotateBall();

        if (Input.GetKeyDown(KeyCode.Space))
            ShootBall();

        if (Input.GetKeyDown(KeyCode.Backspace))
            StopBall();

        if (Input.GetKeyDown(KeyCode.R)) // ✅ เช็กว่าเป็นการกดครั้งเดียว
            ResetCueBall(); // รีเซ็ตเฉพาะลูกขาว
    }
}
