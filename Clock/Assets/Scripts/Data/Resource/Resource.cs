using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MSuhinin.Clock
{
    [Serializable]
    public struct Resource
    {
        public string ID;
        public string Name;
        public AssetReferenceSprite Sprite;
        public ScriptableObject AdditionalData;
    }
}