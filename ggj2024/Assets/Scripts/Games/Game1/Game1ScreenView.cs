using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game1ScreenView : GameScreenView
{
    public Game1Presenter MyPresenter { get { return presenter as Game1Presenter; } }

    [SerializeField] private Game01 gameplay;

    protected override void Initialize()
    {
        base.Initialize();
        presenter = new Game1Presenter(this, gameplay);
    }
}
