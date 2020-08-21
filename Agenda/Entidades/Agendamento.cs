using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaDominio.Entidades
{
    [Table("Agenda")]
    public class Agendamento
    {
        public int AtendimentoID { get; set; }
        [Required(ErrorMessage = "Informe o nome do Paciente")]
        public string Paciente { get; set; }
        [Required(ErrorMessage = "Informe o dentista do Paciente")]
        public int Dentista { get; set; }
        [Required(ErrorMessage = "Informe a data do atendimento")]
        public string Data { get; set; }
        [Required(ErrorMessage = "Informe a hora do atendimento")]
        public string Hora { get; set; }
        public List<Agendamento> listaAgendamento { get; set; }
        public List<Agendamento> Agendamentos { get; set; }
    }
}
