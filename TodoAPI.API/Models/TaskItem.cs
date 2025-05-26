using System.ComponentModel.DataAnnotations;

namespace TodoAPI.API.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "La descripción de la tarea es requerida")]
        public string Description { get; set; }
    }
}
