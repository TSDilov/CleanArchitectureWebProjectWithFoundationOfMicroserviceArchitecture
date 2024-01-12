namespace TaskManager.Infrastructure.Constants;

public static class ApiPaths
{
    public static string TaskManagerApiName = "TaskManager.Api";

    public static class TaskManager
    {
        public static string Root = "tasks";
    }

    public static class UserManager
    {
        public static string Login = "account/login";

        public static string Register = "account/register";

        public static string GetUserByEmail = "account/getuserbyemail";
    }
}

