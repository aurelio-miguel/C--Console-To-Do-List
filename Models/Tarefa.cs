using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeTarefas.Models
{
    public class Tarefa
    {
        public int IdTarefa { get; set; }
        public string NomeTarefa { get; set; }
        public DateTime DataTarefa { get; set; }
        public bool StatusTarefa { get; set; }

        public Tarefa(int idTarefa, string nomeTarefa, DateTime dataTarefa, bool statusTarefa)
        {
            IdTarefa = idTarefa;
            NomeTarefa = nomeTarefa;
            DataTarefa = dataTarefa;
            StatusTarefa = statusTarefa;
        }
    }
}