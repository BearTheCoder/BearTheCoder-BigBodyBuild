using UnityEngine;

public class EdgeAvoidment : MonoBehaviour
{
    #region Serialized Variables
    [SerializeField] GameObject Entity;
    [SerializeField] float Speed;
    #endregion

    #region Private Variables
    private Vector3 _heading;
    private float _maxX = 6;
    private float _maxY = 3;
    private float _elapsedTimeX = 0f;
    private float _elapsedTimeY = 0f;
    private float _duration = 2f;
    private Vector3 _savedHeading = Vector3.zero;
    #endregion

    private void Start()
    {
        float headingX = Random.Range(-1f, 1f);
        float headingY = Random.Range(-1f, 1f);
        _heading = new Vector3(headingX, headingY, 0f);
    }

    private void Update()
    {
        FaceTowardHeading();
        MoveEntity();
        EdgeAvoidance();
    }

    private void EdgeAvoidance()
    {
        bool xExceMaxX = Entity.transform.position.x > _maxX;
        bool xExceNegMaxX = Entity.transform.position.x < -_maxX;
        bool yExceMaxY = Entity.transform.position.y > _maxY;
        bool yExceNegMaxY = Entity.transform.position.y < -_maxY;

        if (xExceMaxX || xExceNegMaxX)
        {
            _elapsedTimeX += Time.deltaTime;
            _heading.x = LerpHeading(_savedHeading.x, _elapsedTimeX);
        }

        if (yExceMaxY || yExceNegMaxY)
        {
            _elapsedTimeY += Time.deltaTime;
            _heading.y = LerpHeading(_savedHeading.y, _elapsedTimeY);
        }

        if (!xExceMaxX && !xExceNegMaxX && !yExceMaxY && !yExceNegMaxY)
        {
            _duration = Random.Range(.2f, 1f);
            _savedHeading = _heading;
            _elapsedTimeX = 0f;
            _elapsedTimeY = 0f;
        }
    }

    private void MoveEntity()
    {
        Entity.transform.position += _heading * (Time.deltaTime * Speed);
    }

    private float LerpHeading(float savedHeading, float elapsedTime)
    {
        float t = Mathf.Clamp01(elapsedTime / _duration);
        return Mathf.Lerp(savedHeading, -savedHeading, t);
    }

    private void FaceTowardHeading()
    {
        float angle = Mathf.Atan2(_heading.y, _heading.x) * Mathf.Rad2Deg;

        Entity.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }
}
