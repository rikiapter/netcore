

using Malam.Mastpen.API.Clients;

namespace Malam.Mastpen.API.IntegrationTests.Mock
{
    public static class ClientSettingsMocker
    {
        // todo: Get identity server url from config file
        // todo: Get token request from config file

        public static MastpenIdentityClientSettings GetMastpenIdentityClientSettings(string userName, string password)
            => new MastpenIdentityClientSettings
       {
                 Url = "http://localhost:5000",
                 ClientId = "MastpenWebAPIAPP",
                 ClientSecret = "MastpenWebAPISecret",
                 UserName = userName,
                 Password = password
             };



    }
}