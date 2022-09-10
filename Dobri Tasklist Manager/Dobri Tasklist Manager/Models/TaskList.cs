using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dobri_Tasklist_Manager.Models
{
    public class TaskList
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime DateOfLastEdit { get; set; }
        public int IdCreator { get; set; }
        public int IdLastEditor { get; set; }
    }
}
