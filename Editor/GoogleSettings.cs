using System.IO;
using System.Text.RegularExpressions;
using Chartboost.Editor;
using Chartboost.Logging;
using UnityEditor;
using UnityEngine;

namespace Chartboost.Google.Editor
{
    [CreateAssetMenu(fileName = FileName, menuName = ScriptableObjectMenuName)]
    public class GoogleSettings : ScriptableObject
    {
        private const string GoogleRegex = @"^ca\-app\-pub\-\d\d\d\d\d\d\d\d\d\d\d\d\d\d\d\d~\d\d\d\d\d\d\d\d\d\d$";

        private const string FileName = "GoogleSettings";
        private const string ScriptableObjectMenuName = "Chartboost/Mediation/GoogleSettings";
        private const string MenuItemName = "Chartboost/Google/Configure";
        
        private const string DefaultFieldValue = "Modify this field to enable Google App Id Utilities";

        private static GoogleSettings _instance;

        [MenuItem(MenuItemName)]
        public static void Configure() 
            => Selection.activeObject = Instance;

        // ReSharper disable InconsistentNaming
        public string GoogleAppIdAndroid = DefaultFieldValue;
        public string GoogleAppIdIOS = DefaultFieldValue;
        // ReSharper restore InconsistentNaming

        public static GoogleSettings Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                _instance = EditorGUIUtility.Load($"{FileName}.asset") as GoogleSettings;

                if (_instance != null)
                {
                    LogController.Log("Google Settings Loaded from Resources", LogLevel.Debug);
                    return _instance;
                }

                EditorConstants.ValidateEditorResourcesDirectories();
                _instance = CreateInstance<GoogleSettings>();
                AssetDatabase.CreateAsset(_instance, Path.Combine(EditorConstants.PathToEditorDefaultResources, $"{FileName}.asset"));
                return _instance;
            }
        }

        internal static void ValidateGoogleAppId(string googleAppId)
        {
            var match = Regex.Match(googleAppId, GoogleRegex);

            if (!match.Success)
               LogController.Log($"Input: `{googleAppId}` does not match Google App Id Regular Expression", LogLevel.Warning);
        }
    }
}
