using System;

namespace WC.Lib.Model
{
    
    public enum Result
    {
        Unhandle=0,
        Ok=1,
        Fail=2
    }


    [Serializable]
    public class ResultDto
    {

        public Result Result { get; set; }

        public string Content { get; set; }
    }
}
