using DOS_Assessment.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Web;

namespace DOS_Assessment
{
    public class MSMQ
    {
        public SignUpReply SendMessageToQueue(string queueName, Person p)
        {
            SignUpReply sup = new SignUpReply { errMsg = string.Empty, success = false };

            MessageQueue msMq;

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
                //Create a new message with JSON object body
                Message msg = new Message(p);                           

                msMq.Send(msg);
            }
            catch (MessageQueueException ex)
            {
                sup.errMsg = ex.ToString();
            }
            catch (Exception exc)
            {
                sup.errMsg = exc.ToString();
            }
            //Finally block to handle the queue
            finally
            {
                msMq.Close();
            }

            sup.errMsg = "";
            sup.success = true;

            //Return success bool && Exception message
            return sup;
        }

        public People GetAllMessagesInQueue(string queueName)
        {
            People ppl = new People();
            MessageQueue msMq = new MessageQueue(queueName);

            //Formatter to allow serializing and deserializing of the message body
            msMq.Formatter = new XmlMessageFormatter(new Type[] { typeof(Person) });

            if (!MessageQueue.Exists(queueName))
                return null;

            var queueIter = msMq.GetMessageEnumerator2();
            var timeout = TimeSpan.FromSeconds(3);

            ppl.peoples = new List<Person>();

            //Move through all the messages in the queue
            while (queueIter.MoveNext(timeout))
            {
                using (var message = msMq.ReceiveById(queueIter.Current.Id, timeout))
                {
                    Person pp = (Person)message.Body;

                    //Create a Person object per message
                    ppl.peoples.Add(new Person
                    {
                        FirstName = pp.FirstName,
                        LastName = pp.LastName
                    });
                }
            }

            return ppl;

        }
    }
}