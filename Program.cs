using System;
using System.Collections.Generic;
using System.Linq;


namespace Adivinhe_um_numero
{
    class Program
    {
        public static Dictionary<string, int> usuarios = new Dictionary<string, int>();
        static void Main(string[] args)
        {            
            do
            {
                int opcao = menuPrincipal();

                if (opcao == 1)
                {
                    limpaTela();
                    jogaAdivinha();

                }
                else if (opcao == 2)
                {
                    limpaTela();
                    rank();
                    Console.ReadLine();

                }
                else if (opcao == 3)
                {
                    limpaTela();
                    sobre();

                }
                else if (opcao == 4)
                {
                    limpaTela();
                    Console.WriteLine("Até Mais!");
                    Console.ReadLine();
                    break;
                }
            } while (true);
 
        }

        public static void mesagemPrincipal(string mensagem)
        {      
            Console.WriteLine("\n\t*** {0} ***\n", (mensagem));
        }

        public static void limpaTela()
        {
            Console.Clear();
        }

        public static int menuPrincipal()
        {
            int opcao;
            do
            {
                limpaTela();
                mesagemPrincipal("Bem Vindo ao Adivinha o Número");
                Console.Write("\tSelecione uma das opções abaixo\n(1) - Jogar\n(2) - Rank\n(3) - Sobre\n(4) - Sair\n: ");
                opcao = Int32.Parse(Console.ReadLine());

                if (opcao < 1 || opcao > 4)
                {
                    Console.WriteLine("Digite apenas valores de 1 a 4!!!");
                    Console.ReadLine();
                    limpaTela();
                }
            } while (opcao < 1 || opcao > 4);

            return opcao;
        }

        public static int geraNumeroSecreto()
        {
            Random numeroAleatorio = new Random();
            int numeroSecreto = numeroAleatorio.Next(1, 101);
            return numeroSecreto;
        }

        public static int selecionaNivel()
        {
            Console.Write("\tEm qual nível deseja jogar?\n\n(1) - Fácil\n(2) - Moderado\n(3) - Díficil\n: ");
            int nivel = Int32.Parse(Console.ReadLine());
            int rodadas = 0;
            switch (nivel)
            {
                case 1:
                    rodadas = 20;
                    break;
                case 2:
                    rodadas = 10;
                    break;
                case 3:
                    rodadas = 5;
                    break;
                default:
                    Console.WriteLine("Entrada Inválida");
                    break;
            }

            return rodadas;
        }

        public static int solicitaChute(int tentativa, int rodadas)
        {
            Console.WriteLine("Tentativa " + tentativa + " de " + rodadas);
            Console.WriteLine("Digite um número inteiro entre de 1 a 100");
            Console.WriteLine("Qual o seu chute?");
            int chute = Int32.Parse(Console.ReadLine());
            Console.WriteLine("\nSeu chute foi: " + chute);
            return chute;
        }

        public static void mostraNumeroSecreto(int numeroSecreto)
        {
            Console.WriteLine("Número Secreto: " + numeroSecreto);
        }

        public static int calculaPontuacao(int pontuacao, int chute, int numeroSecreto)
        {
            pontuacao = pontuacao - Math.Abs(numeroSecreto - chute);
            return pontuacao;
        }

        public static void mostraPontuacao(int pontuacao)
        {
            Console.WriteLine("Sua pontuação foi: " + pontuacao);
        }

        public static void jogaAdivinha()
        {
            int rodadas = 0;
            int chute = 0;
            int pontuacao = 1000;
            string nome;

            int numeroSecreto = geraNumeroSecreto();
            
            mesagemPrincipal("Adivinha o Número");
            rodadas = selecionaNivel();
            limpaTela();
            Console.WriteLine("Qual o seu nome?");
            nome = Console.ReadLine();
            Console.WriteLine("\nVamos jogar " + nome);
            for (int tentativa = 1; tentativa <= rodadas; tentativa++)
            {
                chute = solicitaChute(tentativa, rodadas);

                bool acertou = chute == numeroSecreto;
                bool maior = chute > numeroSecreto;
                bool menor = chute < numeroSecreto;

                if (acertou)
                {
                    Console.WriteLine("Você acertou!");
                    pontuacao = pontuacao + 50;
                    break;
                }
                else
                {
                    if (maior)
                    {
                        Console.WriteLine("Seu chute foi maior que o número secreto.\n");
                    }
                    else if (menor)
                    {
                        Console.WriteLine("Seu chute foi menor que o número secreto.\n");
                    }
                }

                pontuacao = calculaPontuacao(pontuacao, chute, numeroSecreto);
            }

            mostraNumeroSecreto(numeroSecreto);
            mostraPontuacao(pontuacao);

            usuarios.Add(nome, pontuacao);

            Console.ReadLine();
        }

        public static void rank()
        {
            mesagemPrincipal("Rank");
            foreach(KeyValuePair<string, int> usuario in usuarios.OrderByDescending(key => key.Value))
            {
                Console.WriteLine("Nome = {0}   Pontuação = {1}", usuario.Key, usuario.Value);
            }
        }

        public static void sobre()
        {
            limpaTela();
            mesagemPrincipal("Sobre o Jogo");
            Console.WriteLine("Desenvolvedor: Ricardo Santana");
            Console.WriteLine("E-mail: slopes.ricardo@gmail.com");
            Console.ReadLine();
        }
    }
}
