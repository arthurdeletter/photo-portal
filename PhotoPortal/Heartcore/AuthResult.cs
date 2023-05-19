using System;
using System.Text.Json.Serialization;

namespace PhotoPortal.Heartcore
{
	public class AuthResult
	{
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
    }
}

