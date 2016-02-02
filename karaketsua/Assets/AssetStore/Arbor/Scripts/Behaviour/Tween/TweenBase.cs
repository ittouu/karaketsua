﻿using UnityEngine;
using System.Collections;

namespace Arbor
{
	[AddComponentMenu("")]
	[HideBehaviour]
	public class TweenBase : StateBehaviour 
	{
		public enum Type
		{
			Once,
			Loop,
			PingPong,
		};

		[SerializeField] private Type _Type;
		[SerializeField] private float _Duration = 1.0f;
		[SerializeField] private AnimationCurve _Curve = AnimationCurve.Linear( 0.0f,0.0f,1.0f,1.0f );
		[SerializeField] private bool _UseRealtime = false;
		[SerializeField] private int _RepeatUntilTransition = 1;
		[SerializeField] private StateLink _NextState;

		protected virtual bool fixedUpdate
		{
			get
			{
				return false;
			}
		}

		private float _BeginTime = 0.0f;

		private float _FromAdvance = 0.0f;
		private float _ToAdvance = 1.0f;

		private int _RepeatCount = 0;
		public int repeatCount
		{
			get
			{
				return _RepeatCount;
            }
		}
		
		private float GetTime()
		{
			if( _UseRealtime )
			{
				return Time.realtimeSinceStartup;
			}

			if (fixedUpdate)
			{
				return Time.fixedTime;
			}

			return Time.time;
		}

		// Use this for enter state
		public override void OnStateBegin() 
		{
			_BeginTime = GetTime();

			_FromAdvance = 0.0f;
			_ToAdvance = 1.0f;
			_RepeatCount = 0;
		}

		protected virtual void OnTweenUpdate( float factor ){}

		void TweenUpdate()
		{
			float nowTime = GetTime();

			float t = 0.0f;

			if (_Duration > 0.0f)
			{
				t = (nowTime - _BeginTime) / _Duration;
			}
			else
			{
				t = 1.0f;
			}

			float factor = Mathf.Lerp(_FromAdvance, _ToAdvance, Mathf.Clamp01(t));

			if (t > 1.0f)
			{
				switch (_Type)
				{
					case Type.Once:
						break;
					case Type.Loop:
						_BeginTime = nowTime;
						break;
					case Type.PingPong:
						_BeginTime = nowTime;

						float temp = _FromAdvance;
						_FromAdvance = _ToAdvance;
						_ToAdvance = temp;
						break;
				}

				if (_Type == Type.Once)
				{
					Transition(_NextState);
				}
				else
				{
					_RepeatCount++;
					if (_RepeatCount >= _RepeatUntilTransition)
					{
						Transition(_NextState);
					}
				}
			}

			OnTweenUpdate(_Curve.Evaluate(factor));
		}
		
		// Update is called once per frame
		void Update () 
		{
			if (!fixedUpdate)
			{
				TweenUpdate();
            }
		}

		void FixedUpdate()
		{
			if ( fixedUpdate )
			{
				TweenUpdate();
			}
		}
	}
}