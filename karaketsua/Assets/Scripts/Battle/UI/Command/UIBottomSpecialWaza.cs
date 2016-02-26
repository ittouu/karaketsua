﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using BattleScene;

namespace BattleScene
{

    public class UIBottomSpecialWaza : UIBottomBase
    {
        Button button;
        Text wazaName;
        public UIBottomCommandParent commandParent;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void UpdateUI()
        {
            wazaName.text = "なし";
            button.interactable = false;

            //テキストの変更
            var chara = BCharacterManager.Instance.GetActiveCharacter();
            if (chara == null) return;

            var param = chara.characterParameter.moveAttackParameter;
            //技がある
            if (param!=null)
            {
                wazaName.text = param.wazaName;
                button.interactable = true;
            }
        }
        public void OnClick()
        {
            //BCharacterManager.Instance.GetActiveCharacter().SelectMoveAttack();
            //commandParent.CreateExecuteAttack();
            UIBottomCommandParent.UICommandState = EUICommandState.ExecuteAttack;
            UIBottomAllParent.Instance.UpdateUI();
        }
    }
}