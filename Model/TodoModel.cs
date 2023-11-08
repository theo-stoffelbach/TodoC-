using System.ComponentModel.DataAnnotations;
using UltimateProject.Controller;
using UltimateProject.View;

namespace UltimateProject.Model
{

    public class TodoModel
    {

        [Key]
        public int Id { get; set; }

        private static EFContext db = new EFContext();

        public string Name { get; set; }

        public string? Description { get; set; }

        public PriorityStatus? Status { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? DueDate { get; set; }

        public bool IsCompleted { get; set; }

        public TodoModel() { }
        public TodoModel(string name, PriorityStatus priorityStatus, DateTime dueDate)
        {
            Name = name;
            Status = priorityStatus;
            CreationDate = DateTime.Now;
            DueDate = dueDate;
            IsCompleted = false;
        }
        public TodoModel(string name, string desc, PriorityStatus priorityStatus, DateTime dueDate)
        {
            Name = name;
            Description = desc;
            Status = priorityStatus;
            CreationDate = DateTime.Now;
            DueDate = dueDate;
            IsCompleted = false;
        }
        
        public static TodoModel AddTodo(TodoModel todoModel)
        {
            try
            {
                db.Add(todoModel);
                db.SaveChanges();
                Print.SucessDisplay("Todo Created with id : " + todoModel.Id);
                return todoModel;
            }
            catch (Exception err)
            {
                Print.ErrorFatalDisplay($"with bdd to : {err}");
                return null;
            }
        }

        public static List<TodoModel> ReadTodos()
        {
            return db.TodoModels.ToList();
        }

        public static TodoModel ReadTodos(int id)
        {
            TodoModel todoModel = db.TodoModels.Find(id);
            if (todoModel == null)
            {
                Print.ErrorDisplay($"Il n'y pas de todo avec l'Id : {id}");
                return null;
            }
            return todoModel;
        }

        public static void ActivateTodo(int id)
        {
            TodoModel? tododb = db.TodoModels.Find(id);
            if (tododb != null)
            {
                try
                {
                    tododb.IsCompleted = !tododb.IsCompleted;
                    db.Update(tododb);
                    db.SaveChanges();
                    Print.SucessDisplay("Todo Update");
                    return;
                }
                catch (Exception ex)
                {
                    Print.ErrorDisplay(ex.ToString());
                }
            }
            Print.ErrorDisplay($"not found Todo with Id : {id}");
        }

        public static void AddDescTodo(int id,string description)
        {
            TodoModel? tododb = db.TodoModels.Find(id);
            if (tododb != null)
            {
                try
                {
                    tododb.Description = description;
                    db.Update(tododb);
                    db.SaveChanges();
                    Print.SucessDisplay("Todo update with the description");
                    return;
                }
                catch (Exception ex)
                {
                    Print.ErrorDisplay(ex.ToString());
                }
            }
            Print.ErrorDisplay($"not found Todo with Id : {id}");
        }
        
        public static void UpdateTodo(int id, TodoModel todoModel)
        {
            TodoModel? tododb = db.TodoModels.Find(id);
            if (tododb != null)
            {
                tododb = TodoController.ChangeTodoValue(tododb, todoModel);
                try
                {
                    db.Update(tododb);
                    db.SaveChanges();
                    Print.SucessDisplay("Todo Update");
                }
                catch (Exception ex)
                {
                    Print.ErrorDisplay(ex.ToString());
                }
            }
            Print.ErrorDisplay($"not found Todo with Id : {id}");
        }

        public static void DeleteTodo(int id)
        {
            TodoModel? todo = db.TodoModels.Find(id);

            if (todo != null)
            {
                db.TodoModels.Remove(todo);
                db.SaveChanges();
                Print.SucessDisplay($"Delete Todo {id} successful");
            }
            else
            {
                Console.WriteLine($"Not found id : {id}");
            }
        }

        public static void DeletePriorityTodos(int id, string type)
        {
            List<TodoModel> todos = db.TodoModels.ToList();

            if (todos.Count == 0)
            {
                Print.ErrorDisplay("Not todos found");
                return;
            }

            foreach (var todo in todos)
            {
                db.TodoModels.Remove(todo);
                Print.SucessDisplay("Delete All Todos successful");
            }
            db.SaveChanges();
        }

        public override string ToString()
        {
            return $"Id : {Id}, " +
                $"Name : {Name}, " +
                $"Description : {Description}, " +
                $"Priority status : {Status}," +
                $"Due Date : {DueDate},";
        }

    }
}
