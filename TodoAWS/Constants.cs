﻿namespace TodoAWSSimpleDB
{
	public static class Constants
	{
		public static string AppName = "shannensApp";

		// AWS
		// Sign up for an AWS account at https://aws.amazon.com/
		// Configure at https://console.aws.amazon.com/cognito/
		public static string CognitoIdentityPoolId = "";

		// OAuth
		// For Google login, configure at https://console.developers.google.com/
		public static string iOSClientId = "<insert IOS client ID here>";
		public static string AndroidClientId = "";

		// These values do not need changing
		public static string Scope = "https://www.googleapis.com/auth/userinfo.email";
		public static string AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
		public static string AccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
		public static string UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

		// Set these to reversed iOS/Android client ids, with :/oauth2redirect appended
		public static string iOSRedirectUrl = "<insert IOS redirect URL here>:/oauth2redirect";
		public static string AndroidRedirectUrl = ":/oauth2redirect";
	}
}
