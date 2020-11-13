using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace _5101_Assign3_SchoolDB_WillMidgette.Areas.HelpPage.ModelDescriptions
{
    public class EnumTypeModelDescription : ModelDescription
    {
        public EnumTypeModelDescription()
        {
            Values = new Collection<EnumValueDescription>();
        }

        public Collection<EnumValueDescription> Values { get; private set; }
    }
}