using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public bool usesInput;
    public bool canMove;
    [SerializeField] float speed;
    [SerializeField] int startPoint;
    [SerializeField] Transform[] m_points;
    [SerializeField] GameObject bars;
    [SerializeField] GameObject[] buttons;

    int m_currentLevel;
    int targeLevel;
    bool reverse;

    public int currentLevel
    {
        get { return m_currentLevel; }
        private set { m_currentLevel = value; }
    }

    public Transform[] points
    {
        get { return m_points; }
        private set { m_points = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[startPoint].position;
        targeLevel = currentLevel = startPoint;

        int n = 0;
        if (buttons != null)
        {
            foreach (var button in buttons)
            {
                if (n < points.Length - 1)
                {
                    button.SetActive(usesInput);
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, points[targeLevel].position) < 0.01f)
        {
            canMove = false;
            bars.SetActive(false);
            currentLevel = targeLevel;

            if (targeLevel == points.Length - 1)
            {
                reverse = true;
                targeLevel--;
                return;
            }
            else if (targeLevel == 0)
            {
                reverse = false;
                targeLevel++;
                return;
            }

            if (reverse) { targeLevel--; }
            else { targeLevel++; }
        }

        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[targeLevel].position, speed * Time.deltaTime);
        }
    }

    public void MoveToLevel(int _level)
    {
        targeLevel = _level;
        canMove = true;
        bars?.SetActive(true);
    }


}
