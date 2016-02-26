﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

using BattleScene;

namespace BattleScene
{


//敵、味方キャラクターの基本クラス

    public class BCharacterBase : MonoBehaviour
    {
        //キャラクターパラメーター
        ///インスペクタから編集
        public CharacterParameter characterParameter;

        //キャタクターアクティブ時
        public event Action<BCharacterBase> OnActiveE;

        public static event Action<BCharacterBase> OnActiveStaticE;

        //キャタクター非アクティブ時
        public event Action<BCharacterBase> OnEndActiveE;

        //キャラクター死亡時
        public event Action<BCharacterBase> OnDeathE;

        //ステータス変更
        public event Action<BCharacterBase> OnStatusUpdateE;

        public bool isEnemy = false;

        //座標
        //現在のキャラクター位置配列
        [System.NonSerialized]
        public IntVect2D positionArray = new IntVect2D(0, 0);
        
        //攻撃
        BCharacterActionManagerBase attacker;

        //移動
        BCharacterActionManagerBase mover;

        //行動中
        public bool IsNowAction {
            get { return attacker.IsNowAction() == true && mover.IsNowAction() == true; } 
        }

        BCharacterAnimator animator;

        //ライフ
        public BCharacterLife Life
        {
            get { return life; }
        }
        BCharacterLife life;

        //画面UIへの参照
        public BCharacterStateUI StateUI
        {
            get { return stateUI; }
        }
        BCharacterStateUI stateUI;

        //詳細UIへの参照
        CharacterDetailStateUI detailUI=null;
        
        //アクティブサークル
        GameObject activeCircle;
        

        public void Init(IntVect2D array)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            positionArray.x = array.x;
            positionArray.y = array.y;

            //ライフ設定
            life.Init(characterParameter);


            //選択マーカー表示
            activeCircle = transform.FindChild("ActiveCircle").gameObject;
            activeCircle.SetActive(false);

        }

        public virtual void Awake()
        {
            life = GetComponent<BCharacterLife>();
            attacker = GetComponent<BCharacterAttackerManagerBase>();
        }

        public virtual void Start()
        {
            //アクティブタイム作成
            CreateActiveTime();

            //位置変更
            SetPositionOnTile();

            //UI作成
            CreateCharacterUI();

        }

        void CreateActiveTime()
        {
            var activeTime = BActiveTimeCreater.Instance.CreateActiveTime();
            activeTime.Init(this);
            //SetActiveTimeEventHandler();
        }

        //タイルの上に移動
        void SetPositionOnTile()
        {
            var tilePosition = BBattleStage.Instance.GetTileXAndZPosition(positionArray);
            CSTransform.SetX(transform, tilePosition.x);
            CSTransform.SetZ(transform, tilePosition.y);
        }

        void CreateCharacterUI()
        {
            stateUI = Instantiate(Resources.Load<BCharacterStateUI>("CharacterStateUI")) as BCharacterStateUI;
            stateUI.Init(this);
        }

        //キャラクターを行動選択状態にする
        public void OnActive()
        {

            if(OnActiveE!=null)OnActiveE(this);
            if (OnActiveStaticE != null) OnActiveStaticE(this);

            
            activeCircle.SetActive(true);
            //タイル変更
            //BattleStage.Instance.UpdateTileColors(this, TileState.Move);
        }

        public void OnEndActive()
        {
            if (OnEndActiveE != null) OnEndActiveE(this);

            activeCircle.SetActive(false);
        }


        //死の実行
        public void DeathMyself()
        {
            //爆発エフェクト
            //Instantiate(Resources.Load<GameObject>("DeathEffect"), transform.position, Quaternion.identity);
            //リストから除く
            //WaitTimeManager.Instance.DestroyWaitTime(this.activeTime);
            //RemoveActiveTimeEventHandler();
            
            
            //activeTime.DeathCharacter();
            //CharacterManager.Instance.DestroyCharacter(this);
            if(OnDeathE!=null)OnDeathE(this);
            Destroy(gameObject);
        }


        public BCameraMove GetCamera()
        {
            return GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BCameraMove>();
        }

    }

}