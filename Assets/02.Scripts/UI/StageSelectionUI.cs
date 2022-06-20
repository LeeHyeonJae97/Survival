using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectionUI : UI
{
    [SerializeField] private Button _backButton;
    [SerializeField] private Transform _stageSlotHolder;
    [SerializeField] private StageSlot _stageSlotPrefab;
    private StageSlot _selected;

    protected override void Awake()
    {
        base.Awake();

        _backButton.onClick.AddListener(OnClickBackButton);
    }

    protected override void OnSetActive(bool value)
    {
        if (value)
        {
            var stages = StageFactory.GetAll();

            for (int i = 0; i < stages.Length; i++)
            {
                if (i < _stageSlotHolder.childCount)
                {
                    _stageSlotHolder.GetChild(i).GetComponent<StageSlot>().Init(stages[i], OnClickStageSlotButton);
                }
                else
                {
                    Instantiate(_stageSlotPrefab, _stageSlotHolder).Init(stages[i], OnClickStageSlotButton);
                }
            }
        }
        else
        {
            _selected = null;
        }
    }

    private void OnClickBackButton()
    {
        SetActive(false);
    }

    private void OnClickStageSlotButton(StageSlot slot)
    {
        if (_selected != slot)
        {
            if (_selected != null) _selected.OnClickSlotButton();
            slot.OnClickSlotButton();
            _selected = slot;
        }
        else
        {
            _selected.OnClickSlotButton();
            _selected = null;
        }
    }
}
