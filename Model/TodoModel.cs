using Microsoft.VisualBasic;
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

        private static EFContext _db = EFContext.Instance;
        public string Name { get; set; }

        public string? Description { get; set; }

        public PriorityStatus? Status { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public int ?UserId { get; set; }

        public TodoModel() { }
        public TodoModel(string name, string desc, PriorityStatus priorityStatus, DateTime dueDate, int userId)
        {
            Name = name;
            Description = desc;
            Status = priorityStatus;
            CreationDate = DateTime.Now;
            DueDate = dueDate.Date;
            IsCompleted = false;
            UserId = userId;
        }
        
        public static TodoModel AddTodo(TodoModel todoModel)
        {
            try
            {
                _db.Add(todoModel);
                _db.SaveChanges();
                return todoModel;
            }
            catch (Exception err)
            {
                Print.ErrorFatalDisplay($"with bdd to : {err}");
                return null;
            }
        }

        public static List<TodoModel>? ReadTodos()
        {
            return _db.TodoModels.ToList();
        }

        public static TodoModel ReadTodo(int id)
        {
            TodoModel? todoModel = _db.TodoModels.Find(id);
            if (todoModel == null)
            {
                Print.ErrorDisplay($"Il n'y pas de todo avec l'Id : {id}");
                return null;
            }
            return todoModel;
        }

        public static TodoModel? ReadTodosWithId(int TodoId)
        {
            try
            {
                TodoModel? todoList = _db.TodoModels.Find(TodoId);

                if (todoList == null)return null;

                return todoList;
            }
            catch (Exception err)
            {
                Print.ErrorFatalDisplay($"with bdd to : {err}");
                return null;
            }
        }

        public static TodoModel? ActivateTodo(int id)
        {
            TodoModel? tododb = _db.TodoModels.Find(id);
            if (tododb != null)
            {
                try
                {
                    tododb.IsCompleted = !tododb.IsCompleted;
                    _db.Update(tododb);
                    _db.SaveChanges();
                    return tododb;
                }
                catch (Exception)
                {
                    Print.ErrorDisplay($"Bug to Activate todo {id}");
                    return null;
                }
            }
            return null;
        }

        public static TodoModel? ChangeUserIdToTodo(int idTodo, int idNewUser)
        {
            TodoModel? tododb = _db.TodoModels.Find(idTodo);
            if (tododb != null)
            {
                try
                {
                    tododb.UserId = idNewUser;
                    _db.Update(tododb);
                    _db.SaveChanges();
                    Print.SuccessDisplay("Todo Update");
                    return tododb;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }

        public static TodoModel? AddDescTodo(int id,string description)
        {
            TodoModel? tododb = _db.TodoModels.Find(id);
            if (tododb != null)
            {
                try
                {
                    tododb.Description = description;
                    _db.Update(tododb);
                    _db.SaveChanges();
                    return tododb;
                }
                catch (Exception ex)
                {
                    Print.ErrorDisplay(ex.ToString());
                }
            }
            return null;
        }
        
        public static TodoModel? UpdateTodo(int id, TodoModel todoModel)
        {
            TodoModel? tododb = _db.TodoModels.Find(id);
            if (tododb == null)

            {
                Print.ErrorDisplay($"not found Todo with Id : {id}");
                return null;
            }

            tododb = _ChangeTodoValue(tododb, todoModel);
            try
            {
                _db.Update(tododb);
                _db.SaveChanges();
                return tododb;
            }
            catch (Exception ex)
            {
                Print.ErrorDisplay(ex.ToString());
                return null;
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

        public static List<TodoModel>? DeleteTodos(PriorityStatus status)
        {
            List<TodoModel> todos = _db.TodoModels.Where(todo => todo.Status == status).ToList();

            if (todos.Count == 0)return null;

            foreach (var todo in todos)
            {
                _db.TodoModels.Remove(todo);
            }

            _db.SaveChanges();
            return todos;
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

        private static TodoModel _ChangeTodoValue(TodoModel todoOrigin, TodoModel todoValue)
        {
            if (todoValue == null) return todoOrigin;

            if (todoValue.Name != null) todoOrigin.Name = todoValue.Name;
            if (todoValue.Description != null) todoOrigin.Description = todoValue.Description;
            if (todoValue.Status != null) todoOrigin.Status = todoValue.Status;
            if (todoValue.DueDate != null) todoOrigin.DueDate = todoValue.DueDate;

            return todoOrigin;
        }


        public override string ToString()
        {
            return $"Id : {Id}, " +
                $"Name : {Name}, " +
                $"Description : {Description}, " +
                $"Priority status : {Status}," +
                $"Due Date : {DueDate}," +
                $"User Id assign : {UserId}," +
                $"Completed : {this.IsCompleted},";
        }
        public string ToString(List<UserModel> list)
        {
            return @$"Id : {Id}, " +
                $"Name : {Name}, " +
                $"Description : {Description}, " +
                $"Priority status : {Status}," +
                $"User : {UserId} " +  
                $"Due Date : {DueDate}," +
                $"Completed : {this.IsCompleted},";
        }

    }
}
