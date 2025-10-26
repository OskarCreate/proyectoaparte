using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyectoIngSoft.ViewModels
{
    public class TrabajadorViewModel
    {
        public string Dni { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string CargoLaboral { get; set; } = string.Empty;
        public int DiasAcumulados { get; set; }
        public decimal TotalPagar { get; set; }
    }
}