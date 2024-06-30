using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using ListaDeTarefas.Models;

List<Tarefa> listaDeTarefas = new List<Tarefa>();
int opcao = 0;

DateTime dataTarefa = new DateTime();

string path = Path.GetTempPath() + "Tarefas.json";

FileStream openStream = File.OpenRead(path);
if (File.ReadAllText(path) != "")
{
    listaDeTarefas = JsonSerializer.Deserialize<List<Tarefa>>(openStream);
}
openStream.Close();

Console.WriteLine($"Olá, a data de hoje é {DateTime.Now.ToString("dd/MM")} - {DateTime.Now.DayOfWeek}");
while (opcao != 9)
{
    separadorLinha();
    Console.WriteLine("\nEscolha uma das opções abaixo: \n1 - Inserir uma nova tarefa.\n2 - Concluir uma tarefa\n3 - Listar todas as tarefas\n4 - Listar tarefas do dia\n5 - Listas todas as tarefas pendentes\n6 - Listar tarefas pendentes do dia\n7 - Listar tarefas concluídas do dia\n8 - Remover tarefa\n9 - Sair.");
    opcao = Int32.Parse(Console.ReadLine());
    Console.Clear();
    switch (opcao)
    {
        case 1:
            InserirTarefa();
            break;
        case 2:
            ListarTarefas(1);
            Console.WriteLine("\nDigite o Id da tarefa que você deseja concluir:");
            int id = Int32.Parse(Console.ReadLine());
            ConcluirTarefa(id);
            break;
        case 3:
            ListarTarefas(1);
            break;
        case 4:
            ListarTarefas(2);
            break;
        case 5:
            ListarTarefas(3);
            break;
        case 6:
            ListarTarefas(4);
            break;
        case 7:
            ListarTarefas(5);
            break;
        case 8:
            ListarTarefas(1);
            Console.WriteLine("Digite o Id da tarefa que você deseja remover: ");
            int tarefaARemover = Int32.Parse(Console.ReadLine());
            RemoverTarefa(tarefaARemover);
            break;
        default:
            break;
    }
}

async void InserirTarefa()
{
    Console.WriteLine("\nDigite o nome da tarefa:");
    string nomeTarefa = Console.ReadLine();
    Console.WriteLine("Digite a data da tarefa no formato dia/mes/ano hora:minutos:");
    inserirData();
    Tarefa novaTarefa = new Tarefa(listaDeTarefas.Count > 0 ? listaDeTarefas[(listaDeTarefas.Count) - 1].IdTarefa + 1 : listaDeTarefas.Count, nomeTarefa, dataTarefa, false);

    listaDeTarefas.Add(novaTarefa);
    string jsonString = JsonSerializer.Serialize<List<Tarefa>>(listaDeTarefas);
    File.WriteAllText(path, jsonString);
    Console.WriteLine($"Tarefa {nomeTarefa} inserida com sucesso!\n");
}

void ListarTarefas(int opcao)
{
    Console.Clear();
    if (listaDeTarefas.Count < 1)
    {
        Console.WriteLine("Nenhuma tarefa inserida.");
    }
    else
    {
        separadorLinha();
        switch (opcao)
        {
            case 1:
                ExibirTarefa(listaDeTarefas);
                break;
            case 2:
                Console.WriteLine($"Hoje é dia {DateTime.Now.ToString("dd/MM")} - {DateTime.Now.DayOfWeek}");

                ExibirTarefa(listaDeTarefas.FindAll(x => x.DataTarefa.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy")));
                break;
            case 3:
                ExibirTarefa(listaDeTarefas.FindAll(x => x.StatusTarefa == false));
                break;
            case 4:
                Console.WriteLine($"Hoje é dia {DateTime.Now.ToString("dd/MM")} - {DateTime.Now.DayOfWeek}");
                ExibirTarefa(listaDeTarefas.FindAll(x => x.DataTarefa.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy") && x.StatusTarefa == false));
                break;
            case 5:
                Console.WriteLine($"Hoje é dia {DateTime.Now.ToString("dd/MM")} - {DateTime.Now.DayOfWeek}");
                ExibirTarefa(listaDeTarefas.FindAll(x => x.DataTarefa.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy") && x.StatusTarefa == true));
                break;
            default:
                break;
        }
        separadorLinha();
    }
}

void ExibirTarefa(List<Tarefa> lista)
{
    if (lista.Count < 1)
    {
        Console.WriteLine("Nenhuma tarefa encontrada.");
    }
    else
    {
        foreach (Tarefa item in lista)
        {
            Console.WriteLine($"\n- Id: {item.IdTarefa} | Tarefa: {item.NomeTarefa} | Data Tarefa: {item.DataTarefa.ToString("dd/MM/yyyy")} | Hora Tarefa: {item.DataTarefa.ToString("HH:mm")} | Status tarefa: {(item.StatusTarefa == true ? "Concluída" : "Pendente")}");
        }
    }
}

void RemoverTarefa(int tarefa)
{
    if (listaDeTarefas.Remove(listaDeTarefas.Find(x => x.IdTarefa == tarefa)))
    {
        Console.Clear();
        string jsonString = JsonSerializer.Serialize<List<Tarefa>>(listaDeTarefas);
        File.WriteAllText(path, jsonString);
        Console.WriteLine("Tarefa removida com sucesso.");
    }
    else
    {
        Console.Clear();
        Console.WriteLine("Tarefa não encontrada, verifique se você digitou corretamente.");
    }
}

void inserirData()
{
    try
    {
        dataTarefa = DateTime.Parse(Console.ReadLine());
    }
    catch (Exception e)
    {
        Console.WriteLine("Erro ao tentar inserir a data, formato inválido.");
    }
}

void ConcluirTarefa(int id)
{
    listaDeTarefas.Find(x => x.IdTarefa == id).StatusTarefa = true;
    string jsonString = JsonSerializer.Serialize<List<Tarefa>>(listaDeTarefas);
    File.WriteAllText(path, jsonString);
}

void separadorLinha()
{
    Console.WriteLine("\n----------------------------------------------------------");
}