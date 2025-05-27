using System.ComponentModel.DataAnnotations;

namespace TodoAPI.API.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La descripción de la tarea es requerida")]
        [MinLength(2,ErrorMessage ="La descripción debe contener un minimo de 2 caracteres")]
        public string? Description { get; set; }
    }
}
