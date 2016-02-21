﻿using UnityEngine;
using System.Collections;
using BattleScene;

namespace BattleScene
{
    public class UIBottomBackToAction : UIBottomButton
    {
        public override void OnClick()
        {
            UIBottomCommandParent.Instance.CreateAction();
            CharacterManager.Instance.GetActiveCharacter().SelectMove();
        }
    }
}