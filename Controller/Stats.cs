using UltimateProject.Model;
using UltimateProject.View;

namespace TP_Theo_Stoffelbach.Controller
{
    public class Stats
    {
        private static float _tasksCount;
        private static float _taskCompleted;
        private static float _taskNotCompleted;
        private static float _taskHigh;
        private static float _taskMedium;
        private static float _taskLow;

        /// <summary>
        /// To calcul the tasks and priority of tasks
        /// </summary>
        /// <param name="todos"> all todos </param>
        public static void _calcultasks(List<TodoModel> todos)
        {
            foreach (var todo in todos)
            {
                if (todo.IsCompleted) _taskCompleted++;
                else _taskNotCompleted++;

                if (todo.Status == PriorityStatus.Low) _taskLow++;
                else if (todo.Status == PriorityStatus.Medium) _taskMedium++;
                else _taskHigh++;
            }
        }

        /// <summary>
        /// reset all stats
        /// </summary>
        public static void _reset()
        {
            _taskCompleted = 0;
            _taskNotCompleted = 0;

            _taskHigh = 0;
            _taskMedium = 0;
            _taskLow = 0;
        }


        /// <summary>
        /// formatting for display the stats
        /// </summary>
        public static void Show()
        {
            _Calcul();
            Print.Display($"Task completed {_taskCompleted} ( {_GetPercentTaskPriority(_taskCompleted)} % )\n" +
                          $"Task not complete : {_taskNotCompleted} ( {_GetPercentTaskPriority(_taskNotCompleted)} % )\n\n" +
                          $"Task Low :  {_taskLow} ( {_GetPercentTaskPriority(_taskLow)}% )\n" +
                          $"Task Medium : {_taskMedium} ( {_GetPercentTaskPriority(_taskMedium)} % )\n" +
                          $"Task High : {_taskHigh} ( {_GetPercentTaskPriority(_taskHigh)} % )");
        }


        /// <summary>
        /// Calcul the stats
        /// </summary>
        private static void _Calcul()
        {
            List<TodoModel>? todos = TodoModel.ReadTodos();
            if (todos == null || todos.Count == 0)
            {
                Print.ErrorDisplay("Not todos found");
            };

            _tasksCount = todos.Count;

            _reset();
            _calcultasks(todos);
        }

        /// <summary>
        /// Get the percent of task priority or task completed/ not completed
        /// </summary>
        /// <param name="taskPriority"></param>
        /// <returns></returns>
        private static int _GetPercentTaskPriority(float taskPriority)
        {
            if (taskPriority == 0)
            {
                return 0;
            }
            float rst = (float)taskPriority / (float)_tasksCount;
            float roundedValue = (float)Math.Round(rst, 2) * 100;
            return (int)roundedValue;
        }
    }
}
