using static DoorSwitchClient.Properties.Settings;
using RestSharp;
using System.Net;
using System.Reflection;
using DoorSwitchClient.Models;
using Microsoft.VisualBasic.ApplicationServices;

namespace DoorSwitchClient {
    public partial class FormDoorSwitch : Form
    {

        private static readonly string ApiBaseUri = Default.ApiBaseUri;
        private static readonly string AppName = Default.AppName;
        private static readonly string ContainerName = Default.ContainerName;
        private static readonly string ContainerData = Default.ContainerData;
        private static readonly string DataName = Default.DataName;

        private readonly RestClient restClient = new RestClient(ApiBaseUri);

        public FormDoorSwitch()
        {
            InitializeComponent();
        }

        #region Requests

        private void CreateContainer(string container_name, string application_name)
        {
            var container = new Container(container_name, application_name);

            var request = new RestRequest($"api/somiod/{application_name}", Method.Post);
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

        private void CreateData(string app_name, string container_data, string data_name, string content)
        {
            var container = new Data(data_name, content);

            var request = new RestRequest($"api/somiod/{app_name}/{container_data}/data", Method.Post);
            request.AddObject(container);

            var response = restClient.Execute(request);

            if (response.StatusCode == 0)
            {
                MessageBox.Show("Could not connect to the API", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show("An error occurred while creating data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void FormLightSwitch_Shown(object sender, EventArgs e)
        {
            CreateContainer(ContainerName, AppName);
        }

        private void btnOn_Click(object sender, EventArgs e)
        {
            CreateData(AppName, ContainerData, DataName, "OPEN");
        }

        private void btnOff_Click(object sender, EventArgs e)
        {
            CreateData(AppName, ContainerData, DataName, "CLOSE");
        }

        private void FormDoorSwitch_Load(object sender, EventArgs e)
        {

        }
    }
}
