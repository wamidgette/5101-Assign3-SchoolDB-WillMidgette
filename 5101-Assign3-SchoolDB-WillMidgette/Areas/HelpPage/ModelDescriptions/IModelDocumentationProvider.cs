using System;
using System.Reflection;

namespace _5101_Assign3_SchoolDB_WillMidgette.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}