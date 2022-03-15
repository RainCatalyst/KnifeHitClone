using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachines;

public class GameStateMenu : BaseState
{
    GameManager game;

    public GameStateMenu(StateMachine stateMachine, GameManager game) : base("Menu", stateMachine)
    {
        this.game = game;
    }

    public override void Enter(BaseState fromState) { }
    public override void Exit() { }
    public override void InputUpdate() { }
    public override void Update() { }
}
