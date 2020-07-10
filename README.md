# UniUIGrayOut

UI オブジェクトをグレーアウトできるコンポーネント

## 使い方

![2020-07-10_104652](https://user-images.githubusercontent.com/6134875/87107404-a52a8200-c29a-11ea-9e1a-b2a5e7148e95.png)

グレーアウトさせたい UI オブジェクトに「UIGrayOut」コンポーネントをアタッチして  

```cs
using Kogane;
using UnityEngine;

public class Example : MonoBehaviour
{
    public UIGrayOut m_grayOutUI;

    private void Update()
    {
        if ( Input.GetKeyDown( KeyCode.Space ) )
        {
            m_grayOutUI.SetGrayOut( !m_grayOutUI.IsGrayOut );
        }
    }
}
```

上記のようなスプライトを記述することで  

![12](https://user-images.githubusercontent.com/6134875/87107481-cee3a900-c29a-11ea-8739-310a81037fc5.gif)

UI オブジェクトをグレーアウトできます  

```cs
UIGrayOut.GrayOutColor = new Color32( 255, 0, 0, 255 );
```

グレーアウトさせた時の色を指定することも可能です  
