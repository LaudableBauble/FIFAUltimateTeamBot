using System;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using UltimateTeam.Toolkit.Constant;
using UltimateTeam.Toolkit.Extension;
using UltimateTeam.Toolkit.Model;
using UltimateTeam.Toolkit.Service;
using HttpMethod = System.Net.Http.HttpMethod;

namespace UltimateTeam.Toolkit.Request
{
    public class LoginRequest : RequestBase
    {
        private IHasher _hasher;

        public IHasher Hasher
        {
            get { return _hasher ?? new Hasher(); }
            set { _hasher = value; }
        }

        public async Task LoginAsync(string username, string password, string securityAnswer)
        {
            if (string.IsNullOrEmpty(username)) throw new ArgumentException("username");
            if (string.IsNullOrEmpty(password)) throw new ArgumentException("password");
            if (string.IsNullOrEmpty(securityAnswer)) throw new ArgumentException("securityAnswer");

            var loginResponse = await LoginRequestAsync(username, password);
            //var shards = await ShardsRequestAsync();
            // TODO: loop through shards until we get a user
            var persona = await AccountInfoRequestAsync();
            var authResponse = await AuthenticationRequestAsync(loginResponse, persona);
            /*var validateResponse = */
            await ValidateRequestAsync(authResponse, securityAnswer);

            //The login is complete, save the persona for future use.
            _Persona = persona;
        }

        private async Task<ValidateResponse> ValidateRequestAsync(AuthenticationResponse authResponse, string securityAnswer)
        {
            var questionUrl = new Uri("http://www.ea.com/p/fut/a/card-ps3/l/en_GB/s/p/ut/game/fifa13/phishing/validate");
            questionUrl = new Uri("http://www.ea.com/p/fut/a/card-360/l/en/s/p/ut/game/fifa13/phishing/validate");

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, questionUrl);
            requestMessage.Headers.TryAddWithoutValidation("X-Ut-Sid", authResponse.SessionId);
            requestMessage.Headers.TryAddWithoutValidation("X-Ut-Embed-Error", "true");
            requestMessage.Headers.ExpectContinue = false;
            requestMessage.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "answer", securityAnswer/*Hasher.Hash(securityAnswer)*/ }
            });

            var response = await Client.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();

            return await Deserialize<ValidateResponse>(response);
        }

        private async Task<AuthenticationResponse> AuthenticationRequestAsync(LoginResponse loginResponse, Persona persona)
        {
            var authUrl = new Uri("http://www.ea.com/p/fut/a/card-ps3/l/en_GB/s/p/ut/auth");
            authUrl = new Uri("http://www.ea.com/p/fut/a/card-360/l/en/s/p/ut/auth");
            var authJson = string.Format(@"{{ ""isReadOnly"": false, ""sku"": ""393A0001"", ""clientVersion"": 3, ""nuc"": {0}, ""nucleusPersonaId"": {1}, ""nucleusPersonaDisplayName"": ""{2}"", ""nucleusPersonaPlatform"": ""{3}"", ""locale"": ""en-GB"", ""method"": ""idm"", ""priorityLevel"":4, ""identification"": {{ ""EASW-Token"": """" }} }}",
                    loginResponse.Player.NucleusId,
                    persona.PersonaId,
                    persona.PersonaName,
                    persona.UserClubList.OrderByDescending(club => club.LastAccessTime).First().Platform
                    );
            Client.DefaultRequestHeaders.ExpectContinue = false;
            var response = await Client.PostAsync(authUrl, new StringContent(authJson, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            var authResponse = await Deserialize<AuthenticationResponse>(response);
            SessionId = authResponse.SessionId;

            return authResponse;
        }

        private async Task<Persona> AccountInfoRequestAsync()
        {
            var accountUrl = new Uri(string.Format("http://www.ea.com/p/fut/a/card-pc/l/en_GB/s/p/ut/game/fifa13/user/accountinfo?timestamp={0}",
                DateTime.UtcNow.ToUnixTimestamp()));
            accountUrl = new Uri(string.Format("http://www.ea.com/p/fut/a/card-360/l/intl/s/p/ut/game/fifa13/user/accountinfo?timestamp={0}",
                DateTime.UtcNow.ToUnixTimestamp()));

            var response = await Client.GetAsync(accountUrl);
            response.EnsureSuccessStatusCode();

            var accounts = await Deserialize<UserAccounts>(response);
            var persona = accounts.UserAccountInfo.Personas.First();

            return persona;
        }

        //private async Task<Shards> ShardsRequestAsync()
        //{
        //    var shardsUrl = new Uri(string.Format("http://www.ea.com/p/fut/a/card/l/en_GB/s/p/ut/shards?timestamp={0}",
        //                                          DateTime.UtcNow.ToUnixTimestamp()));
        //    var response = await Client.GetAsync(shardsUrl);
        //    response.EnsureSuccessStatusCode();

        //    var shards = JsonDeserializer.Deserialize<Shards>(await response.Content.ReadAsStreamAsync());

        //    return shards;
        //}

        private async Task<LoginResponse> LoginRequestAsync(string username, string password)
        {
            var loginUrl = new Uri(Resources.Login);
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "email", username },
                { "password", password },
                { "stay-signed", "ON"}
            });

            var response = await Client.PostAsync(loginUrl, content);
            response.EnsureSuccessStatusCode();

            var xml = XDocument.Parse(await response.Content.ReadAsStringAsync());

            var login = (from x in xml.Descendants("login")
                         let player = x.Element("player")
                         let preferredPersona = player.Element("preferredPersona")
                         select new LoginResponse
                         {
                             Success = (int)x.Element("success"),
                             Player = new Player
                             {
                                 Id = (int)player.Element("id"),
                                 NucleusId = (long)player.Element("nucleusId"),
                                 Email = (string)player.Element("email"),
                                 PreferredPersona = new PreferredPersona
                                 {
                                     Id = (long)preferredPersona.Element("id"),
                                     Gamertag = (string)preferredPersona.Element("gamertag"),
                                     Platform = (string)preferredPersona.Element("platform")
                                 }
                             }
                         })
                         .First();

            return login;
        }
    }
}