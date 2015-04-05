﻿using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Newtonsoft.Json;
using BurgerShop.Messaging.Spec;
using System;
using System.Linq;
using msg = BurgerShop.Messaging;
using BurgerShop.Messaging;
using Amazon.SQS.Util;
using BurgerShop.Core;

namespace BurgerShop.Aws
{
    public class TopicClient : ITopicClient
    {
        private AmazonSimpleNotificationServiceClient _snsClient;
        public string TopicName { get; private set; }
        public string SubscriptionName { get; private set; }
        internal string TopicArn { get; private set; }
        private string _subscriptionArn;
        private QueueClient _queueClient;

        private static string AccessKey
        {
            get { return Config.GetSetting("AWSAccessKey"); }
        }

        private static string SecretKey
        {
            get { return Config.GetSetting("AWSSecretKey"); }
        }

        public TopicClient(string topicName) : this(topicName, null) { }

        public TopicClient(string topicName, string subscriptionName)
        {
            _snsClient = new AmazonSimpleNotificationServiceClient(AccessKey, SecretKey, RegionEndpoint.EUWest1);
            TopicName = topicName;
            SubscriptionName = subscriptionName;
            Ensure();
        }

        private void Ensure()
        {
            if (!TopicExists())
            {
                var request = new CreateTopicRequest();
                request.Name = TopicName;
                var response = _snsClient.CreateTopic(request);
                TopicArn = response.TopicArn;
            }
            if (!string.IsNullOrEmpty(SubscriptionName))
            {
                _queueClient = new QueueClient(SubscriptionName);
                if (!SubscriptionExists())
                {
                    var response = _snsClient.Subscribe(new SubscribeRequest
                    {
                        TopicArn = TopicArn,
                        Protocol = "sqs",
                        Endpoint = _queueClient.QueueArn
                    });
                    _subscriptionArn = response.SubscriptionArn;
                    var attrRequest = new SetSubscriptionAttributesRequest
                    {
                        AttributeName = "RawMessageDelivery",
                        AttributeValue = "true",
                        SubscriptionArn = _subscriptionArn
                    };
                    _snsClient.SetSubscriptionAttributes(attrRequest);
                    _queueClient.AllowSnsToSendMessages(this);
                }
            }
        }

        private bool TopicExists()
        {
            var exists = false;
            var response = _snsClient.ListTopics(); 
            var matchString = string.Format(":{0}", TopicName);
            var matches = response.Topics.Where(x => x.TopicArn.EndsWith(matchString));
            if (matches.Count() == 1)
            {
                exists = true;
                TopicArn = matches.ElementAt(0).TopicArn;
            }
           return exists;
        }

        private bool SubscriptionExists()
        {
            var exists = false;
            var request = new ListSubscriptionsByTopicRequest { 
                TopicArn = TopicArn 
            };
            var response = _snsClient.ListSubscriptionsByTopic(request);
            var matchString = string.Format(":{0}", SubscriptionName);
            var matches = response.Subscriptions.Where(x => x.Endpoint.EndsWith(matchString));
            if (matches.Count() == 1)
            {
                exists = true;
                _subscriptionArn = matches.ElementAt(0).SubscriptionArn;
            }
            return exists;
        }

        public void Subscribe(Action<msg.Message> receiveAction)
        {
            _queueClient.Subscribe(receiveAction);
        }

        public void Unsubscribe()
        {
            _queueClient.Unsubscribe();
        }

        public bool IsListening()
        {
            return _queueClient.IsListening();
        }

        public bool HasMessages()
        {
            return _queueClient.HasMessages();
        }

        public void Publish(Message message)
        {
            var request = new PublishRequest();
            request.TopicArn = TopicArn;
            request.Message = AwsMessage.Wrap(message);
            var response = _snsClient.Publish(request);
        }

        public void DeleteSubscription()
        {
            _queueClient.DeleteQueue();
        }
    }
}
