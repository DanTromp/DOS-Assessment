using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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

        //Tried to implement Unit Tests but not familiar with using them
        //private List<Person> testProducts;
        //public QueuingController(List<Person> testProducts)
        //{
        //    this.testProducts = testProducts;
        //}
        
        [HttpGet]
        public async Task<People> PendingMessages()
        {
            var messages = mSMQ.GetAllMessagesInQueue(queueName);

            return await Task.FromResult(messages);         
        }
        
        [HttpPost]
        public async Task<SignUpReply> SignUp([FromBody]Person value)
        {
            SignUpReply supReply = new SignUpReply();
            
            if (value != null)
                supReply = mSMQ.SendMessageToQueue(queueName, value);

            return await Task.FromResult(supReply);
        }
    }
}