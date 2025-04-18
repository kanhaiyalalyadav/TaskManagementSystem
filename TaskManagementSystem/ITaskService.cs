using System.Data;
using TaskManagementSystem.Models;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;

namespace TaskManagementSystem
{
    public interface ITaskService
    {
        List<TaskModel> GetAllTask(string searchText);
        TaskModel GetById(int id);
        void AddOrUpdate(TaskModel task);
        void Delete(int id);
    }
    public class TaskService : ITaskService
    {
        private readonly string _connStr;

        public TaskService(ConnectionHelper connection)
        {
            _connStr = connection.Default;
        }

        public List<TaskModel> GetAllTask(string searchTerm)
        {
            List<TaskModel> task = new List<TaskModel>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connStr))
                {
                    SqlCommand cmd = new SqlCommand("sp_GetAllTask", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SearchText", searchTerm ?? string.Empty);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        task.Add(new TaskModel
                        {
                            TaskID = dr["TaskID"] is DBNull ? 0 : Convert.ToInt32(dr["TaskID"]),
                            TaskTitle = dr["TaskTitle"].ToString() is DBNull ? string.Empty : dr["TaskTitle"].ToString(),
                            TaskDescription = dr["TaskDescription"].ToString() is DBNull ? string.Empty : dr["TaskDescription"].ToString(),
                            TaskDueDate = dr["TaskDueDate"] is DBNull ? DateTime.Now : Convert.ToDateTime(dr["TaskDueDate"]),
                            TaskStatus = dr["TaskStatus"].ToString() is DBNull ? string.Empty : dr["TaskStatus"].ToString(),
                            TaskRemarks = dr["TaskRemarks"].ToString() is DBNull ? string.Empty : dr["TaskRemarks"].ToString(),
                            CreatedOn = dr["CreatedOn"] is DBNull ? DateTime.Now : Convert.ToDateTime(dr["CreatedOn"]),
                            LastUpdatedOn = dr["LastUpdatedOn"] is DBNull ? (DateTime?)null : Convert.ToDateTime(dr["LastUpdatedOn"]),
                            CreatedBy = dr["CreatedBy"].ToString() is DBNull ? string.Empty : dr["CreatedBy"].ToString(),
                            LastUpdatedBy = dr["LastUpdatedBy"].ToString() is DBNull ? string.Empty : dr["LastUpdatedBy"].ToString(),
                            CreatedById = dr["CreatedById"] is DBNull ? 0 : Convert.ToInt32(dr["CreatedById"]),
                            LastUpdatedById = dr["LastUpdatedById"] is DBNull ? 0 : Convert.ToInt32(dr["LastUpdatedById"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return task;

        }

        public TaskModel GetById(int id)
        {
            TaskModel tasks=new TaskModel();
            using (SqlConnection con = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand("[sp_GetTaskBYId]", con);
                cmd.CommandType= CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@TaskID", id);
                SqlDataReader dr=cmd.ExecuteReader();
                while (dr.Read())
                {
                    tasks.TaskID = id;
                    tasks.TaskTitle = dr["TaskTitle"].ToString();
                    tasks.TaskDescription = dr["TaskDescription"].ToString();
                    tasks.TaskDueDate = Convert.ToDateTime(dr["TaskDueDate"]);
                    tasks.TaskStatus = dr["TaskStatus"].ToString();
                    tasks.TaskRemarks = dr["TaskRemarks"].ToString();
                    tasks.LastUpdatedOn = dr["LastUpdatedOn"] is DBNull ? (DateTime?)null : Convert.ToDateTime(dr["LastUpdatedOn"]);
                    tasks.CreatedOn = dr["CreatedOn"] is DBNull ? DateTime.Now : Convert.ToDateTime(dr["CreatedOn"]);
                }
            }
            return tasks;
        }
        public void AddOrUpdate(TaskModel task)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connStr))
                {
                    SqlCommand cmd = new SqlCommand("sp_AddOrUpdate", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TaskID",task.TaskID);
                    cmd.Parameters.AddWithValue("@TaskTitle", task.TaskTitle);
                    cmd.Parameters.AddWithValue("@TaskDescription", task.TaskDescription);
                    cmd.Parameters.AddWithValue("@TaskDueDate", task.TaskDueDate);
                    cmd.Parameters.AddWithValue("@TaskStatus", task.TaskStatus);
                    cmd.Parameters.AddWithValue("@TaskRemarks", task.TaskRemarks);
                    cmd.Parameters.AddWithValue("@UserName", task.CreatedBy);
                    cmd.Parameters.AddWithValue("@UserId",task.CreatedById);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Delete(int id)
        {
            using var con = new SqlConnection(_connStr);
            var cmd = new SqlCommand("sp_DeleteTask", con) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@TaskID", id);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }


}
