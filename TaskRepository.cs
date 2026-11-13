using CyberSafetyChatbotGUI.Models;
using MySqlConnector;

namespace CyberSafetyChatbotGUI.Data
{
    public interface ITaskRepository
    {
        IReadOnlyList<CyberTask> GetAll();
        CyberTask Add(CyberTask task);
        bool Update(CyberTask task);
        bool Delete(int id);
    }

    public sealed class InMemoryTaskRepository : ITaskRepository
    {
        private readonly List<CyberTask> tasks = new();
        private int nextId = 1;

        public IReadOnlyList<CyberTask> GetAll() => tasks
            .OrderBy(task => task.ReminderAt ?? DateTime.MaxValue)
            .ThenBy(task => task.Id)
            .Select(Clone)
            .ToList();

        public CyberTask Add(CyberTask task)
        {
            CyberTask saved = Clone(task);
            saved.Id = nextId++;
            saved.CreatedAt = DateTime.Now;
            tasks.Add(saved);
            return Clone(saved);
        }

        public bool Update(CyberTask task)
        {
            CyberTask? existing = tasks.FirstOrDefault(item => item.Id == task.Id);
            if (existing is null)
                return false;

            existing.Title = task.Title;
            existing.Description = task.Description;
            existing.ReminderAt = task.ReminderAt;
            existing.IsCompleted = task.IsCompleted;
            return true;
        }

        public bool Delete(int id) => tasks.RemoveAll(task => task.Id == id) > 0;

        private static CyberTask Clone(CyberTask task) => new()
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            ReminderAt = task.ReminderAt,
            IsCompleted = task.IsCompleted,
            CreatedAt = task.CreatedAt
        };
    }

    public class MySqlTaskRepository : ITaskRepository
    {
        private readonly string connectionString;

        public MySqlTaskRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IReadOnlyList<CyberTask> GetAll()
        {
            const string sql = "SELECT task_id, title, description, reminder_date, is_completed, created_at FROM cyber_tasks ORDER BY reminder_date IS NULL, reminder_date, task_id";
            List<CyberTask> result = new();
            using MySqlConnection connection = new(connectionString);
            connection.Open();
            using MySqlCommand command = new(sql, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(ReadTask(reader));
            }

            return result;
        }

        public CyberTask Add(CyberTask task)
        {
            const string sql = "INSERT INTO cyber_tasks (title, description, reminder_date, is_completed) VALUES (@title, @description, @reminder, @completed); SELECT LAST_INSERT_ID();";
            using MySqlConnection connection = new(connectionString);
            connection.Open();
            using MySqlCommand command = new(sql, connection);
            AddParameters(command, task);
            task.Id = Convert.ToInt32(command.ExecuteScalar());
            task.CreatedAt = DateTime.Now;
            return task;
        }

        public bool Update(CyberTask task)
        {
            const string sql = "UPDATE cyber_tasks SET title=@title, description=@description, reminder_date=@reminder, is_completed=@completed WHERE task_id=@id";
            using MySqlConnection connection = new(connectionString);
            connection.Open();
            using MySqlCommand command = new(sql, connection);
            AddParameters(command, task);
            command.Parameters.AddWithValue("@id", task.Id);
            return command.ExecuteNonQuery() > 0;
        }

        public int AddTask(CyberTask task) => Add(task).TaskId;

        public List<CyberTask> GetAllTasks() => GetAll().ToList();

        public void MarkTaskCompleted(int taskId)
        {
            using MySqlConnection connection = new(connectionString);
            connection.Open();
            using MySqlCommand command = new("UPDATE cyber_tasks SET is_completed=TRUE WHERE task_id=@id", connection);
            command.Parameters.AddWithValue("@id", taskId);
            command.ExecuteNonQuery();
        }

        public void UpdateReminder(int taskId, DateTime? reminderDate)
        {
            using MySqlConnection connection = new(connectionString);
            connection.Open();
            using MySqlCommand command = new("UPDATE cyber_tasks SET reminder_date=@reminder WHERE task_id=@id", connection);
            command.Parameters.AddWithValue("@reminder", reminderDate ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@id", taskId);
            command.ExecuteNonQuery();
        }

        public void DeleteTask(int taskId) => Delete(taskId);

        public bool Delete(int id)
        {
            using MySqlConnection connection = new(connectionString);
            connection.Open();
            using MySqlCommand command = new("DELETE FROM cyber_tasks WHERE task_id=@id", connection);
            command.Parameters.AddWithValue("@id", id);
            return command.ExecuteNonQuery() > 0;
        }

        private static void AddParameters(MySqlCommand command, CyberTask task)
        {
            command.Parameters.AddWithValue("@title", task.Title);
            command.Parameters.AddWithValue("@description", task.Description);
            command.Parameters.AddWithValue("@reminder", task.ReminderAt ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@completed", task.IsCompleted);
        }

        private static CyberTask ReadTask(MySqlDataReader reader) => new()
        {
            Id = reader.GetInt32("task_id"),
            Title = reader.GetString("title"),
            Description = reader.GetString("description"),
            ReminderAt = reader.IsDBNull(reader.GetOrdinal("reminder_date")) ? null : reader.GetDateTime("reminder_date"),
            IsCompleted = reader.GetBoolean("is_completed"),
            CreatedAt = reader.GetDateTime("created_at")
        };
    }

    public sealed class TaskRepository : MySqlTaskRepository
    {
        public TaskRepository(string connectionString) : base(connectionString)
        {
        }

        public new int AddTask(CyberTask task) => base.AddTask(task);
        public new List<CyberTask> GetAllTasks() => base.GetAllTasks();
        public new void MarkTaskCompleted(int taskId) => base.MarkTaskCompleted(taskId);
        public new void UpdateReminder(int taskId, DateTime? reminderDate) => base.UpdateReminder(taskId, reminderDate);
        public new void DeleteTask(int taskId) => base.DeleteTask(taskId);
    }
}
