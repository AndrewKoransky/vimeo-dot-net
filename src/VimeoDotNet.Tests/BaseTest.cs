﻿using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using VimeoDotNet.Authorization;
using VimeoDotNet.Tests.Settings;

namespace VimeoDotNet.Tests
{
    public class BaseTest
    {
        protected readonly VimeoApiTestSettings VimeoSettings;

        protected const string TestFilePath = @"VimeoDotNet.Tests.Resources.test.mp4";
        // http://download.wavetlan.com/SVV/Media/HTTP/http-mp4.htm

        protected const string TestTextTrackFilePath = @"VimeoDotNet.Tests.Resources.test.vtt";

        protected const string TextThumbnailFilePath = @"VimeoDotNet.Tests.Resources.test.png";

        protected BaseTest()
        {
            // Load the settings from a file that is not under version control for security
            // The settings loader will create this file in the bin/ folder if it doesn't exist
            VimeoSettings = SettingsLoader.LoadSettings();
        }
        
        protected async Task<VimeoClient> CreateUnauthenticatedClient()
        {
            var authorizationClient = new AuthorizationClient(VimeoSettings.ClientId, VimeoSettings.ClientSecret);
            var unauthenticatedToken = await authorizationClient.GetUnauthenticatedTokenAsync();
            return new VimeoClient(unauthenticatedToken.access_token);
        }

        protected IVimeoClient CreateAuthenticatedClient()
        {
            return new VimeoClient(VimeoSettings.AccessToken);
        }

        protected static Stream GetFileFromEmbeddedResources(string relativePath)
        {
            var assembly = typeof(VimeoClientAsyncTests).GetTypeInfo().Assembly;
            return assembly.GetManifestResourceStream(relativePath);
        }
    }
}