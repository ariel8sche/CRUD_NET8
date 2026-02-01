using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrudEF.Models
{
    public class Contacto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="El nombre es obligatorio")]
        [StringLength(100,ErrorMessage ="El nombre debe se menor a 100 caracteres")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage ="El correo es obligatorio")]
        [EmailAddress(ErrorMessage ="El correo no es válido")]
        [StringLength(150,ErrorMessage ="El correo debe se menor a 150 caracteres")]
        public required string Correo { get; set; }

        [StringLength(20,ErrorMessage ="El telefono debe se menor a 20 caracteres")]
        [Phone(ErrorMessage ="El telefono no es válido")]
        public string? Telefono { get; set; }

        [StringLength(250,ErrorMessage ="La dirección debe se menor a 250 caracteres")]
        public string? Direccion { get; set; }

    }
}