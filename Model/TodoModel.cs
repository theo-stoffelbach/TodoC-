using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using UltimateProject.Controller;
using UltimateProject.View;

namespace UltimateProject.Model
{
    public class TodoModel
    {
        [Key]
        public int Id { get; set; }
        private static EFContext _db = new EFContext();
        public string Name { get; set; }

        public string? Description { get; set; }

        public PriorityStatus? Status { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
        [NotMapped] public List<int> TodosId { get; set; }

        public TodoModel() { }
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
                _db.Add(todoModel);
                _db.SaveChanges();
                Print.SuccessDisplay("Todo Created with id : " + todoModel.Id);
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
            return _db.TodoModels.ToList();
        }

        public static TodoModel ReadTodo(int id)
        {
            TodoModel todoModel = _db.TodoModels.Find(id);
            if (todoModel == null)
            {
                Print.ErrorDisplay($"Il n'y pas de todo avec l'Id : {id}");
                return null;
            }
            return todoModel;
        }

        public static List<TodoModel> ReadTodosWithId(List<int> listUsersId)
        {
            try
            {
                List<TodoModel> todoList = _db.TodoModels.Where(todo => listUsersId.Contains(todo.Id)).ToList();

                if (todoList.Count == 0)
                {
                    Print.ErrorDisplay($"Il n'y a pas de todo avec le User (Id) mentionnés.");
                    return null;
                }

                return todoList;
            }
            catch (Exception err)
            {
                Print.ErrorFatalDisplay($"with bdd to : {err}");
                return null;
            }
        }

        public static void ActivateTodo(int id)
        {
            TodoModel? tododb = _db.TodoModels.Find(id);
            if (tododb != null)
            {
                try
                {
                    tododb.IsCompleted = !tododb.IsCompleted;
                    _db.Update(tododb);
                    _db.SaveChanges();
                    Print.SuccessDisplay("Todo Update");
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
            TodoModel? tododb = _db.TodoModels.Find(id);
            if (tododb != null)
            {
                try
                {
                    tododb.Description = description;
                    _db.Update(tododb);
                    _db.SaveChanges();
                    Print.SuccessDisplay("Todo update with the description");
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
            TodoModel? tododb = _db.TodoModels.Find(id);
            if (tododb == null)
            {
                Print.ErrorDisplay($"not found Todo with Id : {id}");
                return;
            }

            tododb = TodoController.ChangeTodoValue(tododb, todoModel);
            try
            {
                _db.Update(tododb);
                _db.SaveChanges();
                Print.SuccessDisplay("Todo Update");
            }
            catch (Exception ex)
            {
                Print.ErrorDisplay(ex.ToString());
            }
            
        }

        public static void DeleteTodo(int id)
        {
            TodoModel? todo = _db.TodoModels.Find(id);
            if (todo == null)
            {
                Console.WriteLine($"Not found id : {id}");
                return;
            }

            _db.TodoModels.Remove(todo);
            _db.SaveChanges();
            Print.SuccessDisplay($"Delete Todo {id} successful");
        }

        public static void DeleteAllTodos()
        {
            List<TodoModel> todos = _db.TodoModels.ToList();

            if (todos.Count == 0)
            {
                Print.ErrorDisplay("Not todos found");
                return;
            }

            foreach (var todo in todos)
            {
                _db.TodoModels.Remove(todo);
                Print.SuccessDisplay("Delete All Todos successful");
            }
            _db.SaveChanges();
        }

        public static void DeleteTodos(PriorityStatus status)
        {
            List<TodoModel> todos = _db.TodoModels.Where(todo => todo.Status == status).ToList();

            if (todos.Count == 0)
            {
                Print.ErrorDisplay("Not todos found");
                return;
            }

            foreach (var todo in todos)
            {
                TodoUserController.DeleteAllRefOfTodo(todo.Id);
                _db.TodoModels.Remove(todo);
                Print.SuccessDisplay("Delete All Todos successful");
                UserTodosModel.DeleteAllRefOfTodo(todo.Id);

            }
            _db.SaveChanges();
        }

        public string ShowDetailTodo(string[] listName)
        {
            string listNameStr = string.Join(" ", listName);
            return $"Id : {this.Id}, " +
                   $"Name : {this.Name}, " +
                   $"Description : {this.Description}, " +
                   $"Priority status : {this.Status}," +
                   $"Due Date : {this.DueDate}," +
                   $"Completed : {this.IsCompleted}," +
                   $"Users : {listNameStr}";
        }



        public override string ToString()
        {
            return $"Id : {Id}, " +
                $"Name : {Name}, " +
                $"Description : {Description}, " +
                $"Priority status : {Status}," +
                $"Due Date : {DueDate}," +
                $"Completed : {this.IsCompleted},";
        }
        public string ToString(List<UserModel> list)
        {
            return @$"Id : {Id}, " +
                $"Name : {Name}, " +
                $"Description : {Description}, " +
                $"Priority status : {Status}," +
                $"User : {String.Join(", ", list.Select(user => user.Name))} ({list.Count})," +  
                $"Due Date : {DueDate}," +
                $"Completed : {this.IsCompleted},";
        }

    }
}
