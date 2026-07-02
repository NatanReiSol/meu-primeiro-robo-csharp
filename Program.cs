using System;
using System.Collections.Generic; // Para liberar o uso de List<>
using System.Diagnostics; 
using System.IO;

Console.WriteLine("--- LAUNCHER ---");

Console.Write("Qual é o seu nome? ");

string Nameuser = Console.ReadLine();

Console.WriteLine($"\nOlá, {Nameuser}! Iniciando o sistema...");

// 1. AJUSTADO: Apontando direto para 'steamapps', onde ficam os arquivos .acf
string pastaSteam = @"C:\Program Files (x86)\Steam\steamapps";

// 2. AJUSTADO: Adicionado o '!' para o robô barrar se a pasta NÃO existir
if (!Directory.Exists(pastaSteam))
{
    Console.WriteLine("Pasta Steam não encontrada. Verifique se o Steam está instalado corretamente.");
    return; // programa para se nao achar
}

Console.WriteLine("Pasta Steam encontrada. Continuando...");


string[] arquivosejogos = Directory.GetFiles(pastaSteam, "appmanifest_*.acf");


List<JogoSteam> biblioteca = new List<JogoSteam>();


foreach (string arquivo in arquivosejogos)
{
    string id = "";
    string nome = "";

    string[] linhasDoArquivo = File.ReadAllLines(arquivo);

    foreach (string linha in linhasDoArquivo)
    {
        if (linha.Contains("\"appid\""))
        {
            
            
            id = linha.Replace("\"appid\"", "").Replace("\"", "").Trim(); 
        }
        else if (linha.Contains("\"name\""))
        {
            
            nome = linha.Replace("\"name\"", "").Replace("\"", "").Trim(); 
        }
    }

    if (id != "" && nome != "")
    {
        JogoSteam novoJogo = new JogoSteam();
        novoJogo.Id = id;
        novoJogo.Nome = nome;

        biblioteca.Add(novoJogo);
    }
}

// Mostra o total de jogos guardados com sucesso
// 1. Se a gaveta estiver vazia, avisa e para
if (biblioteca.Count == 0)
{
    Console.WriteLine("Nenhum jogo instalado foi encontrado.");
    return;
}

Console.WriteLine($"\nEncontrei {biblioteca.Count} jogos instalados. Escolha um para iniciar:\n");

// 2. Mostra os jogos numerados de 1 até o total encontrado
for (int i = 0; i < biblioteca.Count; i++)
{
    Console.WriteLine($"{i + 1} - {biblioteca[i].Nome}");
}

// 3. Espera o usuário digitar o número
Console.Write("\nDigite o número do jogo desejado: ");
string entrada = Console.ReadLine();

// 4. Converte o texto digitado em um número inteiro real
if (int.TryParse(entrada, out int escolha) && escolha > 0 && escolha <= biblioteca.Count)
{
    JogoSteam jogoEscolhido = biblioteca[escolha - 1];
    
    Console.WriteLine($"\nAbrindo o jogo: {jogoEscolhido.Nome}...");

    // Criamos o link que a Steam entende
    string linkSteam = "steam://run/" + jogoEscolhido.Id;

    // Configuração reforçada para o Windows não bloquear a inicialização
    ProcessStartInfo psi = new ProcessStartInfo
    {
        FileName = linkSteam,
        UseShellExecute = true // Diz ao Windows para usar o executor padrão do sistema
    };

    // O robô executa o comando de fato
    Process.Start(psi);
}
else
{
    Console.WriteLine("Opção inválida! Inicialização cancelada.");
}



class JogoSteam
{
    public string Id { get; set; } = "";
    public string Nome { get; set; } = "";
}
