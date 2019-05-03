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
    [RoutePrefix("api/{controller}/{action}")]
    public class QueuingController : ApiController
    {
        readonly MSMQ mSMQ = new MSMQ();
        const string queueName = @".\Private$\UserQueue";
        
        [HttpGet]
        public People PendingMessages()
        {
            var messages = mSMQ.GetAllMessagesInQueue(queueName);

            return messages;
        }
        
        [HttpPost]
        public SignUpReply SignUp([FromBody]Person value)
        {
            SignUpReply supReply = new SignUpReply();
            
            if (value != null)
                supReply = mSMQ.SendMessageToQueue(queueName, value);

            return supReply;
        }
    }
}