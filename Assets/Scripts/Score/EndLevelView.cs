using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScoreCollector))]
public class EndLevelView : MonoBehaviour
{
    [SerializeField] private Text _status;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _totalScoreText;
    [SerializeField] private GameObject _textModifirePrefab;
    [SerializeField] private GameObject _modifireContainer;
    [SerializeField] private GameObject _goldStar;
    [SerializeField] private GameObject _star1, _star2, _star3;

    private Score _score;
    private ScoreCollector _scoreCollector;
    private List<GameObject> _textModifiresList = new List<GameObject>();
    private List<GameObject> _goldStars = new List<GameObject>();

    public void Initialize()
    {
        _score = FindObjectOfType<ScoreCounter>().GetScore();
        List<Modifier> modifiers = _score.Modifiers;

        _scoreCollector.CollectScore();

        _status.text = _score.IsWin ? "YOU WIN!" : "DEFEAT";
        _scoreText.text = _score.CleanScore == 0 ? "" : _score.CleanScore.ToString();

        if (_score.IsWin)
        {
            for (int i = 0; i < modifiers.Count; i++)
            {
                GameObject textObject = Instantiate(_textModifirePrefab, _modifireContainer.transform);
                textObject.GetComponentInChildren<Text>().text = modifiers[i].Text + modifiers[i].Value;
                _textModifiresList.Add(textObject);
            }
            _totalScoreText.text = _score.TotalScore.ToString();
        }
    }

    public void Clear()
    {
        _status.text = "";
        _scoreText.text = "";
        _totalScoreText.text = "";

        foreach (var item in _goldStars)
        {
            Destroy(item);
        }
        foreach (var item in _textModifiresList)
        {
            Destroy(item);
        }
        _textModifiresList.Clear();
    }

    private void ShowStars()
    {
        if (_score.IsWin)
        {
            switch (_score.Rating)
            {
                case 1:
                    _goldStars.Add(Instantiate(_goldStar, _star1.transform));
                    break;
                case 2:
                    _goldStars.Add(Instantiate(_goldStar, _star1.transform));
                    _goldStars.Add(Instantiate(_goldStar, _star2.transform));
                    break;
                case 3:
                    _goldStars.Add(Instantiate(_goldStar, _star1.transform));
                    _goldStars.Add(Instantiate(_goldStar, _star2.transform));
                    _goldStars.Add(Instantiate(_goldStar, _star3.transform));
                    break;
                default:
                    break;
            }
        }
    }

    private void Awake()
    {
        _scoreCollector = GetComponent<ScoreCollector>();
    }
}
