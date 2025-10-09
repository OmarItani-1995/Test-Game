using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int Rows;
    public int Columns;

    private IGrid _grid;
    private ICamera _camera;
    private ICardManager _cardManager;
    private IGameEnder _gameEnder;
    
    [SerializeField] private float _delayBeforeHidingCards = 3;
    [SerializeField] private float _delayAfterHidingCards = 1;
    void Start()
    {
        _grid = DI.Get<IGrid>();
        _camera = DI.Get<ICamera>();
        _cardManager = DI.Get<ICardManager>();
        _gameEnder = DI.Get<IGameEnder>();
        
        Msg.RegisterListener(typeof(Msg_GameStarted), OnStartGame);
        Msg.RegisterListener(typeof(Msg_AllCardsMatched), OnAllCardsMatched);
    }

    private void OnStartGame(Message message)
    {
        Msg_GameStarted msgStart = message as Msg_GameStarted;
        Rows = msgStart.rows;
        Columns = msgStart.columns;
        StartCoroutine(nameof(InitializeLevel));
    }
    
    private IEnumerator InitializeLevel()
    {
        _grid.GenerateGrid(Rows, Columns);
        _camera.MoveToView(Rows, Columns);        
        yield return new WaitForSeconds(1f);
        yield return _cardManager.SetUpCards(Rows * Columns);
        yield return new WaitForSeconds(_delayBeforeHidingCards);
        _cardManager.HideAllCards();
        yield return new WaitForSeconds(_delayAfterHidingCards);
    }
    
    private void OnAllCardsMatched(Message message)
    {
        _cardManager.AnimateAllMatchedCards();
        _gameEnder.EndGame();
    }
}
