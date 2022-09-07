using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public Action<string> OnGameStarted;

    [SerializeField]
    private RectTransform gameWonPanel, wrongOrderPanel, gameStartPanel;

    [SerializeField]
    private TMP_Text gameWonText, wrongOrderText;

    [SerializeField]
    private Button StartButton;

    [SerializeField]
    private TMP_InputField nameInputField;

    private const string GameWonString = "Exercise Finished!\nYour Time: ";

    private float hangingTime = 1.5f;
    private float showingTime = 0.7f;


    private void Start()
    {
        nameInputField.onEndEdit.AddListener(ValidateInput);
        StartButton.onClick.AddListener(StartGame);
    }

    public void showGameWonPanel(float yourTime)
    {
        gameWonPanel.DOMoveY(gameWonPanel.parent.position.y, showingTime).SetEase(Ease.OutCirc);
        gameWonText.SetText(GameWonString + yourTime.ToString("F1"));
    }

    public void ShowWrongOrderPanel(string wrongOrderDesc)
    {
        wrongOrderText.SetText(wrongOrderDesc);
        Tween show = wrongOrderPanel.DOAnchorPosY(0, showingTime);
        show.onComplete = () => StartCoroutine(HideWrongOrderPanelAfterTime());
    }

    private IEnumerator HideWrongOrderPanelAfterTime()
    {
        yield return new WaitForSeconds(hangingTime);
        wrongOrderPanel.DOAnchorPosY(-wrongOrderPanel.rect.size.y, showingTime);
    }

    private void ValidateInput(string name)
    {
        if(name.Trim().Length > 2)
        {
            StartButton.gameObject.SetActive(true);
        }
        else
        {
            StartButton.gameObject.SetActive(false);
        }
    }

    private void StartGame()
    {
        OnGameStarted?.Invoke(nameInputField.text);
        HideGameStartPanel();
    }

    private void HideGameStartPanel()
    {
        float targetPosY = -gameStartPanel.anchoredPosition.y - gameStartPanel.rect.size.y;
        gameStartPanel.DOAnchorPosY(targetPosY, showingTime).SetEase(Ease.InCirc);
    }


}
