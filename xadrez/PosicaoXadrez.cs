using tabuleiro;

namespace xadrez {
    class PosicaoXadrez {
        public char coluna { get; set; }
        public int linha { get; set; }
        public PosicaoXadrez(char coluna, int linha) {
            this.coluna = coluna;
            this.linha = linha;
        }

        public Posicao toPosicao() {
            return new Posicao(8 - linha, coluna - 'a');
        }

        public override string ToString() {
            return "" + coluna + linha;
        }
    }
}

/*
    8 - linha => converte a linha do tabuleiro de xadrez para um índice baseado em matriz.
        - No xadrez, as linhas são numeradas de 1 a 8 de baixo para cima;
        - Em uma matriz de programacão, as linhas são numeradas de 0 a 7 de cima para baixo;
        - A operacão 8 - linha faz a conversão da linha do xadrez para a matriz de programacão.

    coluna - 'a' => converte a coluna da notacão de xadrez (letras) para um índice numérico.
        - No xadrez, as colunas são letras de 'a' a 'h';
        - Em uma matriz de programacão, as colunas são numeradas de 0 a 7;
        - 'a' tem valor ASCII 97, 'b' tem 98, 'c' tem 99 e assim por diante;
        - Subtrair 'a' do valor da coluna transforma 'a' em 0, 'b' em 1, 'c' em 2, até 'h' ser 7.
*/