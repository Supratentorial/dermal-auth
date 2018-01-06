using dermal.auth.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace dermal.auth.Services
{
    public class AuthMessager : IEmailSender, ISmsSender
    {
        IConfiguration _config;
        static readonly string

        public AuthMessager(IConfiguration config)
        {
            this._config = config;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            
        }

        public async Task SendSmsAsync(string number, string message)
        {
            var accountSid = _config["TwilioSid"];
            var authToken = _config["TwilioAuthToken"];
            var myNumber = _config["TwilioNumber"];

            TwilioClient.Init(accountSid, authToken);
            var messageToSend = await MessageResource.CreateAsync(to:
                new PhoneNumber(number), from: new PhoneNumber(myNumber), body: message);
        }
    }
}
