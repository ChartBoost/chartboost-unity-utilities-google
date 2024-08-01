#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Chartboost.Editor;
using Chartboost.Logging;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Chartboost.Google.Editor
{
    public class GoogleSettingsPreprocessor : IPreprocessBuildWithReport
    {
        private const string GoogleAppIdIdentifier = "com.google.android.gms.ads.APPLICATION_ID";
        private const string MenuItemName = "Chartboost/Google/Patch Android Manifest";

        public int callbackOrder { get; }

        public void OnPreprocessBuild(BuildReport report)
        {
            if (report.summary.platform != BuildTarget.Android)
                return;

            ValidateAndroidManifest();
        }
        
        [MenuItem(MenuItemName)]
        private static void ValidateAndroidManifest()
        {
            if (!EditorConstants.PathToAndroidManifest.FileExist())
            {
                LogController.Log($"{EditorConstants.PathToAndroidManifest} does not exist, this will result in a failed integration for Google SDKs", LogLevel.Warning);
                return;
            }

            var androidManifest = XDocument.Load(EditorConstants.PathToAndroidManifest);

            var mods = new HashSet<bool>
            {
                IncludeElementsOrAdd(androidManifest, GoogleAppIdIdentifier,
                    GoogleSettings.Instance.GoogleAppIdAndroid),
            };

            if (mods.Any(x => x))
            {
                androidManifest.Save(EditorConstants.PathToAndroidManifest);
                AssetDatabase.Refresh();
            }

            static bool IncludeElementsOrAdd(XDocument androidManifest, string elementIdentifier, string elementValue)
            {
                if (androidManifest == null) throw new ArgumentNullException(nameof(androidManifest));
                if (string.IsNullOrEmpty(elementValue))
                    return false;

                GoogleSettings.ValidateGoogleAppId(GoogleSettings.Instance.GoogleAppIdAndroid);

                var targetElement =
                    XElement.Parse($"<meta-data android:name=\"{elementIdentifier}\" android:value=\"{elementValue}\" xmlns:android=\"http://schemas.android.com/apk/res/android\"/>");

                try
                {
                    var targetElementInManifest = androidManifest.Descendants("meta-data")
                        .First(x => x.Attributes().Any(a => a.Value == elementIdentifier));
                    var targetElementMatch = targetElementInManifest.Attributes()
                        .Any(attribute => attribute.Value == elementValue);
                    if (targetElementMatch)
                        LogController.Log($"{elementIdentifier} Found in Android Manifest!", LogLevel.Debug);
                    else
                        LogController.Log($"A {elementIdentifier} was found in manifest but did not match the {elementIdentifier} found in the GoogleSettings.", LogLevel.Warning);
                }
                catch (InvalidOperationException googleAppIdException)
                {
                    LogController.LogException(googleAppIdException);
                    try
                    {
                        var applicationNode = androidManifest.Descendants("application").First();
                        targetElement.LastAttribute.Remove();
                        applicationNode.Add(targetElement);
                    }
                    catch (InvalidOperationException applicationNodeException)
                    {
                        LogController.LogException(new AggregateException($"Could not find an application element in AndroidManifest.xml, your AndroidManifest might be malformed. \n{applicationNodeException.Message}", applicationNodeException));
                    }
                }

                return true;
            }
        }
    }
}
#endif
