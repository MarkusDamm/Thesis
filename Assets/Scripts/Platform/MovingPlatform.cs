using UnityEngine;
using UnityEngine.Events;

public class MovingPlatform : MonoBehaviour
{
    public bool usesInput;
    public bool canMove;
    [SerializeField] float speed;
    [SerializeField] int startPoint;
    [SerializeField] Transform[] m_points;
    [SerializeField] GameObject bars;
    [SerializeField] GameObject[] buttons;
    public UnityEvent onStart;
    public UnityEvent onDestination;


    int m_currentLevel;
    int targeLevel;
    int savedTarget;
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

        if (canMove)
        {
            if (Vector3.Distance(transform.position, points[targeLevel].position) < 0.01f)
            {
                currentLevel = targeLevel;
                Debug.Log("Compare current level: " + currentLevel + " with savedTarget: " + savedTarget);
                if (currentLevel != savedTarget)
                {
                    AdjustTargetLevel(savedTarget);
                }
                else
                {
                    canMove = false;
                    bars.SetActive(false);
                    Debug.Log("Trigger onDestination");
                    onDestination.Invoke();
                    if (currentLevel == points.Length - 1)
                    {
                        reverse = true;
                        targeLevel--;
                        return;
                    }
                    else if (currentLevel == 0)
                    {
                        reverse = false;
                        targeLevel++;
                        return;
                    }

                    if (reverse) { targeLevel--; }
                    else { targeLevel++; }
                }

            }
            transform.position = Vector3.MoveTowards(transform.position, points[targeLevel].position, speed * Time.deltaTime);
        }
    }

    public void MoveToLevel(int _level)
    {
        if (!canMove && currentLevel != _level)
        {
            AdjustTargetLevel(_level);
            canMove = true;
            bars?.SetActive(true);
            onStart.Invoke();
        }
    }

    private void AdjustTargetLevel(int _level)
    {
        savedTarget = _level;
        if (currentLevel + 1 == _level || currentLevel - 1 == _level)
        {
            targeLevel = _level;
        }
        else if (_level < currentLevel)
        {
            targeLevel = currentLevel - 1;
        }
        else
        {
            targeLevel = currentLevel + 1;
        }

    }


}
