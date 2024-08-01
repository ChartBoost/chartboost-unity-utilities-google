#if UNITY_IOS
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Chartboost.Logging;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.iOS.Xcode;
using UnityEngine;

namespace Chartboost.Google.Editor
{
    public class GoogleSettingsPostprocessor : IPostprocessBuildWithReport
    {
        private const string GoogleAppIdKey = "GADApplicationIdentifier";

        public int callbackOrder { get; }
        
        public void OnPostprocessBuild(BuildReport report)
        {
            var buildTarget = report.summary.platform;
            var pathToBuiltProject = report.summary.outputPath;
            
            if (buildTarget != BuildTarget.iOS)
                return;
            
            PListModifications(pathToBuiltProject);
        }
        
        private static void PListModifications(string pathToBuiltProject)
        {
            var plistPath = Path.Combine(pathToBuiltProject, "Info.plist");
            var plist = new PlistDocument();
            plist.ReadFromFile(plistPath);

            var mods = new HashSet<bool>
            {
                IncludeGoogleAppId(plist)
            };
            
            if (mods.Any(x => x))
                plist.WriteToFile(plistPath);
            
            static bool IncludeGoogleAppId(PlistDocument plist)
            {
                if (string.IsNullOrEmpty(GoogleSettings.Instance.GoogleAppIdIOS))
                    return false;
                
                GoogleSettings.ValidateGoogleAppId(GoogleSettings.Instance.GoogleAppIdIOS);
            
                plist.root.SetString(GoogleAppIdKey, GoogleSettings.Instance.GoogleAppIdIOS);
                LogController.Log($"Added {GoogleAppIdKey} to Info.plist", LogLevel.Debug);
                return true;
            }
        }
    }
}
#endif
