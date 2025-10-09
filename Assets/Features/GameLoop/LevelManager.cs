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

    [SerializeField] private float _delayBeforeHidingCards = 3;
    [SerializeField] private float _delayAfterHidingCards = 1;
    void Start()
    {
        _grid = DI.Get<IGrid>();
        _camera = DI.Get<ICamera>();
        _cardManager = DI.Get<ICardManager>();
        
        Msg.RegisterListener(typeof(Msg_StartGame), OnStartGame);
    }

    private void OnStartGame(Message message)
    {
        Msg_StartGame msgStart = message as Msg_StartGame;
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
        Msg.TriggerMessage(new Msg_GameStarted());
    }
}

public class Msg_GameStarted : Message
{
    
}