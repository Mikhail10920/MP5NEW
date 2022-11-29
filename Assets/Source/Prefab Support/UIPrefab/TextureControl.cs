using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureControl : MonoBehaviour
{
    public TexturePlacement Texture = null;
    public XformTextureControl XformTextureControl = null;

    // Use this for initialization
    void Start()
    {
        Debug.Assert(Texture != null);
        Debug.Assert(XformTextureControl != null);
        TexturePlacementChange();
    }

    void TexturePlacementChange()
    {
        XformTextureControl.SetSelectedObject(Texture);
    }
}
