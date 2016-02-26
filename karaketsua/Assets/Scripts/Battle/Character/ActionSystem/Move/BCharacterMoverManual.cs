﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using BattleScene;

namespace BattleScene
{
    public class BCharacterMoverManual : BCharacterMoverBase
    {
        //移動方向のオブジェクト
        public GameObject directionIcon;

        //選択可能時
        public override void Enable()
        {
            if (isDone == true) return;
            base.Enable();
            //OnActiveCharacter();
            //キャラクター移動選択
            IT_Gesture.onDraggingStartE += OnChargeForMove;

            directionIcon.SetActive(true);
        }
        

        public override void Disable()
        {
            Reset();
        }
        
        public override void Reset()
        {
            //キャラクター移動選択
            IT_Gesture.onDraggingStartE -= OnChargeForMove;
            IT_Gesture.onDraggingEndE -= OnDragMove;
            directionIcon.SetActive(false);
            base.Reset();
        }

        //スワイプによる移動操作検知
        void OnChargeForMove(DragInfo dragInfo)
        {
            //自分キャラ
            if (BCharacterManager.Instance.GetCharacterOnTile(dragInfo.pos) != this.character) return;

            //キャラクター移動用
            IT_Gesture.onDraggingEndE += OnDragMove;
        }
        //スワイプによる移動操作終了
        void OnDragMove(DragInfo dragInfo)
        {
            //移動方向決定
            RequestMove(dragInfo.delta);
            IT_Gesture.onDraggingEndE -= OnDragMove;
        }

        //一連の移動判定処理の開始
        void RequestMove(Vector2 delta)
        {
            //カメラから方向取得
            var toVect = BCameraMove.Instance.GetMoveDirection(delta);
            //Vect2D化
            var toVect2D = IntVect2D.GetDirectionFromVector2(toVect);

            RequestMoveFromVect2D(toVect2D);
        }

        //移動行動を受け付けなくする
        public override void UpdateInManualState()
        {
            IT_Gesture.onDraggingStartE -= OnChargeForMove;
            directionIcon.SetActive(false);
            BBattleStage.Instance.ResetAllTileColor();
        }



        public override void CompleteMove()
        {
            base.CompleteMove();

            BBattleStage.Instance.ResetAllTileColor();

            UIBottomCommandParent.UICommandState = EUICommandState.Action;
            UIBottomAllParent.Instance.UpdateUI();
        }
        
    }
}