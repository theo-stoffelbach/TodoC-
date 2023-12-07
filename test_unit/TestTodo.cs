using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateProject.Controller;
using UltimateProject.Model;
using UltimateProject.View;

namespace UltimateProject.test_unit
{
    public class TestTodo
    {
        private static int _idTodoCreadted;

        public static void TestProtocol()
        {
            List<string> errorList = new List<string>();

            if (!_TestTodoCreated()) return;
            if (!_TestTodoUpdate()) return;
            if (!_TestTodoReadWithId()) return;
            if (!_TestTodoReadAll()) return;
            if (!_TestTodoActivate()) return;
            if (!_TestTodoChangeUser()) return;
            if (!_TestTodoAddDesc()) return;

            if (!_TestTodoDeleteWithId()) return;
            if (!_TestTodoDeleteWithType()) return;
        }

        private static bool _TestTodoCreated()
        {
            try
            {
                TodoModel todo = new TodoModel("test_Unit", "test_Unit", PriorityStatus.Low, DateTime.Now, 1);
                TodoModel todoCreated = TodoModel.AddTodo(todo);
                
                if (todoCreated == null) throw new Exception("To Create a todo Because is Null");
                if (todoCreated != todo) throw new Exception("To Create a todo Because is not equal to todo in begging");

                Print.SuccessDisplay("To Create a todo");
                _idTodoCreadted = todo.Id;
                return true;
            }
            catch (Exception err)
            {
                Print.ErrorDisplay($"{err}");
                return false;
            }
        }

        private static bool _TestTodoUpdate()
        {
            try
            {
                TodoModel todo = new TodoModel("test_Unit 2", "test_Unit", PriorityStatus.Low, DateTime.Now, 1);

                TodoModel? newTodo = TodoModel.UpdateTodo(_idTodoCreadted, todo);

                if (newTodo == null) throw new Exception("To Update a todo Because is Null");
                if (newTodo.Name != "test_Unit 2") throw new Exception("To Update a todo Because is Null");

                Print.SuccessDisplay("To Update a todo with id");
                return true;
            }
            catch (Exception err)
            {
                Print.ErrorDisplay($"{err}");
                return false;
            }
        }

        private static bool _TestTodoReadWithId()
        {
            try
            {
                TodoModel? newTodo = TodoModel.ReadTodo(_idTodoCreadted);

                if (newTodo == null) throw new Exception("To Read with id a todo Because is Null");
                if (newTodo.Name != "test_Unit 2") throw new Exception("To Read with id a todo Because is Not the good Title");

                Print.SuccessDisplay("To Read a todo");
                return true;
            }
            catch (Exception err)
            {
                Print.ErrorDisplay($"{err}");
                return false;
            }
        }

        private static bool _TestTodoReadAll()
        {
            try
            {
                List<TodoModel>? newTodo = TodoModel.ReadTodos();

                if (newTodo == null) throw new Exception("To Read with id a todo Because is Null");
                if (newTodo.Count == 0) throw new Exception("To Read with id a todo Because is Empty");
                if (newTodo[newTodo.Count-1].Name != "test_Unit 2") throw new Exception("The list is not good");

                Print.SuccessDisplay("To Read all todos");
                return true;
            }
            catch (Exception err)
            {
                Print.ErrorDisplay($"{err}");
                return false;
            }
        }

        private static bool _TestTodoActivate()
        {
            try
            {
                TodoModel? todo = TodoModel.ActivateTodo(_idTodoCreadted);

                if (todo == null) throw new Exception("To Activate a todo Because is Null");
                if (todo.IsCompleted != true) throw new Exception("To Activate a todo Because is Null");
                
                todo = TodoModel.ActivateTodo(_idTodoCreadted);
                if (todo.IsCompleted != false) throw new Exception("To Activate a todo Because is Null");


                Print.SuccessDisplay($"To Activate A todo");
                return true;
            }
            catch (Exception err)
            {
                Print.ErrorDisplay($"{err}");
                return false;
            }
        }

        private static bool _TestTodoChangeUser()
        {
            try { 
                TodoModel? todo = TodoModel.ChangeUserIdToTodo(_idTodoCreadted, 1);

                if (todo == null) throw new Exception("To Change the UserId to the todo");
                if (todo.UserId != 1) throw new Exception("To Change the UserId to the todo Because is not the good UserId");

                Print.SuccessDisplay($"To Change the UserId ( {todo.UserId} ) to the todo ( {todo.Id} )");
                return true;
            }
            catch (Exception err)
            {
                Print.ErrorDisplay($"{err}");
                return false;
            }
        }

        private static bool _TestTodoAddDesc()
        {
            try
            {
                TodoModel? todo = TodoModel.AddDescTodo(_idTodoCreadted, "This is a Description");

                if (todo == null) throw new Exception("To Change the add description to the todo");
                if (todo.Description != "This is a Description") throw new Exception("To Change the add description to the todo Because is not the good UserId");

                Print.SuccessDisplay($"To add a description to todo ( {todo.Id} )");
                return true;
            }
            catch (Exception err)
            {
                Print.ErrorDisplay($"{err}");
                return false;
            }
        }





        private static bool _TestTodoDeleteWithId()
        {
            Print.Display("");

            try
            {
                TodoModel.DeleteTodo(_idTodoCreadted);

                if (TodoModel.ReadTodo(_idTodoCreadted) != null) throw new Exception("To Delete a todo");
                
                Print.SuccessDisplay($"To Delete a todo with id : {_idTodoCreadted}");
                return true;
            }
            catch (Exception err)
            {
                Print.ErrorDisplay($"{err}");
                return false;
            }
        }

        private static bool _TestTodoDeleteWithType()
        {
            try
            {
                TodoModel todo1 = new TodoModel("test_Unit_Delete_Type1", "test_Unit1", PriorityStatus.High, DateTime.Now, 1);
                TodoModel todo2 = new TodoModel("test_Unit_Delete_Type2", "test_Unit2", PriorityStatus.High, DateTime.Now, 1);
                TodoModel todoCreated1 = TodoModel.AddTodo(todo1);
                TodoModel todoCreated2 = TodoModel.AddTodo(todo2);
                
                List<TodoModel>? listTodos = TodoModel.DeleteTodos(PriorityStatus.High);
                
                if (listTodos == null) throw new Exception("with Delete with type because list is null");

                Print.SuccessDisplay($"To Delete a todo with tpye (todo 1 : {todo1.Id} and todo 2 : {todo2.Id})");

                return true;
            }
            catch (Exception err)
            {
                Print.ErrorDisplay($"{err}");
                return false;
            }
        }



    }
}