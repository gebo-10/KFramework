using System;
using System.Collections.Generic;
using UnityEngine;

namespace kassets
{
    [Serializable]
    public class BundleID
    {
        public int module;
        public int bundle;
    }
    [Serializable]
    public class BundleInfo
    {
        public string name;
        public long len;
        public string hash;
        public string[] assets;
        public BundleID[] deps;
    }

    [Serializable]
    public class ModuleInfo
    {
        public string name;
        public bool DLC;
        public BundleInfo[] bundles = new BundleInfo[0];
    }

    [Serializable]
    public class AtlasInfo
    {
        public string name;
        public string path;
    }

    [CreateAssetMenu(fileName = "Manifest", menuName = "(kassets) Manifest")]
    public class Manifest : ScriptableObject
    {
        //public string version; Ê¹ÓÃmanifeµÄmd5
        public ModuleInfo[] modules;
        public AtlasInfo[]  atlas;
        public BundleInfo[] script;

    }
}