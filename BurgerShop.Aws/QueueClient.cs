﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using Amazon.SQS.Util;
using BurgerShop.Core;
using System.Threading;
using BurgerShop.Messaging.Spec;
using Message = BurgerShop.Messaging.Message;

namespace BurgerShop.Aws
{
    public class QueueClient : IQueueClient
    {
        private AmazonSQSClient _sqsClient;
        public string QueueName { get; private set; }
        internal string QueueUrl { get; private set; }
        internal string QueueArn { get; private set; }
        private Action<Message> _receiveAction;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private bool _isListening;

        private static string AccessKey
        {
            get { return Config.GetSetting("AWSAccessKey"); }
        }

        private static string SecretKey
        {
            get { return Config.GetSetting("AWSSecretKey"); }
        }

        public QueueClient(string queueName)
        {
            _sqsClient = new AmazonSQSClient(AccessKey, SecretKey, RegionEndpoint.EUWest1);
            QueueName = queueName;
            Ensure();
        }

        public void Ensure()
        {
            if (!Exists())
            {
                var request = new CreateQueueRequest();
                request.QueueName = QueueName;
                var response = _sqsClient.CreateQueue(request);
                QueueUrl = response.QueueUrl;
                PopulateArn();
            }
        }

        public bool Exists()
        {
            var exists = false;
            var queues = _sqsClient.ListQueues();
            var matchString = string.Format("/{0}", QueueName);
            var matches = queues.QueueUrls.Where(x => x.EndsWith(QueueName));
            if (matches.Count() == 1)
            {
                exists = true;
                QueueUrl = matches.ElementAt(0);
                PopulateArn();
            }
           return exists;
        }

        private void PopulateArn()
        {
            var attributes = _sqsClient.GetQueueAttributes(new GetQueueAttributesRequest
            {
                AttributeNames = new List<string>(new string[] {SQSConstants.ATTRIBUTE_QUEUE_ARN}),
                QueueUrl = QueueUrl
            });
            QueueArn = attributes.QueueARN;
        }

        public void DeleteQueue()
        {
            var request = new DeleteQueueRequest();
            request.QueueUrl = QueueUrl;
            _sqsClient.DeleteQueue(request);
        }

        public void Unsubscribe()
        {
            _cancellationTokenSource.Cancel();
            _isListening = false;
        }

        private async void Subscribe()
        {
            if (_isListening)
            {
                var request = new ReceiveMessageRequest();
                request.MaxNumberOfMessages = 10;
                request.QueueUrl = QueueUrl;
                var result = await _sqsClient.ReceiveMessageAsync(request, _cancellationTokenSource.Token);
                if (result.Messages.Count > 0)
                {
                    foreach (var message in result.Messages)
                    {
                        if (_receiveAction != null && message != null)
                        {
                            _receiveAction(AwsMessage.Unwrap(message.Body));
                            DeleteMessage(message.ReceiptHandle);
                        }
                    }
                }
            }
            if (_isListening)
            {
                Subscribe();
            }
        }

        private DeleteMessageResponse DeleteMessage(string receiptHandle)
        {
            var request = new DeleteMessageRequest();
            request.QueueUrl = QueueUrl;
            request.ReceiptHandle = receiptHandle;
            return  _sqsClient.DeleteMessage(request);
        }

        
        public void Subscribe(Action<Message> receiveAction)
        {
            _receiveAction = receiveAction;
            _isListening = true;
            Subscribe();
        }

        public void Send(Message message)
        {
            var request = new SendMessageRequest();
            request.QueueUrl = QueueUrl;
            request.MessageBody = AwsMessage.Wrap(message);
            _sqsClient.SendMessage(request);
        }

        internal void AllowSnsToSendMessages(TopicClient topicClient)
        {
            var policyFormat = @"{
  ""Statement"": [
    {
      ""Sid"": ""MySQSPolicy001"",
      ""Effect"": ""Allow"",
      ""Principal"": {
        ""AWS"": ""*""
      },
      ""Action"": ""sqs:SendMessage"",
      ""Resource"": ""%QueueArn%"",
      ""Condition"": {
        ""ArnEquals"": {
          ""aws:SourceArn"": ""%TopicArn%""
        }
      }
    }
  ]
}";
            var policy = policyFormat.Replace("%QueueArn%", QueueArn).Replace("%TopicArn%", topicClient.TopicArn);
            var request = new SetQueueAttributesRequest();
            request.Attributes.Add("Policy", policy);
            request.QueueUrl = QueueUrl;
            var response = _sqsClient.SetQueueAttributes(request);
        }

        public bool HasMessages()
        {
            var request = new GetQueueAttributesRequest 
            {
                 QueueUrl = QueueUrl,
                 AttributeNames = new List<string>(new string[] { SQSConstants.ATTRIBUTE_APPROXIMATE_NUMBER_OF_MESSAGES})
            };
            var response = _sqsClient.GetQueueAttributes(request);
            return response.ApproximateNumberOfMessages > 0;
        }

        public bool IsListening()
        {
            return _isListening;
        }
    }
}
