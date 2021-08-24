using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using System.IO;
namespace kassets
{
    public class ClearProcesser : IProcesser
    {
        public object Process()
        {
            Directory.Delete(BuildScript.tmp,true);
            return null;
        }
    }
}
