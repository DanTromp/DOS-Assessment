using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DOS_Assessment.Models;
using Newtonsoft.Json;

namespace DOS_Assessment.Controllers
{
    public class QueuingController : ApiController
    {
        MSMQ mSMQ = new MSMQ();
        const string queueName = @".\Private$\UserQueue";

        // GET api/<controller>
        public Message[] Get()
        {

            Message[] messages = mSMQ.GetAllMessagesInQueue(queueName);

            return messages;
        }
        
        public string Post([FromBody]Person value)
        {
            string successfulMsg = string.Empty;
            
            if (value != null)
                successfulMsg = mSMQ.SendMessageToQueue(queueName, value);

            return successfulMsg;

        }
    }
}