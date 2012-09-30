using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NiCris.BusinessObjects;

namespace NiCris.DataAccess.Interfaces
{
    public interface ITaskRepository
    {
        TaskEx GetTask(int id);
        List<TaskEx> GetAllTasks();
        List<TaskEx> GetTasksByFilter(string name);

        TaskEx AddTask(TaskEx task);
        void UpdateTask(TaskEx task);
        void DeleteTask(int id);
    }
}
