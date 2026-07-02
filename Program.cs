using System;
// 1. Precisamos dessa linha nova no topo para dar comandos ao sistema operacional
using System.Diagnostics; 

Console.WriteLine("--- SISTEMA DE INICIALIZAÇÃO ---");
Console.WriteLine("Olá! Eu sou o CyberNet. Qual é o seu nome?");
string nomeUsuario = Console.ReadLine();
Console.WriteLine("Acesso concedido, " + nomeUsuario + "!");

Console.WriteLine("Digite o ID do jogo da Steam que você deseja jogar:");

string idJogo = Console.ReadLine();

Console.WriteLine("Avisando a Steam para iniciar o jogo...");

string linkSteam = "steam://run/" + idJogo;
Process.Start(new ProcessStartInfo(linkSteam) { UseShellExecute = true });