using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using somiod.Helpers;
using somiod.Models;

namespace somiod.Controllers {
    public class MainController : ApiController {

        private readonly List<string> _validEventTypes = new List<string>() { "CREATE", "DELETE", "BOTH" };

        string connectionDB = somiod.Properties.Settings.Default.ConnStr;

        #region Apps

        //Todos as Applications:
        // GET: /api/somiod/applications/
        [HttpGet]
        [Route("api/somiod")]
        public IEnumerable<Application> GetAllApps() {
            List<Application> apps = new List<Application>();
            SqlConnection conn = null;

            try {
                conn = new SqlConnection(connectionDB);
                conn.Open();

                string sql = "SELECT * FROM Applications";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    Application app = new Application {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Creation_dt = reader.GetDateTime(2).ToString(),
                    };
                    apps.Add(app);
                }
                reader.Close();
                conn.Close();

            }
            catch (Exception ex) {

                if (conn.State == System.Data.ConnectionState.Open) {
                    conn.Close();
                }
            }
            return apps;

        }

        //Criar novaApplication
        // POST: api/somiod/applications
        [HttpPost]
        [Route("api/somiod")]
        public IHttpActionResult InsertApplication([FromBody] Application application) {

            SqlConnection conn = null;
            try {
                conn = new SqlConnection(connectionDB);
                conn.Open();

                /*
                if (GetByName(application.Name)) {
                    return BadRequest("Application already exists");
                }
                */

                string sql = "INSERT INTO Applications (name) VALUES (@name)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", application.Name);

                int result = cmd.ExecuteNonQuery();
                conn.Close();

                if (result == 1) {
                    return Ok(result);
                }
                else {
                    return BadRequest();
                }

            }
            catch (Exception ex) {

                if (conn.State == System.Data.ConnectionState.Open) {
                    conn.Close();
                }
                return NotFound();
            }

        }

        //Applications por nome
        // GET: /api/somiod/applications/app1
        [HttpGet]
        [Route("api/somiod/{app_name}")]
        public IHttpActionResult GetByName(string app_name) {
            SqlConnection conn = null;
            Application app = null;
            try {
                conn = new SqlConnection(connectionDB);
                conn.Open();

                string sql = "SELECT * FROM Applications WHERE name = @nameValue";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nameValue", app_name);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) {
                    app = new Application {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                        Creation_dt = reader.GetDateTime(2).ToString(),
                    };
                }
                reader.Close();
                conn.Close();

                if (app != null) {
                    return Ok(app);
                }
                else {
                    return BadRequest();
                }

            }
            catch (Exception ex) {

                if (conn.State == System.Data.ConnectionState.Open) {
                    conn.Close();
                }
                return BadRequest();
            }
        }

        //Alterar Application
        // PUT: api/somiod/applications/app1
        [HttpPut]
        [Route("api/somiod/{app_name}")]
        public IHttpActionResult PutApplication(string app_name, [FromBody] Application application) {
            /*
            if (GetByName(application.Name)) {
                return BadRequest("Application already exists");
            }
            */
            string queryString = "UPDATE Applications SET Name=@name WHERE Name=@oldName";
            using (SqlConnection conn = new SqlConnection(connectionDB)) {
                SqlCommand cmd = new SqlCommand(queryString, conn);
                cmd.Parameters.AddWithValue("@name", application.Name);
                cmd.Parameters.AddWithValue("@oldName", app_name);

                try {

                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    //conn.Close():  -> nao é preciso porque estamos a meter o codigo dentro do USING
                    if (result > 0) {
                        return Ok();
                    }
                    else {
                        return BadRequest();
                    }

                }
                catch (Exception) {
                    return InternalServerError();
                }

            }
        }

        //Eliminar Application
        // DELETE: api/somiod/applications/app1
        [HttpDelete]
        [Route("api/somiod/{app_name}")]
        public IHttpActionResult DeleteApplication(string app_name) {
            //if (GetByName(app_name)) {

            string queryString = "DELETE FROM Applications WHERE name=@name";
            using (SqlConnection conn = new SqlConnection(connectionDB)) {
                SqlCommand cmd = new SqlCommand(queryString, conn);
                cmd.Parameters.AddWithValue("@name", app_name);

                try {

                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    //conn.Close():  -> nao é preciso porque estamos a meter o codigo dentro do USING
                    if (result > 0) {
                        return Ok();
                    }
                    else {
                        return BadRequest();
                    }

                }
                catch (Exception) {
                    return InternalServerError();
                }

            }

            /*
            } else {
                return BadRequest("Application doesn't exists");
            }
            */

        }

        #endregion

        #region Containers
        //Todos as Applications:
        // GET: /api/somiod/applications/
        /**
        [HttpGet]
        [Route("api/somiod/containers")]
        public IEnumerable<Container> GetAllContainers() {
            List<Container> containers = new List<Container>();
            SqlConnection conn = null;

            try {
                conn = new SqlConnection(connectionDB);
                conn.Open();

                string sql = "SELECT * FROM Containers";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    //ver primeiro o nome do parent
                    Container container = new Container {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                        Creation_dt = reader.GetDateTime(2).ToString(),
                    };
                    containers.Add(container);
                }
                reader.Close();
                conn.Close();

            }
            catch (Exception ex) {

                if (conn.State == System.Data.ConnectionState.Open) {
                    conn.Close();
                }
            }
            return containers;

        }
        **/


        //[HttpGet]
        //[Route("api/somiod/{app_name}/containers")]
        /**
        public IEnumerable<Container> GetAllAppContainers(string app_name) {
            List<Container> containers = new List<Container>();
            SqlConnection conn = null;

            try {
                conn = new SqlConnection(connectionDB);
                conn.Open();

                string sqlIdParent = "SELECT id FROM Applications WHERE name = @appName";
                SqlCommand cmd2 = new SqlCommand(sqlIdParent, conn);
                cmd2.Parameters.AddWithValue("@appName", app_name);
                int resultIdParent = 0;

                using (SqlDataReader reader = cmd2.ExecuteReader()) {
                    if (reader.Read()) {
                        resultIdParent = (int)reader["id"];
                    }
                }

                string sql = "SELECT * FROM Containers WHERE parent = @ParentID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ParentID", resultIdParent);

                SqlDataReader reader2 = cmd.ExecuteReader();
                while (reader2.Read()) {
                    //ver primeiro o nome do parent
                    Container container = new Container {
                        Id = (int)reader2["Id"],
                        Name = (string)reader2["Name"],
                        Creation_dt = reader2.GetDateTime(2).ToString(),
                    };
                    containers.Add(container);
                }
                reader2.Close();
                conn.Close();

            }
            catch (Exception ex) {

                if (conn.State == System.Data.ConnectionState.Open) {
                    conn.Close();
                }
            }
            return containers;

        }
        **/

        [HttpPost]
        [Route("api/somiod/{app_name}")]
        public IHttpActionResult InsertContainer(string app_name, [FromBody] Container container) {

            SqlConnection conn = null;
            try {
                conn = new SqlConnection(connectionDB);
                conn.Open();

                //verifica se o container já existe
                /*
                if (GetByName(app_name, container.Name)) {
                    return BadRequest("Container already exists");
                }
                */

                //vai buscar o ID do parent a ser passado no URL
                string sqlIdParent = "SELECT id FROM Applications WHERE name = @appName";
                SqlCommand cmd2 = new SqlCommand(sqlIdParent, conn);
                cmd2.Parameters.AddWithValue("@appName", app_name);
                int resultIdParent = 0;

                using (SqlDataReader reader = cmd2.ExecuteReader()) {
                    if (reader.Read()) {
                        resultIdParent = (int)reader["id"];
                    }
                }
                // FIM

                string sql = "INSERT INTO Containers (name, parent) VALUES (@name, @parent)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", container.Name);
                cmd.Parameters.AddWithValue("@parent", resultIdParent);

                int result = cmd.ExecuteNonQuery();
                conn.Close();

                if (result == 1) {
                    return Ok(result);
                }
                else {
                    return BadRequest();
                }

            }
            catch (Exception ex) {

                if (conn.State == System.Data.ConnectionState.Open) {
                    conn.Close();
                }
                return NotFound();
            }

        }

        [HttpGet]
        [Route("api/somiod/{app_name}/{container_name}")]
        public IHttpActionResult GetByName(string app_name, string container_name) {
            using (SqlConnection conn = new SqlConnection(connectionDB)) {
                try {
                    conn.Open();

                    // Adjusted SQL to fetch data from the Data table that are children of the specified container
                    string sql = @"
                    SELECT d.* 
                    FROM Data d
                    INNER JOIN Containers c ON d.Parent = c.Id
                    INNER JOIN Applications a ON c.Parent = a.Id
                    WHERE a.Name = @appName AND c.Name = @containerName";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@appName", app_name);
                    cmd.Parameters.AddWithValue("@containerName", container_name);

                    List<Data> dataList = new List<Data>();
                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            Data data = new Data {
                                Id = (int)reader["Id"],
                                Content = (string)reader["Content"],
                                // Adjust below line if Creation_dt is nullable in your database
                                Creation_dt = reader["Creation_dt"] == DBNull.Value ? null : reader["Creation_dt"].ToString(),
                                Parent = (int)reader["Parent"]
                            };
                            dataList.Add(data);
                        }
                    }

                    if (dataList.Count > 0) {
                        return Ok(dataList);
                    }
                    else {
                        return NotFound();
                    }
                }
                catch (Exception ex) {
                    // Consider logging the exception for debugging purposes
                    return InternalServerError(ex);
                }
            }
        }

        [HttpPut]
        [Route("api/somiod/{app_name}/{container_name}")]
        public IHttpActionResult PutContainer(string app_name, string container_name, [FromBody] Container container) {
            /*
            if (GetByName(app_name,container.Name)) {
                return BadRequest("Container already exists");
            }
            */
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            using (SqlConnection conn = new SqlConnection(connectionDB)) {
                conn.Open();

                //vai buscar o ID do parent
                string sqlIdParent = "SELECT id FROM Applications WHERE name = @appName";
                SqlCommand cmd2 = new SqlCommand(sqlIdParent, conn);
                cmd2.Parameters.AddWithValue("@appName", app_name);
                int resultIdParent = 0;

                using (SqlDataReader reader = cmd2.ExecuteReader()) {
                    if (reader.Read()) {
                        resultIdParent = (int)reader["id"];
                    }
                }
                //fim

                string queryString = "UPDATE Containers SET Name=@name, Parent=@parent WHERE Name=@oldName";
                SqlCommand cmd = new SqlCommand(queryString, conn);
                cmd.Parameters.AddWithValue("@name", container.Name);
                cmd.Parameters.AddWithValue("@oldName", container_name);
                if (container.Parent != 0) {
                    cmd.Parameters.AddWithValue("@parent", container.Parent);
                }
                else {
                    cmd.Parameters.AddWithValue("@parent", resultIdParent);
                }


                try {
                    int result = cmd.ExecuteNonQuery();
                    //conn.Close():  -> nao é preciso porque estamos a meter o codigo dentro do USING
                    if (result > 0) {
                        return Ok();
                    }
                    else {
                        return BadRequest();
                    }

                }
                catch (Exception) {
                    return InternalServerError();
                }

            }
        }

        [HttpDelete]
        [Route("api/somiod/{app_name}/{container_name}")]
        public IHttpActionResult DeleteContainer(string app_name, string container_name) {
            //if (GetByName(app_name, container_name)) {

            string queryString = "DELETE FROM Containers WHERE name=@name";
            using (SqlConnection conn = new SqlConnection(connectionDB)) {
                SqlCommand cmd = new SqlCommand(queryString, conn);
                cmd.Parameters.AddWithValue("@name", container_name);

                try {

                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    //conn.Close():  -> nao é preciso porque estamos a meter o codigo dentro do USING
                    if (result > 0) {
                        return Ok();
                    }
                    else {
                        return BadRequest();
                    }

                }
                catch (Exception) {
                    return InternalServerError();
                }

            }
            /*
        } else {
            return BadRequest("Container doesn't exists");
        }
            */

        }
        #endregion

        #region Data

        //[HttpGet]
        //[Route("api/somiod/data")]
        /**
        public IEnumerable<Data> GetAllData() {
            List<Data> dataList = new List<Data>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(connectionDB);
                conn.Open();

                string sql = "SELECT * FROM Data";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    Data data = new Data {
                        Id = (int)reader["Id"],
                        Content = (string)reader["Content"],
                        Creation_dt = reader.GetDateTime(2).ToString(),
                        Parent = reader["Parent"] == DBNull.Value ? 0 : (int)reader["Parent"]
                    };
                    dataList.Add(data);
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception) {

                throw;
            }
            return dataList;
        }
        **/

        // Get data by name
        [HttpGet]
        [Route("api/somiod/{appName}/{containerName}/data/{data_name}")]
        public IHttpActionResult GetDataByName(string appName, string containerName, string data_name) {
            using (SqlConnection conn = new SqlConnection(connectionDB)) {
                try {
                    conn.Open();
                    string sql = @"
                    SELECT d.Id, d.Name, d.Content, d.Creation_dt, d.Parent 
                    FROM Data d
                    INNER JOIN Containers c ON d.Parent = c.Id
                    INNER JOIN Applications a ON c.Parent = a.Id
                    WHERE a.Name = @appName AND c.Name = @containerName AND d.Name = @data_name";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@appName", appName);
                    cmd.Parameters.AddWithValue("@containerName", containerName);
                    cmd.Parameters.AddWithValue("@data_name", data_name);

                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        if (reader.Read()) {
                            Data data = new Data {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                Content = (string)reader["Content"],
                                Creation_dt = reader["Creation_dt"] == DBNull.Value ? null : reader["Creation_dt"].ToString(),
                                Parent = (int)reader["Parent"]
                            };
                            return Ok(data);
                        }
                        else {
                            return Content(HttpStatusCode.NotFound, "Data with the specified name not found.");
                        }
                    }
                }
                catch (Exception ex) {
                    // Log the exception here
                    return InternalServerError(ex); // Return the exception for debugging. In production, use a more general error message.
                }
            }
        }

        [HttpPost]
        [Route("api/somiod/{application_name}/{container_name}/data")]
        public IHttpActionResult InsertData(string application_name, string container_name, [FromBody] Data data) {
            if (data == null) {
                return BadRequest("Data is null.");
            }

            if (string.IsNullOrEmpty(container_name)) {
                return BadRequest("Container name is null or empty.");
            }

            using (SqlConnection conn = new SqlConnection(connectionDB)) {
                try {
                    conn.Open();

                    // Fetch the Container ID while ensuring it belongs to the specified Application
                    //-------------------------------------
                    string fetchParentIdSql = @"
                    SELECT c.Id 
                    FROM Containers c
                    INNER JOIN Applications a ON c.Parent = a.Id
                    WHERE c.Name = @containerName AND a.Name = @applicationName";
                    SqlCommand fetchCmd = new SqlCommand(fetchParentIdSql, conn);
                    fetchCmd.Parameters.AddWithValue("@containerName", container_name);
                    fetchCmd.Parameters.AddWithValue("@applicationName", application_name);

                    object parentIdResult = fetchCmd.ExecuteScalar();
                    if (parentIdResult == null) {
                        return Content(HttpStatusCode.NotFound, "Container not found or does not belong to the specified application.");
                    }

                    int parentId = (int)parentIdResult;

                    //--------------------------------------
                    //get number so the name is unique and non repeatable
                    string getMaxIDSql = "SELECT MAX(Id) FROM Data";
                    SqlCommand getMaxIDCmd = new SqlCommand(getMaxIDSql, conn);
                    object result3Obj = getMaxIDCmd.ExecuteScalar();

                    int result3 = (result3Obj != DBNull.Value) ? Convert.ToInt32(result3Obj) : 0;
                    string uniqueName = data.Name + (result3 + 1).ToString();

                    // Insert into the Data table
                    string sql = "INSERT INTO Data (Name, Content, Parent) VALUES (@name, @content, @parent)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@name", uniqueName);  // Ensure this matches the new schema
                    cmd.Parameters.AddWithValue("@content", data.Content);
                    cmd.Parameters.AddWithValue("@parent", parentId);
                    int result = cmd.ExecuteNonQuery();

                    //----------------------------------------
                    //Get subscription name to fire the notification
                    /*
                    string sql2 = "SELECT name FROM Subscriptions WHERE parent=@Parent";
                    SqlCommand cmd2 = new SqlCommand(sql2, conn);
                    cmd2.Parameters.AddWithValue("@parent", parentId);

                    object nameResult = cmd2.ExecuteScalar();
                    if (nameResult == null) {
                        return Content(HttpStatusCode.NotFound, "Wasn't possible to retrieve the subscription name");
                    }
                    string subName = (string)nameResult;
                    */

                    //-----------------------------------------

                    if (result >= 1) {
                        Notify(parentId, container_name, "CREATE", data.Content);
                        return Ok("Data inserted successfully.");
                    } else {
                        return BadRequest("Failed to insert data.");
                    }
                    
                }
                catch (Exception ex) {
                    // Consider logging the exception
                    return InternalServerError(ex);
                }
            }
        }


        [HttpDelete]
        [Route("api/somiod/{application}/{container}/data/{data_name}")]
        public IHttpActionResult DeleteDataByName(string application, string container, string data_name) {
            using (SqlConnection conn = new SqlConnection(connectionDB)) {
                try {
                    conn.Open();

                    string sql = "DELETE FROM Data WHERE Name = @data_name";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@data_name", data_name);

                    int result = cmd.ExecuteNonQuery();
                    return result >= 1 ? (IHttpActionResult)Ok($"Data with the specified name '{data_name}' deleted successfully.") : Content(HttpStatusCode.NotFound, $"Data with name '{data_name}' not found.");
                }
                catch (Exception ex) {
                    return InternalServerError(ex);
                }
            }
        }


        #endregion

        #region Subscriptions

        public void Notify(int parentId, string containerName, string eventType, string content) {
            var notification = new Notification(eventType, content);
            using (SqlConnection conn = new SqlConnection(connectionDB)) {
                conn.Open();
                string sql = "SELECT * FROM Subscriptions WHERE parent=@Parent AND eventtype=@EventType OR eventtype='BOTH'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Parent", parentId);
                cmd.Parameters.AddWithValue("@eventtype", eventType.ToUpper());
                var reader = cmd.ExecuteReader();

                while (reader.Read()) {
                    BrokerHelper.FireNotification(reader.GetString(5), containerName, notification);
                }
                reader.Close();
            }
        }

        [HttpPost]
        [Route("api/somiod/{app_name}/{container_name}/sub")]
        public IHttpActionResult CreateSub(string app_name, string container_name, [FromBody] Subscription subscription) {
            using (SqlConnection conn = new SqlConnection(connectionDB)) {
                conn.Open();
                string sqlIdParent = @"
                SELECT c.id 
                FROM Containers c 
                INNER JOIN Applications a ON c.Parent = a.Id 
                WHERE a.Name = @appName AND c.Name = @containerName";
                SqlCommand cmd = new SqlCommand(sqlIdParent, conn);
                cmd.Parameters.AddWithValue("@appName", app_name);
                cmd.Parameters.AddWithValue("@containerName", container_name);

                int? resultIdParent = null;

                using (SqlDataReader reader = cmd.ExecuteReader()) {
                    if (reader.Read()) {
                        resultIdParent = (int)reader["id"];
                    }
                }

                if (!resultIdParent.HasValue) {
                    return BadRequest("Specified application or container does not exist.");
                }

                string queryString = "INSERT INTO Subscriptions (name, parent, eventtype, endpoint) VALUES (@name, @parent, @eventtype, @endpoint)";
                SqlCommand cmd2 = new SqlCommand(queryString, conn);
                cmd2.Parameters.AddWithValue("@name", subscription.Name);
                cmd2.Parameters.AddWithValue("@parent", resultIdParent.Value);
                cmd2.Parameters.AddWithValue("@eventtype", subscription.EventType.ToUpper());
                cmd2.Parameters.AddWithValue("@endpoint", subscription.Endpoint.ToUpper());

                int result = cmd2.ExecuteNonQuery();

                if (result > 0) {
                    // Assuming Notify is a method defined elsewhere to send notifications
                    //Notify(resultIdParent.Value, container_name, "CREATE", subscription.Name);
                    return Ok("Subscribed with success");
                }
                else {
                    return BadRequest("Failed to create subscription.");
                }
            }
        }

        [HttpDelete]
        [Route("api/somiod/{app_name}/{container_name}/sub/{sub_name}")]
        public IHttpActionResult DeleteSub(string app_name, string container_name, string sub_name) {
            using (SqlConnection conn = new SqlConnection(connectionDB)) {
                conn.Open();
                string sqlIdParent = "SELECT id FROM Applications WHERE name = @appName";
                SqlCommand cmd = new SqlCommand(sqlIdParent, conn);
                cmd.Parameters.AddWithValue("@appName", app_name);
                int resultIdParent = 0;
                string subContent = null;

                using (SqlDataReader reader = cmd.ExecuteReader()) {
                    if (reader.Read()) {
                        resultIdParent = (int)reader["id"];
                        subContent = reader.GetString(2);
                    }
                }

                string queryString = "DELETE FROM Subscriptions WHERE name = @name AND parent = @parent";
                SqlCommand cmd2 = new SqlCommand(queryString, conn);
                cmd2.Parameters.AddWithValue("@name", sub_name);
                cmd2.Parameters.AddWithValue("@parent", resultIdParent);

                int result = cmd2.ExecuteNonQuery();
                //conn.Close():  -> nao é preciso porque estamos a meter o codigo dentro do USING

                if (result > 0) {
                    //Notify(resultIdParent, container_name, "DELETE", subContent);
                    return Ok();
                }
                else {
                    return BadRequest();
                }
            }
        }

        #endregion
    }
}
