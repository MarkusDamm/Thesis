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
    int i;
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
        i = currentLevel = startPoint;

        int n = 0;
        foreach (var button in buttons)
        {
            if (n < points.Length - 1)
            {
                button.SetActive(usesInput);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, points[i].position) < 0.01f)
        {
            canMove = false;
            bars.SetActive(false);
            currentLevel = i;

            if (i == points.Length - 1)
            {
                reverse = true;
                i--;
                return;
            }
            else if (i == 0)
            {
                reverse = false;
                i++;
                return;
            }

            if (reverse) { i--; }
            else { i++; }
        }

        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        }
    }

    public void MoveToLevel(int _level)
    {
        i = _level;
        canMove = true;
        bars.SetActive(true);
    }


}
