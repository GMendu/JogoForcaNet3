using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace JogoForcaNet3
{
    class Program
    {
        public struct pontuacao //estrutura de dados relacionados a pontuação
        {
            public string nome;
            public double pontos;
            public DateTime data;
        }
        static void Comecar() // contagem regressiva ao iniciar uma partida
        {
            for (int i = 0; i < 3; i++)
            {
                Console.Clear();
                Console.WriteLine($"PREPARE-SE O JOGO VAI COMEÇAR EM {3 - i}");   // 3, 2, 1..
                Thread.Sleep(500);
            }
            Console.Clear();
        }
        static int Menu() // menu do jogo
        {
            Console.WriteLine(
            "\n 1 Jogar" +
            "\n 2 Vocabulário" +
            "\n 3 Placar" +
            "\n 4 Créditos" +
            "\n 5 Sair");
            return int.Parse(Console.ReadLine());
        }
        static double Jogo(string palavra)// codigo base do jogo
        {
            List<string> erradas = new List<string>();
            List<string> acertos = new List<string>();
            int vidas = palavra.Length;
            int count = 0;
            int lastcount = 0;
            string[,] Palav = new string[palavra.Length, 2];
            Comecar();
            Console.WriteLine($"a palavra da vez possui {palavra.Length} letras");
            for (int i = 0; i < palavra.Length; i++)
            {
                Palav[i, 0] = palavra.Substring(i, 1);
                Console.Write(" ___ ");
            }
            bool check = true;
            while (check)
            {
                Console.WriteLine($"\nVocê possui {vidas} de {palavra.Length} vidas! \nDe o seu palpite");
                string palpite = Console.ReadLine();
                if (erradas.Contains(palpite))
                {
                    Console.WriteLine("Você já tentou esta letra");
                }
                else if (acertos.Contains(palpite))
                {
                    Console.WriteLine("Você já tentou esta letra");
                }
                else
                {
                    Console.Clear();
                    for (int i = 0; i < palavra.Length; i++)
                    {
                        if (palavra.Substring(i, 1) == palpite)
                        {
                            Palav[i, 1] = "descoberto";
                            count++;
                        }
                    }
                    for (int i = 0; i < palavra.Length; i++)
                    {
                        if (Palav[i, 1] == "descoberto")
                        {
                            Console.Write($" {Palav[i, 0]} ");
                        }
                        else
                        {
                            Console.Write(" ___ ");
                        }
                    }
                    if (lastcount != count)
                    {
                        Console.WriteLine($"\nparabéns, acertou a letra {palpite}");
                        acertos.Add(palpite);
                    }
                    else
                    {
                        vidas--;
                        Console.WriteLine($"\na letra {palpite} não está na palavra, você perdeu 1 vida({vidas}/{palavra.Length})");  
                        erradas.Add(palpite);
                        if (vidas == 0)
                        {
                            Console.WriteLine($"fim de jogo, a palavra era {palavra}");
                            return 0;
                        }
                    }
                    Console.Write("\n- ");
                    foreach (string err in erradas)
                    {
                        Console.Write($"{err} - ");
                    }

                    lastcount = count;
                }
                int checknum = 0;
                for (int x = 0; x < palavra.Length; x++)
                {
                    if (Palav[x, 1] == "descoberto")
                    {
                        checknum++;
                    }

                }
                if (checknum == palavra.Length)
                {
                    check = false;
                    break;
                }
            }
            Console.Clear();
            return vidas * 100 / palavra.Length;//adicionar
        }
        static pontuacao Placar(double pontos) // atribuição de pontos ao jogador no final da partida
        {
            Console.WriteLine($"Voce atingiu uma pontuação de {pontos}");
            Console.WriteLine("Qual seu nome, jogador?");
            string nome = Console.ReadLine();
            pontuacao t;
            t.pontos = pontos;
            t.nome = nome;
            t.data = DateTime.Now;
            return t;
            Console.Clear();
        }

        static void Main(string[] args)
        {
            List<pontuacao> listaplacar = new List<pontuacao>(); //placar
            List<String> Vocabulario = new List<string>(); //vocabulário
            Vocabulario.Add("palavra"); //palavras iniciais do vocabulário do jogo
            int escolha = Menu();
            while (escolha != 5)
            {
                switch (escolha)
                {
                    case 1:
                        {//jogar
                            Random r = new Random();
                            int index = r.Next(Vocabulario.Count);
                            string palavra = Vocabulario[index];
                            listaplacar.Add(Placar(Jogo(palavra)));

                            break;
                        }
                    case 2:
                        {//vocabulario do jogo pode ser modificado pelo usuario durante a execução do código
                            bool adicionar = true;
                            while (adicionar)
                            {
                                Console.Clear();
                                Console.Write("atual vocabulário: ");
                                foreach (string voc in Vocabulario)
                                {
                                    Console.Write($" {voc},");
                                }
                                Console.WriteLine("\nDeseja adicionar uma nova palavra ao vocabulário do jogo? S/N");
                                string escolhaVoc = Console.ReadLine();

                                if (escolhaVoc == "S" || escolhaVoc == "s")
                                {
                                    Console.WriteLine("Digite uma palavra");
                                    Vocabulario.Add(Console.ReadLine());
                                }
                                else
                                {
                                    adicionar = false;
                                }
                            }
                            break;
                        }
                    case 3:
                        {//placar
                            Console.Clear();
                            foreach (pontuacao x in listaplacar)
                            {
                                Console.WriteLine($"{x.pontos} - {x.nome} - {x.data}");
                            }
                            break;
                        }
                    case 4:
                        {//créditos
                            Console.Clear();
                            Console.WriteLine("Jogo criado por Gabriel Mendonça");
                            Console.WriteLine("CEET-Vasco Coutinho - info mod 2");
                            break;
                        }
                    case 5:
                        {//sair
                            Console.WriteLine("obrigado por jogar!");
                            break;
                        }
                }
                escolha = Menu();
            }
        }
    }
}
