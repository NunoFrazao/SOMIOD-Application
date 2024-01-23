using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Web.Http;
using System.Xml;
using somiod.Models;
using uPLibrary.Networking.M2Mqtt;

namespace somiod.Helpers {
    public static class BrokerHelper {
        private static readonly string Guid = System.Guid.NewGuid().ToString();

        public static void FireNotification(string endPoint, string topic, Notification notification) {
            var mClient = new MqttClient(endPoint);
            mClient.Connect(Guid);

            // Serialize the notification to an XmlDocument, then get the string representation
            XmlDocument xmlDoc = XmlHelper.Serialize(notification);
            string xmlPayload = xmlDoc.OuterXml;

            mClient.Publish(topic, Encoding.UTF8.GetBytes(xmlPayload));

            // Allow some time for the message to be sent before disconnecting
            
            Thread.Sleep(1000);
            if (mClient.IsConnected) {
                mClient.Disconnect();
            }
            
        }
    }
}