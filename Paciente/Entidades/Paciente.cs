using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacienteDominio.Entidades
{
    [Table("Pacientes")]
    public class Paciente
    {
        public int PacienteID { get; set; }
        [Required (ErrorMessage ="Informe o nome do Paciente")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Informe o sobrenome do Paciente")]
        public string Sobrenome { get; set; }
        [Required(ErrorMessage = "Informe o endereço do Paciente")]
        public string Endereço { get; set; }
        [Required(ErrorMessage = "Informe o email do Paciente")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Informe o telefone do Paciente")]
        public string Telefone { get; set; }
        [Required(ErrorMessage = "Informe o whatsapp do Paciente")]
        public string Whatsapp { get; set; }
        [Required(ErrorMessage = "Informe o RG do Paciente")]
        public string RG { get; set; }
        [Required(ErrorMessage = "Informe o CPF do Paciente")]
        public string CPF { get; set; }
        [Required(ErrorMessage = "Informe a indicação do Paciente")]
        public string Indicação { get; set; }
        [Required(ErrorMessage = "Informe o dentista do Paciente")]
        public string Dentista { get; set; }
        public byte[] Imagem { get; set; }
        public string ImagemTipo { get; set; }
        public List<Paciente> listaPaciente { get; set; }
        public List<Paciente> Pacientes { get; set; }
    }
}