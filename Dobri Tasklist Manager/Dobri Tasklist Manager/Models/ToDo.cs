using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dobri_Tasklist_Manager.Models
{
    public class ToDo
    {
        [Key]
        public int Id { get; set; }
        public int IdTaskList { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime DateOfLastChange { get; set; }
        public int IdCreator { get; set; }
        public int IdLastEditor { get; set; }
    }
}
