using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NiCris.BusinessObjects;
using NiCris.DataAccess.Interfaces;
using Dapper;

namespace NiCris.DataAccess.Dapper
{
    public class TaskRepository : ITaskRepository
    {
        public TaskEx GetTask(int id)
        {
            // Dapper - Single
            using (var conn = ConnectionFactory.GetOpenConnection())
            {
                var task = conn.Query<TaskEx>("SELECT * FROM Tasks WHERE id = @id", new { id = id }).First();
                return task;
            }
        }

        public List<TaskEx> GetAllTasks()
        {
            // Dapper – Simple List
            using (var conn = ConnectionFactory.GetOpenConnection())
            {
                var tasks = conn.Query<TaskEx>("SELECT * FROM Tasks").ToList();
                return tasks;
            }
        }

        public List<TaskEx> GetTasksByFilter(string name)
        {
            // Dapper – Filtered List
            using (var conn = ConnectionFactory.GetOpenConnection())
            {
                var tasks = conn.Query<TaskEx>("SELECT * FROM Tasks WHERE Name LIKE @name",
                  new { name = string.Format("%{0}%", name) }).ToList();
                return tasks;
            }
        }

        public TaskEx AddTask(TaskEx task)
        {
            task.ResolvedDate = (task.Status == "2") ? DateTime.Now : (DateTime?)null;

            // Dapper - Insert
            using (var conn = ConnectionFactory.GetOpenConnection())
            {
                conn.Execute(@"INSERT INTO Tasks(Name, CreatedBy, CreatedDate, Resolver, ResolvedDate, Status)
                                     VALUES (@Name, @CreatedBy, @CreatedDate, @Resolver, @ResolvedDate, @Status);",
                                     new
                                     {
                                         task.Name,
                                         task.CreatedBy,
                                         task.CreatedDate,
                                         task.Resolver,
                                         task.ResolvedDate,
                                         task.Status
                                     });

                task.Id = (int)conn.Query(@"SELECT @@IDENTITY AS Id").First().Id;
                return task;
            }
        }

        public void UpdateTask(TaskEx task)
        {
            task.ResolvedDate = (task.Status == "2") ? DateTime.Now : (DateTime?)null;

            // Dapper - Update
            using (var conn = ConnectionFactory.GetOpenConnection())
            {
                var dbTask = conn.Query<TaskEx>("SELECT * FROM Tasks where Id = @id", new { id = task.Id }).First();

                dbTask.Name = task.Name;
                dbTask.CreatedBy = task.CreatedBy;
                dbTask.CreatedDate = task.CreatedDate;
                dbTask.Resolver = task.Resolver;
                dbTask.ResolvedDate = task.ResolvedDate;
                dbTask.Status = task.Status;

                conn.Execute(@"UPDATE Tasks SET 
                               Name = @Name, CreatedBy = @CreatedBy, CreatedDate = @CreatedDate, Resolver = @Resolver, 
                               ResolvedDate = @ResolvedDate, Status = @Status 
                               WHERE Id = @id;",
                  new
                  {
                      dbTask.Id,
                      dbTask.Name,
                      dbTask.CreatedBy,
                      dbTask.CreatedDate,
                      dbTask.Resolver,
                      dbTask.ResolvedDate,
                      dbTask.Status
                  });
            }
        }

        public void DeleteTask(int id)
        {
            // Dapper - Delete

            // With Dapper, if you already know the property values of the object then you don’t need to return the object from the database first, 
            // you can simply call the SQL delete statement with the criteria.
            using (var conn = ConnectionFactory.GetOpenConnection())
            {
                conn.Execute(@"DELETE FROM Tasks WHERE Id = @id", new { id = id });
            }
        }
    }
}
