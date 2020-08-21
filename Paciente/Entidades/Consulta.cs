using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacienteDominio.Entidades
{
    [Table("Agenda")]
    public class Consulta
    {
        [Key]
        public int AtendimentoID { get; set; }
        [Required(ErrorMessage = "Informe o nome do Paciente")]
        public string Paciente { get; set; }
        [Required(ErrorMessage = "Informe o nome do Dentista")]
        public string Dentista { get; set; }
        [Required(ErrorMessage = "Informe o dia da Consulta")]
        public string Dia { get; set; }
        [Required(ErrorMessage = "Informe a hora da Consulta")]
        public string Hora { get; set; }
        public List<Consulta> listaConsulta { get; set; }
        public List<Consulta> Consultas { get; set; }

    }
}
