using System.Net;
using System.Text;
using System.Xml.Serialization;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using static DoorClient.Properties.Settings;
using RestSharp;
using Application = DoorClient.Models.Application;
using DoorClient.Models;

namespace DoorClient {
    public partial class FormDoor : Form
    {

        private static readonly string BrokerIp = Default.BrokerIp;
        private static readonly string ApiBaseUri = Default.ApiBaseUri;
        //private static readonly HttpStatusCode CustomApiError = (HttpStatusCode)Default.CustomApiError;
        private static readonly string ApplicationName = Default.AppName;
        private static readonly string ContainerName = Default.ContainerName;
        private static readonly string SubscriptionName = Default.SubName;
        private static readonly string EventType = Default.EventType;
        private static readonly string Endpoint = Default.EndPoint;
        private static readonly string[] Topic = { Default.Topic };

        private MqttClient mosqClient;
        private readonly RestClient restClient = new RestClient(ApiBaseUri);
        private bool lightState;

        public FormDoor()
        {
            InitializeComponent();
        }

        #region Helpers


        private void UpdateDoorState(string content)
        {
            // This method is safe to call from any thread.
            this.Invoke((MethodInvoker)delegate
            {
                txtDisplayState.Text = content;
            });
        }

        private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs args)
        {
            // Decode the message
            string message = Encoding.UTF8.GetString(args.Message);
            // Perform the UI update on the UI thread
            this.Invoke((MethodInvoker)delegate
            {
                using (TextReader reader = new StringReader(message))
                {
                    var notification = (Notification)new XmlSerializer(typeof(Notification)).Deserialize(reader);
                    if (notification.EventType != "CREATE") {
                        return;
                    }

                    // Use the actual content to update the state
                    UpdateDoorState(notification.Content);
                }
            });
        }
        #endregion

        #region Broker Methods

        private void ConnectToBroker()
        {
            mosqClient = new MqttClient(BrokerIp);
            mosqClient.Connect(Guid.NewGuid().ToString());

            if (!mosqClient.IsConnected)
            {
                MessageBox.Show("Could not connect to the message broker", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void SubscribeToTopics()
        {
            mosqClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE };
            mosqClient.Subscribe(Topic, qosLevels);
        }

        #endregion

        #region Requests

        private void CreateApplication(string app_name)
        {
            var app = new Application(app_name);

            var request = new RestRequest("api/somiod/", Method.Post);
            request.AddObject(app);

            var response = restClient.Execute(request);

            /*
            if (CheckEntityAlreadyExists(response))
                return;
            */

            if (response.StatusCode == 0)
            {
                MessageBox.Show("Could not connect to the API", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show("An error occurred while creating the Application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateContainer(string cont_name, string app_name)
        {
            var container = new Container(cont_name, app_name);

            var request = new RestRequest($"api/somiod/{app_name}/", Method.Post);
            request.AddObject(container);

            var response = restClient.Execute(request);

            /*
            if (CheckEntityAlreadyExists(response))
                return;
            */

            if (response.StatusCode == 0)
            {
                MessageBox.Show("Could not connect to the API", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show("An error occurred while creating the Container", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateSubscription(string sub_name, string cont_name, string app_name, string eventType, string endpoint)
        {
            var subscription = new Subscription(sub_name, cont_name, eventType, endpoint);

            var request = new RestRequest($"api/somiod/{app_name}/{cont_name}/sub", Method.Post);
            request.AddObject(subscription);

            var response = restClient.Execute(request);

            /*
            if (CheckEntityAlreadyExists(response))
                return;
            */

            if (response.StatusCode == 0)
            {
                MessageBox.Show("Could not connect to the API", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show("An error occurred while creating the Subscription", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion


        private void FormLight_Shown(object sender, EventArgs e)
        {
            ConnectToBroker();
            SubscribeToTopics();
            CreateApplication(ApplicationName);
            CreateContainer(ContainerName, ApplicationName);
            CreateSubscription(SubscriptionName, ContainerName, ApplicationName, EventType, Endpoint);
        }

        private void FormLight_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mosqClient.IsConnected)
            {
                mosqClient.Unsubscribe(Topic);
                mosqClient.Disconnect();
            }
        }

        private void FormDoor_Load(object sender, EventArgs e)
        {

        }
    }
}
