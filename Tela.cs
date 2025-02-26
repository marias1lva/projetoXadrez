using System;
using tabuleiro;

namespace Xadrez {
    class Tela {
        public static void imprimirTabuleiro(Tabuleiro tab) {
            for (int i = 0; i < tab.linhas; i++) {
                Console.Write(8 - i + " |");
                for (int j = 0; j < tab.colunas; j++) {
                    if (tab.peca(i, j) == null) {
                        Console.Write("- ");
                    } else {
                        imprimirPeca(tab.peca(i, j));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            //Console.WriteLine("   _______________");
            Console.WriteLine("   a b c d e f g h");
        }

        public static void imprimirPeca(Peca peca) {
            if(peca.cor == Cor.Branca) {
                Console.Write(peca);
            }
            else {
                ConsoleColor aux = Console.ForegroundColor; // Salva a cor atual do console
                Console.ForegroundColor = ConsoleColor.Yellow; // Muda a cor do texto para amarelo
                Console.Write(peca); // Imprime a peça
                Console.ForegroundColor = aux; // Restaura a cor original do console
            }
        }
    }
}
