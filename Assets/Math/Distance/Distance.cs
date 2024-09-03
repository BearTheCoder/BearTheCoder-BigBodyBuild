using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Distance : MonoBehaviour
{
    #region Serialized Variables
    [SerializeField] TextMeshProUGUI RedText;
    [SerializeField] TextMeshProUGUI BlueText;
    [SerializeField] TextMeshProUGUI GreenText;
    [SerializeField] TextMeshProUGUI DifferenceText;
    [SerializeField] TextMeshProUGUI DistanceText;
    #endregion

    #region Private Variables
    private GameObject _redObject;
    private GameObject _blueObject;
    private GameObject _whiteObject;
    private GameObject _greenObject;

    private LineRenderer _cLine;
    private LineRenderer _aLine;
    private LineRenderer _bLine;

    private float _lineThickness = .05f;

    #endregion

    private void Start()
    {
        Utilities.CreateGrid(transform);

        // Create Objects
        Vector3 redPosition = new Vector3(Random.Range(-7, 7), Random.Range(-4, 4), 0f);
        Vector3 bluePosition = new Vector3(Random.Range(-7, 7), Random.Range(-4, 4), 0f);

        _redObject = new GameObject("Red Vector");
        _blueObject = new GameObject("Blue Vector");
        _whiteObject = new GameObject("Origin");
        _greenObject = new GameObject("Green Vector");

        _redObject.transform.position = redPosition;
        _blueObject.transform.position = bluePosition;
        _whiteObject.transform.position = Vector3.zero;
        _greenObject.transform.position = Vector3.zero;

        _redObject.transform.localScale = new Vector3(.5f, .5f, .5f);
        _blueObject.transform.localScale = new Vector3(.5f, .5f, .5f);
        _whiteObject.transform.localScale = new Vector3(.5f, .5f, .5f);
        _greenObject.transform.localScale = new Vector3(.5f, .5f, .5f);

        _redObject.AddComponent<CircleCollider2D>();
        _blueObject.AddComponent<CircleCollider2D>();

        _redObject.AddComponent<HoverAndClickController>();
        _blueObject.AddComponent<HoverAndClickController>();

        SpriteRenderer srRed = _redObject.AddComponent<SpriteRenderer>();
        SpriteRenderer srBlue = _blueObject.AddComponent<SpriteRenderer>();
        SpriteRenderer srWhite = _whiteObject.AddComponent<SpriteRenderer>();
        SpriteRenderer srGreen = _greenObject.AddComponent<SpriteRenderer>();

        srRed.sprite = Utilities.CreateCircleSprite(20, Color.red);
        srBlue.sprite = Utilities.CreateCircleSprite(20, Color.blue); 
        srWhite.sprite = Utilities.CreateCircleSprite(20, Color.white);
        srGreen.sprite = Utilities.CreateCircleSprite(20, Color.green);

        srRed.sortingOrder = 1;
        srBlue.sortingOrder = 1;
        srWhite.sortingOrder = 1;
        srGreen.sortingOrder = 1;

        // Create Lines
        GameObject cObject = new GameObject("C Object");
        GameObject aObject = new GameObject("A Object");
        GameObject bObject = new GameObject("B Object");

        _aLine = aObject.AddComponent<LineRenderer>();
        _bLine = bObject.AddComponent<LineRenderer>();
        _cLine = cObject.AddComponent<LineRenderer>();

        Material mat = new Material(Shader.Find("Sprites/Default"));

        _aLine.material = mat;
        _bLine.material = mat;
        _cLine.material = mat;

        Gradient gradient = new Gradient();

        GradientColorKey[] colors = new GradientColorKey[2];
        colors[0] = new GradientColorKey(Color.blue, 0f);
        colors[1] = new GradientColorKey(Color.green, 1f);
        gradient.colorKeys = colors;
        _aLine.colorGradient = gradient;

        colors = new GradientColorKey[2];
        colors[0] = new GradientColorKey(Color.red, 0f);
        colors[1] = new GradientColorKey(Color.green, 1f);
        gradient.colorKeys = colors;
        _bLine.colorGradient = gradient;

        colors = new GradientColorKey[2];
        colors[0] = new GradientColorKey(Color.red, 0f);
        colors[1] = new GradientColorKey(Color.blue, 1f);
        gradient.colorKeys = colors;
        _cLine.colorGradient = gradient;

        _aLine.startWidth = _lineThickness;
        _bLine.startWidth = _lineThickness;
        _cLine.startWidth = _lineThickness;

        _aLine.endWidth = _lineThickness;
        _bLine.endWidth = _lineThickness;
        _cLine.endWidth = _lineThickness;

        _aLine.positionCount = 2;
        _bLine.positionCount = 2;
        _cLine.positionCount = 2;
    }

    private void Update()
    {
        float distance = CalculateDistance(_redObject.transform.position, _blueObject.transform.position);

        _greenObject.transform.position = new Vector3(_redObject.transform.position.x, _blueObject.transform.position.y, 0f);

        _aLine.SetPosition(0, _blueObject.transform.position);
        _aLine.SetPosition(1, _greenObject.transform.position);

        _bLine.SetPosition(0, _redObject.transform.position);
        _bLine.SetPosition(1, _greenObject.transform.position);

        _cLine.SetPosition(0, _redObject.transform.position);
        _cLine.SetPosition(1, _blueObject.transform.position);

        RedText.text = $"Red Vector: {_redObject.transform.position}";
        BlueText.text = $"Blue Vector: {_blueObject.transform.position}";
        GreenText.text = $"Green Vector: {_greenObject.transform.position}";
        DifferenceText.text = $"Difference: {_redObject.transform.position - _blueObject.transform.position}";
        DistanceText.text = $"Distance: {distance}";
    }

    private float CalculateDistance(Vector3 firstVector, Vector3 secondVector)
    {
        // Vector3.Distance() also works.
        // Essentially calculating distance is the same as the pythagorean thereom
        // c*c = a*a + b*b
        // c = Mathf.sqrt((a*a) + (b*b))
        // c = Mathf.sqrt((a.x - b.x)^2 + (a.y-b.y)^2)

        // When you subtract two vectors then plot the difference, the distance between the new Vector and the origin will be the
        // same length as the distance between the two original vectors.

        // So you get the difference between two vectors, then calculate the distance using the pythagorean theorem

        Vector3 difference = firstVector - secondVector;

        float a = Mathf.Pow(difference.x, 2);
        float b = Mathf.Pow(difference.y, 2);
        float c = Mathf.Sqrt(a + b);

        // This is the same thing.
        /*float distance2 = Vector3.Distance(firstVector, secondVector);*/

        return c; 
    }
}
