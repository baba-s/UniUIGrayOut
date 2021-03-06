﻿using UnityEngine;

namespace Kogane
{
	/// <summary>
	/// UI のグレーアウトを管理するコンポーネント
	/// </summary>
	[DisallowMultipleComponent]
	public sealed class UIGrayOut : MonoBehaviour
	{
		//==============================================================================
		// 変数
		//==============================================================================
		private bool           m_isInit;
		private CanvasRenderer m_canvasRenderer;
		private UIGrayOut[]    m_children;
		private Color          m_defaultColor;
		private Color          m_grayOutColor;

		//==============================================================================
		// プロパティ
		//==============================================================================
		public bool IsGrayOut { get; private set; }

		//==============================================================================
		// プロパティ(static)
		//==============================================================================
		public static Color GrayOutColor { get; set; } = Color.gray;

		//==============================================================================
		// 関数
		//==============================================================================
		/// <summary>
		/// 初期化します
		/// </summary>
		private void Init()
		{
			if ( m_isInit ) return;
			m_isInit = true;

			m_canvasRenderer = GetComponent<CanvasRenderer>();
			m_defaultColor   = m_canvasRenderer.GetColor();

			m_grayOutColor = new Color
			(
				m_defaultColor.r * GrayOutColor.r,
				m_defaultColor.g * GrayOutColor.g,
				m_defaultColor.b * GrayOutColor.b,
				m_defaultColor.a * GrayOutColor.a
			);
		}

		/// <summary>
		/// 有効になった時に呼び出されます
		/// </summary>
		private void OnEnable()
		{
			Init();

			// ゲームオブジェクトが非アクティブになってから再度アクティブになると
			// CanvasRenderer に設定したカラーがリセットされてしまうので
			// アクティブになった時にグレーアウトしているかどうかで色を再設定する
			SetGrayOutWithoutChildrenImpl( IsGrayOut );
		}

		/// <summary>
		/// グレーアウトするかどうかを設定します
		/// </summary>
		public void SetGrayOutWithoutChildren( bool isGrayOut )
		{
			Init();

			if ( IsGrayOut == isGrayOut ) return;
			IsGrayOut = isGrayOut;

			SetGrayOutWithoutChildrenImpl( isGrayOut );
		}

		/// <summary>
		/// グレーアウトするかどうかを設定します
		/// </summary>
		private void SetGrayOutWithoutChildrenImpl( bool isGrayOut )
		{
			var color = isGrayOut ? m_grayOutColor : m_defaultColor;
			m_canvasRenderer.SetColor( color );
		}

		/// <summary>
		/// すべての子オブジェクトを含めグレーアウトするかどうかを設定します
		/// </summary>
		public void SetGrayOut( bool isGrayOut )
		{
			Init();

			if ( m_children == null )
			{
				m_children = GetComponentsInChildren<UIGrayOut>( true );
			}

			for ( var i = 0; i < m_children.Length; i++ )
			{
				var child = m_children[ i ];
				child.SetGrayOutWithoutChildren( isGrayOut );
			}
		}
	}

	/// <summary>
	/// UIGrayOut 型の拡張メソッドを管理するクラス
	/// </summary>
	public static class UIGrayOutExt
	{
		/// <summary>
		/// グレーアウトしている場合 true を返します
		/// </summary>
		public static bool IsGrayOutIfNotNull( this UIGrayOut self )
		{
			return self != null && self.IsGrayOut;
		}

		/// <summary>
		/// グレーアウトするかどうかを設定します
		/// </summary>
		public static void SetGrayOutWithoutChildrenIfNotNull( this UIGrayOut self, bool isGrayOut )
		{
			if ( self == null ) return;
			self.SetGrayOutWithoutChildren( isGrayOut );
		}

		/// <summary>
		/// すべての子オブジェクトを含めグレーアウトするかどうかを設定します
		/// </summary>
		public static void SetGrayOutIfNotNull( this UIGrayOut self, bool isGrayOut )
		{
			if ( self == null ) return;
			self.SetGrayOut( isGrayOut );
		}
	}
}