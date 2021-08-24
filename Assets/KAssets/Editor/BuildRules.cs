using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace kassets
{
    [Serializable]
    public class BuildRule
    {
        [Tooltip("搜索路径")] public string path;

        [Tooltip("搜索通配符")] public string pattern;

        [Tooltip("名称")] public string bundleName;
        //[Tooltip("是否程序动态加载")] public bool dynamic; //减少catlog 表
    }

    [Serializable]
    public class BuildModule
    {
        [Tooltip("模块")] public string name;
        [Tooltip("是否是DLC")] public bool DLC;
        public BuildRule[] rules = new BuildRule[0];
    }

    [CreateAssetMenu(fileName = "BuildRules", menuName = "(kassets) BuildRules")]
    public class BuildRules : ScriptableObject
    {
        public BuildModule[] modules = new BuildModule[0];
        public BuildRule[] Script = new BuildRule[0];
    }
}