using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
	[InitializeOnLoad]
	internal static class AddComponentChecker
	{
		static AddComponentChecker()
		{
			ObjectFactory.componentWasAdded += OnComponentWasAdded;
		}

		private static void OnComponentWasAdded( Component component )
		{
			// CanvasRenderer を所持していないゲームオブジェクトには
			// UIGrayOut コンポーネントをアタッチできないようにする

			// UIGrayOut コンポーネントではない場合は無視
			if ( !( component is UIGrayOut ) ) return;

			// CanvasRenderer を所持している場合は無視
			if ( component.GetComponent<CanvasRenderer>() != null ) return;
			
			// 1 フレーム遅らせないと下記のエラーが発生する
			// Assertion failed on expression: 'm_InstanceID != InstanceID_None'
			EditorApplication.delayCall += () => Object.DestroyImmediate( component );
		}
	}
}