using UnityEngine;
using UnityEngine.UI;

public class TouchManager : MonoBehaviour {

    public Text touchCount;

    public GameObject canvas;

    public GameObject ball;
    Plane plane;
    Vector3 movement;
    Vector2 ballPos;

    private void Start()
    {
        Camera.main.gameObject.transform.position = canvas.transform.position;
        Camera.main.orthographicSize = canvas.transform.position.y;

        ballPos = ball.transform.position;
    }

    void Update()
    {
        touchCount.text = "Touchcount: " + Input.touchCount.ToString();

        ballPos = (Vector2)ball.transform.position;

        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (Input.GetTouch(0).position == ballPos)
                {
                    Debug.Log("Ball was hit");
                    
                    plane = new Plane(Camera.main.transform.forward * -1, ball.transform.position);

                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    float distance;
                    plane.Raycast(ray, out distance);
                    movement = ball.transform.position - ray.GetPoint(distance);
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved && ball)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                float distance;
                if (plane.Raycast(ray, out distance))
                {
                    ball.transform.position = ray.GetPoint(distance) + movement;
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended && ball)
            {
                ball = null;
            }
        }
    }
}