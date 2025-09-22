using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Updater.Localization
{
    public class LocString
    {
        protected string _rusStr;
        protected string _engStr;
        protected string _korStr;
        protected string _chiStr;

        public LocString(string rusStr, string engStr, string korStr, string chiStr)
        {
            _rusStr = rusStr;
            _engStr = engStr;
            _korStr = korStr;
            _chiStr = chiStr;
        }

        public virtual string GetLocStr 
        {
            get
            {
                switch (LangInfo.Lang)
                {
                    case Languages.Rus:
                        return _rusStr;
                    case Languages.Eng:
                        return _engStr;
                    case Languages.Kor:
                        return _korStr;
                    case Languages.Chi:
                        return _chiStr;
                    default:
                        return _rusStr;
                }
            }
        }

        public override string ToString()
        {
            return GetLocStr;
        }
    }
}
