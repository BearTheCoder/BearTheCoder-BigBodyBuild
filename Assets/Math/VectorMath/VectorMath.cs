using TMPro;
using UnityEngine;

public class VectorMath : MonoBehaviour
{
    #region Serialized Variables
    [SerializeField] float LineThickness;
    [SerializeField] Operand selectedOp;
    [SerializeField] TextMeshProUGUI OperandText;
    [SerializeField] TextMeshProUGUI RedVectorText;
    [SerializeField] TextMeshProUGUI BlueVectorText;
    [SerializeField] TextMeshProUGUI GreenVectorText;
    #endregion

    #region Private Variables
    private Vector3 RedVelocity;
    private Vector3 BlueVelocity;
    private LineRenderer _redLRenderer;
    private LineRenderer _blueLRenderer;
    private LineRenderer _resultLRenderer;
    private GameObject _blueVelObj;
    private GameObject _redVelObj;
    private GameObject _centerObj;
    private GameObject _resultObj;
    private int _cycle;
    #endregion

    enum Operand
    {
        Addition, 
        SubtractRedFromBlue,
        SubtractBlueFromRed,
        Multiplication,
        CrossProduct
    }

    private void Start()
    {
        Utilities.CreateGrid(transform);

        _cycle = 0;
        selectedOp = (Operand)_cycle;

        RedVelocity = new Vector3 (-1f, -1f, 0f);
        BlueVelocity = new Vector3 (1f, 1f, 0f);

        _blueVelObj = new GameObject("Velocity1");
        _redVelObj = new GameObject("Velocity2");
        _centerObj = new GameObject("Origin");
        _resultObj = new GameObject("Result");

        _blueVelObj.transform.position = RedVelocity;
        _redVelObj.transform.position = BlueVelocity;

        _blueVelObj.transform.localScale = new Vector3(.5f, .5f, .5f);
        _redVelObj.transform.localScale = new Vector3(.5f, .5f, .5f);
        _resultObj.transform.localScale = new Vector3(.5f, .5f, .5f);

        _blueVelObj.AddComponent<CircleCollider2D>();
        _redVelObj.AddComponent<CircleCollider2D>();

        _blueVelObj.AddComponent<HoverAndClickController>();
        _redVelObj.AddComponent<HoverAndClickController>();

        SpriteRenderer sr1 = _blueVelObj.AddComponent<SpriteRenderer>();
        SpriteRenderer sr2 = _redVelObj.AddComponent<SpriteRenderer>();
        SpriteRenderer sr3 = _centerObj.AddComponent<SpriteRenderer>();
        SpriteRenderer sr4 = _resultObj.AddComponent<SpriteRenderer>();

        sr1.sprite = Utilities.CreateCircleSprite(30, Color.red);
        sr2.sprite = Utilities.CreateCircleSprite(30, Color.blue);
        sr3.sprite = Utilities.CreateCircleSprite(10, Color.white);
        sr4.sprite = Utilities.CreateCircleSprite(20, Color.green);

        _blueLRenderer = _blueVelObj.AddComponent<LineRenderer>();
        _redLRenderer = _redVelObj.AddComponent<LineRenderer>();
        _resultLRenderer = _resultObj.AddComponent<LineRenderer>();

        Material lineMaterial = new Material(Shader.Find("Sprites/Default"));

        _blueLRenderer.material = lineMaterial;
        _redLRenderer.material = lineMaterial;
        _resultLRenderer.material = lineMaterial;

        Gradient gradient = new Gradient();

        GradientColorKey[] colors = new GradientColorKey[2];
        colors[0] = new GradientColorKey(Color.red, 0f);
        colors[1] = new GradientColorKey(Color.red, 1f);
        gradient.colorKeys = colors;
        _redLRenderer.colorGradient = gradient;

        colors = new GradientColorKey[2];
        colors[0] = new GradientColorKey(Color.blue, 0f);
        colors[1] = new GradientColorKey(Color.blue, 1f);
        gradient.colorKeys = colors;
        _blueLRenderer.colorGradient = gradient;

        colors = new GradientColorKey[2];
        colors[0] = new GradientColorKey(Color.green, 0f);
        colors[1] = new GradientColorKey(Color.green, 1f);
        gradient.colorKeys = colors;
        _resultLRenderer.colorGradient = gradient;

        _blueLRenderer.startWidth = LineThickness;
        _redLRenderer.startWidth = LineThickness;
        _resultLRenderer.startWidth = LineThickness;

        _blueLRenderer.endWidth = LineThickness;
        _redLRenderer.endWidth = LineThickness;
        _resultLRenderer.endWidth = LineThickness;

        _blueLRenderer.positionCount = 2;
        _redLRenderer.positionCount = 2;
        _resultLRenderer.positionCount = 2;

        _blueLRenderer.SetPosition(0, Vector3.zero);
        _redLRenderer.SetPosition(0, Vector3.zero);
        _resultLRenderer.SetPosition(0, Vector3.zero);
    }

    private void Update()
    {
        RedVelocity = _blueVelObj.transform.position;
        BlueVelocity = _redVelObj.transform.position;

        Vector3 result = CalculatePosition();
        _resultObj.transform.position = result;

        _blueLRenderer.SetPosition(0, BlueVelocity);
        _redLRenderer.SetPosition(0, RedVelocity);
        _resultLRenderer.SetPosition(0, result);

        OperandText.text = $"Operand (Cycle with 'W'): {selectedOp} ";
        RedVectorText.text = $"Red Vector: Vector3{RedVelocity}";
        BlueVectorText.text = $"Blue Vector: Vector3{BlueVelocity}";
        GreenVectorText.text = $"Green Vector: Vector3{result}";

        if (!Input.GetKeyDown(KeyCode.W)) return;

        _cycle++;

        selectedOp = (Operand)_cycle;

        if (_cycle == 4) _cycle = -1;
    }

    private Vector3 CalculatePosition()
    {
        Vector3 result = Vector3.zero;
        switch (selectedOp)
        {
            case Operand.Addition:
                result = RedVelocity + BlueVelocity;
                break;
            case Operand.SubtractRedFromBlue:
                result = RedVelocity - BlueVelocity;
                break;
            case Operand.SubtractBlueFromRed:
                result = BlueVelocity - RedVelocity;
                break;
            case Operand.Multiplication:
                result = Vector3.Scale(RedVelocity, BlueVelocity);
                break;
            case Operand.CrossProduct:
                result = Vector3.Cross(RedVelocity, BlueVelocity);
                break;
        }

        return result;
    }
}
