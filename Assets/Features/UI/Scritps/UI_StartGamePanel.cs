using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_StartGamePanel : MonoBehaviour
{
    [SerializeField] private UI_Manager uiManager;
    [SerializeField] private UI_ErrorText errorText;
    [SerializeField] private TMP_InputField rowsInput;
    [SerializeField] private TMP_InputField columnsInput;
    
    private IGameRules_Layout _gameRules;
    
    private int rows;
    private int columns;

    void Start()
    {
        _gameRules = DI.Get<IGameRules_Layout>();
        rows = _gameRules.DefaultRows;
        columns = _gameRules.DefaultColumns;
        rowsInput.text = rows.ToString();
        columnsInput.text = columns.ToString();
    }
    
    public void StartGame()
    {
        if (!_gameRules.IsRowAndColumnValid(rows, columns))
        {
            errorText.ShowError("Rows x Columns must be divisible by 2");
            return;
        }

        uiManager.StartGame(rows, columns);
    }

    public void SetRows(string value)
    {
        if (!int.TryParse(value, out rows))
        {
            errorText.ShowError("Rows must be a number");
            return;
        }

        if (rows < _gameRules.MinRows || rows > _gameRules.MaxRows)
        {
            errorText.ShowError($"Rows must be greater than {_gameRules.MinRows} and less than {_gameRules.MaxRows}");
            return;
        }
        
        this.rows = rows;
    }
    
    public void SetColumns(string value)
    {
        if (!int.TryParse(value, out columns))
        {
            errorText.ShowError("Columns must be a number");
            return;
        }
        
        if (columns < _gameRules.MinColumns || columns > _gameRules.MaxColumns)
        {
            errorText.ShowError($"Columns must be greater than {_gameRules.MinColumns} and less than {_gameRules.MaxColumns}");
            return;
        }
        
        this.columns = columns;
    }
}
