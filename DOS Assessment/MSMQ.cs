using DOS_Assessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Web;

namespace DOS_Assessment
{
    public class MSMQ
    {
        public string SendMessageToQueue(string queueName, Person p)

        {
            string successMsg = string.Empty;

            // check if queue exists, if not create it

            MessageQueue msMq = null;

            if (!MessageQueue.Exists(queueName))

            {

                msMq = MessageQueue.Create(queueName);

            }

            else

            {

                msMq = new MessageQueue(queueName);

            }

            try

            {

                // msMq.Send("Sending data to MSMQ at " + DateTime.Now.ToString());                
                    msMq.Send(p);                
            }

            catch (MessageQueueException ee)

            {

                successMsg = ee.ToString();

            }

            catch (Exception eee)

            {

                successMsg = eee.ToString();

            }

            finally

            {

                msMq.Close();                
            }

            if (successMsg == string.Empty)
                successMsg = "Message sent ......";

            return successMsg;

        }

        public Message[] GetAllMessagesInQueue(string queueName)
        {
            MessageQueue msMq = null;

            if (!MessageQueue.Exists(queueName))
            {
                return null;
            }
            
                msMq = new MessageQueue(queueName);

            var pendingMsgs = msMq.GetAllMessages();


            return pendingMsgs;

        }
    }
}