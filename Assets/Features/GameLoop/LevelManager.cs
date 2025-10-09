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
    
    void Start()
    {
        _grid = DI.Get<IGrid>();
        _camera = DI.Get<ICamera>();
        _cardManager = DI.Get<ICardManager>();
        
        Msg.RegisterListener(typeof(Msg_GameStarted), OnGameStarted);
    }

    private void OnGameStarted(Message message)
    {
        Msg_GameStarted msg = message as Msg_GameStarted;
        Rows = msg.rows;
        Columns = msg.columns;
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
    }
}
