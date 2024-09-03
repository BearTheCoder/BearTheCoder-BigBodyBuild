using TMPro;
using UnityEngine;

public class MoveTowardsTarget : MonoBehaviour
{
    #region Serialized Variables
    [SerializeField] private TextMeshProUGUI TargetText;
    [SerializeField] private TextMeshProUGUI FollowerText;
    [SerializeField] private TextMeshProUGUI DirectionText;
    #endregion

    #region Private Variables
    private GameObject _target;
    private GameObject _follower;
    #endregion

    private void Start()
    {
        Utilities.CreateGrid(transform);

        _target = new GameObject("Target");
        _follower = new GameObject("Follower");

        _target.AddComponent<HoverAndClickController>();
        _target.AddComponent<CircleCollider2D>();

        SpriteRenderer srTarget = _target.AddComponent<SpriteRenderer>();
        SpriteRenderer srFollower = _follower.AddComponent<SpriteRenderer>();

        srTarget.sprite = Utilities.CreateCircleSprite(20, Color.red);
        srFollower.sprite = Utilities.CreateCircleSprite(20, Color.white);

        _target.transform.position = new Vector3(Random.Range(-7, 7), Random.Range(-4, 4), 0);
        _follower.transform.position = new Vector3(Random.Range(-7, 7), Random.Range(-4, 4), 0);

        _target.transform.localScale = new Vector3(.5f, .5f, .5f);
        _follower.transform.localScale = new Vector3(.5f, .5f, .5f);
    }

    private void Update()
    {
        Vector3 direction = (_target.transform.position - _follower.transform.position).normalized;

        _follower.transform.position += direction * Time.deltaTime;

        TargetText.text = $"Target Position: {_target.transform.position}";
        FollowerText.text = $"Follower Position: {_follower.transform.position}";
        DirectionText.text = $"Heading: {direction}";
    }
}
