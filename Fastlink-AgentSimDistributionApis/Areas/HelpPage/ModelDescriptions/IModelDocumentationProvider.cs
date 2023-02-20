using System;
using System.Reflection;

namespace Fastlink_AgentSimDistributionApis.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}