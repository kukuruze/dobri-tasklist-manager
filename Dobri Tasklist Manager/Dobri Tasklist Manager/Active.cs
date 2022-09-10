using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dobri_Tasklist_Manager
{
    public static class Active
    {
        public static int CurrentUserId { get; set; }
        public static bool IsAdmin { get; set; }
        public static bool IsLogged { get; set; }
        public static int CurrentTaskListId { get; set; }
    }
}
