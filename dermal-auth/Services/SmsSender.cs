using dermal.auth.Interfaces;
using dermal.auth.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace dermal.auth.Services
{
    public class SmsSender : ISmsSender
    {

        private readonly TwilioOptions _twilioOptions;

        public SmsSender(IOptions<TwilioOptions> options)
        {
            _twilioOptions = options.Value;
        }

        public async Task SendSmsAsync(string number, string message)
        {
            var accountSid = _twilioOptions.TwilioSid;
            var authToken = _twilioOptions.TwilioAuthToken;
            var myNumber = _twilioOptions.TwilioNumber;

            TwilioClient.Init(accountSid, authToken);
            var messageToSend = await MessageResource.CreateAsync(to:
                new PhoneNumber(number), from: new PhoneNumber(myNumber), body: message);
        }
    }
}
