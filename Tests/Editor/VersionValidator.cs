using Chartboost.Editor;
using NUnit.Framework;

namespace Charboost.Utilities.Google.Tests.Editor
{
    public class VersionValidator
    {
        private const string UnityPackageManagerPackageName = "com.chartboost.unity.utilities.google";
        private const string NuGetPackageName = "Chartboost.CSharp.Utilities.Google.Unity";
        
        [Test]
        public void ValidateVersion() 
            => VersionCheck.ValidateVersions(UnityPackageManagerPackageName, NuGetPackageName);
    }
}
