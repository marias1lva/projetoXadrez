using System;
using Tabuleiro;

namespace Xadrez {
    class Program {
        static void Main(string[] args) {
            Posicao P;
            P = new Posicao(3, 4);

            Console.WriteLine("Posicão: " + P);
        }
    }
}