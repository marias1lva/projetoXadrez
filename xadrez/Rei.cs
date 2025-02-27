using tabuleiro;

namespace xadrez {
    internal class Rei : Peca {
        public Rei(Tabuleiro tab, Cor cor) : base(tab, cor) {
        }
        public override string ToString() {
            return "R";
        }

        private bool podeMover(Posicao pos) {
            Peca p = tab.peca(pos);
            return p == null || p.cor != cor; // O Rei só pode se mover se a posição estiver vazia ou a peça for de cor diferente
        }

        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            // Acima
            pos.definirValores(posicao.linha - 1, posicao.coluna);
            if (tab.posicaoValida(pos) && podeMover(pos)) { // Se a posição é válida e se pode mover
                mat[pos.linha, pos.coluna] = true; // Verifica se a posição acima do Rei está livre ou com peça adversária, se sim, ele pode se mover para lá
            }

            // Nordeste
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) { 
                mat[pos.linha, pos.coluna] = true; 
            }

            // Direita
            pos.definirValores(posicao.linha, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) { 
                mat[pos.linha, pos.coluna] = true; 
            }

            // Sudeste
            pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) { 
                mat[pos.linha, pos.coluna] = true; 
            }

            // Abaixo
            pos.definirValores(posicao.linha + 1, posicao.coluna);
            if (tab.posicaoValida(pos) && podeMover(pos)) { 
                mat[pos.linha, pos.coluna] = true; 
            }

            // Sudoeste
            pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) { 
                mat[pos.linha, pos.coluna] = true; 
            }

            // Esquerda
            pos.definirValores(posicao.linha, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) { 
                mat[pos.linha, pos.coluna] = true;
            }

            // Noroeste
            pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) { 
                mat[pos.linha, pos.coluna] = true; 
            }

            return mat;
        }
    }
}
