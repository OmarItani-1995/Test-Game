using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Matcher : MonoBehaviour, ICardMatcher
{
    private ICardManager _cardManager;
    private List<Card_Matcher_Instance> _matches = new List<Card_Matcher_Instance>();
    private List<Card_Matcher_Instance> _doneMatches = new List<Card_Matcher_Instance>();
    
    private List<Card_Holder> _shownCards = new List<Card_Holder>();
    void Awake()
    {
        DI.Register<ICardMatcher>(this);
    }

    void Start()
    {
        _cardManager = DI.Get<ICardManager>();
    }

    void Update()
    {
        if (_matches.Count == 0) return;
        for (int i = _matches.Count - 1; i >= 0; i--)
        {
            _matches[i].Update();
        }
    }

    public void CardClicked(Card_Holder holder)
    {
        if (holder.IsHidden())
        {
            ShowCard(holder);
        }
        else
        {
            HideCard(holder);
        }
    }

    private void ShowCard(Card_Holder holder)
    {
        if (_shownCards.Contains(holder)) return;
        
        if (_shownCards.Count >= 2) return;

        _shownCards.Add(holder);
        holder.ShowCard();

        if (_shownCards.Count == 2)
        {
            CreateMatchInstance();
        }   
    }

    private void CreateMatchInstance()
    {
        var matchInstance = new Card_Matcher_Instance(_shownCards, OnMatchSuccess, OnMatchFailed);
        _matches.Add(matchInstance);
        _shownCards = new List<Card_Holder>();
    }

    private void HideCard(Card_Holder holder)
    {
        if (!_shownCards.Contains(holder))
        {
            return;
        }
        
        holder.HideCard();
        _shownCards.Remove(holder);
    }
    
    private void OnMatchSuccess(Card_Matcher_Instance instance)
    {
        MoveMatchToDone(instance);
        _cardManager.MatchHappened(instance.ShownCards);
    }
    
    private void OnMatchFailed(Card_Matcher_Instance instance)
    {
        MoveMatchToDone(instance);
    }

    private void MoveMatchToDone(Card_Matcher_Instance instance)
    {
        _matches.Remove(instance);
        _doneMatches.Add(instance);
    }
}

public interface ICardMatcher
{
    void CardClicked(Card_Holder holder);
}

public class Card_Matcher_Instance
{
    private List<Card_Holder> _shownCards = new List<Card_Holder>();
    private System.Action<Card_Matcher_Instance> _onMatchSuccess;
    private System.Action<Card_Matcher_Instance> _onMatchFailed;

    private float _timer = 0;
    private float _maxTime = 1f;
    
    public List<Card_Holder> ShownCards => _shownCards;
    public Card_Matcher_Instance(List<Card_Holder> shownCards, System.Action<Card_Matcher_Instance> onMatchSuccess, System.Action<Card_Matcher_Instance> onMatchFailed)
    {
        _shownCards = shownCards;
        _onMatchSuccess = onMatchSuccess;
        _onMatchFailed = onMatchFailed;
        
        foreach (var card in _shownCards)
        {
            card.DisableInput();
        }
    }

    public void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _maxTime)
        {
            CheckMatch();
        }    
    }
    
    void CheckMatch()
    {
        if (_shownCards[0].HasSameCard(_shownCards[1]))
        {
            _onMatchSuccess?.Invoke(this);
        }
        else
        {
            foreach (var card in _shownCards)
            {
                card.HideCard(true);
                card.EnableInput();
            }
            _onMatchFailed?.Invoke(this);
        }
    }
}