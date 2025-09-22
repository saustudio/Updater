using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Updater.Localization
{
    public class LocStringTwoParams : LocString
    {
        public string Param1 { get; set; }
        private string Param2 { get; set; }

        public LocStringTwoParams(string rusStr, string engStr) : base(rusStr, engStr,"","")
        {

        }

        public override string GetLocStr
        {
            get
            {
                switch (LangInfo.Lang)
                {
                    case Languages.Rus:
                        return string.Format(_rusStr, Param1, Param2);
                    case Languages.Eng:
                        return string.Format(_engStr, Param1, Param2);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
