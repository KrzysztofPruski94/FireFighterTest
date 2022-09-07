using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private RaycastController raycastController;

    [SerializeField]
    private MyThirdPersonController myThirdPersonController; 
    
    [SerializeField]
    private FiremanController firemanController;  
    
    [SerializeField]
    private InputController inputController;   
    
    [SerializeField]
    private FiresController firesController; 
    
    [SerializeField]
    private UiController uiController;

    private string playerName;

    void Start()
    {
        inputController.onLeftMouseClick += raycastController.RaycastOnClick;
        inputController.onRightMouseUpAndDown += firemanController.ToggleExtinguisherFoam;
        inputController.onEscapeClick += Quit;

        raycastController.OnFloorClick += myThirdPersonController.StartMovingTowards;
        raycastController.OnEquipmentClick += firemanController.WearEquipment;

        firemanController.OnWrongEquipment += uiController.ShowWrongOrderPanel;
        firesController.OnAllFiresExtinguished += uiController.showGameWonPanel;

        uiController.OnGameStarted += StartGame;

        Pause();
    }

    private void OnDestroy()
    {
        inputController.onLeftMouseClick -= raycastController.RaycastOnClick;
        inputController.onRightMouseUpAndDown -= firemanController.ToggleExtinguisherFoam;
        inputController.onEscapeClick -= Quit;

        raycastController.OnFloorClick -= myThirdPersonController.StartMovingTowards;
        raycastController.OnEquipmentClick -= firemanController.WearEquipment;

        firemanController.OnWrongEquipment -= uiController.ShowWrongOrderPanel;
        firesController.OnAllFiresExtinguished -= uiController.showGameWonPanel;

        uiController.OnGameStarted -= StartGame;
    }

    private void Quit()
    {
        Application.Quit();
    }

    private void Pause()
    {
        Time.timeScale = 0;
    }

    private void StartGame(string name)
    {
        Time.timeScale = 1;
        playerName = name;
    }
}
