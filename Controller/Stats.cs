using UltimateProject.Controller;
using UltimateProject.Model;
using UltimateProject.View;

namespace TP_Theo_Stoffelbach.Controller
{
    public class Stats
    {
        private static Stats instance = null;
        private static int _taskCompleted;
        private static int _tasksCount;
        private static int _taskNotCompleted;
        private static int _taskHigh;
        private static int _taskMedium;
        private static int _taskLow;

        private static void Calcul()
        {
            List<TodoModel> todos = TodoModel.ReadTodos();
            _tasksCount = todos.Count;

            foreach (var todo in todos)
            {
                if (todo.IsCompleted) _taskCompleted++;
                else _taskNotCompleted++;

                if (todo.Status == PriorityStatus.Low) _taskLow++;
                else if (todo.Status == PriorityStatus.Medium) _taskMedium++;
                else _taskHigh++;
            }

        }

        public static void Show()
        {
            Calcul();
            Print.Display($"Task completed {_taskCompleted} ( {getPercentTaskPriority(_taskCompleted)} % )\n" +
                          $"Task not complete : {_taskNotCompleted} ( {getPercentTaskPriority(_taskNotCompleted)} % )\n\n" +
                          $"Task Low :  {_taskLow} ( {getPercentTaskPriority(_taskLow)}% )\n" +
                          $"Task Medium : {_taskMedium} ( {getPercentTaskPriority(_taskMedium)} % )\n" +
                          $"Task High : {_taskHigh} ( {getPercentTaskPriority(_taskHigh)} % )");
        }

        private static int getPercentTaskPriority(int taskPriority)
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
