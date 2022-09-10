using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dobri_Tasklist_Manager.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime DateOfLastEdit { get; set; }
        public int IdLastEditor { get; set; }
        public int IdCreator { get; set; }
        public string ListsCreatedByMe { get; set; }
        public string ListsSharedWithMe { get; set; }
    }
}
