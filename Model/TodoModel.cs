using System.ComponentModel.DataAnnotations;
using UltimateProject.View;

namespace UltimateProject.Model
{
    public class TodoModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public PriorityStatus? Status { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public int ?UserId { get; set; }


        private static EFContext _db = EFContext.Instance;

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
        
        /// <summary>
        /// Add Todo to BDD
        /// </summary>
        /// <param name="todoModel"> is TodoModel </param>
        /// <returns></returns>
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

        /// <summary>
        /// To read all todos
        /// </summary>
        /// <returns> all todos</returns>
        public static List<TodoModel>? ReadTodos()
        {
            return _db.TodoModels.ToList();
        }

        /// <summary>
        /// Read 1 todo with id
        /// </summary>
        /// <param name="id"> the id of todo </param>
        /// <returns></returns>
        public static TodoModel? ReadTodo(int id)
        {
            TodoModel? todoModel = _db.TodoModels.Find(id);
            if (todoModel == null)
            {
                Print.ErrorDisplay($"There's no todo with Id: {id}");
                return null;
            }
            return todoModel;
        }

        /// <summary>
        /// To activate a todo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// To change the user of a todo
        /// </summary>
        /// <param name="idTodo"> int the id of todo </param>
        /// <param name="idNewUser"> int of the new todo </param>
        /// <returns></returns>
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

        /// <summary>
        /// To get all todos with user id
        /// </summary>
        /// <param name="userId"> the id of user</param>
        /// <returns></returns>
        public static List<TodoModel>? GetTodosWithUserId(int userId)
        {
            try
            {
                var listTodos = _db.TodoModels.Where(todo => todo.UserId == userId).ToList();

                if (listTodos == null) return null;
                
                return listTodos;
            }
            catch (Exception)
            {
                return null;
            }

        }

        /// <summary>
        /// To get a description to a todo or change it
        /// </summary>
        /// <param name="id"> the todo of id </param>
        /// <param name="description"> the new description of todo</param>
        /// <returns></returns>
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
        
        /// <summary>
        /// Update a todo
        /// </summary>
        /// <param name="id"> int the user id</param>
        /// <param name="todoModel"> the change the old todo </param>
        /// <returns></returns>
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

        /// <summary>
        /// To delete a todo
        /// </summary>
        /// <param name="id"> the user id</param>
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

        /* The method for Version more than 1.0.0 ( in a future)
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
        }*/

        /// <summary>
        /// To delete all todos with status
        /// </summary>
        /// <param name="status"> a status to delete a certain</param>
        /// <returns></returns>
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

        /// <summary>
        /// To change the value of todo
        /// </summary>
        /// <param name="todoOrigin"> The reference of the todo </param>
        /// <param name="todoValue"> the new value of todo </param>
        /// <returns></returns>
        private static TodoModel _ChangeTodoValue(TodoModel todoOrigin, TodoModel todoValue)
        {
            if (todoValue == null) return todoOrigin;

            if (todoValue.Name != null) todoOrigin.Name = todoValue.Name;
            if (todoValue.Description != null) todoOrigin.Description = todoValue.Description;
            if (todoValue.Status != null) todoOrigin.Status = todoValue.Status;
            if (todoValue.DueDate != null) todoOrigin.DueDate = todoValue.DueDate;

            return todoOrigin;
        }

        /// <summary>
        /// To string a todo
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// To string a todo with user
        /// </summary>
        /// <param name="user"> a new user </param>
        /// <returns></returns>
        public string ToString(UserModel user)
        {
            return @$"Id : {Id}, " +
                $"Name : {Name}, " +
                $"Description : {Description}, " +
                $"Priority status : {Status}," +
                $"User : {user.Name} ( {user.Id} ) " +  
                $"Due Date : {DueDate}," +
                $"Completed : {this.IsCompleted},";
        }

    }
}
